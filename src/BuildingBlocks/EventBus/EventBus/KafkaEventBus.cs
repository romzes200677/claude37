using System;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
using EventBus.Events;
using EventBus.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EventBus
{
    /// <summary>
    /// Реализация шины событий на основе Kafka
    /// </summary>
    public class KafkaEventBus : IEventBus, IDisposable
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<KafkaEventBus> _logger;
        private readonly string _bootstrapServers;
        private readonly IProducer<string, string> _producer;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="serviceScopeFactory">Фабрика для создания области видимости сервисов</param>
        /// <param name="logger">Логгер</param>
        /// <param name="bootstrapServers">Адрес серверов Kafka</param>
        public KafkaEventBus(
            IServiceScopeFactory serviceScopeFactory,
            ILogger<KafkaEventBus> logger,
            string bootstrapServers)
        {
            _serviceScopeFactory = serviceScopeFactory ?? throw new ArgumentNullException(nameof(serviceScopeFactory));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _bootstrapServers = bootstrapServers ?? throw new ArgumentNullException(nameof(bootstrapServers));

            var config = new ProducerConfig
            {
                BootstrapServers = _bootstrapServers,
                Acks = Acks.All
            };

            _producer = new ProducerBuilder<string, string>(config).Build();
        }

        /// <summary>
        /// Публикация события
        /// </summary>
        /// <param name="event">Событие</param>
        /// <returns>Задача</returns>
        public async Task PublishAsync<T>(T @event) where T : IntegrationEvent
        {
            var eventName = @event.GetType().Name;
            var message = JsonConvert.SerializeObject(@event);

            _logger.LogInformation("Publishing event {EventName}: {Message}", eventName, message);

            try
            {
                var deliveryResult = await _producer.ProduceAsync(
                    eventName,
                    new Message<string, string>
                    {
                        Key = @event.Id.ToString(),
                        Value = message
                    });

                _logger.LogInformation("Event {EventName} published successfully. Status: {Status}", 
                    eventName, deliveryResult.Status);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error publishing event {EventName}", eventName);
                throw;
            }
        }

        /// <summary>
        /// Подписка на событие
        /// </summary>
        /// <typeparam name="T">Тип события</typeparam>
        /// <typeparam name="TH">Тип обработчика события</typeparam>
        public void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = typeof(T).Name;
            _logger.LogInformation("Subscribing to event {EventName} with handler {HandlerName}", 
                eventName, typeof(TH).Name);

            var consumerConfig = new ConsumerConfig
            {
                BootstrapServers = _bootstrapServers,
                GroupId = $"{eventName}-group",
                AutoOffsetReset = AutoOffsetReset.Earliest,
                EnableAutoCommit = false
            };

            var consumer = new ConsumerBuilder<string, string>(consumerConfig).Build();
            consumer.Subscribe(eventName);

            Task.Run(async () =>
            {
                try
                {
                    while (true)
                    {
                        var consumeResult = consumer.Consume();
                        if (consumeResult?.Message == null) continue;

                        var message = consumeResult.Message.Value;
                        var @event = JsonConvert.DeserializeObject<T>(message);

                        _logger.LogInformation("Processing event {EventName}", eventName);

                        using var scope = _serviceScopeFactory.CreateScope();
                        var handler = scope.ServiceProvider.GetService<TH>();

                        if (handler == null)
                        {
                            _logger.LogWarning("No handler found for event {EventName}", eventName);
                            continue;
                        }

                        await handler.HandleAsync(@event);
                        consumer.Commit(consumeResult);
                    }
                }
                catch (OperationCanceledException)
                {
                    _logger.LogInformation("Consumer for {EventName} stopped", eventName);
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing event {EventName}", eventName);
                }
                finally
                {
                    consumer.Close();
                }
            });
        }

        /// <summary>
        /// Отписка от события
        /// </summary>
        /// <typeparam name="T">Тип события</typeparam>
        /// <typeparam name="TH">Тип обработчика события</typeparam>
        public void Unsubscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            // В текущей реализации отписка не поддерживается
            // Для полной реализации необходимо хранить ссылки на созданные консьюмеры
            _logger.LogWarning("Unsubscribe not implemented for Kafka event bus");
        }

        /// <summary>
        /// Освобождение ресурсов
        /// </summary>
        public void Dispose()
        {
            _producer?.Dispose();
        }
    }
}
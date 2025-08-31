using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventBus.Events;
using EventBus.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace EventBus.Services
{
    /// <summary>
    /// Сервис для обработки интеграционных событий
    /// </summary>
    public class IntegrationEventProcessor
    {
        private readonly IEventBus _eventBus;
        private readonly IServiceProvider _serviceProvider;
        private readonly ILogger<IntegrationEventProcessor> _logger;
        private readonly Dictionary<string, Type> _eventTypes;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="eventBus">Шина событий</param>
        /// <param name="serviceProvider">Провайдер сервисов</param>
        /// <param name="logger">Логгер</param>
        public IntegrationEventProcessor(IEventBus eventBus, IServiceProvider serviceProvider, ILogger<IntegrationEventProcessor> logger)
        {
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _serviceProvider = serviceProvider ?? throw new ArgumentNullException(nameof(serviceProvider));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _eventTypes = new Dictionary<string, Type>();
        }

        /// <summary>
        /// Подписаться на событие
        /// </summary>
        /// <typeparam name="T">Тип события</typeparam>
        /// <typeparam name="TH">Тип обработчика</typeparam>
        public void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = typeof(T).Name;

            if (!_eventTypes.ContainsKey(eventName))
            {
                _eventTypes.Add(eventName, typeof(T));
            }

            _logger.LogInformation("Subscribing to event {EventName} with {EventHandler}", eventName, typeof(TH).Name);

            _eventBus.Subscribe<T, TH>();
        }

        /// <summary>
        /// Отписаться от события
        /// </summary>
        /// <typeparam name="T">Тип события</typeparam>
        /// <typeparam name="TH">Тип обработчика</typeparam>
        public void Unsubscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>
        {
            var eventName = typeof(T).Name;

            _logger.LogInformation("Unsubscribing from event {EventName} with {EventHandler}", eventName, typeof(TH).Name);

            _eventBus.Unsubscribe<T, TH>();
        }
    }
}
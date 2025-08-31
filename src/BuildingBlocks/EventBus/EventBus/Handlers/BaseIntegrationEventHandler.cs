using System;
using System.Threading.Tasks;
using EventBus.Events;
using EventBus.Interfaces;
using Microsoft.Extensions.Logging;

namespace EventBus.Handlers
{
    /// <summary>
    /// Базовый класс для обработчиков интеграционных событий
    /// </summary>
    /// <typeparam name="TIntegrationEvent">Тип интеграционного события</typeparam>
    public abstract class BaseIntegrationEventHandler<TIntegrationEvent> : IIntegrationEventHandler<TIntegrationEvent>
        where TIntegrationEvent : IntegrationEvent
    {
        /// <summary>
        /// Логгер
        /// </summary>
        protected readonly ILogger<BaseIntegrationEventHandler<TIntegrationEvent>> Logger;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="logger">Логгер</param>
        protected BaseIntegrationEventHandler(ILogger<BaseIntegrationEventHandler<TIntegrationEvent>> logger)
        {
            Logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Обработка события
        /// </summary>
        /// <param name="event">Событие</param>
        /// <returns>Задача</returns>
        public async Task HandleAsync(TIntegrationEvent @event)
        {
            try
            {
                Logger.LogInformation("Handling integration event: {IntegrationEventId} - ({@IntegrationEvent})", 
                    @event.Id, @event);

                await ProcessEventAsync(@event);
            }
            catch (Exception ex)
            {
                Logger.LogError(ex, "Error handling integration event: {IntegrationEventId}", @event.Id);
                throw;
            }
        }

        /// <summary>
        /// Обработка события
        /// </summary>
        /// <param name="event">Событие</param>
        /// <returns>Задача</returns>
        protected abstract Task ProcessEventAsync(TIntegrationEvent @event);
    }
}
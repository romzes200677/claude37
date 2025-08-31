using System;
using System.Threading.Tasks;
using EventBus.Events;
using EventBus.Interfaces;
using Microsoft.Extensions.Logging;

namespace EventBus.Services
{
    /// <summary>
    /// Сервис для публикации интеграционных событий
    /// </summary>
    public class IntegrationEventPublisher
    {
        private readonly IEventBus _eventBus;
        private readonly ILogger<IntegrationEventPublisher> _logger;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="eventBus">Шина событий</param>
        /// <param name="logger">Логгер</param>
        public IntegrationEventPublisher(IEventBus eventBus, ILogger<IntegrationEventPublisher> logger)
        {
            _eventBus = eventBus ?? throw new ArgumentNullException(nameof(eventBus));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Опубликовать событие
        /// </summary>
        /// <param name="event">Событие</param>
        /// <returns>Задача</returns>
        public async Task PublishAsync(IntegrationEvent @event)
        {
            try
            {
                _logger.LogInformation("Publishing integration event: {IntegrationEventId} - ({@IntegrationEvent})", 
                    @event.Id, @event);

                await _eventBus.PublishAsync(@event);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error publishing integration event: {IntegrationEventId}", @event.Id);
                throw;
            }
        }
    }
}
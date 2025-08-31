using System.Threading.Tasks;
using EventBus.Events;

namespace EventBus.Interfaces
{
    /// <summary>
    /// Интерфейс для обработчика интеграционных событий
    /// </summary>
    public interface IIntegrationEventHandler<in TIntegrationEvent> where TIntegrationEvent : IntegrationEvent
    {
        /// <summary>
        /// Обработка события
        /// </summary>
        /// <param name="event">Событие</param>
        /// <returns>Задача</returns>
        Task HandleAsync(TIntegrationEvent @event);
    }
}
using System.Threading.Tasks;
using EventBus.Events;

namespace EventBus.Interfaces
{
    /// <summary>
    /// Интерфейс для шины событий
    /// </summary>
    public interface IEventBus
    {
        /// <summary>
        /// Публикация события
        /// </summary>
        /// <param name="event">Событие</param>
        /// <returns>Задача</returns>
        Task PublishAsync<T>(T @event) where T : IntegrationEvent;

        /// <summary>
        /// Подписка на событие
        /// </summary>
        /// <typeparam name="T">Тип события</typeparam>
        /// <typeparam name="TH">Тип обработчика события</typeparam>
        void Subscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>;

        /// <summary>
        /// Отписка от события
        /// </summary>
        /// <typeparam name="T">Тип события</typeparam>
        /// <typeparam name="TH">Тип обработчика события</typeparam>
        void Unsubscribe<T, TH>()
            where T : IntegrationEvent
            where TH : IIntegrationEventHandler<T>;
    }
}
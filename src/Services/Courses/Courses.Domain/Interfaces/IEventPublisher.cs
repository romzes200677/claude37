using System.Threading.Tasks;

namespace Courses.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса публикации событий
    /// </summary>
    public interface IEventPublisher
    {
        /// <summary>
        /// Опубликовать событие
        /// </summary>
        /// <typeparam name="T">Тип события</typeparam>
        /// <param name="event">Событие</param>
        /// <returns>Задача</returns>
        Task PublishAsync<T>(T @event) where T : class;
    }
}
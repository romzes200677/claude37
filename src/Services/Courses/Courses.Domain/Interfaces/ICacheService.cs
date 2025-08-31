using System;
using System.Threading.Tasks;

namespace Courses.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса кэширования
    /// </summary>
    public interface ICacheService
    {
        /// <summary>
        /// Получить данные из кэша
        /// </summary>
        /// <typeparam name="T">Тип данных</typeparam>
        /// <param name="key">Ключ</param>
        /// <returns>Данные</returns>
        Task<T> GetAsync<T>(string key);

        /// <summary>
        /// Установить данные в кэш
        /// </summary>
        /// <typeparam name="T">Тип данных</typeparam>
        /// <param name="key">Ключ</param>
        /// <param name="value">Значение</param>
        /// <param name="expirationTime">Время истечения</param>
        /// <returns>Задача</returns>
        Task SetAsync<T>(string key, T value, TimeSpan? expirationTime = null);

        /// <summary>
        /// Удалить данные из кэша
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <returns>Задача</returns>
        Task RemoveAsync(string key);

        /// <summary>
        /// Проверить наличие данных в кэше
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <returns>Результат проверки</returns>
        Task<bool> ExistsAsync(string key);
    }
}
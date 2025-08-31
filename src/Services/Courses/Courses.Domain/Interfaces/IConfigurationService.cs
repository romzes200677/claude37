using System.Threading.Tasks;

namespace Courses.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса конфигурации
    /// </summary>
    public interface IConfigurationService
    {
        /// <summary>
        /// Получить значение настройки
        /// </summary>
        /// <typeparam name="T">Тип значения</typeparam>
        /// <param name="key">Ключ настройки</param>
        /// <returns>Значение настройки</returns>
        T GetValue<T>(string key);

        /// <summary>
        /// Получить значение настройки асинхронно
        /// </summary>
        /// <typeparam name="T">Тип значения</typeparam>
        /// <param name="key">Ключ настройки</param>
        /// <returns>Значение настройки</returns>
        Task<T> GetValueAsync<T>(string key);

        /// <summary>
        /// Установить значение настройки
        /// </summary>
        /// <typeparam name="T">Тип значения</typeparam>
        /// <param name="key">Ключ настройки</param>
        /// <param name="value">Значение настройки</param>
        void SetValue<T>(string key, T value);

        /// <summary>
        /// Установить значение настройки асинхронно
        /// </summary>
        /// <typeparam name="T">Тип значения</typeparam>
        /// <param name="key">Ключ настройки</param>
        /// <param name="value">Значение настройки</param>
        /// <returns>Задача</returns>
        Task SetValueAsync<T>(string key, T value);

        /// <summary>
        /// Проверить существование настройки
        /// </summary>
        /// <param name="key">Ключ настройки</param>
        /// <returns>Результат проверки</returns>
        bool HasValue(string key);

        /// <summary>
        /// Проверить существование настройки асинхронно
        /// </summary>
        /// <param name="key">Ключ настройки</param>
        /// <returns>Результат проверки</returns>
        Task<bool> HasValueAsync(string key);

        /// <summary>
        /// Удалить настройку
        /// </summary>
        /// <param name="key">Ключ настройки</param>
        void RemoveValue(string key);

        /// <summary>
        /// Удалить настройку асинхронно
        /// </summary>
        /// <param name="key">Ключ настройки</param>
        /// <returns>Задача</returns>
        Task RemoveValueAsync(string key);

        /// <summary>
        /// Получить секцию конфигурации
        /// </summary>
        /// <typeparam name="T">Тип секции</typeparam>
        /// <param name="sectionName">Название секции</param>
        /// <returns>Секция конфигурации</returns>
        T GetSection<T>(string sectionName) where T : class, new();

        /// <summary>
        /// Получить секцию конфигурации асинхронно
        /// </summary>
        /// <typeparam name="T">Тип секции</typeparam>
        /// <param name="sectionName">Название секции</param>
        /// <returns>Секция конфигурации</returns>
        Task<T> GetSectionAsync<T>(string sectionName) where T : class, new();
    }
}
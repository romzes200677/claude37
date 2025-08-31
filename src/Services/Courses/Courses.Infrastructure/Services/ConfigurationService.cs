using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Courses.Domain.Interfaces;

namespace Courses.Infrastructure.Services
{
    /// <summary>
    /// Сервис конфигурации
    /// </summary>
    public class ConfigurationService : IConfigurationService
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="configuration">Конфигурация</param>
        public ConfigurationService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Получить значение настройки
        /// </summary>
        /// <typeparam name="T">Тип значения</typeparam>
        /// <param name="key">Ключ настройки</param>
        /// <returns>Значение настройки</returns>
        public T GetValue<T>(string key)
        {
            return _configuration.GetValue<T>(key);
        }

        /// <summary>
        /// Получить значение настройки асинхронно
        /// </summary>
        /// <typeparam name="T">Тип значения</typeparam>
        /// <param name="key">Ключ настройки</param>
        /// <returns>Значение настройки</returns>
        public Task<T> GetValueAsync<T>(string key)
        {
            return Task.FromResult(GetValue<T>(key));
        }

        /// <summary>
        /// Установить значение настройки
        /// </summary>
        /// <typeparam name="T">Тип значения</typeparam>
        /// <param name="key">Ключ настройки</param>
        /// <param name="value">Значение настройки</param>
        public void SetValue<T>(string key, T value)
        {
            throw new NotImplementedException("Изменение конфигурации в runtime не поддерживается");
        }

        /// <summary>
        /// Установить значение настройки асинхронно
        /// </summary>
        /// <typeparam name="T">Тип значения</typeparam>
        /// <param name="key">Ключ настройки</param>
        /// <param name="value">Значение настройки</param>
        /// <returns>Задача</returns>
        public Task SetValueAsync<T>(string key, T value)
        {
            throw new NotImplementedException("Изменение конфигурации в runtime не поддерживается");
        }

        /// <summary>
        /// Проверить существование настройки
        /// </summary>
        /// <param name="key">Ключ настройки</param>
        /// <returns>Результат проверки</returns>
        public bool HasValue(string key)
        {
            return _configuration[key] != null;
        }

        /// <summary>
        /// Проверить существование настройки асинхронно
        /// </summary>
        /// <param name="key">Ключ настройки</param>
        /// <returns>Результат проверки</returns>
        public Task<bool> HasValueAsync(string key)
        {
            return Task.FromResult(HasValue(key));
        }

        /// <summary>
        /// Удалить настройку
        /// </summary>
        /// <param name="key">Ключ настройки</param>
        public void RemoveValue(string key)
        {
            throw new NotImplementedException("Изменение конфигурации в runtime не поддерживается");
        }

        /// <summary>
        /// Удалить настройку асинхронно
        /// </summary>
        /// <param name="key">Ключ настройки</param>
        /// <returns>Задача</returns>
        public Task RemoveValueAsync(string key)
        {
            throw new NotImplementedException("Изменение конфигурации в runtime не поддерживается");
        }

        /// <summary>
        /// Получить секцию конфигурации
        /// </summary>
        /// <typeparam name="T">Тип секции</typeparam>
        /// <param name="sectionName">Название секции</param>
        /// <returns>Секция конфигурации</returns>
        public T GetSection<T>(string sectionName) where T : class, new()
        {
            return _configuration.GetSection(sectionName).Get<T>();
        }

        /// <summary>
        /// Получить секцию конфигурации асинхронно
        /// </summary>
        /// <typeparam name="T">Тип секции</typeparam>
        /// <param name="sectionName">Название секции</param>
        /// <returns>Секция конфигурации</returns>
        public Task<T> GetSectionAsync<T>(string sectionName) where T : class, new()
        {
            return Task.FromResult(GetSection<T>(sectionName));
        }
    }
}
using System;
using Microsoft.Extensions.Logging;
using Courses.Domain.Interfaces;

namespace Courses.Infrastructure.Services
{
    /// <summary>
    /// Сервис логирования
    /// </summary>
    public class LogService : ILogService
    {
        private readonly ILogger<LogService> _logger;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="logger">Логгер</param>
        public LogService(ILogger<LogService> logger)
        {
            _logger = logger;
        }

        /// <summary>
        /// Записать информационное сообщение
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="args">Аргументы</param>
        public void Information(string message, params object[] args)
        {
            _logger.LogInformation(message, args);
        }

        /// <summary>
        /// Записать предупреждение
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="args">Аргументы</param>
        public void Warning(string message, params object[] args)
        {
            _logger.LogWarning(message, args);
        }

        /// <summary>
        /// Записать ошибку
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="args">Аргументы</param>
        public void Error(string message, params object[] args)
        {
            _logger.LogError(message, args);
        }

        /// <summary>
        /// Записать ошибку с исключением
        /// </summary>
        /// <param name="exception">Исключение</param>
        /// <param name="message">Сообщение</param>
        /// <param name="args">Аргументы</param>
        public void Error(Exception exception, string message, params object[] args)
        {
            _logger.LogError(exception, message, args);
        }

        /// <summary>
        /// Записать отладочное сообщение
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="args">Аргументы</param>
        public void Debug(string message, params object[] args)
        {
            _logger.LogDebug(message, args);
        }
    }
}
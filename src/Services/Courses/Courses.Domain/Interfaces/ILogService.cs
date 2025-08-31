using System;

namespace Courses.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса логирования
    /// </summary>
    public interface ILogService
    {
        /// <summary>
        /// Записать информационное сообщение
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="args">Аргументы</param>
        void Information(string message, params object[] args);

        /// <summary>
        /// Записать предупреждение
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="args">Аргументы</param>
        void Warning(string message, params object[] args);

        /// <summary>
        /// Записать ошибку
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="args">Аргументы</param>
        void Error(string message, params object[] args);

        /// <summary>
        /// Записать ошибку с исключением
        /// </summary>
        /// <param name="exception">Исключение</param>
        /// <param name="message">Сообщение</param>
        /// <param name="args">Аргументы</param>
        void Error(Exception exception, string message, params object[] args);

        /// <summary>
        /// Записать отладочное сообщение
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="args">Аргументы</param>
        void Debug(string message, params object[] args);
    }
}
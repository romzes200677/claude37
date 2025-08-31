using System;
using System.Collections.Generic;

namespace Courses.Domain.Models
{
    /// <summary>
    /// Модель записи лога
    /// </summary>
    public class LogEntryModel
    {
        /// <summary>
        /// Идентификатор записи
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Временная метка
        /// </summary>
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// Уровень логирования
        /// </summary>
        public LogLevel Level { get; set; }

        /// <summary>
        /// Сообщение
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Исключение
        /// </summary>
        public string Exception { get; set; }

        /// <summary>
        /// Имя логгера
        /// </summary>
        public string LoggerName { get; set; }

        /// <summary>
        /// Имя приложения
        /// </summary>
        public string ApplicationName { get; set; }

        /// <summary>
        /// Имя машины
        /// </summary>
        public string MachineName { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// IP-адрес
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// Дополнительные свойства
        /// </summary>
        public Dictionary<string, string> Properties { get; set; } = new Dictionary<string, string>();
    }

    /// <summary>
    /// Уровень логирования
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// Отладка
        /// </summary>
        Debug,

        /// <summary>
        /// Информация
        /// </summary>
        Information,

        /// <summary>
        /// Предупреждение
        /// </summary>
        Warning,

        /// <summary>
        /// Ошибка
        /// </summary>
        Error,

        /// <summary>
        /// Критическая ошибка
        /// </summary>
        Critical
    }

    /// <summary>
    /// Модель фильтра логов
    /// </summary>
    public class LogFilterModel : PaginationRequestModel
    {
        /// <summary>
        /// Минимальный уровень логирования
        /// </summary>
        public LogLevel? MinLevel { get; set; }

        /// <summary>
        /// Поисковый запрос
        /// </summary>
        public string SearchQuery { get; set; }

        /// <summary>
        /// Дата начала периода
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Дата окончания периода
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Имя логгера
        /// </summary>
        public string LoggerName { get; set; }

        /// <summary>
        /// Имя приложения
        /// </summary>
        public string ApplicationName { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// IP-адрес
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// Только записи с исключениями
        /// </summary>
        public bool? OnlyWithExceptions { get; set; }
    }
}
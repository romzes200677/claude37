using System;
using System.Collections.Generic;

namespace Courses.Domain.Models
{
    /// <summary>
    /// Модель электронного письма
    /// </summary>
    public class EmailModel
    {
        /// <summary>
        /// Адрес отправителя
        /// </summary>
        public string From { get; set; }

        /// <summary>
        /// Имя отправителя
        /// </summary>
        public string FromName { get; set; }

        /// <summary>
        /// Адрес получателя
        /// </summary>
        public string To { get; set; }

        /// <summary>
        /// Адреса получателей копии
        /// </summary>
        public List<string> Cc { get; set; } = new List<string>();

        /// <summary>
        /// Адреса получателей скрытой копии
        /// </summary>
        public List<string> Bcc { get; set; } = new List<string>();

        /// <summary>
        /// Тема письма
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Тело письма
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Флаг HTML-формата
        /// </summary>
        public bool IsHtml { get; set; } = true;

        /// <summary>
        /// Вложения
        /// </summary>
        public List<EmailAttachmentModel> Attachments { get; set; } = new List<EmailAttachmentModel>();

        /// <summary>
        /// Приоритет письма
        /// </summary>
        public EmailPriority Priority { get; set; } = EmailPriority.Normal;
    }

    /// <summary>
    /// Модель вложения электронного письма
    /// </summary>
    public class EmailAttachmentModel
    {
        /// <summary>
        /// Имя файла
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Путь к файлу
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Содержимое файла в виде массива байтов
        /// </summary>
        public byte[] Content { get; set; }

        /// <summary>
        /// MIME-тип
        /// </summary>
        public string ContentType { get; set; }
    }

    /// <summary>
    /// Приоритет электронного письма
    /// </summary>
    public enum EmailPriority
    {
        /// <summary>
        /// Низкий
        /// </summary>
        Low,

        /// <summary>
        /// Нормальный
        /// </summary>
        Normal,

        /// <summary>
        /// Высокий
        /// </summary>
        High
    }

    /// <summary>
    /// Модель шаблона электронного письма
    /// </summary>
    public class EmailTemplateModel
    {
        /// <summary>
        /// Идентификатор шаблона
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название шаблона
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Код шаблона
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// Тема письма
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Тело письма
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Описание шаблона
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Флаг активности
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Дата обновления
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    }

    /// <summary>
    /// Модель результата отправки электронного письма
    /// </summary>
    public class EmailSendResultModel
    {
        /// <summary>
        /// Успешность отправки
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Идентификатор сообщения
        /// </summary>
        public string MessageId { get; set; }

        /// <summary>
        /// Дата отправки
        /// </summary>
        public DateTime SentAt { get; set; }
    }
}
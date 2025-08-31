using System;
using System.Collections.Generic;

namespace Courses.Domain.Models
{
    /// <summary>
    /// Модель уведомления
    /// </summary>
    public class NotificationModel
    {
        /// <summary>
        /// Идентификатор уведомления
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Тип уведомления
        /// </summary>
        public NotificationType Type { get; set; }

        /// <summary>
        /// Заголовок
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Сообщение
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Дата и время создания
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Прочитано ли уведомление
        /// </summary>
        public bool IsRead { get; set; } = false;

        /// <summary>
        /// Дата и время прочтения
        /// </summary>
        public DateTime? ReadAt { get; set; }

        /// <summary>
        /// URL для перехода
        /// </summary>
        public string ActionUrl { get; set; }

        /// <summary>
        /// Текст действия
        /// </summary>
        public string ActionText { get; set; }

        /// <summary>
        /// Дополнительные данные
        /// </summary>
        public Dictionary<string, string> AdditionalData { get; set; } = new Dictionary<string, string>();
    }

    /// <summary>
    /// Тип уведомления
    /// </summary>
    public enum NotificationType
    {
        /// <summary>
        /// Информация
        /// </summary>
        Info,

        /// <summary>
        /// Успех
        /// </summary>
        Success,

        /// <summary>
        /// Предупреждение
        /// </summary>
        Warning,

        /// <summary>
        /// Ошибка
        /// </summary>
        Error,

        /// <summary>
        /// Зачисление на курс
        /// </summary>
        Enrollment,

        /// <summary>
        /// Завершение курса
        /// </summary>
        CourseCompletion,

        /// <summary>
        /// Выдача сертификата
        /// </summary>
        CertificateIssued,

        /// <summary>
        /// Новый отзыв
        /// </summary>
        NewReview,

        /// <summary>
        /// Публикация курса
        /// </summary>
        CoursePublished,

        /// <summary>
        /// Новый урок
        /// </summary>
        NewLesson,

        /// <summary>
        /// Напоминание
        /// </summary>
        Reminder,

        /// <summary>
        /// Обновление курса
        /// </summary>
        CourseUpdate
    }

    /// <summary>
    /// Модель настроек уведомлений пользователя
    /// </summary>
    public class UserNotificationSettingsModel
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Включены ли уведомления по электронной почте
        /// </summary>
        public bool EmailNotificationsEnabled { get; set; } = true;

        /// <summary>
        /// Включены ли push-уведомления
        /// </summary>
        public bool PushNotificationsEnabled { get; set; } = true;

        /// <summary>
        /// Включены ли SMS-уведомления
        /// </summary>
        public bool SmsNotificationsEnabled { get; set; } = false;

        /// <summary>
        /// Включены ли уведомления о зачислении на курс
        /// </summary>
        public bool EnrollmentNotificationsEnabled { get; set; } = true;

        /// <summary>
        /// Включены ли уведомления о завершении курса
        /// </summary>
        public bool CourseCompletionNotificationsEnabled { get; set; } = true;

        /// <summary>
        /// Включены ли уведомления о выдаче сертификата
        /// </summary>
        public bool CertificateNotificationsEnabled { get; set; } = true;

        /// <summary>
        /// Включены ли уведомления о новых отзывах
        /// </summary>
        public bool ReviewNotificationsEnabled { get; set; } = true;

        /// <summary>
        /// Включены ли уведомления о публикации курса
        /// </summary>
        public bool CoursePublishedNotificationsEnabled { get; set; } = true;

        /// <summary>
        /// Включены ли уведомления о новых уроках
        /// </summary>
        public bool NewLessonNotificationsEnabled { get; set; } = true;

        /// <summary>
        /// Включены ли напоминания
        /// </summary>
        public bool ReminderNotificationsEnabled { get; set; } = true;

        /// <summary>
        /// Включены ли уведомления об обновлении курса
        /// </summary>
        public bool CourseUpdateNotificationsEnabled { get; set; } = true;

        /// <summary>
        /// Частота отправки дайджеста
        /// </summary>
        public DigestFrequency DigestFrequency { get; set; } = DigestFrequency.Daily;
    }

    /// <summary>
    /// Частота отправки дайджеста
    /// </summary>
    public enum DigestFrequency
    {
        /// <summary>
        /// Никогда
        /// </summary>
        Never,

        /// <summary>
        /// Ежедневно
        /// </summary>
        Daily,

        /// <summary>
        /// Еженедельно
        /// </summary>
        Weekly,

        /// <summary>
        /// Ежемесячно
        /// </summary>
        Monthly
    }
}
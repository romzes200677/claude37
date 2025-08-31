using System;

namespace Courses.Domain.Entities
{
    /// <summary>
    /// Настройки уведомлений пользователя
    /// </summary>
    public class NotificationSettings : BaseEntity
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Включены ли email-уведомления
        /// </summary>
        public bool EmailEnabled { get; set; }

        /// <summary>
        /// Включены ли push-уведомления
        /// </summary>
        public bool PushEnabled { get; set; }

        /// <summary>
        /// Включены ли SMS-уведомления
        /// </summary>
        public bool SmsEnabled { get; set; }

        /// <summary>
        /// Включены ли уведомления о новых курсах
        /// </summary>
        public bool NewCoursesEnabled { get; set; }

        /// <summary>
        /// Включены ли уведомления о новых материалах курса
        /// </summary>
        public bool CourseUpdatesEnabled { get; set; }

        /// <summary>
        /// Включены ли уведомления о дедлайнах
        /// </summary>
        public bool DeadlinesEnabled { get; set; }

        /// <summary>
        /// Включены ли уведомления о форумах и обсуждениях
        /// </summary>
        public bool ForumEnabled { get; set; }

        /// <summary>
        /// Включены ли уведомления о системных событиях
        /// </summary>
        public bool SystemEnabled { get; set; }

        /// <summary>
        /// Включены ли уведомления о маркетинговых акциях
        /// </summary>
        public bool MarketingEnabled { get; set; }

        /// <summary>
        /// Предпочитаемое время получения дайджеста (в формате HH:mm)
        /// </summary>
        public string DigestTime { get; set; }

        /// <summary>
        /// Частота получения дайджеста (Daily, Weekly, Monthly, Never)
        /// </summary>
        public string DigestFrequency { get; set; }

        /// <summary>
        /// Дополнительные настройки (JSON)
        /// </summary>
        public string AdditionalSettings { get; set; }

        /// <summary>
        /// Конструктор
        /// </summary>
        public NotificationSettings()
        {
            EmailEnabled = true;
            PushEnabled = true;
            SmsEnabled = false;
            NewCoursesEnabled = true;
            CourseUpdatesEnabled = true;
            DeadlinesEnabled = true;
            ForumEnabled = true;
            SystemEnabled = true;
            MarketingEnabled = false;
            DigestFrequency = "Daily";
            DigestTime = "09:00";
        }
    }
}
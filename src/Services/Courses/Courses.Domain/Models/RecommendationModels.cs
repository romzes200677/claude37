using System;
using System.Collections.Generic;

namespace Courses.Domain.Models
{
    /// <summary>
    /// Модель рекомендации курса
    /// </summary>
    public class CourseRecommendationModel
    {
        /// <summary>
        /// Идентификатор курса
        /// </summary>
        public Guid CourseId { get; set; }

        /// <summary>
        /// Название курса
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание курса
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// URL изображения
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Автор курса
        /// </summary>
        public string Author { get; set; }

        /// <summary>
        /// Идентификатор автора
        /// </summary>
        public Guid AuthorId { get; set; }

        /// <summary>
        /// Рейтинг курса
        /// </summary>
        public decimal Rating { get; set; }

        /// <summary>
        /// Количество отзывов
        /// </summary>
        public int ReviewCount { get; set; }

        /// <summary>
        /// Уровень сложности
        /// </summary>
        public string DifficultyLevel { get; set; }

        /// <summary>
        /// Продолжительность (в минутах)
        /// </summary>
        public int DurationMinutes { get; set; }

        /// <summary>
        /// Категория
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Теги
        /// </summary>
        public List<string> Tags { get; set; } = new List<string>();

        /// <summary>
        /// Оценка релевантности
        /// </summary>
        public decimal RelevanceScore { get; set; }

        /// <summary>
        /// Причина рекомендации
        /// </summary>
        public string RecommendationReason { get; set; }
    }

    /// <summary>
    /// Модель интересов пользователя
    /// </summary>
    public class UserInterestsModel
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Список интересов
        /// </summary>
        public List<string> Interests { get; set; } = new List<string>();

        /// <summary>
        /// Предпочтительные категории
        /// </summary>
        public List<string> PreferredCategories { get; set; } = new List<string>();

        /// <summary>
        /// Предпочтительный уровень сложности
        /// </summary>
        public string PreferredDifficultyLevel { get; set; }

        /// <summary>
        /// Предпочтительная продолжительность (в минутах)
        /// </summary>
        public int? PreferredDurationMinutes { get; set; }
    }

    /// <summary>
    /// Модель события просмотра курса
    /// </summary>
    public class CourseViewEvent
    {
        /// <summary>
        /// Идентификатор события
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Идентификатор курса
        /// </summary>
        public Guid CourseId { get; set; }

        /// <summary>
        /// Дата и время просмотра
        /// </summary>
        public DateTime ViewedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// IP-адрес
        /// </summary>
        public string IpAddress { get; set; }

        /// <summary>
        /// Источник перехода
        /// </summary>
        public string ReferralSource { get; set; }

        /// <summary>
        /// Устройство
        /// </summary>
        public string Device { get; set; }
    }
}
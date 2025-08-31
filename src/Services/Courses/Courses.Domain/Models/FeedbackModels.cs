using System;
using System.Collections.Generic;

namespace Courses.Domain.Models
{
    /// <summary>
    /// Модель обратной связи
    /// </summary>
    public class FeedbackModel
    {
        /// <summary>
        /// Идентификатор обратной связи
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Электронная почта пользователя
        /// </summary>
        public string UserEmail { get; set; }

        /// <summary>
        /// Тип обратной связи
        /// </summary>
        public FeedbackType Type { get; set; }

        /// <summary>
        /// Тема
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Сообщение
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Статус
        /// </summary>
        public FeedbackStatus Status { get; set; } = FeedbackStatus.New;

        /// <summary>
        /// Приоритет
        /// </summary>
        public FeedbackPriority Priority { get; set; } = FeedbackPriority.Medium;

        /// <summary>
        /// Дата и время создания
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Дата и время обновления
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Идентификатор сотрудника, назначенного на обратную связь
        /// </summary>
        public Guid? AssigneeId { get; set; }

        /// <summary>
        /// Имя сотрудника, назначенного на обратную связь
        /// </summary>
        public string AssigneeName { get; set; }

        /// <summary>
        /// Сообщение ответа
        /// </summary>
        public string ResponseMessage { get; set; }

        /// <summary>
        /// Дата и время ответа
        /// </summary>
        public DateTime? ResponseDate { get; set; }

        /// <summary>
        /// Идентификатор связанной сущности
        /// </summary>
        public Guid? RelatedEntityId { get; set; }

        /// <summary>
        /// Тип связанной сущности
        /// </summary>
        public string RelatedEntityType { get; set; }

        /// <summary>
        /// URL вложения
        /// </summary>
        public string AttachmentUrl { get; set; }

        /// <summary>
        /// Оценка удовлетворенности
        /// </summary>
        public int? SatisfactionRating { get; set; }
    }

    /// <summary>
    /// Тип обратной связи
    /// </summary>
    public enum FeedbackType
    {
        /// <summary>
        /// Общий вопрос
        /// </summary>
        GeneralQuestion,

        /// <summary>
        /// Техническая проблема
        /// </summary>
        TechnicalIssue,

        /// <summary>
        /// Предложение по улучшению
        /// </summary>
        Suggestion,

        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        BugReport,

        /// <summary>
        /// Запрос функции
        /// </summary>
        FeatureRequest,

        /// <summary>
        /// Жалоба
        /// </summary>
        Complaint,

        /// <summary>
        /// Благодарность
        /// </summary>
        Praise,

        /// <summary>
        /// Другое
        /// </summary>
        Other
    }

    /// <summary>
    /// Статус обратной связи
    /// </summary>
    public enum FeedbackStatus
    {
        /// <summary>
        /// Новый
        /// </summary>
        New,

        /// <summary>
        /// В обработке
        /// </summary>
        InProgress,

        /// <summary>
        /// Требует дополнительной информации
        /// </summary>
        NeedsInfo,

        /// <summary>
        /// Решено
        /// </summary>
        Resolved,

        /// <summary>
        /// Закрыто
        /// </summary>
        Closed,

        /// <summary>
        /// Отклонено
        /// </summary>
        Rejected,

        /// <summary>
        /// Дублирует другую обратную связь
        /// </summary>
        Duplicate
    }

    /// <summary>
    /// Приоритет обратной связи
    /// </summary>
    public enum FeedbackPriority
    {
        /// <summary>
        /// Низкий
        /// </summary>
        Low,

        /// <summary>
        /// Средний
        /// </summary>
        Medium,

        /// <summary>
        /// Высокий
        /// </summary>
        High,

        /// <summary>
        /// Критический
        /// </summary>
        Critical
    }

    /// <summary>
    /// Модель комментария к обратной связи
    /// </summary>
    public class FeedbackCommentModel
    {
        /// <summary>
        /// Идентификатор комментария
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Идентификатор обратной связи
        /// </summary>
        public Guid FeedbackId { get; set; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Текст комментария
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Дата и время создания
        /// </summary>
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Является ли комментарий внутренним (видимым только для сотрудников)
        /// </summary>
        public bool IsInternal { get; set; } = false;
    }

    /// <summary>
    /// Модель фильтра обратной связи
    /// </summary>
    public class FeedbackFilterModel : PaginationRequestModel
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid? UserId { get; set; }

        /// <summary>
        /// Тип обратной связи
        /// </summary>
        public FeedbackType? Type { get; set; }

        /// <summary>
        /// Статус
        /// </summary>
        public FeedbackStatus? Status { get; set; }

        /// <summary>
        /// Приоритет
        /// </summary>
        public FeedbackPriority? Priority { get; set; }

        /// <summary>
        /// Идентификатор сотрудника, назначенного на обратную связь
        /// </summary>
        public Guid? AssigneeId { get; set; }

        /// <summary>
        /// Дата начала
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Дата окончания
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Поисковый запрос
        /// </summary>
        public string SearchQuery { get; set; }

        /// <summary>
        /// Идентификатор связанной сущности
        /// </summary>
        public Guid? RelatedEntityId { get; set; }

        /// <summary>
        /// Тип связанной сущности
        /// </summary>
        public string RelatedEntityType { get; set; }
    }

    /// <summary>
    /// Модель статистики обратной связи
    /// </summary>
    public class FeedbackStatisticsModel
    {
        /// <summary>
        /// Общее количество обратной связи
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Количество новой обратной связи
        /// </summary>
        public int NewCount { get; set; }

        /// <summary>
        /// Количество обратной связи в обработке
        /// </summary>
        public int InProgressCount { get; set; }

        /// <summary>
        /// Количество решенной обратной связи
        /// </summary>
        public int ResolvedCount { get; set; }

        /// <summary>
        /// Количество закрытой обратной связи
        /// </summary>
        public int ClosedCount { get; set; }

        /// <summary>
        /// Среднее время ответа (в часах)
        /// </summary>
        public double AverageResponseTime { get; set; }

        /// <summary>
        /// Среднее время решения (в часах)
        /// </summary>
        public double AverageResolutionTime { get; set; }

        /// <summary>
        /// Средняя оценка удовлетворенности
        /// </summary>
        public double AverageSatisfactionRating { get; set; }

        /// <summary>
        /// Статистика по типам обратной связи
        /// </summary>
        public Dictionary<FeedbackType, int> TypeStatistics { get; set; } = new Dictionary<FeedbackType, int>();

        /// <summary>
        /// Статистика по приоритетам обратной связи
        /// </summary>
        public Dictionary<FeedbackPriority, int> PriorityStatistics { get; set; } = new Dictionary<FeedbackPriority, int>();
    }
}
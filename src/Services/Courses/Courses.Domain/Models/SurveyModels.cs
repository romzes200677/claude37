using System;
using System.Collections.Generic;

namespace Courses.Domain.Models
{
    /// <summary>
    /// Модель создания опроса
    /// </summary>
    public class CreateSurveyModel
    {
        /// <summary>
        /// Название опроса
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание опроса
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Дата начала
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Дата окончания
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Идентификатор курса (если опрос связан с курсом)
        /// </summary>
        public Guid? CourseId { get; set; }

        /// <summary>
        /// Идентификатор урока (если опрос связан с уроком)
        /// </summary>
        public Guid? LessonId { get; set; }

        /// <summary>
        /// Является ли опрос анонимным
        /// </summary>
        public bool IsAnonymous { get; set; } = false;

        /// <summary>
        /// Разрешено ли несколько ответов от одного пользователя
        /// </summary>
        public bool AllowMultipleResponses { get; set; } = false;

        /// <summary>
        /// Вопросы опроса
        /// </summary>
        public List<SurveyQuestionModel> Questions { get; set; } = new List<SurveyQuestionModel>();
    }

    /// <summary>
    /// Модель обновления опроса
    /// </summary>
    public class UpdateSurveyModel
    {
        /// <summary>
        /// Название опроса
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание опроса
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Дата начала
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Дата окончания
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Является ли опрос анонимным
        /// </summary>
        public bool IsAnonymous { get; set; }

        /// <summary>
        /// Разрешено ли несколько ответов от одного пользователя
        /// </summary>
        public bool AllowMultipleResponses { get; set; }

        /// <summary>
        /// Вопросы опроса
        /// </summary>
        public List<SurveyQuestionModel> Questions { get; set; } = new List<SurveyQuestionModel>();
    }

    /// <summary>
    /// Модель представления опроса
    /// </summary>
    public class SurveyViewModel
    {
        /// <summary>
        /// Идентификатор опроса
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название опроса
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание опроса
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Дата начала
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Дата окончания
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Идентификатор курса (если опрос связан с курсом)
        /// </summary>
        public Guid? CourseId { get; set; }

        /// <summary>
        /// Название курса
        /// </summary>
        public string CourseName { get; set; }

        /// <summary>
        /// Идентификатор урока (если опрос связан с уроком)
        /// </summary>
        public Guid? LessonId { get; set; }

        /// <summary>
        /// Название урока
        /// </summary>
        public string LessonName { get; set; }

        /// <summary>
        /// Идентификатор создателя
        /// </summary>
        public Guid CreatedById { get; set; }

        /// <summary>
        /// Имя создателя
        /// </summary>
        public string CreatedByName { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Дата обновления
        /// </summary>
        public DateTime? UpdatedAt { get; set; }

        /// <summary>
        /// Статус опроса
        /// </summary>
        public SurveyStatus Status { get; set; }

        /// <summary>
        /// Является ли опрос анонимным
        /// </summary>
        public bool IsAnonymous { get; set; }

        /// <summary>
        /// Разрешено ли несколько ответов от одного пользователя
        /// </summary>
        public bool AllowMultipleResponses { get; set; }

        /// <summary>
        /// Количество ответов
        /// </summary>
        public int ResponseCount { get; set; }

        /// <summary>
        /// Вопросы опроса
        /// </summary>
        public List<SurveyQuestionModel> Questions { get; set; } = new List<SurveyQuestionModel>();
    }

    /// <summary>
    /// Статус опроса
    /// </summary>
    public enum SurveyStatus
    {
        /// <summary>
        /// Черновик
        /// </summary>
        Draft,

        /// <summary>
        /// Опубликован
        /// </summary>
        Published,

        /// <summary>
        /// Закрыт
        /// </summary>
        Closed,

        /// <summary>
        /// Архивирован
        /// </summary>
        Archived
    }

    /// <summary>
    /// Модель вопроса опроса
    /// </summary>
    public class SurveyQuestionModel
    {
        /// <summary>
        /// Идентификатор вопроса
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Текст вопроса
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Тип вопроса
        /// </summary>
        public QuestionType Type { get; set; }

        /// <summary>
        /// Является ли вопрос обязательным
        /// </summary>
        public bool IsRequired { get; set; } = true;

        /// <summary>
        /// Порядковый номер
        /// </summary>
        public int OrderIndex { get; set; }

        /// <summary>
        /// Варианты ответов (для вопросов с выбором)
        /// </summary>
        public List<SurveyQuestionOptionModel> Options { get; set; } = new List<SurveyQuestionOptionModel>();
    }

    /// <summary>
    /// Тип вопроса
    /// </summary>
    public enum QuestionType
    {
        /// <summary>
        /// Текстовый ответ
        /// </summary>
        Text,

        /// <summary>
        /// Многострочный текстовый ответ
        /// </summary>
        TextArea,

        /// <summary>
        /// Один вариант ответа
        /// </summary>
        SingleChoice,

        /// <summary>
        /// Несколько вариантов ответа
        /// </summary>
        MultipleChoice,

        /// <summary>
        /// Шкала оценки
        /// </summary>
        Rating,

        /// <summary>
        /// Дата
        /// </summary>
        Date,

        /// <summary>
        /// Файл
        /// </summary>
        File
    }

    /// <summary>
    /// Модель варианта ответа на вопрос
    /// </summary>
    public class SurveyQuestionOptionModel
    {
        /// <summary>
        /// Идентификатор варианта ответа
        /// </summary>
        public Guid Id { get; set; } = Guid.NewGuid();

        /// <summary>
        /// Текст варианта ответа
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Порядковый номер
        /// </summary>
        public int OrderIndex { get; set; }
    }

    /// <summary>
    /// Модель ответа на опрос
    /// </summary>
    public class SurveyResponseModel
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid? UserId { get; set; }

        /// <summary>
        /// Ответы на вопросы
        /// </summary>
        public List<SurveyQuestionResponseModel> QuestionResponses { get; set; } = new List<SurveyQuestionResponseModel>();
    }

    /// <summary>
    /// Модель ответа на вопрос опроса
    /// </summary>
    public class SurveyQuestionResponseModel
    {
        /// <summary>
        /// Идентификатор вопроса
        /// </summary>
        public Guid QuestionId { get; set; }

        /// <summary>
        /// Текстовый ответ
        /// </summary>
        public string TextResponse { get; set; }

        /// <summary>
        /// Идентификаторы выбранных вариантов ответа
        /// </summary>
        public List<Guid> SelectedOptionIds { get; set; } = new List<Guid>();

        /// <summary>
        /// Оценка (для вопросов типа Rating)
        /// </summary>
        public int? RatingResponse { get; set; }

        /// <summary>
        /// Дата (для вопросов типа Date)
        /// </summary>
        public DateTime? DateResponse { get; set; }

        /// <summary>
        /// URL файла (для вопросов типа File)
        /// </summary>
        public string FileUrl { get; set; }
    }

    /// <summary>
    /// Модель представления ответа на опрос
    /// </summary>
    public class SurveyResponseViewModel
    {
        /// <summary>
        /// Идентификатор ответа
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор опроса
        /// </summary>
        public Guid SurveyId { get; set; }

        /// <summary>
        /// Название опроса
        /// </summary>
        public string SurveyTitle { get; set; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid? UserId { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Дата и время ответа
        /// </summary>
        public DateTime SubmittedAt { get; set; }

        /// <summary>
        /// Ответы на вопросы
        /// </summary>
        public List<SurveyQuestionResponseViewModel> QuestionResponses { get; set; } = new List<SurveyQuestionResponseViewModel>();
    }

    /// <summary>
    /// Модель представления ответа на вопрос опроса
    /// </summary>
    public class SurveyQuestionResponseViewModel
    {
        /// <summary>
        /// Идентификатор вопроса
        /// </summary>
        public Guid QuestionId { get; set; }

        /// <summary>
        /// Текст вопроса
        /// </summary>
        public string QuestionText { get; set; }

        /// <summary>
        /// Тип вопроса
        /// </summary>
        public QuestionType QuestionType { get; set; }

        /// <summary>
        /// Текстовый ответ
        /// </summary>
        public string TextResponse { get; set; }

        /// <summary>
        /// Выбранные варианты ответа
        /// </summary>
        public List<SurveyQuestionOptionViewModel> SelectedOptions { get; set; } = new List<SurveyQuestionOptionViewModel>();

        /// <summary>
        /// Оценка (для вопросов типа Rating)
        /// </summary>
        public int? RatingResponse { get; set; }

        /// <summary>
        /// Дата (для вопросов типа Date)
        /// </summary>
        public DateTime? DateResponse { get; set; }

        /// <summary>
        /// URL файла (для вопросов типа File)
        /// </summary>
        public string FileUrl { get; set; }
    }

    /// <summary>
    /// Модель представления варианта ответа на вопрос
    /// </summary>
    public class SurveyQuestionOptionViewModel
    {
        /// <summary>
        /// Идентификатор варианта ответа
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Текст варианта ответа
        /// </summary>
        public string Text { get; set; }
    }

    /// <summary>
    /// Модель результатов опроса
    /// </summary>
    public class SurveyResultsModel
    {
        /// <summary>
        /// Идентификатор опроса
        /// </summary>
        public Guid SurveyId { get; set; }

        /// <summary>
        /// Название опроса
        /// </summary>
        public string SurveyTitle { get; set; }

        /// <summary>
        /// Общее количество ответов
        /// </summary>
        public int TotalResponses { get; set; }

        /// <summary>
        /// Результаты по вопросам
        /// </summary>
        public List<QuestionResultModel> QuestionResults { get; set; } = new List<QuestionResultModel>();
    }

    /// <summary>
    /// Модель результатов вопроса
    /// </summary>
    public class QuestionResultModel
    {
        /// <summary>
        /// Идентификатор вопроса
        /// </summary>
        public Guid QuestionId { get; set; }

        /// <summary>
        /// Текст вопроса
        /// </summary>
        public string QuestionText { get; set; }

        /// <summary>
        /// Тип вопроса
        /// </summary>
        public QuestionType QuestionType { get; set; }

        /// <summary>
        /// Результаты для вариантов ответа (для вопросов с выбором)
        /// </summary>
        public List<OptionResultModel> OptionResults { get; set; } = new List<OptionResultModel>();

        /// <summary>
        /// Текстовые ответы (для текстовых вопросов)
        /// </summary>
        public List<string> TextResponses { get; set; } = new List<string>();

        /// <summary>
        /// Средняя оценка (для вопросов типа Rating)
        /// </summary>
        public double? AverageRating { get; set; }

        /// <summary>
        /// Распределение оценок (для вопросов типа Rating)
        /// </summary>
        public Dictionary<int, int> RatingDistribution { get; set; } = new Dictionary<int, int>();
    }

    /// <summary>
    /// Модель результатов варианта ответа
    /// </summary>
    public class OptionResultModel
    {
        /// <summary>
        /// Идентификатор варианта ответа
        /// </summary>
        public Guid OptionId { get; set; }

        /// <summary>
        /// Текст варианта ответа
        /// </summary>
        public string OptionText { get; set; }

        /// <summary>
        /// Количество выборов
        /// </summary>
        public int Count { get; set; }

        /// <summary>
        /// Процент выборов
        /// </summary>
        public double Percentage { get; set; }
    }

    /// <summary>
    /// Модель фильтра опросов
    /// </summary>
    public class SurveyFilterModel : PaginationRequestModel
    {
        /// <summary>
        /// Идентификатор создателя
        /// </summary>
        public Guid? CreatedById { get; set; }

        /// <summary>
        /// Статус опроса
        /// </summary>
        public SurveyStatus? Status { get; set; }

        /// <summary>
        /// Идентификатор курса
        /// </summary>
        public Guid? CourseId { get; set; }

        /// <summary>
        /// Идентификатор урока
        /// </summary>
        public Guid? LessonId { get; set; }

        /// <summary>
        /// Поисковый запрос
        /// </summary>
        public string SearchQuery { get; set; }

        /// <summary>
        /// Дата начала
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Дата окончания
        /// </summary>
        public DateTime? EndDate { get; set; }
    }
}
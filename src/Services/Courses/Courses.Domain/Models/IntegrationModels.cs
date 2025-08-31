using System;

namespace Courses.Domain.Models
{
    /// <summary>
    /// Модель информации о пользователе
    /// </summary>
    public class UserInfoModel
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Полное имя
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Электронная почта
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// URL аватара
        /// </summary>
        public string AvatarUrl { get; set; }

        /// <summary>
        /// Роли пользователя
        /// </summary>
        public string[] Roles { get; set; }
    }

    /// <summary>
    /// Модель информации о тесте
    /// </summary>
    public class TestInfoModel
    {
        /// <summary>
        /// Идентификатор теста
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название теста
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание теста
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Максимальный балл
        /// </summary>
        public int MaxScore { get; set; }

        /// <summary>
        /// Проходной балл
        /// </summary>
        public int PassingScore { get; set; }

        /// <summary>
        /// Время на выполнение (в минутах)
        /// </summary>
        public int TimeLimit { get; set; }

        /// <summary>
        /// Количество вопросов
        /// </summary>
        public int QuestionCount { get; set; }
    }

    /// <summary>
    /// Модель результатов теста
    /// </summary>
    public class TestResultModel
    {
        /// <summary>
        /// Идентификатор результата
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор теста
        /// </summary>
        public Guid TestId { get; set; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Набранный балл
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Максимальный балл
        /// </summary>
        public int MaxScore { get; set; }

        /// <summary>
        /// Процент правильных ответов
        /// </summary>
        public decimal Percentage { get; set; }

        /// <summary>
        /// Результат (сдан/не сдан)
        /// </summary>
        public bool Passed { get; set; }

        /// <summary>
        /// Дата начала
        /// </summary>
        public DateTime StartedAt { get; set; }

        /// <summary>
        /// Дата завершения
        /// </summary>
        public DateTime CompletedAt { get; set; }

        /// <summary>
        /// Затраченное время (в минутах)
        /// </summary>
        public int TimeSpent { get; set; }
    }

    /// <summary>
    /// Модель информации о задаче по программированию
    /// </summary>
    public class CodeTaskInfoModel
    {
        /// <summary>
        /// Идентификатор задачи
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название задачи
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание задачи
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Сложность задачи
        /// </summary>
        public string Difficulty { get; set; }

        /// <summary>
        /// Максимальный балл
        /// </summary>
        public int MaxScore { get; set; }

        /// <summary>
        /// Проходной балл
        /// </summary>
        public int PassingScore { get; set; }

        /// <summary>
        /// Время на выполнение (в минутах)
        /// </summary>
        public int TimeLimit { get; set; }

        /// <summary>
        /// Поддерживаемые языки программирования
        /// </summary>
        public string[] SupportedLanguages { get; set; }
    }

    /// <summary>
    /// Модель результатов выполнения задачи по программированию
    /// </summary>
    public class CodeTaskResultModel
    {
        /// <summary>
        /// Идентификатор результата
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Идентификатор задачи
        /// </summary>
        public Guid CodeTaskId { get; set; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Язык программирования
        /// </summary>
        public string Language { get; set; }

        /// <summary>
        /// Исходный код решения
        /// </summary>
        public string SourceCode { get; set; }

        /// <summary>
        /// Набранный балл
        /// </summary>
        public int Score { get; set; }

        /// <summary>
        /// Максимальный балл
        /// </summary>
        public int MaxScore { get; set; }

        /// <summary>
        /// Процент успешных тестов
        /// </summary>
        public decimal Percentage { get; set; }

        /// <summary>
        /// Результат (сдан/не сдан)
        /// </summary>
        public bool Passed { get; set; }

        /// <summary>
        /// Дата начала
        /// </summary>
        public DateTime StartedAt { get; set; }

        /// <summary>
        /// Дата завершения
        /// </summary>
        public DateTime CompletedAt { get; set; }

        /// <summary>
        /// Затраченное время (в минутах)
        /// </summary>
        public int TimeSpent { get; set; }

        /// <summary>
        /// Результаты тестов
        /// </summary>
        public CodeTaskTestResultModel[] TestResults { get; set; }
    }

    /// <summary>
    /// Модель результата теста задачи по программированию
    /// </summary>
    public class CodeTaskTestResultModel
    {
        /// <summary>
        /// Идентификатор теста
        /// </summary>
        public Guid TestId { get; set; }

        /// <summary>
        /// Название теста
        /// </summary>
        public string TestName { get; set; }

        /// <summary>
        /// Результат (успешно/неуспешно)
        /// </summary>
        public bool Passed { get; set; }

        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Время выполнения (в миллисекундах)
        /// </summary>
        public int ExecutionTime { get; set; }

        /// <summary>
        /// Использованная память (в килобайтах)
        /// </summary>
        public int MemoryUsed { get; set; }
    }
}
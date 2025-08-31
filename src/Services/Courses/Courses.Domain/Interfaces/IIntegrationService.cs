using System;
using System.Threading.Tasks;
using Courses.Domain.Models;

namespace Courses.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса интеграции с внешними системами
    /// </summary>
    public interface IIntegrationService
    {
        /// <summary>
        /// Получить информацию о пользователе
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns>Информация о пользователе</returns>
        Task<UserInfoModel> GetUserInfoAsync(Guid userId);

        /// <summary>
        /// Получить информацию о тесте
        /// </summary>
        /// <param name="testId">Идентификатор теста</param>
        /// <returns>Информация о тесте</returns>
        Task<TestInfoModel> GetTestInfoAsync(Guid testId);

        /// <summary>
        /// Получить результаты теста
        /// </summary>
        /// <param name="testId">Идентификатор теста</param>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns>Результаты теста</returns>
        Task<TestResultModel> GetTestResultsAsync(Guid testId, Guid userId);

        /// <summary>
        /// Получить информацию о задаче по программированию
        /// </summary>
        /// <param name="codeTaskId">Идентификатор задачи</param>
        /// <returns>Информация о задаче</returns>
        Task<CodeTaskInfoModel> GetCodeTaskInfoAsync(Guid codeTaskId);

        /// <summary>
        /// Получить результаты выполнения задачи по программированию
        /// </summary>
        /// <param name="codeTaskId">Идентификатор задачи</param>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns>Результаты выполнения задачи</returns>
        Task<CodeTaskResultModel> GetCodeTaskResultsAsync(Guid codeTaskId, Guid userId);

        /// <summary>
        /// Синхронизировать данные о курсе с другими микросервисами
        /// </summary>
        /// <param name="courseId">Идентификатор курса</param>
        /// <returns>Задача</returns>
        Task SyncCourseDataAsync(Guid courseId);
    }
}
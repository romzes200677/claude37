using System;
using System.Threading.Tasks;

namespace Courses.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса планировщика задач
    /// </summary>
    public interface ISchedulerService
    {
        /// <summary>
        /// Запланировать задачу на однократное выполнение
        /// </summary>
        /// <param name="jobName">Название задачи</param>
        /// <param name="jobData">Данные задачи</param>
        /// <param name="executeAt">Время выполнения</param>
        /// <returns>Идентификатор задачи</returns>
        Task<string> ScheduleJobAsync(string jobName, object jobData, DateTime executeAt);

        /// <summary>
        /// Запланировать повторяющуюся задачу
        /// </summary>
        /// <param name="jobName">Название задачи</param>
        /// <param name="jobData">Данные задачи</param>
        /// <param name="cronExpression">Выражение CRON</param>
        /// <returns>Идентификатор задачи</returns>
        Task<string> ScheduleRecurringJobAsync(string jobName, object jobData, string cronExpression);

        /// <summary>
        /// Отменить задачу
        /// </summary>
        /// <param name="jobId">Идентификатор задачи</param>
        /// <returns>Задача</returns>
        Task CancelJobAsync(string jobId);

        /// <summary>
        /// Получить статус задачи
        /// </summary>
        /// <param name="jobId">Идентификатор задачи</param>
        /// <returns>Статус задачи</returns>
        Task<string> GetJobStatusAsync(string jobId);

        /// <summary>
        /// Запустить задачу немедленно
        /// </summary>
        /// <param name="jobId">Идентификатор задачи</param>
        /// <returns>Задача</returns>
        Task TriggerJobAsync(string jobId);

        /// <summary>
        /// Приостановить задачу
        /// </summary>
        /// <param name="jobId">Идентификатор задачи</param>
        /// <returns>Задача</returns>
        Task PauseJobAsync(string jobId);

        /// <summary>
        /// Возобновить задачу
        /// </summary>
        /// <param name="jobId">Идентификатор задачи</param>
        /// <returns>Задача</returns>
        Task ResumeJobAsync(string jobId);
    }
}
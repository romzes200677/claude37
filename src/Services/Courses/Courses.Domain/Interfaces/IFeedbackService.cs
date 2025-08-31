using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Courses.Domain.Models;

namespace Courses.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса обратной связи
    /// </summary>
    public interface IFeedbackService
    {
        /// <summary>
        /// Отправить обратную связь
        /// </summary>
        /// <param name="feedback">Модель обратной связи</param>
        /// <returns>Идентификатор обратной связи</returns>
        Task<Guid> SubmitFeedbackAsync(FeedbackModel feedback);

        /// <summary>
        /// Получить обратную связь по идентификатору
        /// </summary>
        /// <param name="feedbackId">Идентификатор обратной связи</param>
        /// <returns>Модель обратной связи</returns>
        Task<FeedbackModel> GetFeedbackByIdAsync(Guid feedbackId);

        /// <summary>
        /// Получить список обратной связи
        /// </summary>
        /// <param name="filter">Фильтр</param>
        /// <returns>Список обратной связи</returns>
        Task<PaginationResponseModel<FeedbackModel>> GetFeedbackListAsync(FeedbackFilterModel filter);

        /// <summary>
        /// Обновить статус обратной связи
        /// </summary>
        /// <param name="feedbackId">Идентификатор обратной связи</param>
        /// <param name="status">Новый статус</param>
        /// <param name="responseMessage">Сообщение ответа</param>
        /// <returns>Задача</returns>
        Task UpdateFeedbackStatusAsync(Guid feedbackId, FeedbackStatus status, string responseMessage = null);

        /// <summary>
        /// Назначить обратную связь сотруднику
        /// </summary>
        /// <param name="feedbackId">Идентификатор обратной связи</param>
        /// <param name="assigneeId">Идентификатор сотрудника</param>
        /// <returns>Задача</returns>
        Task AssignFeedbackAsync(Guid feedbackId, Guid assigneeId);

        /// <summary>
        /// Добавить комментарий к обратной связи
        /// </summary>
        /// <param name="feedbackId">Идентификатор обратной связи</param>
        /// <param name="comment">Комментарий</param>
        /// <returns>Идентификатор комментария</returns>
        Task<Guid> AddFeedbackCommentAsync(Guid feedbackId, FeedbackCommentModel comment);

        /// <summary>
        /// Получить комментарии к обратной связи
        /// </summary>
        /// <param name="feedbackId">Идентификатор обратной связи</param>
        /// <returns>Список комментариев</returns>
        Task<List<FeedbackCommentModel>> GetFeedbackCommentsAsync(Guid feedbackId);

        /// <summary>
        /// Получить статистику обратной связи
        /// </summary>
        /// <param name="startDate">Дата начала</param>
        /// <param name="endDate">Дата окончания</param>
        /// <returns>Статистика обратной связи</returns>
        Task<FeedbackStatisticsModel> GetFeedbackStatisticsAsync(DateTime? startDate = null, DateTime? endDate = null);
    }
}
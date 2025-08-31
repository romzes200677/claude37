using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courses.Domain.Entities;
using Courses.Domain.Interfaces;
using Courses.Domain.Models;
using Microsoft.Extensions.Logging;

namespace Courses.Infrastructure.Services
{
    /// <summary>
    /// Сервис обратной связи
    /// </summary>
    public class FeedbackService : IFeedbackService
    {
        private readonly IFeedbackRepository _feedbackRepository;
        private readonly IFeedbackCommentRepository _feedbackCommentRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly ILogService _logService;
        private readonly ILogger<FeedbackService> _logger;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="feedbackRepository">Репозиторий обратной связи</param>
        /// <param name="feedbackCommentRepository">Репозиторий комментариев к обратной связи</param>
        /// <param name="courseRepository">Репозиторий курсов</param>
        /// <param name="logService">Сервис логирования</param>
        /// <param name="logger">Логгер</param>
        public FeedbackService(
            IFeedbackRepository feedbackRepository,
            IFeedbackCommentRepository feedbackCommentRepository,
            ICourseRepository courseRepository,
            ILogService logService,
            ILogger<FeedbackService> logger)
        {
            _feedbackRepository = feedbackRepository ?? throw new ArgumentNullException(nameof(feedbackRepository));
            _feedbackCommentRepository = feedbackCommentRepository ?? throw new ArgumentNullException(nameof(feedbackCommentRepository));
            _courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
            _logService = logService ?? throw new ArgumentNullException(nameof(logService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Отправить обратную связь
        /// </summary>
        /// <param name="feedback">Обратная связь</param>
        /// <returns>Созданная обратная связь</returns>
        public async Task<Feedback> SubmitFeedbackAsync(Feedback feedback)
        {
            try
            {
                _logService.Information("Отправка обратной связи от пользователя {UserId} по курсу {CourseId}", feedback.UserId, feedback.CourseId);

                // Проверка существования курса, если обратная связь привязана к курсу
                if (feedback.CourseId.HasValue)
                {
                    var course = await _courseRepository.GetByIdAsync(feedback.CourseId.Value);
                    if (course == null)
                    {
                        _logService.Warning("Курс с идентификатором {CourseId} не найден", feedback.CourseId.Value);
                        throw new ArgumentException($"Курс с идентификатором {feedback.CourseId.Value} не найден");
                    }
                }

                // Установка даты создания и статуса
                feedback.CreatedAt = DateTime.UtcNow;
                feedback.Status = "pending"; // Статус "ожидает рассмотрения"

                // Сохранение обратной связи
                return await _feedbackRepository.AddAsync(feedback);
            }
            catch (Exception ex)
            {
                _logService.Error(ex, "Ошибка при отправке обратной связи от пользователя {UserId} по курсу {CourseId}", feedback.UserId, feedback.CourseId);
                throw;
            }
        }

        /// <summary>
        /// Получить обратную связь по идентификатору
        /// </summary>
        /// <param name="feedbackId">Идентификатор обратной связи</param>
        /// <returns>Обратная связь</returns>
        public async Task<Feedback> GetFeedbackByIdAsync(Guid feedbackId)
        {
            try
            {
                _logService.Information("Получение обратной связи по идентификатору {FeedbackId}", feedbackId);
                return await _feedbackRepository.GetByIdAsync(feedbackId);
            }
            catch (Exception ex)
            {
                _logService.Error(ex, "Ошибка при получении обратной связи по идентификатору {FeedbackId}", feedbackId);
                throw;
            }
        }

        /// <summary>
        /// Получить обратную связь по идентификатору пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns>Коллекция обратной связи</returns>
        public async Task<IEnumerable<Feedback>> GetFeedbackByUserIdAsync(Guid userId)
        {
            try
            {
                _logService.Information("Получение обратной связи по идентификатору пользователя {UserId}", userId);
                return await _feedbackRepository.GetFeedbackByUserIdAsync(userId);
            }
            catch (Exception ex)
            {
                _logService.Error(ex, "Ошибка при получении обратной связи по идентификатору пользователя {UserId}", userId);
                throw;
            }
        }

        /// <summary>
        /// Получить обратную связь по идентификатору курса
        /// </summary>
        /// <param name="courseId">Идентификатор курса</param>
        /// <returns>Коллекция обратной связи</returns>
        public async Task<IEnumerable<Feedback>> GetFeedbackByCourseIdAsync(Guid courseId)
        {
            try
            {
                _logService.Information("Получение обратной связи по идентификатору курса {CourseId}", courseId);
                return await _feedbackRepository.GetFeedbackByCourseIdAsync(courseId);
            }
            catch (Exception ex)
            {
                _logService.Error(ex, "Ошибка при получении обратной связи по идентификатору курса {CourseId}", courseId);
                throw;
            }
        }

        /// <summary>
        /// Обновить обратную связь
        /// </summary>
        /// <param name="feedback">Обратная связь</param>
        /// <returns>Обновленная обратная связь</returns>
        public async Task<Feedback> UpdateFeedbackAsync(Feedback feedback)
        {
            try
            {
                _logService.Information("Обновление обратной связи {FeedbackId}", feedback.Id);

                // Проверка существования обратной связи
                var existingFeedback = await _feedbackRepository.GetByIdAsync(feedback.Id);
                if (existingFeedback == null)
                {
                    _logService.Warning("Обратная связь с идентификатором {FeedbackId} не найдена", feedback.Id);
                    throw new ArgumentException($"Обратная связь с идентификатором {feedback.Id} не найдена");
                }

                // Обновление полей обратной связи
                existingFeedback.Title = feedback.Title;
                existingFeedback.Description = feedback.Description;
                existingFeedback.Category = feedback.Category;
                existingFeedback.Priority = feedback.Priority;
                existingFeedback.UpdatedAt = DateTime.UtcNow;

                // Сохранение обратной связи
                return await _feedbackRepository.UpdateAsync(existingFeedback);
            }
            catch (Exception ex)
            {
                _logService.Error(ex, "Ошибка при обновлении обратной связи {FeedbackId}", feedback.Id);
                throw;
            }
        }

        /// <summary>
        /// Назначить обратную связь
        /// </summary>
        /// <param name="feedbackId">Идентификатор обратной связи</param>
        /// <param name="assigneeId">Идентификатор назначенного пользователя</param>
        /// <returns>Обновленная обратная связь</returns>
        public async Task<Feedback> AssignFeedbackAsync(Guid feedbackId, Guid assigneeId)
        {
            try
            {
                _logService.Information("Назначение обратной связи {FeedbackId} пользователю {AssigneeId}", feedbackId, assigneeId);

                // Проверка существования обратной связи
                var feedback = await _feedbackRepository.GetByIdAsync(feedbackId);
                if (feedback == null)
                {
                    _logService.Warning("Обратная связь с идентификатором {FeedbackId} не найдена", feedbackId);
                    throw new ArgumentException($"Обратная связь с идентификатором {feedbackId} не найдена");
                }

                // Обновление назначенного пользователя и статуса
                feedback.AssigneeId = assigneeId;
                feedback.Status = "assigned"; // Статус "назначено"
                feedback.UpdatedAt = DateTime.UtcNow;

                // Сохранение обратной связи
                return await _feedbackRepository.UpdateAsync(feedback);
            }
            catch (Exception ex)
            {
                _logService.Error(ex, "Ошибка при назначении обратной связи {FeedbackId} пользователю {AssigneeId}", feedbackId, assigneeId);
                throw;
            }
        }

        /// <summary>
        /// Обновить статус обратной связи
        /// </summary>
        /// <param name="feedbackId">Идентификатор обратной связи</param>
        /// <param name="status">Статус</param>
        /// <returns>Обновленная обратная связь</returns>
        public async Task<Feedback> UpdateFeedbackStatusAsync(Guid feedbackId, string status)
        {
            try
            {
                _logService.Information("Обновление статуса обратной связи {FeedbackId} на {Status}", feedbackId, status);

                // Проверка существования обратной связи
                var feedback = await _feedbackRepository.GetByIdAsync(feedbackId);
                if (feedback == null)
                {
                    _logService.Warning("Обратная связь с идентификатором {FeedbackId} не найдена", feedbackId);
                    throw new ArgumentException($"Обратная связь с идентификатором {feedbackId} не найдена");
                }

                // Проверка допустимости статуса
                var validStatuses = new[] { "pending", "assigned", "in_progress", "resolved", "closed", "reopened" };
                if (!validStatuses.Contains(status))
                {
                    _logService.Warning("Недопустимый статус обратной связи: {Status}", status);
                    throw new ArgumentException($"Недопустимый статус обратной связи: {status}");
                }

                // Обновление статуса
                feedback.Status = status;
                feedback.UpdatedAt = DateTime.UtcNow;

                // Если статус "resolved" или "closed", устанавливаем дату разрешения
                if (status == "resolved" || status == "closed")
                {
                    feedback.ResolvedAt = DateTime.UtcNow;
                }

                // Сохранение обратной связи
                return await _feedbackRepository.UpdateAsync(feedback);
            }
            catch (Exception ex)
            {
                _logService.Error(ex, "Ошибка при обновлении статуса обратной связи {FeedbackId} на {Status}", feedbackId, status);
                throw;
            }
        }

        /// <summary>
        /// Добавить комментарий к обратной связи
        /// </summary>
        /// <param name="comment">Комментарий</param>
        /// <returns>Созданный комментарий</returns>
        public async Task<FeedbackComment> AddCommentAsync(FeedbackComment comment)
        {
            try
            {
                _logService.Information("Добавление комментария к обратной связи {FeedbackId} от пользователя {UserId}", comment.FeedbackId, comment.UserId);

                // Проверка существования обратной связи
                var feedback = await _feedbackRepository.GetByIdAsync(comment.FeedbackId);
                if (feedback == null)
                {
                    _logService.Warning("Обратная связь с идентификатором {FeedbackId} не найдена", comment.FeedbackId);
                    throw new ArgumentException($"Обратная связь с идентификатором {comment.FeedbackId} не найдена");
                }

                // Установка даты создания
                comment.CreatedAt = DateTime.UtcNow;

                // Сохранение комментария
                return await _feedbackCommentRepository.AddAsync(comment);
            }
            catch (Exception ex)
            {
                _logService.Error(ex, "Ошибка при добавлении комментария к обратной связи {FeedbackId} от пользователя {UserId}", comment.FeedbackId, comment.UserId);
                throw;
            }
        }

        /// <summary>
        /// Получить комментарии к обратной связи
        /// </summary>
        /// <param name="feedbackId">Идентификатор обратной связи</param>
        /// <returns>Коллекция комментариев</returns>
        public async Task<IEnumerable<FeedbackComment>> GetCommentsByFeedbackIdAsync(Guid feedbackId)
        {
            try
            {
                _logService.Information("Получение комментариев к обратной связи {FeedbackId}", feedbackId);

                // Проверка существования обратной связи
                var feedback = await _feedbackRepository.GetByIdAsync(feedbackId);
                if (feedback == null)
                {
                    _logService.Warning("Обратная связь с идентификатором {FeedbackId} не найдена", feedbackId);
                    throw new ArgumentException($"Обратная связь с идентификатором {feedbackId} не найдена");
                }

                return await _feedbackCommentRepository.GetCommentsByFeedbackIdAsync(feedbackId);
            }
            catch (Exception ex)
            {
                _logService.Error(ex, "Ошибка при получении комментариев к обратной связи {FeedbackId}", feedbackId);
                throw;
            }
        }

        /// <summary>
        /// Получить статистику по обратной связи
        /// </summary>
        /// <returns>Статистика по обратной связи</returns>
        public async Task<FeedbackStatisticsModel> GetFeedbackStatisticsAsync()
        {
            try
            {
                _logService.Information("Получение статистики по обратной связи");

                // Получение всей обратной связи
                var allFeedback = await _feedbackRepository.GetAllAsync();

                // Расчет статистики
                var statistics = new FeedbackStatisticsModel
                {
                    TotalFeedback = allFeedback.Count(),
                    PendingFeedback = allFeedback.Count(f => f.Status == "pending"),
                    AssignedFeedback = allFeedback.Count(f => f.Status == "assigned"),
                    InProgressFeedback = allFeedback.Count(f => f.Status == "in_progress"),
                    ResolvedFeedback = allFeedback.Count(f => f.Status == "resolved"),
                    ClosedFeedback = allFeedback.Count(f => f.Status == "closed"),
                    ReopenedFeedback = allFeedback.Count(f => f.Status == "reopened"),
                    AverageResolutionTime = CalculateAverageResolutionTime(allFeedback),
                    FeedbackByCategory = CalculateFeedbackByCategory(allFeedback),
                    FeedbackByPriority = CalculateFeedbackByPriority(allFeedback)
                };

                return statistics;
            }
            catch (Exception ex)
            {
                _logService.Error(ex, "Ошибка при получении статистики по обратной связи");
                throw;
            }
        }

        /// <summary>
        /// Получить статистику по обратной связи для курса
        /// </summary>
        /// <param name="courseId">Идентификатор курса</param>
        /// <returns>Статистика по обратной связи</returns>
        public async Task<FeedbackStatisticsModel> GetFeedbackStatisticsByCourseIdAsync(Guid courseId)
        {
            try
            {
                _logService.Information("Получение статистики по обратной связи для курса {CourseId}", courseId);

                // Проверка существования курса
                var course = await _courseRepository.GetByIdAsync(courseId);
                if (course == null)
                {
                    _logService.Warning("Курс с идентификатором {CourseId} не найден", courseId);
                    throw new ArgumentException($"Курс с идентификатором {courseId} не найден");
                }

                // Получение обратной связи для курса
                var courseFeedback = await _feedbackRepository.GetFeedbackByCourseIdAsync(courseId);

                // Расчет статистики
                var statistics = new FeedbackStatisticsModel
                {
                    TotalFeedback = courseFeedback.Count(),
                    PendingFeedback = courseFeedback.Count(f => f.Status == "pending"),
                    AssignedFeedback = courseFeedback.Count(f => f.Status == "assigned"),
                    InProgressFeedback = courseFeedback.Count(f => f.Status == "in_progress"),
                    ResolvedFeedback = courseFeedback.Count(f => f.Status == "resolved"),
                    ClosedFeedback = courseFeedback.Count(f => f.Status == "closed"),
                    ReopenedFeedback = courseFeedback.Count(f => f.Status == "reopened"),
                    AverageResolutionTime = CalculateAverageResolutionTime(courseFeedback),
                    FeedbackByCategory = CalculateFeedbackByCategory(courseFeedback),
                    FeedbackByPriority = CalculateFeedbackByPriority(courseFeedback)
                };

                return statistics;
            }
            catch (Exception ex)
            {
                _logService.Error(ex, "Ошибка при получении статистики по обратной связи для курса {CourseId}", courseId);
                throw;
            }
        }

        /// <summary>
        /// Рассчитать среднее время разрешения обратной связи
        /// </summary>
        /// <param name="feedbacks">Коллекция обратной связи</param>
        /// <returns>Среднее время разрешения в часах</returns>
        private double CalculateAverageResolutionTime(IEnumerable<Feedback> feedbacks)
        {
            var resolvedFeedbacks = feedbacks.Where(f => f.ResolvedAt.HasValue && f.CreatedAt.HasValue).ToList();
            if (!resolvedFeedbacks.Any())
            {
                return 0;
            }

            var totalHours = resolvedFeedbacks.Sum(f => (f.ResolvedAt.Value - f.CreatedAt.Value).TotalHours);
            return Math.Round(totalHours / resolvedFeedbacks.Count, 2);
        }

        /// <summary>
        /// Рассчитать количество обратной связи по категориям
        /// </summary>
        /// <param name="feedbacks">Коллекция обратной связи</param>
        /// <returns>Словарь с количеством обратной связи по категориям</returns>
        private Dictionary<string, int> CalculateFeedbackByCategory(IEnumerable<Feedback> feedbacks)
        {
            return feedbacks
                .GroupBy(f => f.Category ?? "uncategorized")
                .ToDictionary(g => g.Key, g => g.Count());
        }

        /// <summary>
        /// Рассчитать количество обратной связи по приоритетам
        /// </summary>
        /// <param name="feedbacks">Коллекция обратной связи</param>
        /// <returns>Словарь с количеством обратной связи по приоритетам</returns>
        private Dictionary<string, int> CalculateFeedbackByPriority(IEnumerable<Feedback> feedbacks)
        {
            return feedbacks
                .GroupBy(f => f.Priority ?? "normal")
                .ToDictionary(g => g.Key, g => g.Count());
        }
    }
}
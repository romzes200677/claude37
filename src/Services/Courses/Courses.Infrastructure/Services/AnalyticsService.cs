using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courses.Domain.Interfaces;
using Courses.Domain.Models;
using Microsoft.Extensions.Logging;

namespace Courses.Infrastructure.Services
{
    /// <summary>
    /// Сервис аналитики
    /// </summary>
    public class AnalyticsService : IAnalyticsService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IModuleRepository _moduleRepository;
        private readonly ILessonRepository _lessonRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly ILogService _logService;
        private readonly ILogger<AnalyticsService> _logger;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="courseRepository">Репозиторий курсов</param>
        /// <param name="enrollmentRepository">Репозиторий зачислений</param>
        /// <param name="moduleRepository">Репозиторий модулей</param>
        /// <param name="lessonRepository">Репозиторий уроков</param>
        /// <param name="reviewRepository">Репозиторий отзывов</param>
        /// <param name="logService">Сервис логирования</param>
        /// <param name="logger">Логгер</param>
        public AnalyticsService(
            ICourseRepository courseRepository,
            IEnrollmentRepository enrollmentRepository,
            IModuleRepository moduleRepository,
            ILessonRepository lessonRepository,
            IReviewRepository reviewRepository,
            ILogService logService,
            ILogger<AnalyticsService> logger)
        {
            _courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
            _enrollmentRepository = enrollmentRepository ?? throw new ArgumentNullException(nameof(enrollmentRepository));
            _moduleRepository = moduleRepository ?? throw new ArgumentNullException(nameof(moduleRepository));
            _lessonRepository = lessonRepository ?? throw new ArgumentNullException(nameof(lessonRepository));
            _reviewRepository = reviewRepository ?? throw new ArgumentNullException(nameof(reviewRepository));
            _logService = logService ?? throw new ArgumentNullException(nameof(logService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Получить статистику по курсу
        /// </summary>
        /// <param name="courseId">Идентификатор курса</param>
        /// <returns>Статистика по курсу</returns>
        public async Task<CourseStatisticsModel> GetCourseStatisticsAsync(Guid courseId)
        {
            try
            {
                _logService.Information("Получение статистики по курсу {CourseId}", courseId);

                var course = await _courseRepository.GetByIdAsync(courseId);
                if (course == null)
                {
                    _logService.Warning("Курс с идентификатором {CourseId} не найден", courseId);
                    return null;
                }

                var enrollments = await _enrollmentRepository.GetEnrollmentsByCourseIdAsync(courseId);
                var reviews = await _reviewRepository.GetReviewsByCourseIdAsync(courseId);
                var modules = await _moduleRepository.GetModulesByCourseIdAsync(courseId);

                var moduleStatistics = new List<ModuleStatisticsModel>();
                foreach (var module in modules)
                {
                    var lessons = await _lessonRepository.GetLessonsByModuleIdAsync(module.Id);
                    var lessonStatistics = lessons.Select(lesson => new LessonStatisticsModel
                    {
                        LessonId = lesson.Id,
                        LessonTitle = lesson.Title,
                        CompletionPercentage = 0, // Здесь должна быть логика расчета
                        AverageCompletionTime = 0, // Здесь должна быть логика расчета
                        AverageScore = 0 // Здесь должна быть логика расчета
                    }).ToList();

                    moduleStatistics.Add(new ModuleStatisticsModel
                    {
                        ModuleId = module.Id,
                        ModuleTitle = module.Title,
                        AverageCompletionPercentage = 0, // Здесь должна быть логика расчета
                        AverageCompletionTime = 0, // Здесь должна быть логика расчета
                        LessonStatistics = lessonStatistics
                    });
                }

                var activeEnrollments = enrollments.Count(e => e.Status == "Active");
                var completedEnrollments = enrollments.Count(e => e.Status == "Completed");
                var averageRating = reviews.Any() ? (decimal)reviews.Average(r => r.Rating) : 0;

                return new CourseStatisticsModel
                {
                    CourseId = course.Id,
                    CourseTitle = course.Title,
                    TotalEnrollments = enrollments.Count(),
                    ActiveEnrollments = activeEnrollments,
                    CompletedEnrollments = completedEnrollments,
                    AverageRating = averageRating,
                    ReviewCount = reviews.Count(),
                    AverageCompletionPercentage = 0, // Здесь должна быть логика расчета
                    AverageCompletionTime = 0, // Здесь должна быть логика расчета
                    ModuleStatistics = moduleStatistics
                };
            }
            catch (Exception ex)
            {
                _logService.Error(ex, "Ошибка при получении статистики по курсу {CourseId}", courseId);
                throw;
            }
        }

        /// <summary>
        /// Получить статистику по студенту
        /// </summary>
        /// <param name="studentId">Идентификатор студента</param>
        /// <returns>Статистика по студенту</returns>
        public async Task<StudentStatisticsModel> GetStudentStatisticsAsync(Guid studentId)
        {
            try
            {
                _logService.Information("Получение статистики по студенту {StudentId}", studentId);

                var enrollments = await _enrollmentRepository.GetEnrollmentsByStudentIdAsync(studentId);
                if (!enrollments.Any())
                {
                    _logService.Warning("Зачисления для студента {StudentId} не найдены", studentId);
                    return new StudentStatisticsModel
                    {
                        StudentId = studentId,
                        StudentName = "Неизвестно", // Здесь должно быть получение имени студента
                        TotalEnrollments = 0,
                        ActiveEnrollments = 0,
                        CompletedEnrollments = 0,
                        AverageCompletionPercentage = 0,
                        AverageGrade = 0,
                        CertificatesEarned = 0,
                        CourseStatistics = new List<StudentCourseStatisticsModel>()
                    };
                }

                var courseStatistics = new List<StudentCourseStatisticsModel>();
                foreach (var enrollment in enrollments)
                {
                    var course = await _courseRepository.GetByIdAsync(enrollment.CourseId);
                    if (course != null)
                    {
                        courseStatistics.Add(new StudentCourseStatisticsModel
                        {
                            CourseId = course.Id,
                            CourseTitle = course.Title,
                            EnrollmentStatus = enrollment.Status,
                            CompletionPercentage = enrollment.CompletionPercentage,
                            Grade = enrollment.Grade ?? 0,
                            StartDate = enrollment.EnrollmentDate,
                            CompletionDate = enrollment.CompletionDate,
                            CertificateIssued = enrollment.CertificateIssued
                        });
                    }
                }

                var activeEnrollments = enrollments.Count(e => e.Status == "Active");
                var completedEnrollments = enrollments.Count(e => e.Status == "Completed");
                var certificatesEarned = enrollments.Count(e => e.CertificateIssued);
                var averageCompletionPercentage = enrollments.Any() ? (decimal)enrollments.Average(e => e.CompletionPercentage) : 0;
                var averageGrade = enrollments.Any(e => e.Grade.HasValue) ? (decimal)enrollments.Where(e => e.Grade.HasValue).Average(e => e.Grade.Value) : 0;

                return new StudentStatisticsModel
                {
                    StudentId = studentId,
                    StudentName = "Неизвестно", // Здесь должно быть получение имени студента
                    TotalEnrollments = enrollments.Count(),
                    ActiveEnrollments = activeEnrollments,
                    CompletedEnrollments = completedEnrollments,
                    AverageCompletionPercentage = averageCompletionPercentage,
                    AverageGrade = averageGrade,
                    CertificatesEarned = certificatesEarned,
                    CourseStatistics = courseStatistics
                };
            }
            catch (Exception ex)
            {
                _logService.Error(ex, "Ошибка при получении статистики по студенту {StudentId}", studentId);
                throw;
            }
        }

        /// <summary>
        /// Получить статистику по автору
        /// </summary>
        /// <param name="authorId">Идентификатор автора</param>
        /// <returns>Статистика по автору</returns>
        public async Task<AuthorStatisticsModel> GetAuthorStatisticsAsync(Guid authorId)
        {
            try
            {
                _logService.Information("Получение статистики по автору {AuthorId}", authorId);

                var courses = await _courseRepository.GetCoursesByAuthorIdAsync(authorId);
                if (!courses.Any())
                {
                    _logService.Warning("Курсы для автора {AuthorId} не найдены", authorId);
                    return new AuthorStatisticsModel
                    {
                        AuthorId = authorId,
                        AuthorName = "Неизвестно", // Здесь должно быть получение имени автора
                        TotalCourses = 0,
                        PublishedCourses = 0,
                        DraftCourses = 0,
                        TotalStudents = 0,
                        TotalCompletions = 0,
                        AverageRating = 0,
                        TotalReviews = 0,
                        CourseStatistics = new List<AuthorCourseStatisticsModel>()
                    };
                }

                int totalStudents = 0;
                int totalCompletions = 0;
                int totalReviews = 0;
                decimal totalRating = 0;

                var courseStatistics = new List<AuthorCourseStatisticsModel>();
                foreach (var course in courses)
                {
                    var enrollments = await _enrollmentRepository.GetEnrollmentsByCourseIdAsync(course.Id);
                    var reviews = await _reviewRepository.GetReviewsByCourseIdAsync(course.Id);

                    var students = enrollments.Count();
                    var completions = enrollments.Count(e => e.Status == "Completed");
                    var reviewCount = reviews.Count();
                    var averageRating = reviews.Any() ? (decimal)reviews.Average(r => r.Rating) : 0;

                    totalStudents += students;
                    totalCompletions += completions;
                    totalReviews += reviewCount;
                    totalRating += reviewCount > 0 ? averageRating * reviewCount : 0;

                    courseStatistics.Add(new AuthorCourseStatisticsModel
                    {
                        CourseId = course.Id,
                        CourseTitle = course.Title,
                        IsPublished = course.IsPublished,
                        EnrollmentCount = students,
                        CompletionCount = completions,
                        ReviewCount = reviewCount,
                        AverageRating = averageRating
                    });
                }

                var publishedCourses = courses.Count(c => c.IsPublished);
                var draftCourses = courses.Count(c => !c.IsPublished);
                var averageOverallRating = totalReviews > 0 ? totalRating / totalReviews : 0;

                return new AuthorStatisticsModel
                {
                    AuthorId = authorId,
                    AuthorName = "Неизвестно", // Здесь должно быть получение имени автора
                    TotalCourses = courses.Count(),
                    PublishedCourses = publishedCourses,
                    DraftCourses = draftCourses,
                    TotalStudents = totalStudents,
                    TotalCompletions = totalCompletions,
                    AverageRating = averageOverallRating,
                    TotalReviews = totalReviews,
                    CourseStatistics = courseStatistics
                };
            }
            catch (Exception ex)
            {
                _logService.Error(ex, "Ошибка при получении статистики по автору {AuthorId}", authorId);
                throw;
            }
        }

        /// <summary>
        /// Получить популярные курсы
        /// </summary>
        /// <param name="count">Количество</param>
        /// <returns>Список популярных курсов</returns>
        public async Task<List<PopularCourseModel>> GetPopularCoursesAsync(int count = 10)
        {
            try
            {
                _logService.Information("Получение списка популярных курсов (количество: {Count})", count);

                var courses = await _courseRepository.GetAllAsync();
                var popularCourses = new List<PopularCourseModel>();

                foreach (var course in courses.Where(c => c.IsPublished && c.IsActive))
                {
                    var enrollments = await _enrollmentRepository.GetEnrollmentsByCourseIdAsync(course.Id);
                    var reviews = await _reviewRepository.GetReviewsByCourseIdAsync(course.Id);

                    var enrollmentCount = enrollments.Count();
                    var averageRating = reviews.Any() ? (decimal)reviews.Average(r => r.Rating) : 0;
                    var reviewCount = reviews.Count();

                    // Простая формула популярности: количество зачислений * 0.6 + средний рейтинг * 10 * 0.4
                    var popularityScore = (enrollmentCount * 0.6m) + (averageRating * 10 * 0.4m);

                    popularCourses.Add(new PopularCourseModel
                    {
                        CourseId = course.Id,
                        CourseTitle = course.Title,
                        EnrollmentCount = enrollmentCount,
                        AverageRating = averageRating,
                        ReviewCount = reviewCount,
                        PopularityScore = popularityScore
                    });
                }

                return popularCourses.OrderByDescending(c => c.PopularityScore).Take(count).ToList();
            }
            catch (Exception ex)
            {
                _logService.Error(ex, "Ошибка при получении списка популярных курсов");
                throw;
            }
        }

        /// <summary>
        /// Получить статистику завершения курсов
        /// </summary>
        /// <param name="startDate">Дата начала</param>
        /// <param name="endDate">Дата окончания</param>
        /// <returns>Статистика завершения курсов</returns>
        public async Task<CourseCompletionStatisticsModel> GetCourseCompletionStatisticsAsync(DateTime startDate, DateTime endDate)
        {
            try
            {
                _logService.Information("Получение статистики завершения курсов за период с {StartDate} по {EndDate}", startDate, endDate);

                var enrollments = await _enrollmentRepository.GetEnrollmentsByDateRangeAsync(startDate, endDate);
                var completedEnrollments = enrollments.Where(e => e.Status == "Completed" && e.CompletionDate.HasValue);

                var courseCompletions = new Dictionary<Guid, int>();
                var monthlyCompletions = new Dictionary<string, int>();

                foreach (var enrollment in completedEnrollments)
                {
                    // Подсчет завершений по курсам
                    if (courseCompletions.ContainsKey(enrollment.CourseId))
                    {
                        courseCompletions[enrollment.CourseId]++;
                    }
                    else
                    {
                        courseCompletions[enrollment.CourseId] = 1;
                    }

                    // Подсчет завершений по месяцам
                    if (enrollment.CompletionDate.HasValue)
                    {
                        var monthKey = enrollment.CompletionDate.Value.ToString("yyyy-MM");
                        if (monthlyCompletions.ContainsKey(monthKey))
                        {
                            monthlyCompletions[monthKey]++;
                        }
                        else
                        {
                            monthlyCompletions[monthKey] = 1;
                        }
                    }
                }

                // Получение информации о курсах
                var courseCompletionDetails = new List<CourseCompletionDetailModel>();
                foreach (var courseCompletion in courseCompletions)
                {
                    var course = await _courseRepository.GetByIdAsync(courseCompletion.Key);
                    if (course != null)
                    {
                        courseCompletionDetails.Add(new CourseCompletionDetailModel
                        {
                            CourseId = course.Id,
                            CourseTitle = course.Title,
                            CompletionCount = courseCompletion.Value
                        });
                    }
                }

                // Формирование статистики по месяцам
                var monthlyStatistics = monthlyCompletions
                    .OrderBy(m => m.Key)
                    .Select(m => new MonthlyCompletionModel
                    {
                        Month = m.Key,
                        CompletionCount = m.Value
                    })
                    .ToList();

                return new CourseCompletionStatisticsModel
                {
                    TotalCompletions = completedEnrollments.Count(),
                    StartDate = startDate,
                    EndDate = endDate,
                    CourseCompletions = courseCompletionDetails.OrderByDescending(c => c.CompletionCount).ToList(),
                    MonthlyCompletions = monthlyStatistics
                };
            }
            catch (Exception ex)
            {
                _logService.Error(ex, "Ошибка при получении статистики завершения курсов");
                throw;
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Courses.Domain.Interfaces;
using Courses.Domain.Models;
using Microsoft.Extensions.Logging;

namespace Courses.Infrastructure.Services
{
    /// <summary>
    /// Сервис для генерации отчетов
    /// </summary>
    public class ReportService : IReportService
    {
        private readonly ICourseRepository _courseRepository;
        private readonly IEnrollmentRepository _enrollmentRepository;
        private readonly IModuleRepository _moduleRepository;
        private readonly ILessonRepository _lessonRepository;
        private readonly IReviewRepository _reviewRepository;
        private readonly IAnalyticsService _analyticsService;
        private readonly ILogService _logService;
        private readonly ILogger<ReportService> _logger;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="courseRepository">Репозиторий курсов</param>
        /// <param name="enrollmentRepository">Репозиторий зачислений</param>
        /// <param name="moduleRepository">Репозиторий модулей</param>
        /// <param name="lessonRepository">Репозиторий уроков</param>
        /// <param name="reviewRepository">Репозиторий отзывов</param>
        /// <param name="analyticsService">Сервис аналитики</param>
        /// <param name="logService">Сервис логирования</param>
        /// <param name="logger">Логгер</param>
        public ReportService(
            ICourseRepository courseRepository,
            IEnrollmentRepository enrollmentRepository,
            IModuleRepository moduleRepository,
            ILessonRepository lessonRepository,
            IReviewRepository reviewRepository,
            IAnalyticsService analyticsService,
            ILogService logService,
            ILogger<ReportService> logger)
        {
            _courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
            _enrollmentRepository = enrollmentRepository ?? throw new ArgumentNullException(nameof(enrollmentRepository));
            _moduleRepository = moduleRepository ?? throw new ArgumentNullException(nameof(moduleRepository));
            _lessonRepository = lessonRepository ?? throw new ArgumentNullException(nameof(lessonRepository));
            _reviewRepository = reviewRepository ?? throw new ArgumentNullException(nameof(reviewRepository));
            _analyticsService = analyticsService ?? throw new ArgumentNullException(nameof(analyticsService));
            _logService = logService ?? throw new ArgumentNullException(nameof(logService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Генерация отчета по курсу
        /// </summary>
        /// <param name="courseId">Идентификатор курса</param>
        /// <param name="format">Формат отчета</param>
        /// <returns>Отчет в виде массива байтов</returns>
        public async Task<byte[]> GenerateCourseReportAsync(Guid courseId, string format = "csv")
        {
            try
            {
                _logService.Information("Генерация отчета по курсу {CourseId} в формате {Format}", courseId, format);

                var courseStatistics = await _analyticsService.GetCourseStatisticsAsync(courseId);
                if (courseStatistics == null)
                {
                    _logService.Warning("Не удалось получить статистику для курса {CourseId}", courseId);
                    return null;
                }

                return format.ToLower() switch
                {
                    "csv" => GenerateCourseReportCsv(courseStatistics),
                    "json" => GenerateCourseReportJson(courseStatistics),
                    "pdf" => await GenerateCourseReportPdfAsync(courseStatistics),
                    _ => throw new ArgumentException($"Неподдерживаемый формат отчета: {format}")
                };
            }
            catch (Exception ex)
            {
                _logService.Error(ex, "Ошибка при генерации отчета по курсу {CourseId}", courseId);
                throw;
            }
        }

        /// <summary>
        /// Генерация отчета по студенту
        /// </summary>
        /// <param name="studentId">Идентификатор студента</param>
        /// <param name="format">Формат отчета</param>
        /// <returns>Отчет в виде массива байтов</returns>
        public async Task<byte[]> GenerateStudentReportAsync(Guid studentId, string format = "csv")
        {
            try
            {
                _logService.Information("Генерация отчета по студенту {StudentId} в формате {Format}", studentId, format);

                var studentStatistics = await _analyticsService.GetStudentStatisticsAsync(studentId);
                if (studentStatistics == null)
                {
                    _logService.Warning("Не удалось получить статистику для студента {StudentId}", studentId);
                    return null;
                }

                return format.ToLower() switch
                {
                    "csv" => GenerateStudentReportCsv(studentStatistics),
                    "json" => GenerateStudentReportJson(studentStatistics),
                    "pdf" => await GenerateStudentReportPdfAsync(studentStatistics),
                    _ => throw new ArgumentException($"Неподдерживаемый формат отчета: {format}")
                };
            }
            catch (Exception ex)
            {
                _logService.Error(ex, "Ошибка при генерации отчета по студенту {StudentId}", studentId);
                throw;
            }
        }

        /// <summary>
        /// Генерация отчета по автору
        /// </summary>
        /// <param name="authorId">Идентификатор автора</param>
        /// <param name="format">Формат отчета</param>
        /// <returns>Отчет в виде массива байтов</returns>
        public async Task<byte[]> GenerateAuthorReportAsync(Guid authorId, string format = "csv")
        {
            try
            {
                _logService.Information("Генерация отчета по автору {AuthorId} в формате {Format}", authorId, format);

                var authorStatistics = await _analyticsService.GetAuthorStatisticsAsync(authorId);
                if (authorStatistics == null)
                {
                    _logService.Warning("Не удалось получить статистику для автора {AuthorId}", authorId);
                    return null;
                }

                return format.ToLower() switch
                {
                    "csv" => GenerateAuthorReportCsv(authorStatistics),
                    "json" => GenerateAuthorReportJson(authorStatistics),
                    "pdf" => await GenerateAuthorReportPdfAsync(authorStatistics),
                    _ => throw new ArgumentException($"Неподдерживаемый формат отчета: {format}")
                };
            }
            catch (Exception ex)
            {
                _logService.Error(ex, "Ошибка при генерации отчета по автору {AuthorId}", authorId);
                throw;
            }
        }

        /// <summary>
        /// Генерация отчета по зачислениям
        /// </summary>
        /// <param name="startDate">Дата начала</param>
        /// <param name="endDate">Дата окончания</param>
        /// <param name="format">Формат отчета</param>
        /// <returns>Отчет в виде массива байтов</returns>
        public async Task<byte[]> GenerateEnrollmentReportAsync(DateTime startDate, DateTime endDate, string format = "csv")
        {
            try
            {
                _logService.Information("Генерация отчета по зачислениям за период с {StartDate} по {EndDate} в формате {Format}", startDate, endDate, format);

                var enrollments = await _enrollmentRepository.GetEnrollmentsByDateRangeAsync(startDate, endDate);
                if (enrollments == null || !enrollments.Any())
                {
                    _logService.Warning("Не найдено зачислений за период с {StartDate} по {EndDate}", startDate, endDate);
                    return null;
                }

                // Группировка зачислений по курсам
                var enrollmentsByDate = enrollments
                    .GroupBy(e => e.EnrollmentDate.Date)
                    .Select(g => new
                    {
                        Date = g.Key,
                        Count = g.Count()
                    })
                    .OrderBy(g => g.Date)
                    .ToList();

                // Группировка зачислений по курсам
                var enrollmentsByCourse = new Dictionary<Guid, int>();
                foreach (var enrollment in enrollments)
                {
                    if (enrollmentsByCourse.ContainsKey(enrollment.CourseId))
                    {
                        enrollmentsByCourse[enrollment.CourseId]++;
                    }
                    else
                    {
                        enrollmentsByCourse[enrollment.CourseId] = 1;
                    }
                }

                // Получение информации о курсах
                var courseEnrollments = new List<dynamic>();
                foreach (var courseEnrollment in enrollmentsByCourse)
                {
                    var course = await _courseRepository.GetByIdAsync(courseEnrollment.Key);
                    if (course != null)
                    {
                        courseEnrollments.Add(new
                        {
                            CourseId = course.Id,
                            CourseTitle = course.Title,
                            EnrollmentCount = courseEnrollment.Value
                        });
                    }
                }

                // Формирование отчета
                var reportData = new
                {
                    StartDate = startDate,
                    EndDate = endDate,
                    TotalEnrollments = enrollments.Count(),
                    EnrollmentsByDate = enrollmentsByDate,
                    EnrollmentsByCourse = courseEnrollments.OrderByDescending(c => c.EnrollmentCount).ToList()
                };

                return format.ToLower() switch
                {
                    "csv" => GenerateEnrollmentReportCsv(reportData),
                    "json" => GenerateEnrollmentReportJson(reportData),
                    "pdf" => await GenerateEnrollmentReportPdfAsync(reportData),
                    _ => throw new ArgumentException($"Неподдерживаемый формат отчета: {format}")
                };
            }
            catch (Exception ex)
            {
                _logService.Error(ex, "Ошибка при генерации отчета по зачислениям");
                throw;
            }
        }

        /// <summary>
        /// Генерация отчета по прогрессу студента в курсе
        /// </summary>
        /// <param name="studentId">Идентификатор студента</param>
        /// <param name="courseId">Идентификатор курса</param>
        /// <param name="format">Формат отчета</param>
        /// <returns>Отчет в виде массива байтов</returns>
        public async Task<byte[]> GenerateStudentCourseProgressReportAsync(Guid studentId, Guid courseId, string format = "csv")
        {
            try
            {
                _logService.Information("Генерация отчета по прогрессу студента {StudentId} в курсе {CourseId} в формате {Format}", studentId, courseId, format);

                var enrollment = await _enrollmentRepository.GetEnrollmentByStudentAndCourseIdAsync(studentId, courseId);
                if (enrollment == null)
                {
                    _logService.Warning("Не найдено зачисление студента {StudentId} на курс {CourseId}", studentId, courseId);
                    return null;
                }

                var course = await _courseRepository.GetByIdAsync(courseId);
                if (course == null)
                {
                    _logService.Warning("Курс с идентификатором {CourseId} не найден", courseId);
                    return null;
                }

                var modules = await _moduleRepository.GetModulesByCourseIdAsync(courseId);
                var moduleProgress = new List<dynamic>();

                foreach (var module in modules)
                {
                    var lessons = await _lessonRepository.GetLessonsByModuleIdAsync(module.Id);
                    var lessonProgress = new List<dynamic>();

                    foreach (var lesson in lessons)
                    {
                        var progress = await _lessonRepository.GetLessonProgressAsync(studentId, lesson.Id);
                        lessonProgress.Add(new
                        {
                            LessonId = lesson.Id,
                            LessonTitle = lesson.Title,
                            IsCompleted = progress?.IsCompleted ?? false,
                            CompletionDate = progress?.CompletionDate,
                            Score = progress?.Score ?? 0,
                            TimeSpent = progress?.TimeSpent ?? TimeSpan.Zero
                        });
                    }

                    moduleProgress.Add(new
                    {
                        ModuleId = module.Id,
                        ModuleTitle = module.Title,
                        CompletedLessons = lessonProgress.Count(lp => (bool)lp.IsCompleted),
                        TotalLessons = lessonProgress.Count,
                        CompletionPercentage = lessonProgress.Any() ? lessonProgress.Count(lp => (bool)lp.IsCompleted) * 100.0 / lessonProgress.Count : 0,
                        LessonProgress = lessonProgress
                    });
                }

                var reportData = new
                {
                    StudentId = studentId,
                    StudentName = "Неизвестно", // Здесь должно быть получение имени студента
                    CourseId = course.Id,
                    CourseTitle = course.Title,
                    EnrollmentDate = enrollment.EnrollmentDate,
                    Status = enrollment.Status,
                    CompletionPercentage = enrollment.CompletionPercentage,
                    Grade = enrollment.Grade,
                    CompletionDate = enrollment.CompletionDate,
                    CertificateIssued = enrollment.CertificateIssued,
                    ModuleProgress = moduleProgress
                };

                return format.ToLower() switch
                {
                    "csv" => GenerateStudentCourseProgressReportCsv(reportData),
                    "json" => GenerateStudentCourseProgressReportJson(reportData),
                    "pdf" => await GenerateStudentCourseProgressReportPdfAsync(reportData),
                    _ => throw new ArgumentException($"Неподдерживаемый формат отчета: {format}")
                };
            }
            catch (Exception ex)
            {
                _logService.Error(ex, "Ошибка при генерации отчета по прогрессу студента {StudentId} в курсе {CourseId}", studentId, courseId);
                throw;
            }
        }

        /// <summary>
        /// Генерация отчета по активности студента
        /// </summary>
        /// <param name="studentId">Идентификатор студента</param>
        /// <param name="startDate">Дата начала</param>
        /// <param name="endDate">Дата окончания</param>
        /// <param name="format">Формат отчета</param>
        /// <returns>Отчет в виде массива байтов</returns>
        public async Task<byte[]> GenerateStudentActivityReportAsync(Guid studentId, DateTime startDate, DateTime endDate, string format = "csv")
        {
            try
            {
                _logService.Information("Генерация отчета по активности студента {StudentId} за период с {StartDate} по {EndDate} в формате {Format}", studentId, startDate, endDate, format);

                // В реальном приложении здесь была бы логика получения активности студента
                // Например, из таблицы логов активности или из другого источника данных
                // Для демонстрации создаем фиктивные данные

                var activities = new List<dynamic>
                {
                    new { Date = startDate.AddDays(1), ActivityType = "Просмотр урока", CourseId = Guid.NewGuid(), CourseTitle = "Курс 1", ModuleTitle = "Модуль 1", LessonTitle = "Урок 1", Duration = TimeSpan.FromMinutes(15) },
                    new { Date = startDate.AddDays(2), ActivityType = "Выполнение теста", CourseId = Guid.NewGuid(), CourseTitle = "Курс 1", ModuleTitle = "Модуль 1", LessonTitle = "Урок 2", Duration = TimeSpan.FromMinutes(10) },
                    new { Date = startDate.AddDays(3), ActivityType = "Просмотр урока", CourseId = Guid.NewGuid(), CourseTitle = "Курс 2", ModuleTitle = "Модуль 1", LessonTitle = "Урок 1", Duration = TimeSpan.FromMinutes(20) },
                    new { Date = startDate.AddDays(4), ActivityType = "Выполнение задания", CourseId = Guid.NewGuid(), CourseTitle = "Курс 2", ModuleTitle = "Модуль 1", LessonTitle = "Урок 2", Duration = TimeSpan.FromMinutes(30) },
                    new { Date = startDate.AddDays(5), ActivityType = "Просмотр урока", CourseId = Guid.NewGuid(), CourseTitle = "Курс 1", ModuleTitle = "Модуль 2", LessonTitle = "Урок 1", Duration = TimeSpan.FromMinutes(25) }
                };

                // Группировка активности по дням
                var activityByDate = activities
                    .GroupBy(a => ((DateTime)a.Date).Date)
                    .Select(g => new
                    {
                        Date = g.Key,
                        Count = g.Count(),
                        TotalDuration = TimeSpan.FromMinutes(g.Sum(a => ((TimeSpan)a.Duration).TotalMinutes))
                    })
                    .OrderBy(g => g.Date)
                    .ToList();

                // Группировка активности по типам
                var activityByType = activities
                    .GroupBy(a => a.ActivityType)
                    .Select(g => new
                    {
                        ActivityType = g.Key,
                        Count = g.Count(),
                        TotalDuration = TimeSpan.FromMinutes(g.Sum(a => ((TimeSpan)a.Duration).TotalMinutes))
                    })
                    .OrderByDescending(g => g.Count)
                    .ToList();

                // Группировка активности по курсам
                var activityByCourse = activities
                    .GroupBy(a => new { CourseId = a.CourseId, CourseTitle = a.CourseTitle })
                    .Select(g => new
                    {
                        CourseId = g.Key.CourseId,
                        CourseTitle = g.Key.CourseTitle,
                        Count = g.Count(),
                        TotalDuration = TimeSpan.FromMinutes(g.Sum(a => ((TimeSpan)a.Duration).TotalMinutes))
                    })
                    .OrderByDescending(g => g.Count)
                    .ToList();

                var reportData = new
                {
                    StudentId = studentId,
                    StudentName = "Неизвестно", // Здесь должно быть получение имени студента
                    StartDate = startDate,
                    EndDate = endDate,
                    TotalActivities = activities.Count,
                    TotalDuration = TimeSpan.FromMinutes(activities.Sum(a => ((TimeSpan)a.Duration).TotalMinutes)),
                    ActivityByDate = activityByDate,
                    ActivityByType = activityByType,
                    ActivityByCourse = activityByCourse,
                    Activities = activities.OrderByDescending(a => a.Date).ToList()
                };

                return format.ToLower() switch
                {
                    "csv" => GenerateStudentActivityReportCsv(reportData),
                    "json" => GenerateStudentActivityReportJson(reportData),
                    "pdf" => await GenerateStudentActivityReportPdfAsync(reportData),
                    _ => throw new ArgumentException($"Неподдерживаемый формат отчета: {format}")
                };
            }
            catch (Exception ex)
            {
                _logService.Error(ex, "Ошибка при генерации отчета по активности студента {StudentId}", studentId);
                throw;
            }
        }

        #region CSV Report Generators

        private byte[] GenerateCourseReportCsv(CourseStatisticsModel courseStatistics)
        {
            var csv = new StringBuilder();

            // Заголовок отчета
            csv.AppendLine("Отчет по курсу");
            csv.AppendLine($"Идентификатор курса: {courseStatistics.CourseId}");
            csv.AppendLine($"Название курса: {courseStatistics.CourseTitle}");
            csv.AppendLine($"Дата формирования отчета: {DateTime.Now}");
            csv.AppendLine();

            // Основная информация
            csv.AppendLine("Основная информация");
            csv.AppendLine($"Всего зачислений: {courseStatistics.TotalEnrollments}");
            csv.AppendLine($"Активных зачислений: {courseStatistics.ActiveEnrollments}");
            csv.AppendLine($"Завершенных зачислений: {courseStatistics.CompletedEnrollments}");
            csv.AppendLine($"Средний рейтинг: {courseStatistics.AverageRating}");
            csv.AppendLine($"Количество отзывов: {courseStatistics.ReviewCount}");
            csv.AppendLine($"Средний процент завершения: {courseStatistics.AverageCompletionPercentage}%");
            csv.AppendLine($"Среднее время завершения: {courseStatistics.AverageCompletionTime} дней");
            csv.AppendLine();

            // Статистика по модулям
            csv.AppendLine("Статистика по модулям");
            csv.AppendLine("Идентификатор модуля,Название модуля,Средний процент завершения,Среднее время завершения (дней)");
            foreach (var module in courseStatistics.ModuleStatistics)
            {
                csv.AppendLine($"{module.ModuleId},{module.ModuleTitle},{module.AverageCompletionPercentage},{module.AverageCompletionTime}");
            }
            csv.AppendLine();

            // Статистика по урокам
            csv.AppendLine("Статистика по урокам");
            csv.AppendLine("Идентификатор модуля,Название модуля,Идентификатор урока,Название урока,Процент завершения,Среднее время завершения (минут),Средний балл");
            foreach (var module in courseStatistics.ModuleStatistics)
            {
                foreach (var lesson in module.LessonStatistics)
                {
                    csv.AppendLine($"{module.ModuleId},{module.ModuleTitle},{lesson.LessonId},{lesson.LessonTitle},{lesson.CompletionPercentage},{lesson.AverageCompletionTime},{lesson.AverageScore}");
                }
            }

            return Encoding.UTF8.GetBytes(csv.ToString());
        }

        private byte[] GenerateStudentReportCsv(StudentStatisticsModel studentStatistics)
        {
            var csv = new StringBuilder();

            // Заголовок отчета
            csv.AppendLine("Отчет по студенту");
            csv.AppendLine($"Идентификатор студента: {studentStatistics.StudentId}");
            csv.AppendLine($"Имя студента: {studentStatistics.StudentName}");
            csv.AppendLine($"Дата формирования отчета: {DateTime.Now}");
            csv.AppendLine();

            // Основная информация
            csv.AppendLine("Основная информация");
            csv.AppendLine($"Всего зачислений: {studentStatistics.TotalEnrollments}");
            csv.AppendLine($"Активных зачислений: {studentStatistics.ActiveEnrollments}");
            csv.AppendLine($"Завершенных зачислений: {studentStatistics.CompletedEnrollments}");
            csv.AppendLine($"Средний процент завершения: {studentStatistics.AverageCompletionPercentage}%");
            csv.AppendLine($"Средний балл: {studentStatistics.AverageGrade}");
            csv.AppendLine($"Полученных сертификатов: {studentStatistics.CertificatesEarned}");
            csv.AppendLine();

            // Статистика по курсам
            csv.AppendLine("Статистика по курсам");
            csv.AppendLine("Идентификатор курса,Название курса,Статус зачисления,Процент завершения,Оценка,Дата начала,Дата завершения,Сертификат выдан");
            foreach (var course in studentStatistics.CourseStatistics)
            {
                csv.AppendLine($"{course.CourseId},{course.CourseTitle},{course.EnrollmentStatus},{course.CompletionPercentage},{course.Grade},{course.StartDate},{course.CompletionDate},{course.CertificateIssued}");
            }

            return Encoding.UTF8.GetBytes(csv.ToString());
        }

        private byte[] GenerateAuthorReportCsv(AuthorStatisticsModel authorStatistics)
        {
            var csv = new StringBuilder();

            // Заголовок отчета
            csv.AppendLine("Отчет по автору");
            csv.AppendLine($"Идентификатор автора: {authorStatistics.AuthorId}");
            csv.AppendLine($"Имя автора: {authorStatistics.AuthorName}");
            csv.AppendLine($"Дата формирования отчета: {DateTime.Now}");
            csv.AppendLine();

            // Основная информация
            csv.AppendLine("Основная информация");
            csv.AppendLine($"Всего курсов: {authorStatistics.TotalCourses}");
            csv.AppendLine($"Опубликованных курсов: {authorStatistics.PublishedCourses}");
            csv.AppendLine($"Черновиков курсов: {authorStatistics.DraftCourses}");
            csv.AppendLine($"Всего студентов: {authorStatistics.TotalStudents}");
            csv.AppendLine($"Всего завершений: {authorStatistics.TotalCompletions}");
            csv.AppendLine($"Средний рейтинг: {authorStatistics.AverageRating}");
            csv.AppendLine($"Всего отзывов: {authorStatistics.TotalReviews}");
            csv.AppendLine();

            // Статистика по курсам
            csv.AppendLine("Статистика по курсам");
            csv.AppendLine("Идентификатор курса,Название курса,Опубликован,Количество зачислений,Количество завершений,Количество отзывов,Средний рейтинг");
            foreach (var course in authorStatistics.CourseStatistics)
            {
                csv.AppendLine($"{course.CourseId},{course.CourseTitle},{course.IsPublished},{course.EnrollmentCount},{course.CompletionCount},{course.ReviewCount},{course.AverageRating}");
            }

            return Encoding.UTF8.GetBytes(csv.ToString());
        }

        private byte[] GenerateEnrollmentReportCsv(dynamic reportData)
        {
            var csv = new StringBuilder();

            // Заголовок отчета
            csv.AppendLine("Отчет по зачислениям");
            csv.AppendLine($"Период: с {reportData.StartDate} по {reportData.EndDate}");
            csv.AppendLine($"Дата формирования отчета: {DateTime.Now}");
            csv.AppendLine();

            // Основная информация
            csv.AppendLine("Основная информация");
            csv.AppendLine($"Всего зачислений: {reportData.TotalEnrollments}");
            csv.AppendLine();

            // Зачисления по дням
            csv.AppendLine("Зачисления по дням");
            csv.AppendLine("Дата,Количество зачислений");
            foreach (var item in reportData.EnrollmentsByDate)
            {
                csv.AppendLine($"{item.Date:yyyy-MM-dd},{item.Count}");
            }
            csv.AppendLine();

            // Зачисления по курсам
            csv.AppendLine("Зачисления по курсам");
            csv.AppendLine("Идентификатор курса,Название курса,Количество зачислений");
            foreach (var item in reportData.EnrollmentsByCourse)
            {
                csv.AppendLine($"{item.CourseId},{item.CourseTitle},{item.EnrollmentCount}");
            }

            return Encoding.UTF8.GetBytes(csv.ToString());
        }

        private byte[] GenerateStudentCourseProgressReportCsv(dynamic reportData)
        {
            var csv = new StringBuilder();

            // Заголовок отчета
            csv.AppendLine("Отчет по прогрессу студента в курсе");
            csv.AppendLine($"Идентификатор студента: {reportData.StudentId}");
            csv.AppendLine($"Имя студента: {reportData.StudentName}");
            csv.AppendLine($"Идентификатор курса: {reportData.CourseId}");
            csv.AppendLine($"Название курса: {reportData.CourseTitle}");
            csv.AppendLine($"Дата формирования отчета: {DateTime.Now}");
            csv.AppendLine();

            // Основная информация
            csv.AppendLine("Основная информация");
            csv.AppendLine($"Дата зачисления: {reportData.EnrollmentDate}");
            csv.AppendLine($"Статус: {reportData.Status}");
            csv.AppendLine($"Процент завершения: {reportData.CompletionPercentage}%");
            csv.AppendLine($"Оценка: {reportData.Grade}");
            csv.AppendLine($"Дата завершения: {reportData.CompletionDate}");
            csv.AppendLine($"Сертификат выдан: {reportData.CertificateIssued}");
            csv.AppendLine();

            // Прогресс по модулям
            csv.AppendLine("Прогресс по модулям");
            csv.AppendLine("Идентификатор модуля,Название модуля,Завершенные уроки,Всего уроков,Процент завершения");
            foreach (var module in reportData.ModuleProgress)
            {
                csv.AppendLine($"{module.ModuleId},{module.ModuleTitle},{module.CompletedLessons},{module.TotalLessons},{module.CompletionPercentage}%");
            }
            csv.AppendLine();

            // Прогресс по урокам
            csv.AppendLine("Прогресс по урокам");
            csv.AppendLine("Идентификатор модуля,Название модуля,Идентификатор урока,Название урока,Завершен,Дата завершения,Оценка,Затраченное время (минуты)");
            foreach (var module in reportData.ModuleProgress)
            {
                foreach (var lesson in module.LessonProgress)
                {
                    csv.AppendLine($"{module.ModuleId},{module.ModuleTitle},{lesson.LessonId},{lesson.LessonTitle},{lesson.IsCompleted},{lesson.CompletionDate},{lesson.Score},{((TimeSpan)lesson.TimeSpent).TotalMinutes}");
                }
            }

            return Encoding.UTF8.GetBytes(csv.ToString());
        }

        private byte[] GenerateStudentActivityReportCsv(dynamic reportData)
        {
            var csv = new StringBuilder();

            // Заголовок отчета
            csv.AppendLine("Отчет по активности студента");
            csv.AppendLine($"Идентификатор студента: {reportData.StudentId}");
            csv.AppendLine($"Имя студента: {reportData.StudentName}");
            csv.AppendLine($"Период: с {reportData.StartDate} по {reportData.EndDate}");
            csv.AppendLine($"Дата формирования отчета: {DateTime.Now}");
            csv.AppendLine();

            // Основная информация
            csv.AppendLine("Основная информация");
            csv.AppendLine($"Всего активностей: {reportData.TotalActivities}");
            csv.AppendLine($"Общее затраченное время: {((TimeSpan)reportData.TotalDuration).TotalHours:F2} часов");
            csv.AppendLine();

            // Активность по дням
            csv.AppendLine("Активность по дням");
            csv.AppendLine("Дата,Количество активностей,Общее время (минуты)");
            foreach (var item in reportData.ActivityByDate)
            {
                csv.AppendLine($"{item.Date:yyyy-MM-dd},{item.Count},{((TimeSpan)item.TotalDuration).TotalMinutes}");
            }
            csv.AppendLine();

            // Активность по типам
            csv.AppendLine("Активность по типам");
            csv.AppendLine("Тип активности,Количество,Общее время (минуты)");
            foreach (var item in reportData.ActivityByType)
            {
                csv.AppendLine($"{item.ActivityType},{item.Count},{((TimeSpan)item.TotalDuration).TotalMinutes}");
            }
            csv.AppendLine();

            // Активность по курсам
            csv.AppendLine("Активность по курсам");
            csv.AppendLine("Идентификатор курса,Название курса,Количество активностей,Общее время (минуты)");
            foreach (var item in reportData.ActivityByCourse)
            {
                csv.AppendLine($"{item.CourseId},{item.CourseTitle},{item.Count},{((TimeSpan)item.TotalDuration).TotalMinutes}");
            }
            csv.AppendLine();

            // Детальная активность
            csv.AppendLine("Детальная активность");
            csv.AppendLine("Дата,Тип активности,Идентификатор курса,Название курса,Название модуля,Название урока,Время (минуты)");
            foreach (var activity in reportData.Activities)
            {
                csv.AppendLine($"{((DateTime)activity.Date):yyyy-MM-dd HH:mm:ss},{activity.ActivityType},{activity.CourseId},{activity.CourseTitle},{activity.ModuleTitle},{activity.LessonTitle},{((TimeSpan)activity.Duration).TotalMinutes}");
            }

            return Encoding.UTF8.GetBytes(csv.ToString());
        }

        #endregion

        #region JSON Report Generators

        private byte[] GenerateCourseReportJson(CourseStatisticsModel courseStatistics)
        {
            // В реальном приложении здесь была бы логика сериализации объекта в JSON
            // Для демонстрации просто возвращаем строку JSON
            var json = $"{{ \"courseId\": \"{courseStatistics.CourseId}\", \"courseTitle\": \"{courseStatistics.CourseTitle}\", \"totalEnrollments\": {courseStatistics.TotalEnrollments} }}";
            return Encoding.UTF8.GetBytes(json);
        }

        private byte[] GenerateStudentReportJson(StudentStatisticsModel studentStatistics)
        {
            // В реальном приложении здесь была бы логика сериализации объекта в JSON
            // Для демонстрации просто возвращаем строку JSON
            var json = $"{{ \"studentId\": \"{studentStatistics.StudentId}\", \"studentName\": \"{studentStatistics.StudentName}\", \"totalEnrollments\": {studentStatistics.TotalEnrollments} }}";
            return Encoding.UTF8.GetBytes(json);
        }

        private byte[] GenerateAuthorReportJson(AuthorStatisticsModel authorStatistics)
        {
            // В реальном приложении здесь была бы логика сериализации объекта в JSON
            // Для демонстрации просто возвращаем строку JSON
            var json = $"{{ \"authorId\": \"{authorStatistics.AuthorId}\", \"authorName\": \"{authorStatistics.AuthorName}\", \"totalCourses\": {authorStatistics.TotalCourses} }}";
            return Encoding.UTF8.GetBytes(json);
        }

        private byte[] GenerateEnrollmentReportJson(dynamic reportData)
        {
            // В реальном приложении здесь была бы логика сериализации объекта в JSON
            // Для демонстрации просто возвращаем строку JSON
            var json = $"{{ \"startDate\": \"{reportData.StartDate}\", \"endDate\": \"{reportData.EndDate}\", \"totalEnrollments\": {reportData.TotalEnrollments} }}";
            return Encoding.UTF8.GetBytes(json);
        }

        private byte[] GenerateStudentCourseProgressReportJson(dynamic reportData)
        {
            // В реальном приложении здесь была бы логика сериализации объекта в JSON
            // Для демонстрации просто возвращаем строку JSON
            var json = $"{{ \"studentId\": \"{reportData.StudentId}\", \"courseId\": \"{reportData.CourseId}\", \"completionPercentage\": {reportData.CompletionPercentage} }}";
            return Encoding.UTF8.GetBytes(json);
        }

        private byte[] GenerateStudentActivityReportJson(dynamic reportData)
        {
            // В реальном приложении здесь была бы логика сериализации объекта в JSON
            // Для демонстрации просто возвращаем строку JSON
            var json = $"{{ \"studentId\": \"{reportData.StudentId}\", \"startDate\": \"{reportData.StartDate}\", \"endDate\": \"{reportData.EndDate}\", \"totalActivities\": {reportData.TotalActivities} }}";
            return Encoding.UTF8.GetBytes(json);
        }

        #endregion

        #region PDF Report Generators

        private Task<byte[]> GenerateCourseReportPdfAsync(CourseStatisticsModel courseStatistics)
        {
            // В реальном приложении здесь была бы логика генерации PDF
            // Для демонстрации просто возвращаем текст в виде PDF
            var text = $"Отчет по курсу\nИдентификатор курса: {courseStatistics.CourseId}\nНазвание курса: {courseStatistics.CourseTitle}\nВсего зачислений: {courseStatistics.TotalEnrollments}";
            return Task.FromResult(Encoding.UTF8.GetBytes(text));
        }

        private Task<byte[]> GenerateStudentReportPdfAsync(StudentStatisticsModel studentStatistics)
        {
            // В реальном приложении здесь была бы логика генерации PDF
            // Для демонстрации просто возвращаем текст в виде PDF
            var text = $"Отчет по студенту\nИдентификатор студента: {studentStatistics.StudentId}\nИмя студента: {studentStatistics.StudentName}\nВсего зачислений: {studentStatistics.TotalEnrollments}";
            return Task.FromResult(Encoding.UTF8.GetBytes(text));
        }

        private Task<byte[]> GenerateAuthorReportPdfAsync(AuthorStatisticsModel authorStatistics)
        {
            // В реальном приложении здесь была бы логика генерации PDF
            // Для демонстрации просто возвращаем текст в виде PDF
            var text = $"Отчет по автору\nИдентификатор автора: {authorStatistics.AuthorId}\nИмя автора: {authorStatistics.AuthorName}\nВсего курсов: {authorStatistics.TotalCourses}";
            return Task.FromResult(Encoding.UTF8.GetBytes(text));
        }

        private Task<byte[]> GenerateEnrollmentReportPdfAsync(dynamic reportData)
        {
            // В реальном приложении здесь была бы логика генерации PDF
            // Для демонстрации просто возвращаем текст в виде PDF
            var text = $"Отчет по зачислениям\nПериод: с {reportData.StartDate} по {reportData.EndDate}\nВсего зачислений: {reportData.TotalEnrollments}";
            return Task.FromResult(Encoding.UTF8.GetBytes(text));
        }

        private Task<byte[]> GenerateStudentCourseProgressReportPdfAsync(dynamic reportData)
        {
            // В реальном приложении здесь была бы логика генерации PDF
            // Для демонстрации просто возвращаем текст в виде PDF
            var text = $"Отчет по прогрессу студента в курсе\nИдентификатор студента: {reportData.StudentId}\nИдентификатор курса: {reportData.CourseId}\nПроцент завершения: {reportData.CompletionPercentage}%";
            return Task.FromResult(Encoding.UTF8.GetBytes(text));
        }

        private Task<byte[]> GenerateStudentActivityReportPdfAsync(dynamic reportData)
        {
            // В реальном приложении здесь была бы логика генерации PDF
            // Для демонстрации просто возвращаем текст в виде PDF
            var text = $"Отчет по активности студента\nИдентификатор студента: {reportData.StudentId}\nПериод: с {reportData.StartDate} по {reportData.EndDate}\nВсего активностей: {reportData.TotalActivities}";
            return Task.FromResult(Encoding.UTF8.GetBytes(text));
        }

        #endregion
    }
}
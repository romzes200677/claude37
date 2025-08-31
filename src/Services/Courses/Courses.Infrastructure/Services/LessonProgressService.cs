using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Courses.Domain.Entities;
using Courses.Domain.Interfaces;
using Microsoft.Extensions.Logging;

namespace Courses.Infrastructure.Services
{
    /// <summary>
    /// Сервис для работы с прогрессом по урокам
    /// </summary>
    public class LessonProgressService : ILessonProgressService
    {
        private readonly ILessonProgressRepository _lessonProgressRepository;
        private readonly ILessonRepository _lessonRepository;
        private readonly ILogService _logService;
        private readonly ILogger<LessonProgressService> _logger;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="lessonProgressRepository">Репозиторий прогресса по урокам</param>
        /// <param name="lessonRepository">Репозиторий уроков</param>
        /// <param name="logService">Сервис логирования</param>
        /// <param name="logger">Логгер</param>
        public LessonProgressService(
            ILessonProgressRepository lessonProgressRepository,
            ILessonRepository lessonRepository,
            ILogService logService,
            ILogger<LessonProgressService> logger)
        {
            _lessonProgressRepository = lessonProgressRepository;
            _lessonRepository = lessonRepository;
            _logService = logService;
            _logger = logger;
        }

        /// <summary>
        /// Получить прогресс по идентификатору
        /// </summary>
        public async Task<LessonProgress> GetProgressByIdAsync(Guid progressId)
        {
            try
            {
                var progress = await _lessonProgressRepository.GetByIdAsync(progressId);
                if (progress == null)
                {
                    _logger.LogWarning($"Прогресс с ID {progressId} не найден");
                    return null;
                }

                return progress;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при получении прогресса по ID {progressId}");
                await _logService.LogErrorAsync("LessonProgressService", "GetProgressByIdAsync", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Получить прогресс по идентификатору студента и урока
        /// </summary>
        public async Task<LessonProgress> GetProgressByStudentAndLessonIdAsync(Guid studentId, Guid lessonId)
        {
            try
            {
                var progress = await _lessonProgressRepository.GetProgressByStudentAndLessonIdAsync(studentId, lessonId);
                return progress;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при получении прогресса для студента {studentId} и урока {lessonId}");
                await _logService.LogErrorAsync("LessonProgressService", "GetProgressByStudentAndLessonIdAsync", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Получить прогресс студента по всем урокам модуля
        /// </summary>
        public async Task<IEnumerable<LessonProgress>> GetProgressByStudentAndModuleIdAsync(Guid studentId, Guid moduleId)
        {
            try
            {
                var progress = await _lessonProgressRepository.GetProgressByStudentAndModuleIdAsync(studentId, moduleId);
                return progress;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при получении прогресса для студента {studentId} по модулю {moduleId}");
                await _logService.LogErrorAsync("LessonProgressService", "GetProgressByStudentAndModuleIdAsync", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Получить прогресс студента по всем урокам курса
        /// </summary>
        public async Task<IEnumerable<LessonProgress>> GetProgressByStudentAndCourseIdAsync(Guid studentId, Guid courseId)
        {
            try
            {
                var progress = await _lessonProgressRepository.GetProgressByStudentAndCourseIdAsync(studentId, courseId);
                return progress;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при получении прогресса для студента {studentId} по курсу {courseId}");
                await _logService.LogErrorAsync("LessonProgressService", "GetProgressByStudentAndCourseIdAsync", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Начать урок
        /// </summary>
        public async Task<LessonProgress> StartLessonAsync(Guid studentId, Guid lessonId)
        {
            try
            {
                // Проверяем, существует ли уже прогресс для этого студента и урока
                var existingProgress = await _lessonProgressRepository.GetProgressByStudentAndLessonIdAsync(studentId, lessonId);
                if (existingProgress != null)
                {
                    // Если урок уже начат, возвращаем существующий прогресс
                    _logger.LogInformation($"Урок {lessonId} уже начат студентом {studentId}");
                    return existingProgress;
                }

                // Проверяем существование урока
                var lesson = await _lessonRepository.GetByIdAsync(lessonId);
                if (lesson == null)
                {
                    _logger.LogWarning($"Урок с ID {lessonId} не найден");
                    return null;
                }

                // Создаем новый прогресс
                var newProgress = new LessonProgress
                {
                    StudentId = studentId,
                    LessonId = lessonId,
                    Status = "В процессе",
                    CompletionPercentage = 0,
                    StartedAt = DateTime.UtcNow,
                    TimeSpentMinutes = 0
                };

                await _lessonProgressRepository.AddAsync(newProgress);
                await _logService.LogInfoAsync("LessonProgressService", "StartLessonAsync", 
                    $"Студент {studentId} начал урок {lessonId}");

                return newProgress;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при начале урока {lessonId} студентом {studentId}");
                await _logService.LogErrorAsync("LessonProgressService", "StartLessonAsync", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Обновить прогресс по уроку
        /// </summary>
        public async Task<LessonProgress> UpdateProgressAsync(Guid progressId, int completionPercentage)
        {
            try
            {
                var progress = await _lessonProgressRepository.GetByIdAsync(progressId);
                if (progress == null)
                {
                    _logger.LogWarning($"Прогресс с ID {progressId} не найден");
                    return null;
                }

                // Валидация процента завершения
                if (completionPercentage < 0 || completionPercentage > 100)
                {
                    _logger.LogWarning($"Некорректный процент завершения: {completionPercentage}");
                    throw new ArgumentOutOfRangeException(nameof(completionPercentage), "Процент завершения должен быть от 0 до 100");
                }

                // Обновляем прогресс
                progress.CompletionPercentage = completionPercentage;
                
                // Если процент завершения 100%, то меняем статус на "Завершен"
                if (completionPercentage == 100 && progress.Status != "Завершен")
                {
                    progress.Status = "Завершен";
                    progress.CompletedAt = DateTime.UtcNow;
                    
                    // Вычисляем затраченное время
                    if (progress.StartedAt.HasValue && progress.CompletedAt.HasValue)
                    {
                        var timeSpent = progress.CompletedAt.Value - progress.StartedAt.Value;
                        progress.TimeSpentMinutes = (int)timeSpent.TotalMinutes;
                    }
                }

                await _lessonProgressRepository.UpdateAsync(progress);
                await _logService.LogInfoAsync("LessonProgressService", "UpdateProgressAsync", 
                    $"Обновлен прогресс {progressId} до {completionPercentage}%");

                return progress;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при обновлении прогресса {progressId}");
                await _logService.LogErrorAsync("LessonProgressService", "UpdateProgressAsync", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Завершить урок
        /// </summary>
        public async Task<LessonProgress> CompleteLessonAsync(Guid progressId, decimal? score)
        {
            try
            {
                var progress = await _lessonProgressRepository.GetByIdAsync(progressId);
                if (progress == null)
                {
                    _logger.LogWarning($"Прогресс с ID {progressId} не найден");
                    return null;
                }

                // Валидация оценки, если она указана
                if (score.HasValue && (score.Value < 0 || score.Value > 100))
                {
                    _logger.LogWarning($"Некорректная оценка: {score}");
                    throw new ArgumentOutOfRangeException(nameof(score), "Оценка должна быть от 0 до 100");
                }

                // Обновляем прогресс
                progress.Status = "Завершен";
                progress.CompletionPercentage = 100;
                progress.CompletedAt = DateTime.UtcNow;
                progress.Score = score;

                // Вычисляем затраченное время
                if (progress.StartedAt.HasValue && progress.CompletedAt.HasValue)
                {
                    var timeSpent = progress.CompletedAt.Value - progress.StartedAt.Value;
                    progress.TimeSpentMinutes = (int)timeSpent.TotalMinutes;
                }

                await _lessonProgressRepository.UpdateAsync(progress);
                await _logService.LogInfoAsync("LessonProgressService", "CompleteLessonAsync", 
                    $"Завершен урок {progress.LessonId} студентом {progress.StudentId} с оценкой {score}");

                return progress;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при завершении урока (прогресс {progressId})");
                await _logService.LogErrorAsync("LessonProgressService", "CompleteLessonAsync", ex.Message);
                throw;
            }
        }

        /// <summary>
        /// Получить общий прогресс студента по курсу в процентах
        /// </summary>
        public async Task<int> GetOverallCourseProgressPercentageAsync(Guid studentId, Guid courseId)
        {
            try
            {
                // Получаем прогресс по всем урокам курса
                var progressList = await _lessonProgressRepository.GetProgressByStudentAndCourseIdAsync(studentId, courseId);
                if (progressList == null || !progressList.Any())
                {
                    return 0; // Если прогресса нет, возвращаем 0%
                }

                // Получаем все уроки курса для подсчета общего количества
                var lessons = await _lessonRepository.GetAllAsync();
                var courseLessons = lessons.Where(l => l.Module.CourseId == courseId).ToList();
                
                if (!courseLessons.Any())
                {
                    return 0; // Если уроков в курсе нет, возвращаем 0%
                }

                // Вычисляем общий прогресс
                int totalLessons = courseLessons.Count;
                int completedLessons = progressList.Count(p => p.Status == "Завершен");
                int inProgressLessons = progressList.Count(p => p.Status == "В процессе");
                
                // Учитываем уроки в процессе с их процентом завершения
                double progressPercentage = ((double)completedLessons / totalLessons) * 100;
                
                if (inProgressLessons > 0)
                {
                    double inProgressContribution = progressList
                        .Where(p => p.Status == "В процессе")
                        .Sum(p => (double)p.CompletionPercentage / 100) / totalLessons;
                    
                    progressPercentage += inProgressContribution * 100;
                }

                return (int)Math.Round(progressPercentage);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при получении общего прогресса студента {studentId} по курсу {courseId}");
                await _logService.LogErrorAsync("LessonProgressService", "GetOverallCourseProgressPercentageAsync", ex.Message);
                throw;
            }
        }
    }
}
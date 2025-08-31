using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Courses.Domain.Entities;
using Courses.Domain.Interfaces;
using Courses.Domain.Models;
using Microsoft.Extensions.Logging;

namespace Courses.Infrastructure.Services
{
    /// <summary>
    /// Сервис опросов
    /// </summary>
    public class SurveyService : ISurveyService
    {
        private readonly ISurveyRepository _surveyRepository;
        private readonly ISurveyResponseRepository _surveyResponseRepository;
        private readonly ICourseRepository _courseRepository;
        private readonly ILogService _logService;
        private readonly ILogger<SurveyService> _logger;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="surveyRepository">Репозиторий опросов</param>
        /// <param name="surveyResponseRepository">Репозиторий ответов на опросы</param>
        /// <param name="courseRepository">Репозиторий курсов</param>
        /// <param name="logService">Сервис логирования</param>
        /// <param name="logger">Логгер</param>
        public SurveyService(
            ISurveyRepository surveyRepository,
            ISurveyResponseRepository surveyResponseRepository,
            ICourseRepository courseRepository,
            ILogService logService,
            ILogger<SurveyService> logger)
        {
            _surveyRepository = surveyRepository ?? throw new ArgumentNullException(nameof(surveyRepository));
            _surveyResponseRepository = surveyResponseRepository ?? throw new ArgumentNullException(nameof(surveyResponseRepository));
            _courseRepository = courseRepository ?? throw new ArgumentNullException(nameof(courseRepository));
            _logService = logService ?? throw new ArgumentNullException(nameof(logService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Создать опрос
        /// </summary>
        /// <param name="survey">Опрос</param>
        /// <returns>Созданный опрос</returns>
        public async Task<Survey> CreateSurveyAsync(Survey survey)
        {
            try
            {
                _logService.Information("Создание опроса для курса {CourseId}", survey.CourseId);

                // Проверка существования курса, если опрос привязан к курсу
                if (survey.CourseId.HasValue)
                {
                    var course = await _courseRepository.GetByIdAsync(survey.CourseId.Value);
                    if (course == null)
                    {
                        _logService.Warning("Курс с идентификатором {CourseId} не найден", survey.CourseId.Value);
                        throw new ArgumentException($"Курс с идентификатором {survey.CourseId.Value} не найден");
                    }
                }

                // Установка даты создания и статуса
                survey.CreatedAt = DateTime.UtcNow;
                survey.IsActive = true;

                // Сохранение опроса
                return await _surveyRepository.AddAsync(survey);
            }
            catch (Exception ex)
            {
                _logService.Error(ex, "Ошибка при создании опроса для курса {CourseId}", survey.CourseId);
                throw;
            }
        }

        /// <summary>
        /// Обновить опрос
        /// </summary>
        /// <param name="survey">Опрос</param>
        /// <returns>Обновленный опрос</returns>
        public async Task<Survey> UpdateSurveyAsync(Survey survey)
        {
            try
            {
                _logService.Information("Обновление опроса {SurveyId}", survey.Id);

                // Проверка существования опроса
                var existingSurvey = await _surveyRepository.GetByIdAsync(survey.Id);
                if (existingSurvey == null)
                {
                    _logService.Warning("Опрос с идентификатором {SurveyId} не найден", survey.Id);
                    throw new ArgumentException($"Опрос с идентификатором {survey.Id} не найден");
                }

                // Проверка существования курса, если опрос привязан к курсу
                if (survey.CourseId.HasValue)
                {
                    var course = await _courseRepository.GetByIdAsync(survey.CourseId.Value);
                    if (course == null)
                    {
                        _logService.Warning("Курс с идентификатором {CourseId} не найден", survey.CourseId.Value);
                        throw new ArgumentException($"Курс с идентификатором {survey.CourseId.Value} не найден");
                    }
                }

                // Обновление полей опроса
                existingSurvey.Title = survey.Title;
                existingSurvey.Description = survey.Description;
                existingSurvey.CourseId = survey.CourseId;
                existingSurvey.Questions = survey.Questions;
                existingSurvey.IsActive = survey.IsActive;
                existingSurvey.UpdatedAt = DateTime.UtcNow;

                // Сохранение опроса
                return await _surveyRepository.UpdateAsync(existingSurvey);
            }
            catch (Exception ex)
            {
                _logService.Error(ex, "Ошибка при обновлении опроса {SurveyId}", survey.Id);
                throw;
            }
        }

        /// <summary>
        /// Удалить опрос
        /// </summary>
        /// <param name="surveyId">Идентификатор опроса</param>
        /// <returns>Задача</returns>
        public async Task DeleteSurveyAsync(Guid surveyId)
        {
            try
            {
                _logService.Information("Удаление опроса {SurveyId}", surveyId);

                // Проверка существования опроса
                var survey = await _surveyRepository.GetByIdAsync(surveyId);
                if (survey == null)
                {
                    _logService.Warning("Опрос с идентификатором {SurveyId} не найден", surveyId);
                    throw new ArgumentException($"Опрос с идентификатором {surveyId} не найден");
                }

                // Удаление всех ответов на опрос
                await _surveyResponseRepository.DeleteResponsesBySurveyIdAsync(surveyId);

                // Удаление опроса
                await _surveyRepository.DeleteAsync(surveyId);
            }
            catch (Exception ex)
            {
                _logService.Error(ex, "Ошибка при удалении опроса {SurveyId}", surveyId);
                throw;
            }
        }

        /// <summary>
        /// Получить опрос по идентификатору
        /// </summary>
        /// <param name="surveyId">Идентификатор опроса</param>
        /// <returns>Опрос</returns>
        public async Task<Survey> GetSurveyByIdAsync(Guid surveyId)
        {
            try
            {
                _logService.Information("Получение опроса по идентификатору {SurveyId}", surveyId);
                return await _surveyRepository.GetByIdAsync(surveyId);
            }
            catch (Exception ex)
            {
                _logService.Error(ex, "Ошибка при получении опроса по идентификатору {SurveyId}", surveyId);
                throw;
            }
        }

        /// <summary>
        /// Получить опросы по идентификатору курса
        /// </summary>
        /// <param name="courseId">Идентификатор курса</param>
        /// <returns>Коллекция опросов</returns>
        public async Task<IEnumerable<Survey>> GetSurveysByCourseIdAsync(Guid courseId)
        {
            try
            {
                _logService.Information("Получение опросов по идентификатору курса {CourseId}", courseId);
                return await _surveyRepository.GetSurveysByCourseIdAsync(courseId);
            }
            catch (Exception ex)
            {
                _logService.Error(ex, "Ошибка при получении опросов по идентификатору курса {CourseId}", courseId);
                throw;
            }
        }

        /// <summary>
        /// Получить активные опросы
        /// </summary>
        /// <returns>Коллекция опросов</returns>
        public async Task<IEnumerable<Survey>> GetActiveSurveysAsync()
        {
            try
            {
                _logService.Information("Получение активных опросов");
                return await _surveyRepository.GetActiveSurveysAsync();
            }
            catch (Exception ex)
            {
                _logService.Error(ex, "Ошибка при получении активных опросов");
                throw;
            }
        }

        /// <summary>
        /// Создать ответ на опрос
        /// </summary>
        /// <param name="response">Ответ на опрос</param>
        /// <returns>Созданный ответ на опрос</returns>
        public async Task<SurveyResponse> CreateSurveyResponseAsync(SurveyResponse response)
        {
            try
            {
                _logService.Information("Создание ответа на опрос {SurveyId} от студента {StudentId}", response.SurveyId, response.StudentId);

                // Проверка существования опроса
                var survey = await _surveyRepository.GetByIdAsync(response.SurveyId);
                if (survey == null)
                {
                    _logService.Warning("Опрос с идентификатором {SurveyId} не найден", response.SurveyId);
                    throw new ArgumentException($"Опрос с идентификатором {response.SurveyId} не найден");
                }

                // Проверка активности опроса
                if (!survey.IsActive)
                {
                    _logService.Warning("Опрос с идентификатором {SurveyId} не активен", response.SurveyId);
                    throw new InvalidOperationException($"Опрос с идентификатором {response.SurveyId} не активен");
                }

                // Проверка наличия ответа от студента
                var existingResponse = await _surveyResponseRepository.GetResponseByStudentAndSurveyAsync(response.StudentId, response.SurveyId);
                if (existingResponse != null)
                {
                    _logService.Warning("Студент {StudentId} уже ответил на опрос {SurveyId}", response.StudentId, response.SurveyId);
                    throw new InvalidOperationException($"Студент уже ответил на этот опрос");
                }

                // Установка даты создания
                response.CreatedAt = DateTime.UtcNow;

                // Сохранение ответа
                return await _surveyResponseRepository.AddAsync(response);
            }
            catch (Exception ex)
            {
                _logService.Error(ex, "Ошибка при создании ответа на опрос {SurveyId} от студента {StudentId}", response.SurveyId, response.StudentId);
                throw;
            }
        }

        /// <summary>
        /// Получить ответы на опрос
        /// </summary>
        /// <param name="surveyId">Идентификатор опроса</param>
        /// <returns>Коллекция ответов на опрос</returns>
        public async Task<IEnumerable<SurveyResponse>> GetSurveyResponsesAsync(Guid surveyId)
        {
            try
            {
                _logService.Information("Получение ответов на опрос {SurveyId}", surveyId);

                // Проверка существования опроса
                var survey = await _surveyRepository.GetByIdAsync(surveyId);
                if (survey == null)
                {
                    _logService.Warning("Опрос с идентификатором {SurveyId} не найден", surveyId);
                    throw new ArgumentException($"Опрос с идентификатором {surveyId} не найден");
                }

                return await _surveyResponseRepository.GetResponsesBySurveyIdAsync(surveyId);
            }
            catch (Exception ex)
            {
                _logService.Error(ex, "Ошибка при получении ответов на опрос {SurveyId}", surveyId);
                throw;
            }
        }

        /// <summary>
        /// Получить ответы студента на опросы
        /// </summary>
        /// <param name="studentId">Идентификатор студента</param>
        /// <returns>Коллекция ответов на опросы</returns>
        public async Task<IEnumerable<SurveyResponse>> GetStudentSurveyResponsesAsync(Guid studentId)
        {
            try
            {
                _logService.Information("Получение ответов студента {StudentId} на опросы", studentId);
                return await _surveyResponseRepository.GetResponsesByStudentIdAsync(studentId);
            }
            catch (Exception ex)
            {
                _logService.Error(ex, "Ошибка при получении ответов студента {StudentId} на опросы", studentId);
                throw;
            }
        }

        /// <summary>
        /// Экспортировать результаты опроса в CSV
        /// </summary>
        /// <param name="surveyId">Идентификатор опроса</param>
        /// <returns>Содержимое CSV файла</returns>
        public async Task<string> ExportSurveyResultsToCsvAsync(Guid surveyId)
        {
            try
            {
                _logService.Information("Экспорт результатов опроса {SurveyId} в CSV", surveyId);

                // Получение опроса
                var survey = await _surveyRepository.GetByIdAsync(surveyId);
                if (survey == null)
                {
                    _logService.Warning("Опрос с идентификатором {SurveyId} не найден", surveyId);
                    throw new ArgumentException($"Опрос с идентификатором {surveyId} не найден");
                }

                // Получение ответов на опрос
                var responses = await _surveyResponseRepository.GetResponsesBySurveyIdAsync(surveyId);

                // Создание CSV
                var csv = new StringBuilder();

                // Заголовок
                csv.AppendLine($"Survey: {survey.Title}");
                csv.AppendLine("StudentId,SubmissionDate,Question,Answer");

                // Данные
                foreach (var response in responses)
                {
                    foreach (var answer in response.Answers)
                    {
                        // Найти вопрос по идентификатору
                        var question = survey.Questions.FirstOrDefault(q => q.Id == answer.QuestionId);
                        var questionText = question != null ? question.Text : "Unknown Question";

                        csv.AppendLine($"{response.StudentId},{response.CreatedAt:yyyy-MM-dd HH:mm:ss},\"{questionText}\",\"{answer.Text}\"");
                    }
                }

                return csv.ToString();
            }
            catch (Exception ex)
            {
                _logService.Error(ex, "Ошибка при экспорте результатов опроса {SurveyId} в CSV", surveyId);
                throw;
            }
        }

        /// <summary>
        /// Экспортировать результаты опроса в JSON
        /// </summary>
        /// <param name="surveyId">Идентификатор опроса</param>
        /// <returns>Содержимое JSON файла</returns>
        public async Task<string> ExportSurveyResultsToJsonAsync(Guid surveyId)
        {
            try
            {
                _logService.Information("Экспорт результатов опроса {SurveyId} в JSON", surveyId);

                // Получение опроса
                var survey = await _surveyRepository.GetByIdAsync(surveyId);
                if (survey == null)
                {
                    _logService.Warning("Опрос с идентификатором {SurveyId} не найден", surveyId);
                    throw new ArgumentException($"Опрос с идентификатором {surveyId} не найден");
                }

                // Получение ответов на опрос
                var responses = await _surveyResponseRepository.GetResponsesBySurveyIdAsync(surveyId);

                // Создание модели для экспорта
                var exportModel = new
                {
                    Survey = new
                    {
                        Id = survey.Id,
                        Title = survey.Title,
                        Description = survey.Description,
                        CourseId = survey.CourseId,
                        Questions = survey.Questions
                    },
                    Responses = responses.Select(r => new
                    {
                        r.Id,
                        r.StudentId,
                        r.CreatedAt,
                        Answers = r.Answers.Select(a => new
                        {
                            a.QuestionId,
                            QuestionText = survey.Questions.FirstOrDefault(q => q.Id == a.QuestionId)?.Text ?? "Unknown Question",
                            a.Text
                        })
                    })
                };

                // Сериализация в JSON
                return JsonSerializer.Serialize(exportModel, new JsonSerializerOptions
                {
                    WriteIndented = true
                });
            }
            catch (Exception ex)
            {
                _logService.Error(ex, "Ошибка при экспорте результатов опроса {SurveyId} в JSON", surveyId);
                throw;
            }
        }

        /// <summary>
        /// Получить статистику по опросу
        /// </summary>
        /// <param name="surveyId">Идентификатор опроса</param>
        /// <returns>Статистика по опросу</returns>
        public async Task<SurveyStatisticsModel> GetSurveyStatisticsAsync(Guid surveyId)
        {
            try
            {
                _logService.Information("Получение статистики по опросу {SurveyId}", surveyId);

                // Получение опроса
                var survey = await _surveyRepository.GetByIdAsync(surveyId);
                if (survey == null)
                {
                    _logService.Warning("Опрос с идентификатором {SurveyId} не найден", surveyId);
                    throw new ArgumentException($"Опрос с идентификатором {surveyId} не найден");
                }

                // Получение ответов на опрос
                var responses = await _surveyResponseRepository.GetResponsesBySurveyIdAsync(surveyId);

                // Создание модели статистики
                var statistics = new SurveyStatisticsModel
                {
                    SurveyId = surveyId,
                    SurveyTitle = survey.Title,
                    TotalResponses = responses.Count(),
                    QuestionStatistics = new List<QuestionStatisticsModel>()
                };

                // Расчет статистики по каждому вопросу
                foreach (var question in survey.Questions)
                {
                    var questionStats = new QuestionStatisticsModel
                    {
                        QuestionId = question.Id,
                        QuestionText = question.Text,
                        QuestionType = question.Type,
                        TotalAnswers = 0,
                        AnswerDistribution = new Dictionary<string, int>()
                    };

                    // Подсчет ответов на вопрос
                    foreach (var response in responses)
                    {
                        var answer = response.Answers.FirstOrDefault(a => a.QuestionId == question.Id);
                        if (answer != null)
                        {
                            questionStats.TotalAnswers++;

                            // Для вопросов с выбором вариантов подсчитываем распределение ответов
                            if (question.Type == "single_choice" || question.Type == "multiple_choice")
                            {
                                var answerValues = answer.Text.Split(',');
                                foreach (var value in answerValues)
                                {
                                    var trimmedValue = value.Trim();
                                    if (!string.IsNullOrEmpty(trimmedValue))
                                    {
                                        if (questionStats.AnswerDistribution.ContainsKey(trimmedValue))
                                        {
                                            questionStats.AnswerDistribution[trimmedValue]++;
                                        }
                                        else
                                        {
                                            questionStats.AnswerDistribution[trimmedValue] = 1;
                                        }
                                    }
                                }
                            }
                        }
                    }

                    statistics.QuestionStatistics.Add(questionStats);
                }

                return statistics;
            }
            catch (Exception ex)
            {
                _logService.Error(ex, "Ошибка при получении статистики по опросу {SurveyId}", surveyId);
                throw;
            }
        }
    }
}
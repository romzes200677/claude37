using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Courses.Domain.Interfaces;
using Courses.Domain.Models;

namespace Courses.Infrastructure.Services
{
    /// <summary>
    /// Сервис для интеграции с внешними системами
    /// </summary>
    public class IntegrationService : IIntegrationService
    {
        private readonly ILogService _logService;
        private readonly IConfigurationService _configurationService;
        private readonly HttpClient _httpClient;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="logService">Сервис логирования</param>
        /// <param name="configurationService">Сервис конфигурации</param>
        /// <param name="httpClient">HTTP клиент</param>
        public IntegrationService(
            ILogService logService,
            IConfigurationService configurationService,
            HttpClient httpClient)
        {
            _logService = logService;
            _configurationService = configurationService;
            _httpClient = httpClient;
        }

        /// <summary>
        /// Получить информацию о пользователе
        /// </summary>
        public async Task<UserInfoModel> GetUserInfoAsync(Guid userId)
        {
            try
            {
                _logService.Information($"Получение информации о пользователе: {userId}");
                
                // Получаем базовый URL сервиса идентификации из конфигурации
                var identityServiceUrl = await _configurationService.GetValueAsync<string>("IdentityServiceUrl");
                var url = $"{identityServiceUrl}/api/users/{userId}";
                
                // Выполняем запрос к сервису идентификации
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                
                // Десериализуем ответ
                var content = await response.Content.ReadAsStringAsync();
                var userInfo = JsonSerializer.Deserialize<UserInfoModel>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                
                return userInfo;
            }
            catch (Exception ex)
            {
                _logService.Error(ex, $"Ошибка при получении информации о пользователе: {userId}");
                // В случае ошибки возвращаем базовую информацию о пользователе
                return new UserInfoModel
                {
                    Id = userId,
                    Username = "Unknown",
                    FullName = "Unknown User",
                    Email = "unknown@example.com"
                };
            }
        }

        /// <summary>
        /// Получить информацию о тесте
        /// </summary>
        public async Task<TestInfoModel> GetTestInfoAsync(Guid testId)
        {
            try
            {
                _logService.Information($"Получение информации о тесте: {testId}");
                
                // Получаем базовый URL сервиса тестирования из конфигурации
                var testingServiceUrl = await _configurationService.GetValueAsync<string>("TestingServiceUrl");
                var url = $"{testingServiceUrl}/api/tests/{testId}";
                
                // Выполняем запрос к сервису тестирования
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                
                // Десериализуем ответ
                var content = await response.Content.ReadAsStringAsync();
                var testInfo = JsonSerializer.Deserialize<TestInfoModel>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                
                return testInfo;
            }
            catch (Exception ex)
            {
                _logService.Error(ex, $"Ошибка при получении информации о тесте: {testId}");
                // В случае ошибки возвращаем базовую информацию о тесте
                return new TestInfoModel
                {
                    Id = testId,
                    Title = "Unknown Test",
                    Description = "Test information unavailable"
                };
            }
        }

        /// <summary>
        /// Получить результаты теста
        /// </summary>
        public async Task<TestResultModel> GetTestResultsAsync(Guid testId, Guid userId)
        {
            try
            {
                _logService.Information($"Получение результатов теста: {testId} для пользователя: {userId}");
                
                // Получаем базовый URL сервиса тестирования из конфигурации
                var testingServiceUrl = await _configurationService.GetValueAsync<string>("TestingServiceUrl");
                var url = $"{testingServiceUrl}/api/tests/{testId}/results/{userId}";
                
                // Выполняем запрос к сервису тестирования
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                
                // Десериализуем ответ
                var content = await response.Content.ReadAsStringAsync();
                var testResult = JsonSerializer.Deserialize<TestResultModel>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                
                return testResult;
            }
            catch (Exception ex)
            {
                _logService.Error(ex, $"Ошибка при получении результатов теста: {testId} для пользователя: {userId}");
                // В случае ошибки возвращаем пустой результат
                return new TestResultModel
                {
                    TestId = testId,
                    UserId = userId,
                    Score = 0,
                    MaxScore = 0,
                    Percentage = 0,
                    Passed = false
                };
            }
        }

        /// <summary>
        /// Получить информацию о задаче по программированию
        /// </summary>
        public async Task<CodeTaskInfoModel> GetCodeTaskInfoAsync(Guid codeTaskId)
        {
            try
            {
                _logService.Information($"Получение информации о задаче по программированию: {codeTaskId}");
                
                // Получаем базовый URL сервиса выполнения кода из конфигурации
                var codeExecServiceUrl = await _configurationService.GetValueAsync<string>("CodeExecServiceUrl");
                var url = $"{codeExecServiceUrl}/api/tasks/{codeTaskId}";
                
                // Выполняем запрос к сервису выполнения кода
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                
                // Десериализуем ответ
                var content = await response.Content.ReadAsStringAsync();
                var codeTaskInfo = JsonSerializer.Deserialize<CodeTaskInfoModel>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                
                return codeTaskInfo;
            }
            catch (Exception ex)
            {
                _logService.Error(ex, $"Ошибка при получении информации о задаче по программированию: {codeTaskId}");
                // В случае ошибки возвращаем базовую информацию о задаче
                return new CodeTaskInfoModel
                {
                    Id = codeTaskId,
                    Title = "Unknown Code Task",
                    Description = "Code task information unavailable"
                };
            }
        }

        /// <summary>
        /// Получить результаты выполнения задачи по программированию
        /// </summary>
        public async Task<CodeTaskResultModel> GetCodeTaskResultsAsync(Guid codeTaskId, Guid userId)
        {
            try
            {
                _logService.Information($"Получение результатов задачи по программированию: {codeTaskId} для пользователя: {userId}");
                
                // Получаем базовый URL сервиса выполнения кода из конфигурации
                var codeExecServiceUrl = await _configurationService.GetValueAsync<string>("CodeExecServiceUrl");
                var url = $"{codeExecServiceUrl}/api/tasks/{codeTaskId}/results/{userId}";
                
                // Выполняем запрос к сервису выполнения кода
                var response = await _httpClient.GetAsync(url);
                response.EnsureSuccessStatusCode();
                
                // Десериализуем ответ
                var content = await response.Content.ReadAsStringAsync();
                var codeTaskResult = JsonSerializer.Deserialize<CodeTaskResultModel>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
                
                return codeTaskResult;
            }
            catch (Exception ex)
            {
                _logService.Error(ex, $"Ошибка при получении результатов задачи по программированию: {codeTaskId} для пользователя: {userId}");
                // В случае ошибки возвращаем пустой результат
                return new CodeTaskResultModel
                {
                    CodeTaskId = codeTaskId,
                    UserId = userId,
                    Score = 0,
                    MaxScore = 0,
                    Percentage = 0,
                    Passed = false
                };
            }
        }

        /// <summary>
        /// Синхронизировать данные о курсе с другими микросервисами
        /// </summary>
        public async Task SyncCourseDataAsync(Guid courseId)
        {
            try
            {
                _logService.Information($"Синхронизация данных о курсе: {courseId} с другими микросервисами");
                
                // Получаем базовые URL сервисов из конфигурации
                var identityServiceUrl = await _configurationService.GetValueAsync<string>("IdentityServiceUrl");
                var testingServiceUrl = await _configurationService.GetValueAsync<string>("TestingServiceUrl");
                var codeExecServiceUrl = await _configurationService.GetValueAsync<string>("CodeExecServiceUrl");
                
                // Создаем объект для синхронизации
                var syncData = new
                {
                    CourseId = courseId,
                    Timestamp = DateTime.UtcNow
                };
                
                var json = JsonSerializer.Serialize(syncData);
                var content = new StringContent(json, Encoding.UTF8, "application/json");
                
                // Синхронизируем с сервисом идентификации
                await _httpClient.PostAsync($"{identityServiceUrl}/api/integration/courses/sync", content);
                
                // Синхронизируем с сервисом тестирования
                await _httpClient.PostAsync($"{testingServiceUrl}/api/integration/courses/sync", content);
                
                // Синхронизируем с сервисом выполнения кода
                await _httpClient.PostAsync($"{codeExecServiceUrl}/api/integration/courses/sync", content);
                
                _logService.Information($"Синхронизация данных о курсе: {courseId} успешно завершена");
            }
            catch (Exception ex)
            {
                _logService.Error(ex, $"Ошибка при синхронизации данных о курсе: {courseId}");
                throw;
            }
        }
    }
}
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Testing.Application.Services;

namespace Testing.Infrastructure.Services
{
    public class AiService : IAiService
    {
        private readonly HttpClient _httpClient;
        private readonly ILogger<AiService> _loggeri
        private readonly string _apiKey;
        private readonly string _apiEndpoint;

        public AiService(
            HttpClient httpClient,
            IConfiguration configuration,
            ILogger<AiService> logger)
        {
            _httpClient = httpClient;
            _logger = logger;
            
            // Получаем настройки из конфигурации
            _apiKey = configuration["AiService:ApiKey"];
            _apiEndpoint = configuration["AiService:ApiEndpoint"];
            
            // Настраиваем базовый адрес и заголовки для HTTP-клиента
            if (!string.IsNullOrEmpty(_apiEndpoint))
            {
                _httpClient.BaseAddress = new Uri(_apiEndpoint);
            }
            
            if (!string.IsNullOrEmpty(_apiKey))
            {
                _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
            }
        }

        public async Task<AiEvaluationResult> EvaluateTextAnswerAsync(string questionText, string correctAnswer, string userAnswer)
        {
            try
            {
                // Формируем запрос к ИИ сервису
                var requestData = new
                {
                    question = questionText,
                    correctAnswer = correctAnswer,
                    userAnswer = userAnswer,
                    type = "text"
                };

                return await SendEvaluationRequestAsync(requestData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error evaluating text answer with AI service");
                
                // В случае ошибки возвращаем результат по умолчанию
                return new AiEvaluationResult
                {
                    Score = 0,
                    Explanation = $"Ошибка при оценке ответа: {ex.Message}"
                };
            }
        }

        public async Task<AiEvaluationResult> EvaluateCodeAnswerAsync(string questionText, string correctCode, string userCode)
        {
            try
            {
                // Формируем запрос к ИИ сервису
                var requestData = new
                {
                    question = questionText,
                    correctAnswer = correctCode,
                    userAnswer = userCode,
                    type = "code"
                };

                return await SendEvaluationRequestAsync(requestData);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error evaluating code answer with AI service");
                
                // В случае ошибки возвращаем результат по умолчанию
                return new AiEvaluationResult
                {
                    Score = 0,
                    Explanation = $"Ошибка при оценке кода: {ex.Message}"
                };
            }
        }

        private async Task<AiEvaluationResult> SendEvaluationRequestAsync(object requestData)
        {
            // Отправляем запрос к ИИ сервису
            var response = await _httpClient.PostAsJsonAsync("/api/evaluate", requestData);
            
            // Проверяем успешность запроса
            if (!response.IsSuccessStatusCode)
            {
                var errorContent = await response.Content.ReadAsStringAsync();
                _logger.LogError($"AI service returned error: {response.StatusCode}, {errorContent}");
                
                throw new HttpRequestException($"AI service returned {response.StatusCode}: {errorContent}");
            }
            
            // Десериализуем ответ
            var result = await response.Content.ReadFromJsonAsync<AiEvaluationResult>();
            
            if (result == null)
            {
                throw new InvalidOperationException("AI service returned null result");
            }
            
            return result;
        }
    }
}
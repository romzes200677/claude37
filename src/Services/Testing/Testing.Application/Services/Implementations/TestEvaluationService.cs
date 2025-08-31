using Microsoft.Extensions.Logging;
using System;
using System.Threading.Tasks;
using Testing.Domain.Entities;
using Testing.Domain.Enums;
using Testing.Domain.Repositories;

namespace Testing.Application.Services.Implementations
{
    public class TestEvaluationService : ITestEvaluationService
    {
        private readonly ITestQuestionResponseRepository _responseRepository;
        private readonly ITestQuestionRepository _questionRepository;
        private readonly IAiService _aiService;
        private readonly ILogger<TestEvaluationService> _logger;

        public TestEvaluationService(
            ITestQuestionResponseRepository responseRepository,
            ITestQuestionRepository questionRepository,
            IAiService aiService,
            ILogger<TestEvaluationService> logger)
        {
            _responseRepository = responseRepository;
            _questionRepository = questionRepository;
            _aiService = aiService;
            _logger = logger;
        }

        public async Task<TestQuestionResponse> EvaluateResponseAsync(Guid responseId)
        {
            try
            {
                var response = await _responseRepository.GetByIdAsync(responseId);
                if (response == null)
                {
                    throw new KeyNotFoundException($"Test question response with ID {responseId} not found");
                }

                var question = await _questionRepository.GetByIdAsync(response.QuestionId);
                if (question == null)
                {
                    throw new KeyNotFoundException($"Test question with ID {response.QuestionId} not found");
                }

                // Оцениваем ответ в зависимости от типа вопроса
                switch (question.QuestionType)
                {
                    case QuestionType.Text:
                        return await EvaluateTextResponseAsync(response);
                    case QuestionType.Code:
                        return await EvaluateCodeResponseAsync(response);
                    default:
                        // Для других типов вопросов (SingleChoice, MultipleChoice) оценка происходит автоматически
                        // в TestQuestionResponseService.EvaluateResponseInternalAsync
                        _logger.LogWarning($"Question type {question.QuestionType} is not supported for AI evaluation");
                        return response;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error evaluating response with ID {responseId}");
                throw;
            }
        }

        public async Task<TestQuestionResponse> EvaluateTextResponseAsync(TestQuestionResponse response)
        {
            try
            {
                var question = await _questionRepository.GetByIdAsync(response.QuestionId);
                if (question == null)
                {
                    throw new KeyNotFoundException($"Test question with ID {response.QuestionId} not found");
                }

                // Проверяем, что это текстовый вопрос
                if (question.QuestionType != QuestionType.Text)
                {
                    _logger.LogWarning($"Question type {question.QuestionType} is not supported for text evaluation");
                    return response;
                }

                // Получаем правильный ответ и ответ пользователя
                string correctAnswer = question.CorrectAnswer;
                string userAnswer = response.ResponseText;

                if (string.IsNullOrEmpty(userAnswer))
                {
                    response.IsCorrect = false;
                    response.PointsEarned = 0;
                    response.AiEvaluation = "Ответ не предоставлен";
                    return await _responseRepository.UpdateAsync(response);
                }

                // Оцениваем ответ с использованием ИИ
                var evaluationResult = await _aiService.EvaluateTextAnswerAsync(
                    question.Text,
                    correctAnswer,
                    userAnswer);

                // Обновляем ответ с результатами оценки
                response.IsCorrect = evaluationResult.IsCorrect;
                response.PointsEarned = evaluationResult.IsCorrect ? question.Points : 0;
                response.AiEvaluation = evaluationResult.Explanation;
                response.UpdatedAt = DateTime.UtcNow;

                return await _responseRepository.UpdateAsync(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error evaluating text response for question {response.QuestionId}");
                throw;
            }
        }

        public async Task<TestQuestionResponse> EvaluateCodeResponseAsync(TestQuestionResponse response)
        {
            try
            {
                var question = await _questionRepository.GetByIdAsync(response.QuestionId);
                if (question == null)
                {
                    throw new KeyNotFoundException($"Test question with ID {response.QuestionId} not found");
                }

                // Проверяем, что это вопрос с кодом
                if (question.QuestionType != QuestionType.Code)
                {
                    _logger.LogWarning($"Question type {question.QuestionType} is not supported for code evaluation");
                    return response;
                }

                // Получаем правильный ответ и ответ пользователя
                string correctCode = question.CorrectAnswer;
                string userCode = response.ResponseText;

                if (string.IsNullOrEmpty(userCode))
                {
                    response.IsCorrect = false;
                    response.PointsEarned = 0;
                    response.AiEvaluation = "Код не предоставлен";
                    return await _responseRepository.UpdateAsync(response);
                }

                // Оцениваем ответ с использованием ИИ
                var evaluationResult = await _aiService.EvaluateCodeAnswerAsync(
                    question.Text,
                    correctCode,
                    userCode);

                // Обновляем ответ с результатами оценки
                response.IsCorrect = evaluationResult.IsCorrect;
                response.PointsEarned = evaluationResult.IsCorrect ? question.Points : 0;
                response.AiEvaluation = evaluationResult.Explanation;
                response.UpdatedAt = DateTime.UtcNow;

                return await _responseRepository.UpdateAsync(response);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error evaluating code response for question {response.QuestionId}");
                throw;
            }
        }
    }
}
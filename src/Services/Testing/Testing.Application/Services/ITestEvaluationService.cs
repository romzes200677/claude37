using System;
using System.Threading.Tasks;
using Testing.Domain.Entities;

namespace Testing.Application.Services
{
    /// <summary>
    /// Сервис для автоматической оценки ответов на тестовые вопросы с использованием ИИ
    /// </summary>
    public interface ITestEvaluationService
    {
        /// <summary>
        /// Оценивает текстовый ответ на вопрос с использованием ИИ
        /// </summary>
        /// <param name="questionResponse">Ответ на вопрос</param>
        /// <returns>Обновленный ответ с оценкой ИИ</returns>
        Task<TestQuestionResponse> EvaluateTextResponseAsync(TestQuestionResponse questionResponse);
        
        /// <summary>
        /// Оценивает ответ с кодом на вопрос с использованием ИИ
        /// </summary>
        /// <param name="questionResponse">Ответ на вопрос</param>
        /// <returns>Обновленный ответ с оценкой ИИ</returns>
        Task<TestQuestionResponse> EvaluateCodeResponseAsync(TestQuestionResponse questionResponse);
        
        /// <summary>
        /// Оценивает ответ на вопрос с использованием ИИ в зависимости от типа вопроса
        /// </summary>
        /// <param name="responseId">Идентификатор ответа на вопрос</param>
        /// <returns>Обновленный ответ с оценкой ИИ</returns>
        Task<TestQuestionResponse> EvaluateResponseAsync(Guid responseId);
    }
}
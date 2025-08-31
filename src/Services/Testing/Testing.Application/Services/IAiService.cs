using System.Threading.Tasks;

namespace Testing.Application.Services
{
    /// <summary>
    /// Интерфейс для взаимодействия с внешним ИИ сервисом
    /// </summary>
    public interface IAiService
    {
        /// <summary>
        /// Анализирует текстовый ответ на вопрос с использованием ИИ
        /// </summary>
        /// <param name="questionText">Текст вопроса</param>
        /// <param name="correctAnswer">Правильный ответ</param>
        /// <param name="userAnswer">Ответ пользователя</param>
        /// <returns>Результат анализа ИИ</returns>
        Task<AiEvaluationResult> EvaluateTextAnswerAsync(string questionText, string correctAnswer, string userAnswer);
        
        /// <summary>
        /// Анализирует ответ с кодом на вопрос с использованием ИИ
        /// </summary>
        /// <param name="questionText">Текст вопроса</param>
        /// <param name="correctCode">Правильный код</param>
        /// <param name="userCode">Код пользователя</param>
        /// <returns>Результат анализа ИИ</returns>
        Task<AiEvaluationResult> EvaluateCodeAnswerAsync(string questionText, string correctCode, string userCode);
    }

    /// <summary>
    /// Результат оценки ответа с использованием ИИ
    /// </summary>
    public class AiEvaluationResult
    {
        /// <summary>
        /// Оценка правильности ответа (от 0 до 1)
        /// </summary>
        public double Score { get; set; }
        
        /// <summary>
        /// Текстовое объяснение оценки
        /// </summary>
        public string Explanation { get; set; }
        
        /// <summary>
        /// Флаг, указывающий, считается ли ответ правильным
        /// </summary>
        public bool IsCorrect => Score >= 0.7;
    }
}
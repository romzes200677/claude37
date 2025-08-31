using System.Collections.Generic;

namespace Identity.Domain.Models
{
    /// <summary>
    /// Результат выхода из системы
    /// </summary>
    public class LogoutResult
    {
        /// <summary>
        /// Успешность операции
        /// </summary>
        public bool Succeeded { get; set; }

        /// <summary>
        /// Сообщение
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Список ошибок
        /// </summary>
        public List<string> Errors { get; set; } = new List<string>();

        /// <summary>
        /// Создать успешный результат
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <returns>Результат выхода из системы</returns>
        public static LogoutResult Success(string message = "Выход выполнен успешно")
        {
            return new LogoutResult
            {
                Succeeded = true,
                Message = message
            };
        }

        /// <summary>
        /// Создать неуспешный результат
        /// </summary>
        /// <param name="errors">Список ошибок</param>
        /// <returns>Результат выхода из системы</returns>
        public static LogoutResult Failure(List<string> errors)
        {
            return new LogoutResult
            {
                Succeeded = false,
                Errors = errors,
                Message = "Ошибка при выходе из системы"
            };
        }

        /// <summary>
        /// Создать неуспешный результат
        /// </summary>
        /// <param name="error">Ошибка</param>
        /// <returns>Результат выхода из системы</returns>
        public static LogoutResult Failure(string error)
        {
            return new LogoutResult
            {
                Succeeded = false,
                Errors = new List<string> { error },
                Message = "Ошибка при выходе из системы"
            };
        }
    }
}
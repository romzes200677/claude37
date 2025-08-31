using System.Collections.Generic;

namespace Identity.Domain.Models
{
    /// <summary>
    /// Результат регистрации
    /// </summary>
    public class RegisterResult
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
        /// <returns>Результат регистрации</returns>
        public static RegisterResult Success(string message = "Регистрация выполнена успешно")
        {
            return new RegisterResult
            {
                Succeeded = true,
                Message = message
            };
        }

        /// <summary>
        /// Создать неуспешный результат
        /// </summary>
        /// <param name="errors">Список ошибок</param>
        /// <returns>Результат регистрации</returns>
        public static RegisterResult Failure(List<string> errors)
        {
            return new RegisterResult
            {
                Succeeded = false,
                Errors = errors,
                Message = "Ошибка при регистрации"
            };
        }

        /// <summary>
        /// Создать неуспешный результат
        /// </summary>
        /// <param name="error">Ошибка</param>
        /// <returns>Результат регистрации</returns>
        public static RegisterResult Failure(string error)
        {
            return new RegisterResult
            {
                Succeeded = false,
                Errors = new List<string> { error },
                Message = "Ошибка при регистрации"
            };
        }
    }
}
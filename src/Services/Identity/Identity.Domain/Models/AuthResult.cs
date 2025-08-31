using System;
using System.Collections.Generic;

namespace Identity.Domain.Models
{
    /// <summary>
    /// Результат аутентификации
    /// </summary>
    public class AuthResult
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
        /// Токен доступа
        /// </summary>
        public string AccessToken { get; set; }

        /// <summary>
        /// Токен обновления
        /// </summary>
        public string RefreshToken { get; set; }

        /// <summary>
        /// Срок действия токена
        /// </summary>
        public DateTime Expiration { get; set; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Email пользователя
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Роли пользователя
        /// </summary>
        public List<string> Roles { get; set; } = new List<string>();

        /// <summary>
        /// Список ошибок
        /// </summary>
        public List<string> Errors { get; set; } = new List<string>();

        /// <summary>
        /// Создать успешный результат
        /// </summary>
        /// <param name="accessToken">Токен доступа</param>
        /// <param name="refreshToken">Токен обновления</param>
        /// <param name="expiration">Срок действия токена</param>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="username">Имя пользователя</param>
        /// <param name="email">Email пользователя</param>
        /// <param name="roles">Роли пользователя</param>
        /// <returns>Результат аутентификации</returns>
        public static AuthResult Success(string accessToken, string refreshToken, DateTime expiration, Guid userId, string username, string email, List<string> roles)
        {
            return new AuthResult
            {
                Succeeded = true,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                Expiration = expiration,
                UserId = userId,
                Username = username,
                Email = email,
                Roles = roles,
                Message = "Аутентификация выполнена успешно"
            };
        }

        /// <summary>
        /// Создать неуспешный результат
        /// </summary>
        /// <param name="errors">Список ошибок</param>
        /// <returns>Результат аутентификации</returns>
        public static AuthResult Failure(List<string> errors)
        {
            return new AuthResult
            {
                Succeeded = false,
                Errors = errors,
                Message = "Ошибка при аутентификации"
            };
        }

        /// <summary>
        /// Создать неуспешный результат
        /// </summary>
        /// <param name="error">Ошибка</param>
        /// <returns>Результат аутентификации</returns>
        public static AuthResult Failure(string error)
        {
            return new AuthResult
            {
                Succeeded = false,
                Errors = new List<string> { error },
                Message = "Ошибка при аутентификации"
            };
        }
    }
}
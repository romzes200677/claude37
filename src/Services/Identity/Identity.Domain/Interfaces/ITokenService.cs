using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Identity.Domain.Entities;

namespace Identity.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса токенов
    /// </summary>
    public interface ITokenService
    {
        /// <summary>
        /// Создать токен доступа
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <param name="roles">Роли пользователя</param>
        /// <returns>Токен доступа и срок его действия</returns>
        Task<(string accessToken, DateTime expiration)> CreateAccessTokenAsync(User user, IList<string> roles);

        /// <summary>
        /// Создать токен обновления
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns>Токен обновления</returns>
        Task<string> CreateRefreshTokenAsync(User user);

        /// <summary>
        /// Валидировать токен доступа
        /// </summary>
        /// <param name="token">Токен доступа</param>
        /// <returns>Результат валидации и список утверждений</returns>
        Task<(bool isValid, ClaimsPrincipal principal)> ValidateAccessTokenAsync(string token);

        /// <summary>
        /// Валидировать токен обновления
        /// </summary>
        /// <param name="token">Токен обновления</param>
        /// <returns>Результат валидации и токен обновления</returns>
        Task<(bool isValid, RefreshToken refreshToken)> ValidateRefreshTokenAsync(string token);

        /// <summary>
        /// Отозвать токен обновления
        /// </summary>
        /// <param name="token">Токен обновления</param>
        /// <returns>Результат операции</returns>
        Task<bool> RevokeRefreshTokenAsync(string token);

        /// <summary>
        /// Отозвать все токены обновления пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns>Результат операции</returns>
        Task<bool> RevokeAllRefreshTokensAsync(Guid userId);

        /// <summary>
        /// Создать токен подтверждения email
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns>Токен подтверждения</returns>
        Task<string> CreateEmailConfirmationTokenAsync(User user);

        /// <summary>
        /// Создать токен сброса пароля
        /// </summary>
        /// <param name="user">Пользователь</param>
        /// <returns>Токен сброса пароля</returns>
        Task<string> CreatePasswordResetTokenAsync(User user);
    }
}
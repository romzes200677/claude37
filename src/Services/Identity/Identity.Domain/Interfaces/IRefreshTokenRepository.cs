using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Interfaces;
using Identity.Domain.Entities;

namespace Identity.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория токенов обновления
    /// </summary>
    public interface IRefreshTokenRepository : IRepository<RefreshToken>
    {
        /// <summary>
        /// Получить токен по значению
        /// </summary>
        /// <param name="token">Значение токена</param>
        /// <returns>Токен обновления</returns>
        Task<RefreshToken> GetByTokenAsync(string token);

        /// <summary>
        /// Получить активные токены пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns>Список активных токенов</returns>
        Task<IEnumerable<RefreshToken>> GetActiveTokensByUserIdAsync(Guid userId);

        /// <summary>
        /// Отозвать все токены пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns>Задача</returns>
        Task RevokeAllTokensByUserIdAsync(Guid userId);
    }
}
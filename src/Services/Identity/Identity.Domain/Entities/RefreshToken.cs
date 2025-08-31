using System;

namespace Identity.Domain.Entities
{
    /// <summary>
    /// Токен обновления
    /// </summary>
    public class RefreshToken : BaseEntity
    {
        /// <summary>
        /// Токен
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// Срок действия
        /// </summary>
        public DateTime ExpiryDate { get; set; }

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Использован ли токен
        /// </summary>
        public bool IsUsed { get; set; } = false;

        /// <summary>
        /// Отозван ли токен
        /// </summary>
        public bool IsRevoked { get; set; } = false;

        /// <summary>
        /// Проверить, истек ли срок действия токена
        /// </summary>
        /// <returns>True, если срок действия истек</returns>
        public bool IsExpired() => DateTime.UtcNow >= ExpiryDate;

        /// <summary>
        /// Проверить, активен ли токен
        /// </summary>
        /// <returns>True, если токен активен</returns>
        public bool IsActive() => !IsUsed && !IsRevoked && !IsExpired();
    }
}
using System;
using System.Collections.Generic;

namespace Identity.Domain.Entities
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class User : BaseEntity
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Хеш пароля
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Соль для пароля
        /// </summary>
        public string PasswordSalt { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Дата последнего входа
        /// </summary>
        public DateTime? LastLoginDate { get; set; }

        /// <summary>
        /// Активен ли пользователь
        /// </summary>
        public bool IsActive { get; set; } = true;

        /// <summary>
        /// Подтвержден ли email
        /// </summary>
        public bool EmailConfirmed { get; set; } = false;

        /// <summary>
        /// Роли пользователя
        /// </summary>
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
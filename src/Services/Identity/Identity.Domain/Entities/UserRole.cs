using System;

namespace Identity.Domain.Entities
{
    /// <summary>
    /// Связь пользователя и роли
    /// </summary>
    public class UserRole : BaseEntity
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Пользователь
        /// </summary>
        public User User { get; set; }

        /// <summary>
        /// Идентификатор роли
        /// </summary>
        public Guid RoleId { get; set; }

        /// <summary>
        /// Роль
        /// </summary>
        public Role Role { get; set; }
    }
}
using System.Collections.Generic;

namespace Identity.Domain.Entities
{
    /// <summary>
    /// Роль
    /// </summary>
    public class Role : BaseEntity
    {
        /// <summary>
        /// Название роли
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Описание роли
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Пользователи с этой ролью
        /// </summary>
        public ICollection<UserRole> UserRoles { get; set; } = new List<UserRole>();
    }
}
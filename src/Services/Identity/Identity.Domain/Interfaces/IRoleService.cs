using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Interfaces;
using Common.Models;
using Identity.Domain.Entities;

namespace Identity.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса управления ролями
    /// </summary>
    public interface IRoleService : IService<Role>
    {
        /// <summary>
        /// Получить роль по названию
        /// </summary>
        /// <param name="name">Название роли</param>
        /// <returns>Роль</returns>
        Task<Role> GetByNameAsync(string name);

        /// <summary>
        /// Получить список ролей с пагинацией
        /// </summary>
        /// <param name="parameters">Параметры пагинации</param>
        /// <returns>Список ролей</returns>
        Task<PaginatedList<Role>> GetPaginatedAsync(PaginationParameters parameters);

        /// <summary>
        /// Получить список пользователей в роли
        /// </summary>
        /// <param name="roleName">Название роли</param>
        /// <param name="parameters">Параметры пагинации</param>
        /// <returns>Список пользователей</returns>
        Task<PaginatedList<User>> GetUsersInRoleAsync(string roleName, PaginationParameters parameters);
    }
}
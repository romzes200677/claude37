using System.Threading.Tasks;
using Common.Interfaces;
using Identity.Domain.Entities;

namespace Identity.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория ролей
    /// </summary>
    public interface IRoleRepository : IRepository<Role>
    {
        /// <summary>
        /// Получить роль по названию
        /// </summary>
        /// <param name="name">Название роли</param>
        /// <returns>Роль</returns>
        Task<Role> GetByNameAsync(string name);

        /// <summary>
        /// Проверить, существует ли роль с указанным названием
        /// </summary>
        /// <param name="name">Название роли</param>
        /// <returns>True, если роль существует</returns>
        Task<bool> ExistsByNameAsync(string name);
    }
}
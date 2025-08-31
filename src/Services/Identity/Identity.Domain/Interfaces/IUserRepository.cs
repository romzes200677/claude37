using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Interfaces;
using Identity.Domain.Entities;

namespace Identity.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс репозитория пользователей
    /// </summary>
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// Получить пользователя по имени пользователя
        /// </summary>
        /// <param name="username">Имя пользователя</param>
        /// <returns>Пользователь</returns>
        Task<User> GetByUsernameAsync(string username);

        /// <summary>
        /// Получить пользователя по email
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>Пользователь</returns>
        Task<User> GetByEmailAsync(string email);

        /// <summary>
        /// Получить пользователя с ролями
        /// </summary>
        /// <param name="id">Идентификатор пользователя</param>
        /// <returns>Пользователь с ролями</returns>
        Task<User> GetWithRolesAsync(Guid id);

        /// <summary>
        /// Проверить, существует ли пользователь с указанным именем пользователя
        /// </summary>
        /// <param name="username">Имя пользователя</param>
        /// <returns>True, если пользователь существует</returns>
        Task<bool> ExistsByUsernameAsync(string username);

        /// <summary>
        /// Проверить, существует ли пользователь с указанным email
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>True, если пользователь существует</returns>
        Task<bool> ExistsByEmailAsync(string email);
    }
}
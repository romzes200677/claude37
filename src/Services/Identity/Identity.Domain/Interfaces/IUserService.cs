using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Interfaces;
using Common.Models;
using Identity.Domain.Entities;
using Identity.Domain.Models;

namespace Identity.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса управления пользователями
    /// </summary>
    public interface IUserService : IService<User>
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
        /// Получить список пользователей с пагинацией
        /// </summary>
        /// <param name="parameters">Параметры пагинации</param>
        /// <returns>Список пользователей</returns>
        Task<PaginatedList<User>> GetPaginatedAsync(PaginationParameters parameters);

        /// <summary>
        /// Добавить пользователя в роль
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="roleName">Название роли</param>
        /// <returns>Результат операции</returns>
        Task<bool> AddToRoleAsync(Guid userId, string roleName);

        /// <summary>
        /// Удалить пользователя из роли
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="roleName">Название роли</param>
        /// <returns>Результат операции</returns>
        Task<bool> RemoveFromRoleAsync(Guid userId, string roleName);

        /// <summary>
        /// Изменить пароль пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="currentPassword">Текущий пароль</param>
        /// <param name="newPassword">Новый пароль</param>
        /// <returns>Результат операции</returns>
        Task<bool> ChangePasswordAsync(Guid userId, string currentPassword, string newPassword);

        /// <summary>
        /// Подтвердить email пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="token">Токен подтверждения</param>
        /// <returns>Результат операции</returns>
        Task<bool> ConfirmEmailAsync(Guid userId, string token);

        /// <summary>
        /// Сбросить пароль пользователя
        /// </summary>
        /// <param name="email">Email</param>
        /// <returns>Токен сброса пароля</returns>
        Task<string> ResetPasswordAsync(string email);

        /// <summary>
        /// Подтвердить сброс пароля
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="token">Токен сброса пароля</param>
        /// <param name="newPassword">Новый пароль</param>
        /// <returns>Результат операции</returns>
        Task<bool> ConfirmResetPasswordAsync(Guid userId, string token, string newPassword);
    }
}
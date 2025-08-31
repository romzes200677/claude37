using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Models;

namespace Common.Interfaces
{
    /// <summary>
    /// Базовый интерфейс сервиса для работы с сущностями
    /// </summary>
    /// <typeparam name="T">Тип сущности</typeparam>
    public interface IService<T> where T : BaseEntity
    {
        /// <summary>
        /// Получить все сущности
        /// </summary>
        /// <returns>Коллекция сущностей</returns>
        Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Получить сущность по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        /// <returns>Сущность</returns>
        Task<T> GetByIdAsync(Guid id);

        /// <summary>
        /// Создать сущность
        /// </summary>
        /// <param name="entity">Сущность</param>
        /// <returns>Созданная сущность</returns>
        Task<T> CreateAsync(T entity);

        /// <summary>
        /// Обновить сущность
        /// </summary>
        /// <param name="entity">Сущность</param>
        /// <returns>Обновленная сущность</returns>
        Task<T> UpdateAsync(T entity);

        /// <summary>
        /// Удалить сущность
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        /// <returns>Задача</returns>
        Task DeleteAsync(Guid id);
    }
}
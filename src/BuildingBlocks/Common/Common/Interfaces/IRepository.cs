using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Common.Models;

namespace Common.Interfaces
{
    /// <summary>
    /// Базовый интерфейс репозитория для работы с сущностями
    /// </summary>
    /// <typeparam name="T">Тип сущности</typeparam>
    public interface IRepository<T> where T : BaseEntity
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
        /// Найти сущности по условию
        /// </summary>
        /// <param name="predicate">Условие поиска</param>
        /// <returns>Коллекция сущностей</returns>
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Добавить сущность
        /// </summary>
        /// <param name="entity">Сущность</param>
        /// <returns>Добавленная сущность</returns>
        Task<T> AddAsync(T entity);

        /// <summary>
        /// Обновить сущность
        /// </summary>
        /// <param name="entity">Сущность</param>
        /// <returns>Обновленная сущность</returns>
        Task<T> UpdateAsync(T entity);

        /// <summary>
        /// Удалить сущность
        /// </summary>
        /// <param name="entity">Сущность</param>
        /// <returns>Задача</returns>
        Task DeleteAsync(T entity);

        /// <summary>
        /// Удалить сущность по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор сущности</param>
        /// <returns>Задача</returns>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Сохранить изменения
        /// </summary>
        /// <returns>Задача</returns>
        Task SaveChangesAsync();
    }
}
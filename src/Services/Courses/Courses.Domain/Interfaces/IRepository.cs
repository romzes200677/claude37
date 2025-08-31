using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Courses.Domain.Entities;

namespace Courses.Domain.Interfaces
{
    /// <summary>
    /// Базовый интерфейс репозитория
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
        /// Получить сущности по условию
        /// </summary>
        /// <param name="predicate">Условие</param>
        /// <returns>Коллекция сущностей</returns>
        Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Получить сущность по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Сущность</returns>
        Task<T> GetByIdAsync(Guid id);

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
        /// <param name="id">Идентификатор</param>
        /// <returns>Задача</returns>
        Task DeleteAsync(Guid id);

        /// <summary>
        /// Проверить существование сущности по условию
        /// </summary>
        /// <param name="predicate">Условие</param>
        /// <returns>Результат проверки</returns>
        Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate);
    }
}
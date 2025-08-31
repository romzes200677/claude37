using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Common.Entities;
using Common.Interfaces;

namespace Common.Repositories
{
    /// <summary>
    /// Базовый репозиторий
    /// </summary>
    /// <typeparam name="T">Тип сущности</typeparam>
    public abstract class BaseRepository<T> : IRepository<T> where T : BaseEntity
    {
        /// <summary>
        /// Получить все сущности
        /// </summary>
        /// <returns>Список сущностей</returns>
        public abstract Task<IEnumerable<T>> GetAllAsync();

        /// <summary>
        /// Получить сущность по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Сущность</returns>
        public abstract Task<T> GetByIdAsync(Guid id);

        /// <summary>
        /// Добавить сущность
        /// </summary>
        /// <param name="entity">Сущность</param>
        /// <returns>Добавленная сущность</returns>
        public abstract Task<T> AddAsync(T entity);

        /// <summary>
        /// Обновить сущность
        /// </summary>
        /// <param name="entity">Сущность</param>
        /// <returns>Обновленная сущность</returns>
        public abstract Task<T> UpdateAsync(T entity);

        /// <summary>
        /// Удалить сущность
        /// </summary>
        /// <param name="entity">Сущность</param>
        /// <returns>Задача</returns>
        public abstract Task DeleteAsync(T entity);

        /// <summary>
        /// Удалить сущность по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Задача</returns>
        public abstract Task DeleteAsync(Guid id);

        /// <summary>
        /// Найти сущности по предикату
        /// </summary>
        /// <param name="predicate">Предикат</param>
        /// <returns>Список сущностей</returns>
        public abstract Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate);

        /// <summary>
        /// Сохранить изменения
        /// </summary>
        /// <returns>Задача</returns>
        public abstract Task SaveChangesAsync();
    }
}
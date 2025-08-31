using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Common.Entities;
using Common.Interfaces;

namespace Common.Services
{
    /// <summary>
    /// Базовый сервис
    /// </summary>
    /// <typeparam name="T">Тип сущности</typeparam>
    public abstract class BaseService<T> : IService<T> where T : BaseEntity
    {
        /// <summary>
        /// Репозиторий
        /// </summary>
        protected readonly IRepository<T> Repository;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="repository">Репозиторий</param>
        protected BaseService(IRepository<T> repository)
        {
            Repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        /// <summary>
        /// Получить все сущности
        /// </summary>
        /// <returns>Список сущностей</returns>
        public virtual async Task<IEnumerable<T>> GetAllAsync()
        {
            return await Repository.GetAllAsync();
        }

        /// <summary>
        /// Получить сущность по идентификатору
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Сущность</returns>
        public virtual async Task<T> GetByIdAsync(Guid id)
        {
            return await Repository.GetByIdAsync(id);
        }

        /// <summary>
        /// Создать сущность
        /// </summary>
        /// <param name="entity">Сущность</param>
        /// <returns>Созданная сущность</returns>
        public virtual async Task<T> CreateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;

            var result = await Repository.AddAsync(entity);
            await Repository.SaveChangesAsync();

            return result;
        }

        /// <summary>
        /// Обновить сущность
        /// </summary>
        /// <param name="entity">Сущность</param>
        /// <returns>Обновленная сущность</returns>
        public virtual async Task<T> UpdateAsync(T entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException(nameof(entity));
            }

            var existingEntity = await Repository.GetByIdAsync(entity.Id);
            if (existingEntity == null)
            {
                throw new KeyNotFoundException($"Entity with id {entity.Id} not found");
            }

            entity.CreatedAt = existingEntity.CreatedAt;
            entity.UpdatedAt = DateTime.UtcNow;

            var result = await Repository.UpdateAsync(entity);
            await Repository.SaveChangesAsync();

            return result;
        }

        /// <summary>
        /// Удалить сущность
        /// </summary>
        /// <param name="id">Идентификатор</param>
        /// <returns>Задача</returns>
        public virtual async Task DeleteAsync(Guid id)
        {
            var entity = await Repository.GetByIdAsync(id);
            if (entity == null)
            {
                throw new KeyNotFoundException($"Entity with id {id} not found");
            }

            await Repository.DeleteAsync(entity);
            await Repository.SaveChangesAsync();
        }
    }
}
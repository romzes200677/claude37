using Courses.Domain.Entities;
using Courses.Domain.Interfaces;
using Courses.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Courses.Infrastructure.Repositories
{
    /// <summary>
    /// Базовая реализация репозитория для работы с сущностями
    /// </summary>
    /// <typeparam name="T">Тип сущности</typeparam>
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly CoursesDbContext _context;
        protected readonly DbSet<T> _dbSet;

        public Repository(CoursesDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        /// <summary>
        /// Получить все сущности
        /// </summary>
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _dbSet.ToListAsync();
        }

        /// <summary>
        /// Получить сущности по условию
        /// </summary>
        public async Task<IEnumerable<T>> FindAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.Where(predicate).ToListAsync();
        }

        /// <summary>
        /// Получить сущность по идентификатору
        /// </summary>
        public async Task<T?> GetByIdAsync(Guid id)
        {
            return await _dbSet.FindAsync(id);
        }

        /// <summary>
        /// Добавить сущность
        /// </summary>
        public async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Обновить сущность
        /// </summary>
        public async Task<T> UpdateAsync(T entity)
        {
            entity.UpdatedAt = DateTime.UtcNow;
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entity;
        }

        /// <summary>
        /// Удалить сущность
        /// </summary>
        public async Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            await _context.SaveChangesAsync();
        }

        /// <summary>
        /// Удалить сущность по идентификатору
        /// </summary>
        public async Task DeleteAsync(Guid id)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                await DeleteAsync(entity);
            }
        }

        /// <summary>
        /// Проверить существование сущности по условию
        /// </summary>
        public async Task<bool> ExistsAsync(Expression<Func<T, bool>> predicate)
        {
            return await _dbSet.AnyAsync(predicate);
        }
    }
}
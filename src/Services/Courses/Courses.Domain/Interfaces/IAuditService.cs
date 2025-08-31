using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Courses.Domain.Models;

namespace Courses.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса аудита
    /// </summary>
    public interface IAuditService
    {
        /// <summary>
        /// Записать действие пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="action">Действие</param>
        /// <param name="entityType">Тип сущности</param>
        /// <param name="entityId">Идентификатор сущности</param>
        /// <param name="details">Детали</param>
        /// <returns>Задача</returns>
        Task LogActionAsync(Guid userId, string action, string entityType, Guid entityId, string details = null);

        /// <summary>
        /// Получить историю действий пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="startDate">Дата начала</param>
        /// <param name="endDate">Дата окончания</param>
        /// <param name="page">Номер страницы</param>
        /// <param name="pageSize">Размер страницы</param>
        /// <returns>Список действий пользователя</returns>
        Task<PaginationResponseModel<AuditLogModel>> GetUserActionsAsync(Guid userId, DateTime? startDate = null, DateTime? endDate = null, int page = 1, int pageSize = 10);

        /// <summary>
        /// Получить историю действий для сущности
        /// </summary>
        /// <param name="entityType">Тип сущности</param>
        /// <param name="entityId">Идентификатор сущности</param>
        /// <param name="startDate">Дата начала</param>
        /// <param name="endDate">Дата окончания</param>
        /// <param name="page">Номер страницы</param>
        /// <param name="pageSize">Размер страницы</param>
        /// <returns>Список действий для сущности</returns>
        Task<PaginationResponseModel<AuditLogModel>> GetEntityActionsAsync(string entityType, Guid entityId, DateTime? startDate = null, DateTime? endDate = null, int page = 1, int pageSize = 10);

        /// <summary>
        /// Получить историю действий
        /// </summary>
        /// <param name="filter">Фильтр</param>
        /// <returns>Список действий</returns>
        Task<PaginationResponseModel<AuditLogModel>> GetActionsAsync(AuditLogFilterModel filter);
    }
}
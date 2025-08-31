using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Courses.Domain.Interfaces;
using Courses.Domain.Models;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Courses.Infrastructure.Services
{
    /// <summary>
    /// Сервис аудита
    /// </summary>
    public class AuditService : IAuditService
    {
        private readonly ILogService _logService;
        private readonly IIntegrationService _integrationService;
        private readonly ILogger<AuditService> _logger;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="logService">Сервис логирования</param>
        /// <param name="integrationService">Интеграционный сервис</param>
        /// <param name="logger">Логгер</param>
        public AuditService(
            ILogService logService,
            IIntegrationService integrationService,
            ILogger<AuditService> logger)
        {
            _logService = logService ?? throw new ArgumentNullException(nameof(logService));
            _integrationService = integrationService ?? throw new ArgumentNullException(nameof(integrationService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Записать действие пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="action">Действие</param>
        /// <param name="entityType">Тип сущности</param>
        /// <param name="entityId">Идентификатор сущности</param>
        /// <param name="details">Детали</param>
        /// <returns>Задача</returns>
        public async Task LogActionAsync(Guid userId, string action, string entityType, Guid entityId, string details = null)
        {
            try
            {
                var auditLog = new AuditLogModel
                {
                    Id = Guid.NewGuid(),
                    UserId = userId,
                    Action = action,
                    EntityType = entityType,
                    EntityId = entityId,
                    Details = details,
                    Timestamp = DateTime.UtcNow,
                    IpAddress = GetIpAddress() // В реальном приложении получаем из контекста HTTP
                };

                // Логируем действие
                _logService.Information($"Аудит: {action} для {entityType} (ID: {entityId}) пользователем {userId}");

                // В реальном приложении здесь будет вызов метода интеграционного сервиса для сохранения лога аудита
                // Например, отправка события в шину сообщений или вызов API микросервиса аудита
                // await _integrationService.SendAuditLogAsync(auditLog);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при записи действия аудита: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Получить историю действий пользователя
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <param name="startDate">Дата начала</param>
        /// <param name="endDate">Дата окончания</param>
        /// <param name="page">Номер страницы</param>
        /// <param name="pageSize">Размер страницы</param>
        /// <returns>Список действий пользователя</returns>
        public async Task<PaginationResponseModel<AuditLogModel>> GetUserActionsAsync(
            Guid userId, DateTime? startDate = null, DateTime? endDate = null, int page = 1, int pageSize = 10)
        {
            var filter = new AuditLogFilterModel
            {
                UserId = userId,
                StartDate = startDate,
                EndDate = endDate,
                PageNumber = page,
                PageSize = pageSize
            };

            return await GetActionsAsync(filter);
        }

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
        public async Task<PaginationResponseModel<AuditLogModel>> GetEntityActionsAsync(
            string entityType, Guid entityId, DateTime? startDate = null, DateTime? endDate = null, int page = 1, int pageSize = 10)
        {
            var filter = new AuditLogFilterModel
            {
                EntityType = entityType,
                EntityId = entityId,
                StartDate = startDate,
                EndDate = endDate,
                PageNumber = page,
                PageSize = pageSize
            };

            return await GetActionsAsync(filter);
        }

        /// <summary>
        /// Получить историю действий
        /// </summary>
        /// <param name="filter">Фильтр</param>
        /// <returns>Список действий</returns>
        public async Task<PaginationResponseModel<AuditLogModel>> GetActionsAsync(AuditLogFilterModel filter)
        {
            try
            {
                // В реальном приложении здесь будет вызов метода интеграционного сервиса для получения логов аудита
                // Например, запрос к API микросервиса аудита
                // return await _integrationService.GetAuditLogsAsync(filter);

                // Временная заглушка для демонстрации
                var mockLogs = new List<AuditLogModel>();
                var totalCount = 0;

                if (filter.UserId.HasValue || filter.EntityId.HasValue || !string.IsNullOrEmpty(filter.EntityType))
                {
                    // Имитируем наличие данных при фильтрации
                    totalCount = 5;
                    for (int i = 0; i < Math.Min(filter.PageSize, totalCount); i++)
                    {
                        mockLogs.Add(new AuditLogModel
                        {
                            Id = Guid.NewGuid(),
                            UserId = filter.UserId ?? Guid.NewGuid(),
                            UserName = "Пользователь",
                            Action = "Действие " + i,
                            EntityType = filter.EntityType ?? "Тип сущности",
                            EntityId = filter.EntityId ?? Guid.NewGuid(),
                            Details = "Детали действия",
                            IpAddress = "127.0.0.1",
                            Timestamp = DateTime.UtcNow.AddDays(-i)
                        });
                    }
                }

                var totalPages = (int)Math.Ceiling(totalCount / (double)filter.PageSize);

                return new PaginationResponseModel<AuditLogModel>
                {
                    Items = mockLogs,
                    PageNumber = filter.PageNumber,
                    PageSize = filter.PageSize,
                    TotalCount = totalCount,
                    TotalPages = totalPages,
                    HasPreviousPage = filter.PageNumber > 1,
                    HasNextPage = filter.PageNumber < totalPages
                };
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Ошибка при получении логов аудита: {ex.Message}");
                throw;
            }
        }

        /// <summary>
        /// Получить IP-адрес
        /// </summary>
        /// <returns>IP-адрес</returns>
        private string GetIpAddress()
        {
            // В реальном приложении получаем из контекста HTTP
            return "127.0.0.1";
        }
    }
}
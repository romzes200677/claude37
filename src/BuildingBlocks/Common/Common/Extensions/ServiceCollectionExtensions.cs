using System;
using Common.Models;
using Microsoft.Extensions.DependencyInjection;

namespace Common.Extensions
{
    /// <summary>
    /// Расширения для регистрации сервисов Common
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Добавить общие сервисы
        /// </summary>
        /// <param name="services">Коллекция сервисов</param>
        /// <returns>Коллекция сервисов</returns>
        public static IServiceCollection AddCommonServices(this IServiceCollection services)
        {
            services.AddScoped<ApiResponse>();
            
            return services;
        }
    }
}
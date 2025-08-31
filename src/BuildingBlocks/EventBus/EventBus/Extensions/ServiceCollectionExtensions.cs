using System;
using EventBus.Interfaces;
using EventBus.Services;
using EventBus.Settings;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace EventBus.Extensions
{
    /// <summary>
    /// Расширения для регистрации сервисов EventBus
    /// </summary>
    public static class ServiceCollectionExtensions
    {
        /// <summary>
        /// Добавить Kafka EventBus с настройками из конфигурации
        /// </summary>
        /// <param name="services">Коллекция сервисов</param>
        /// <param name="configuration">Конфигурация</param>
        /// <returns>Коллекция сервисов</returns>
        public static IServiceCollection AddKafkaEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<EventBusSettings>(configuration.GetSection(nameof(EventBusSettings)));

            services.AddSingleton<IEventBus>(sp =>
            {
                var serviceScopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
                var logger = sp.GetRequiredService<ILogger<KafkaEventBus>>();
                var settings = sp.GetRequiredService<IOptions<EventBusSettings>>().Value;
                var bootstrapServers = settings.GetConnectionString();
                
                return new KafkaEventBus(serviceScopeFactory, logger, bootstrapServers);
            });

            services.AddScoped<IntegrationEventPublisher>();
            services.AddSingleton<IntegrationEventProcessor>();

            return services;
        }

        /// <summary>
        /// Добавить Kafka EventBus с указанными настройками
        /// </summary>
        /// <param name="services">Коллекция сервисов</param>
        /// <param name="bootstrapServers">Адрес серверов Kafka</param>
        /// <returns>Коллекция сервисов</returns>
        public static IServiceCollection AddKafkaEventBus(this IServiceCollection services, string bootstrapServers)
        {
            services.AddSingleton<IEventBus>(sp =>
            {
                var serviceScopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
                var logger = sp.GetRequiredService<ILogger<KafkaEventBus>>();
                return new KafkaEventBus(serviceScopeFactory, logger, bootstrapServers);
            });

            services.AddScoped<IntegrationEventPublisher>();
            services.AddSingleton<IntegrationEventProcessor>();

            return services;
        }
    }
}
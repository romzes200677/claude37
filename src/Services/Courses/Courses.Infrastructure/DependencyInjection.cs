using Courses.Domain.Interfaces;
using Courses.Infrastructure.Data;
using Courses.Infrastructure.Repositories;
using Courses.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Courses.Infrastructure
{
    /// <summary>
    /// Класс для регистрации зависимостей инфраструктурного слоя
    /// </summary>
    public static class DependencyInjection
    {
        /// <summary>
        /// Добавить зависимости инфраструктурного слоя
        /// </summary>
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Регистрация контекста базы данных
            services.AddDbContext<CoursesDbContext>(options =>
                options.UseNpgsql(
                    configuration.GetConnectionString("CoursesConnection"),
                    b => b.MigrationsAssembly(typeof(CoursesDbContext).Assembly.FullName)));

            // Регистрация репозиториев
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ICourseRepository, CourseRepository>();
            services.AddScoped<IModuleRepository, ModuleRepository>();
            services.AddScoped<ILessonRepository, LessonRepository>();
            services.AddScoped<IEnrollmentRepository, EnrollmentRepository>();
            services.AddScoped<ILessonProgressRepository, LessonProgressRepository>();
            services.AddScoped<IReviewRepository, ReviewRepository>();
            services.AddScoped<ISurveyRepository, SurveyRepository>();
            services.AddScoped<ISurveyResponseRepository, SurveyResponseRepository>();
            services.AddScoped<IFeedbackRepository, FeedbackRepository>();
            services.AddScoped<IFeedbackCommentRepository, FeedbackCommentRepository>();
            services.AddScoped<IRecommendationRepository, RecommendationRepository>();

            // Регистрация сервисов
            services.AddScoped<ILogService, LogService>();
            services.AddScoped<IAuditService, AuditService>();
            services.AddScoped<IConfigurationService, ConfigurationService>();
            services.AddScoped<IIntegrationService, IntegrationService>();
            services.AddScoped<ICourseService, CourseService>();
            services.AddScoped<IModuleService, ModuleService>();
            services.AddScoped<ILessonService, LessonService>();
            services.AddScoped<IEnrollmentService, EnrollmentService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<IAnalyticsService, AnalyticsService>();
            services.AddScoped<ISearchService, SearchService>();
            services.AddScoped<IReportService, ReportService>();
            services.AddScoped<IReviewService, ReviewService>();
            services.AddScoped<ISurveyService, SurveyService>();
            services.AddScoped<IFeedbackService, FeedbackService>();
            services.AddScoped<ILessonProgressService, LessonProgressService>();
            services.AddScoped<IRecommendationService, RecommendationService>();

            return services;
        }
    }
}
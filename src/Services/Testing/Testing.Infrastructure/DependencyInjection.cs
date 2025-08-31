using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Testing.Domain.Repositories;
using Testing.Infrastructure.Data;
using Testing.Infrastructure.Repositories;

namespace Testing.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            // Register DbContext
            services.AddDbContext<TestingDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("TestingDatabase"),
                    b => b.MigrationsAssembly(typeof(TestingDbContext).Assembly.FullName)));

            // Register repositories
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<ITestTemplateRepository, TestTemplateRepository>();
            services.AddScoped<ITestQuestionRepository, TestQuestionRepository>();
            services.AddScoped<ITestQuestionOptionRepository, TestQuestionOptionRepository>();
            services.AddScoped<ITestAttemptRepository, TestAttemptRepository>();
            services.AddScoped<ITestQuestionResponseRepository, TestQuestionResponseRepository>();
            services.AddScoped<ITestQuestionOptionResponseRepository, TestQuestionOptionResponseRepository>();
            services.AddScoped<ITestCategoryRepository, TestCategoryRepository>();
            services.AddScoped<ITestTagRepository, TestTagRepository>();

            // Register database initializer
            services.AddScoped<TestingDbInitializer>();

            return services;
        }
    }
}
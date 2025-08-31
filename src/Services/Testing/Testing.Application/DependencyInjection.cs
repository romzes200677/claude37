using Microsoft.Extensions.DependencyInjection;
using Testing.Application.Services;
using Testing.Application.Services.Implementations;

namespace Testing.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Register services
            services.AddScoped<ITestTemplateService, TestTemplateService>();
            services.AddScoped<ITestQuestionService, TestQuestionService>();
            services.AddScoped<ITestQuestionOptionService, TestQuestionOptionService>();
            services.AddScoped<ITestAttemptService, TestAttemptService>();
            services.AddScoped<ITestQuestionResponseService, TestQuestionResponseService>();
            services.AddScoped<ITestQuestionOptionResponseService, TestQuestionOptionResponseService>();
            services.AddScoped<ITestCategoryService, TestCategoryService>();
            services.AddScoped<ITestTagService, TestTagService>();
            services.AddScoped<ITestEvaluationService, TestEvaluationService>();

            return services;
        }
    }
}
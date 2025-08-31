using Common.Middleware;
using Microsoft.AspNetCore.Builder;

namespace Common.Extensions
{
    /// <summary>
    /// Расширения для ApplicationBuilder
    /// </summary>
    public static class ApplicationBuilderExtensions
    {
        /// <summary>
        /// Использовать обработку исключений
        /// </summary>
        /// <param name="app">ApplicationBuilder</param>
        /// <returns>ApplicationBuilder</returns>
        public static IApplicationBuilder UseApiExceptionHandling(this IApplicationBuilder app)
        {
            return app.UseMiddleware<ExceptionHandlingMiddleware>();
        }
    }
}
using System;
using System.Net;
using System.Threading.Tasks;
using Common.Exceptions;
using Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Common.Middleware
{
    /// <summary>
    /// Middleware для обработки исключений
    /// </summary>
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="next">Следующий делегат</param>
        /// <param name="logger">Логгер</param>
        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Вызов middleware
        /// </summary>
        /// <param name="context">Контекст HTTP</param>
        /// <returns>Задача</returns>
        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception has occurred: {Message}", ex.Message);
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";

            var statusCode = HttpStatusCode.InternalServerError;
            var message = "Произошла внутренняя ошибка сервера.";

            if (exception is ApiException apiException)
            {
                statusCode = apiException.StatusCode;
                message = apiException.Message;
            }

            context.Response.StatusCode = (int)statusCode;

            var response = ApiResponse.Fail(message);
            var serializedResponse = JsonConvert.SerializeObject(response);

            await context.Response.WriteAsync(serializedResponse);
        }
    }
}
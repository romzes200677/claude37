using Common.Models;
using Microsoft.AspNetCore.Mvc;

namespace Common.Controllers
{
    /// <summary>
    /// Базовый контроллер API
    /// </summary>
    [ApiController]
    [Route("api/[controller]")] 
    public abstract class BaseApiController : ControllerBase
    {
        /// <summary>
        /// Создать успешный ответ
        /// </summary>
        /// <param name="data">Данные</param>
        /// <param name="message">Сообщение</param>
        /// <returns>Ответ</returns>
        protected IActionResult Success(object data = null, string message = "Операция выполнена успешно")
        {
            return Ok(ApiResponse.Success(data, message));
        }

        /// <summary>
        /// Создать ответ с ошибкой
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="statusCode">Код статуса HTTP</param>
        /// <returns>Ответ</returns>
        protected IActionResult Error(string message, int statusCode = 400)
        {
            return StatusCode(statusCode, ApiResponse.Fail(message));
        }

        /// <summary>
        /// Создать ответ с ошибкой валидации
        /// </summary>
        /// <param name="errors">Ошибки валидации</param>
        /// <returns>Ответ</returns>
        protected IActionResult ValidationError(object errors)
        {
            return BadRequest(ApiResponse.Fail("Ошибка валидации", errors));
        }

        /// <summary>
        /// Создать ответ "Не найдено"
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <returns>Ответ</returns>
        protected IActionResult NotFound(string message = "Ресурс не найден")
        {
            return StatusCode(404, ApiResponse.Fail(message));
        }

        /// <summary>
        /// Создать ответ "Запрещено"
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <returns>Ответ</returns>
        protected IActionResult Forbidden(string message = "Доступ запрещен")
        {
            return StatusCode(403, ApiResponse.Fail(message));
        }

        /// <summary>
        /// Создать ответ "Не авторизован"
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <returns>Ответ</returns>
        protected IActionResult Unauthorized(string message = "Не авторизован")
        {
            return StatusCode(401, ApiResponse.Fail(message));
        }
    }
}
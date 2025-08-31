using System.Collections.Generic;

namespace Courses.Domain.Models
{
    /// <summary>
    /// Базовая модель ответа API
    /// </summary>
    public class ApiResponse
    {
        /// <summary>
        /// Успешность операции
        /// </summary>
        public bool Success { get; set; }

        /// <summary>
        /// Сообщение
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Список ошибок
        /// </summary>
        public List<string> Errors { get; set; }

        /// <summary>
        /// Создать успешный ответ
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <returns>Ответ API</returns>
        public static ApiResponse SuccessResponse(string message = "Операция выполнена успешно")
        {
            return new ApiResponse
            {
                Success = true,
                Message = message,
                Errors = null
            };
        }

        /// <summary>
        /// Создать ответ с ошибкой
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="errors">Список ошибок</param>
        /// <returns>Ответ API</returns>
        public static ApiResponse ErrorResponse(string message = "Произошла ошибка", List<string> errors = null)
        {
            return new ApiResponse
            {
                Success = false,
                Message = message,
                Errors = errors ?? new List<string>()
            };
        }
    }

    /// <summary>
    /// Модель ответа API с данными
    /// </summary>
    /// <typeparam name="T">Тип данных</typeparam>
    public class ApiResponse<T> : ApiResponse
    {
        /// <summary>
        /// Данные
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Создать успешный ответ с данными
        /// </summary>
        /// <param name="data">Данные</param>
        /// <param name="message">Сообщение</param>
        /// <returns>Ответ API с данными</returns>
        public static ApiResponse<T> SuccessResponse(T data, string message = "Операция выполнена успешно")
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data,
                Errors = null
            };
        }

        /// <summary>
        /// Создать ответ с ошибкой
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="errors">Список ошибок</param>
        /// <returns>Ответ API с данными</returns>
        public new static ApiResponse<T> ErrorResponse(string message = "Произошла ошибка", List<string> errors = null)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Data = default,
                Errors = errors ?? new List<string>()
            };
        }
    }
}
using System;
using System.Collections.Generic;

namespace Common.Models
{
    /// <summary>
    /// Класс для стандартизации ответов API
    /// </summary>
    /// <typeparam name="T">Тип данных</typeparam>
    public class ApiResponse<T>
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
        /// Данные
        /// </summary>
        public T Data { get; set; }

        /// <summary>
        /// Ошибки
        /// </summary>
        public List<string> Errors { get; set; }

        /// <summary>
        /// Создать успешный ответ
        /// </summary>
        /// <param name="data">Данные</param>
        /// <param name="message">Сообщение</param>
        /// <returns>Ответ API</returns>
        public static ApiResponse<T> Ok(T data, string message = "Operation completed successfully")
        {
            return new ApiResponse<T>
            {
                Success = true,
                Message = message,
                Data = data,
                Errors = new List<string>()
            };
        }

        /// <summary>
        /// Создать ответ с ошибкой
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="errors">Список ошибок</param>
        /// <returns>Ответ API</returns>
        public static ApiResponse<T> Fail(string message, List<string> errors = null)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Data = default,
                Errors = errors ?? new List<string>()
            };
        }

        /// <summary>
        /// Создать ответ с ошибкой
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="error">Ошибка</param>
        /// <returns>Ответ API</returns>
        public static ApiResponse<T> Fail(string message, string error)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = message,
                Data = default,
                Errors = new List<string> { error }
            };
        }

        /// <summary>
        /// Создать ответ с ошибкой
        /// </summary>
        /// <param name="exception">Исключение</param>
        /// <returns>Ответ API</returns>
        public static ApiResponse<T> Fail(Exception exception)
        {
            return new ApiResponse<T>
            {
                Success = false,
                Message = exception.Message,
                Data = default,
                Errors = new List<string> { exception.ToString() }
            };
        }
    }
}
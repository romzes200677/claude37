using System;
using System.Net;

namespace Common.Exceptions
{
    /// <summary>
    /// Базовый класс исключения API
    /// </summary>
    public class ApiException : Exception
    {
        /// <summary>
        /// Код HTTP статуса
        /// </summary>
        public HttpStatusCode StatusCode { get; }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="statusCode">Код HTTP статуса</param>
        public ApiException(string message, HttpStatusCode statusCode = HttpStatusCode.BadRequest) 
            : base(message)
        {
            StatusCode = statusCode;
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="message">Сообщение об ошибке</param>
        /// <param name="innerException">Внутреннее исключение</param>
        /// <param name="statusCode">Код HTTP статуса</param>
        public ApiException(string message, Exception innerException, HttpStatusCode statusCode = HttpStatusCode.BadRequest) 
            : base(message, innerException)
        {
            StatusCode = statusCode;
        }
    }
}
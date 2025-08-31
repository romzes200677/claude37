using System;
using System.ComponentModel.DataAnnotations;

namespace Identity.Domain.Models
{
    /// <summary>
    /// Модель подтверждения email
    /// </summary>
    public class EmailConfirmationModel
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        [Required(ErrorMessage = "Идентификатор пользователя обязателен")]
        public Guid UserId { get; set; }

        /// <summary>
        /// Токен подтверждения
        /// </summary>
        [Required(ErrorMessage = "Токен подтверждения обязателен")]
        public string Token { get; set; }
    }
}
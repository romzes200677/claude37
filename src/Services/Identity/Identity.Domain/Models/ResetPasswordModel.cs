using System;
using System.ComponentModel.DataAnnotations;

namespace Identity.Domain.Models
{
    /// <summary>
    /// Модель сброса пароля
    /// </summary>
    public class ResetPasswordModel
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        [Required(ErrorMessage = "Идентификатор пользователя обязателен")]
        public Guid UserId { get; set; }

        /// <summary>
        /// Токен сброса пароля
        /// </summary>
        [Required(ErrorMessage = "Токен сброса пароля обязателен")]
        public string Token { get; set; }

        /// <summary>
        /// Новый пароль
        /// </summary>
        [Required(ErrorMessage = "Новый пароль обязателен")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Пароль должен содержать от 6 до 100 символов")]
        public string NewPassword { get; set; }

        /// <summary>
        /// Подтверждение нового пароля
        /// </summary>
        [Required(ErrorMessage = "Подтверждение пароля обязательно")]
        [Compare("NewPassword", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmNewPassword { get; set; }
    }
}
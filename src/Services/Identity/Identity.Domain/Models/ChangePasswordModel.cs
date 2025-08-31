using System.ComponentModel.DataAnnotations;

namespace Identity.Domain.Models
{
    /// <summary>
    /// Модель изменения пароля
    /// </summary>
    public class ChangePasswordModel
    {
        /// <summary>
        /// Текущий пароль
        /// </summary>
        [Required(ErrorMessage = "Текущий пароль обязателен")]
        public string CurrentPassword { get; set; }

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
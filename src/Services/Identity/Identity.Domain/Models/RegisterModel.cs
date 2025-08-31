using System.ComponentModel.DataAnnotations;

namespace Identity.Domain.Models
{
    /// <summary>
    /// Модель регистрации пользователя
    /// </summary>
    public class RegisterModel
    {
        /// <summary>
        /// Имя пользователя
        /// </summary>
        [Required(ErrorMessage = "Имя пользователя обязательно")]
        [StringLength(50, MinimumLength = 3, ErrorMessage = "Имя пользователя должно содержать от 3 до 50 символов")]
        public string Username { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        [Required(ErrorMessage = "Email обязателен")]
        [EmailAddress(ErrorMessage = "Некорректный формат email")]
        public string Email { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [Required(ErrorMessage = "Пароль обязателен")]
        [StringLength(100, MinimumLength = 6, ErrorMessage = "Пароль должен содержать от 6 до 100 символов")]
        public string Password { get; set; }

        /// <summary>
        /// Подтверждение пароля
        /// </summary>
        [Required(ErrorMessage = "Подтверждение пароля обязательно")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string ConfirmPassword { get; set; }

        /// <summary>
        /// Имя
        /// </summary>
        [StringLength(50, ErrorMessage = "Имя должно содержать не более 50 символов")]
        public string FirstName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>
        [StringLength(50, ErrorMessage = "Фамилия должна содержать не более 50 символов")]
        public string LastName { get; set; }
    }
}
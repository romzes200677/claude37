using System.ComponentModel.DataAnnotations;

namespace Identity.Domain.Models
{
    /// <summary>
    /// Модель входа в систему
    /// </summary>
    public class LoginModel
    {
        /// <summary>
        /// Имя пользователя или email
        /// </summary>
        [Required(ErrorMessage = "Имя пользователя или email обязательны")]
        public string UsernameOrEmail { get; set; }

        /// <summary>
        /// Пароль
        /// </summary>
        [Required(ErrorMessage = "Пароль обязателен")]
        public string Password { get; set; }

        /// <summary>
        /// Запомнить пользователя
        /// </summary>
        public bool RememberMe { get; set; }
    }
}
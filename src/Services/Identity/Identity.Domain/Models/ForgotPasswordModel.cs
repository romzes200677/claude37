using System.ComponentModel.DataAnnotations;

namespace Identity.Domain.Models
{
    /// <summary>
    /// Модель запроса сброса пароля
    /// </summary>
    public class ForgotPasswordModel
    {
        /// <summary>
        /// Email
        /// </summary>
        [Required(ErrorMessage = "Email обязателен")]
        [EmailAddress(ErrorMessage = "Некорректный формат email")]
        public string Email { get; set; }
    }
}
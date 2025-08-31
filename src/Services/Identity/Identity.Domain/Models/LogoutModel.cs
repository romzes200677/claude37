using System.ComponentModel.DataAnnotations;

namespace Identity.Domain.Models
{
    /// <summary>
    /// Модель выхода из системы
    /// </summary>
    public class LogoutModel
    {
        /// <summary>
        /// Токен обновления
        /// </summary>
        [Required(ErrorMessage = "Токен обновления обязателен")]
        public string RefreshToken { get; set; }
    }
}
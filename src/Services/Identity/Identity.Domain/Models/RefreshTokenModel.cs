using System.ComponentModel.DataAnnotations;

namespace Identity.Domain.Models
{
    /// <summary>
    /// Модель обновления токена
    /// </summary>
    public class RefreshTokenModel
    {
        /// <summary>
        /// Токен доступа
        /// </summary>
        [Required(ErrorMessage = "Токен доступа обязателен")]
        public string AccessToken { get; set; }

        /// <summary>
        /// Токен обновления
        /// </summary>
        [Required(ErrorMessage = "Токен обновления обязателен")]
        public string RefreshToken { get; set; }
    }
}
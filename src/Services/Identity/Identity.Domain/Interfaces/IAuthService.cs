using System.Threading.Tasks;
using Identity.Domain.Models;

namespace Identity.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса аутентификации
    /// </summary>
    public interface IAuthService
    {
        /// <summary>
        /// Зарегистрировать пользователя
        /// </summary>
        /// <param name="model">Модель регистрации</param>
        /// <returns>Результат регистрации</returns>
        Task<RegisterResult> RegisterAsync(RegisterModel model);

        /// <summary>
        /// Аутентифицировать пользователя
        /// </summary>
        /// <param name="model">Модель входа</param>
        /// <returns>Результат аутентификации</returns>
        Task<AuthResult> LoginAsync(LoginModel model);

        /// <summary>
        /// Обновить токен доступа
        /// </summary>
        /// <param name="model">Модель обновления токена</param>
        /// <returns>Результат обновления токена</returns>
        Task<AuthResult> RefreshTokenAsync(RefreshTokenModel model);

        /// <summary>
        /// Выйти из системы
        /// </summary>
        /// <param name="model">Модель выхода</param>
        /// <returns>Результат выхода</returns>
        Task<LogoutResult> LogoutAsync(LogoutModel model);
    }
}
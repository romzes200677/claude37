using System.Threading.Tasks;

namespace Courses.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса локализации
    /// </summary>
    public interface ILocalizationService
    {
        /// <summary>
        /// Получить локализованный текст
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <param name="culture">Культура (если null, используется текущая культура)</param>
        /// <returns>Локализованный текст</returns>
        string GetLocalizedString(string key, string culture = null);

        /// <summary>
        /// Получить локализованный текст асинхронно
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <param name="culture">Культура (если null, используется текущая культура)</param>
        /// <returns>Локализованный текст</returns>
        Task<string> GetLocalizedStringAsync(string key, string culture = null);

        /// <summary>
        /// Получить локализованный текст с параметрами
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <param name="culture">Культура (если null, используется текущая культура)</param>
        /// <param name="args">Аргументы</param>
        /// <returns>Локализованный текст</returns>
        string GetLocalizedString(string key, string culture, params object[] args);

        /// <summary>
        /// Получить локализованный текст с параметрами асинхронно
        /// </summary>
        /// <param name="key">Ключ</param>
        /// <param name="culture">Культура (если null, используется текущая культура)</param>
        /// <param name="args">Аргументы</param>
        /// <returns>Локализованный текст</returns>
        Task<string> GetLocalizedStringAsync(string key, string culture, params object[] args);

        /// <summary>
        /// Установить текущую культуру
        /// </summary>
        /// <param name="culture">Культура</param>
        void SetCurrentCulture(string culture);

        /// <summary>
        /// Получить текущую культуру
        /// </summary>
        /// <returns>Текущая культура</returns>
        string GetCurrentCulture();

        /// <summary>
        /// Получить список доступных культур
        /// </summary>
        /// <returns>Список доступных культур</returns>
        string[] GetAvailableCultures();
    }
}
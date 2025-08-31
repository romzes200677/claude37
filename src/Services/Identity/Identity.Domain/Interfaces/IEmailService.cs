using System.Threading.Tasks;

namespace Identity.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса отправки email
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Отправить email
        /// </summary>
        /// <param name="to">Адрес получателя</param>
        /// <param name="subject">Тема письма</param>
        /// <param name="body">Тело письма</param>
        /// <param name="isHtml">Флаг HTML-формата</param>
        /// <returns>Результат отправки</returns>
        Task<bool> SendEmailAsync(string to, string subject, string body, bool isHtml = true);

        /// <summary>
        /// Отправить email для подтверждения адреса
        /// </summary>
        /// <param name="to">Адрес получателя</param>
        /// <param name="callbackUrl">URL для подтверждения</param>
        /// <returns>Результат отправки</returns>
        Task<bool> SendConfirmationEmailAsync(string to, string callbackUrl);

        /// <summary>
        /// Отправить email для сброса пароля
        /// </summary>
        /// <param name="to">Адрес получателя</param>
        /// <param name="callbackUrl">URL для сброса пароля</param>
        /// <returns>Результат отправки</returns>
        Task<bool> SendPasswordResetEmailAsync(string to, string callbackUrl);
    }
}
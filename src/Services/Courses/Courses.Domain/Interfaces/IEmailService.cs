using System.Threading.Tasks;

namespace Courses.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса отправки электронной почты
    /// </summary>
    public interface IEmailService
    {
        /// <summary>
        /// Отправить электронное письмо
        /// </summary>
        /// <param name="to">Адрес получателя</param>
        /// <param name="subject">Тема</param>
        /// <param name="body">Тело письма</param>
        /// <param name="isHtml">Флаг HTML-формата</param>
        /// <returns>Задача</returns>
        Task SendEmailAsync(string to, string subject, string body, bool isHtml = true);

        /// <summary>
        /// Отправить электронное письмо с вложениями
        /// </summary>
        /// <param name="to">Адрес получателя</param>
        /// <param name="subject">Тема</param>
        /// <param name="body">Тело письма</param>
        /// <param name="attachments">Вложения (пути к файлам)</param>
        /// <param name="isHtml">Флаг HTML-формата</param>
        /// <returns>Задача</returns>
        Task SendEmailWithAttachmentsAsync(string to, string subject, string body, string[] attachments, bool isHtml = true);
    }
}
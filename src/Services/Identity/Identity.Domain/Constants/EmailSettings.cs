namespace Identity.Domain.Constants
{
    /// <summary>
    /// Константы настроек электронной почты
    /// </summary>
    public static class EmailSettings
    {
        /// <summary>
        /// Ключ для настройки SMTP-сервера
        /// </summary>
        public const string SmtpServer = "EmailSettings:SmtpServer";

        /// <summary>
        /// Ключ для настройки порта SMTP-сервера
        /// </summary>
        public const string SmtpPort = "EmailSettings:SmtpPort";

        /// <summary>
        /// Ключ для настройки имени пользователя SMTP-сервера
        /// </summary>
        public const string SmtpUsername = "EmailSettings:SmtpUsername";

        /// <summary>
        /// Ключ для настройки пароля SMTP-сервера
        /// </summary>
        public const string SmtpPassword = "EmailSettings:SmtpPassword";

        /// <summary>
        /// Ключ для настройки адреса отправителя
        /// </summary>
        public const string SenderEmail = "EmailSettings:SenderEmail";

        /// <summary>
        /// Ключ для настройки имени отправителя
        /// </summary>
        public const string SenderName = "EmailSettings:SenderName";

        /// <summary>
        /// Ключ для настройки использования SSL
        /// </summary>
        public const string UseSsl = "EmailSettings:UseSsl";
    }
}
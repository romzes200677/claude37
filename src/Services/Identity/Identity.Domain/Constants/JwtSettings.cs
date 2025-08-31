namespace Identity.Domain.Constants
{
    /// <summary>
    /// Константы настроек JWT
    /// </summary>
    public static class JwtSettings
    {
        /// <summary>
        /// Ключ для настройки секретного ключа
        /// </summary>
        public const string SecretKey = "JwtSettings:SecretKey";

        /// <summary>
        /// Ключ для настройки издателя
        /// </summary>
        public const string Issuer = "JwtSettings:Issuer";

        /// <summary>
        /// Ключ для настройки аудитории
        /// </summary>
        public const string Audience = "JwtSettings:Audience";

        /// <summary>
        /// Ключ для настройки времени жизни токена доступа (в минутах)
        /// </summary>
        public const string AccessTokenExpirationMinutes = "JwtSettings:AccessTokenExpirationMinutes";

        /// <summary>
        /// Ключ для настройки времени жизни токена обновления (в днях)
        /// </summary>
        public const string RefreshTokenExpirationDays = "JwtSettings:RefreshTokenExpirationDays";
    }
}
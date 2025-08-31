namespace EventBus.Settings
{
    /// <summary>
    /// Настройки для EventBus
    /// </summary>
    public class EventBusSettings
    {
        /// <summary>
        /// Имя хоста Kafka
        /// </summary>
        public string HostName { get; set; } = "localhost";

        /// <summary>
        /// Порт Kafka
        /// </summary>
        public int Port { get; set; } = 9092;

        /// <summary>
        /// Получить строку подключения к Kafka
        /// </summary>
        /// <returns>Строка подключения</returns>
        public string GetConnectionString()
        {
            return $"{HostName}:{Port}";
        }
    }
}
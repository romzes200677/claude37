namespace Courses.Domain.Constants
{
    /// <summary>
    /// Статусы записи на курс
    /// </summary>
    public static class EnrollmentStatuses
    {
        /// <summary>
        /// Активна
        /// </summary>
        public const string Active = "Active";

        /// <summary>
        /// Завершена
        /// </summary>
        public const string Completed = "Completed";

        /// <summary>
        /// Отменена
        /// </summary>
        public const string Cancelled = "Cancelled";

        /// <summary>
        /// Приостановлена
        /// </summary>
        public const string Suspended = "Suspended";

        /// <summary>
        /// Все статусы
        /// </summary>
        public static readonly string[] All = { Active, Completed, Cancelled, Suspended };
    }
}
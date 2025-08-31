namespace Courses.Domain.Constants
{
    /// <summary>
    /// Статусы прогресса по уроку
    /// </summary>
    public static class LessonProgressStatuses
    {
        /// <summary>
        /// Не начат
        /// </summary>
        public const string NotStarted = "NotStarted";

        /// <summary>
        /// В процессе
        /// </summary>
        public const string InProgress = "InProgress";

        /// <summary>
        /// Завершен
        /// </summary>
        public const string Completed = "Completed";

        /// <summary>
        /// Все статусы
        /// </summary>
        public static readonly string[] All = { NotStarted, InProgress, Completed };
    }
}
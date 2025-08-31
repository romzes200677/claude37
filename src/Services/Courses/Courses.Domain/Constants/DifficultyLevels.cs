namespace Courses.Domain.Constants
{
    /// <summary>
    /// Уровни сложности курса
    /// </summary>
    public static class DifficultyLevels
    {
        /// <summary>
        /// Начальный
        /// </summary>
        public const string Beginner = "Beginner";

        /// <summary>
        /// Средний
        /// </summary>
        public const string Intermediate = "Intermediate";

        /// <summary>
        /// Продвинутый
        /// </summary>
        public const string Advanced = "Advanced";

        /// <summary>
        /// Эксперт
        /// </summary>
        public const string Expert = "Expert";

        /// <summary>
        /// Все уровни
        /// </summary>
        public static readonly string[] All = { Beginner, Intermediate, Advanced, Expert };
    }
}
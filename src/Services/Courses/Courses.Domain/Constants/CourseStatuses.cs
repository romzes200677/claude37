namespace Courses.Domain.Constants
{
    /// <summary>
    /// Статусы курса
    /// </summary>
    public static class CourseStatuses
    {
        /// <summary>
        /// Черновик
        /// </summary>
        public const string Draft = "Draft";

        /// <summary>
        /// Опубликован
        /// </summary>
        public const string Published = "Published";

        /// <summary>
        /// Архивирован
        /// </summary>
        public const string Archived = "Archived";

        /// <summary>
        /// Все статусы
        /// </summary>
        public static readonly string[] All = { Draft, Published, Archived };
    }
}
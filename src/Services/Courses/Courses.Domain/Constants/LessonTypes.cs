namespace Courses.Domain.Constants
{
    /// <summary>
    /// Типы уроков
    /// </summary>
    public static class LessonTypes
    {
        /// <summary>
        /// Видео
        /// </summary>
        public const string Video = "Video";

        /// <summary>
        /// Текст
        /// </summary>
        public const string Text = "Text";

        /// <summary>
        /// Тест
        /// </summary>
        public const string Quiz = "Quiz";

        /// <summary>
        /// Практическое задание
        /// </summary>
        public const string Assignment = "Assignment";

        /// <summary>
        /// Интерактивный код
        /// </summary>
        public const string InteractiveCode = "InteractiveCode";

        /// <summary>
        /// Все типы
        /// </summary>
        public static readonly string[] All = { Video, Text, Quiz, Assignment, InteractiveCode };
    }
}
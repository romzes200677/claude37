namespace Identity.Domain.Constants
{
    /// <summary>
    /// Константы ролей пользователей
    /// </summary>
    public static class UserRoles
    {
        /// <summary>
        /// Администратор
        /// </summary>
        public const string Admin = "Admin";

        /// <summary>
        /// Преподаватель
        /// </summary>
        public const string Teacher = "Teacher";

        /// <summary>
        /// Студент
        /// </summary>
        public const string Student = "Student";

        /// <summary>
        /// Гость
        /// </summary>
        public const string Guest = "Guest";

        /// <summary>
        /// Все доступные роли
        /// </summary>
        public static readonly string[] All = { Admin, Teacher, Student, Guest };
    }
}
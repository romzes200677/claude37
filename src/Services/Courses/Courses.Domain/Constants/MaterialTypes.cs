namespace Courses.Domain.Constants
{
    /// <summary>
    /// Типы материалов урока
    /// </summary>
    public static class MaterialTypes
    {
        /// <summary>
        /// PDF документ
        /// </summary>
        public const string Pdf = "Pdf";

        /// <summary>
        /// Видео
        /// </summary>
        public const string Video = "Video";

        /// <summary>
        /// Ссылка
        /// </summary>
        public const string Link = "Link";

        /// <summary>
        /// Изображение
        /// </summary>
        public const string Image = "Image";

        /// <summary>
        /// Презентация
        /// </summary>
        public const string Presentation = "Presentation";

        /// <summary>
        /// Архив
        /// </summary>
        public const string Archive = "Archive";

        /// <summary>
        /// Код
        /// </summary>
        public const string Code = "Code";

        /// <summary>
        /// Все типы
        /// </summary>
        public static readonly string[] All = { Pdf, Video, Link, Image, Presentation, Archive, Code };
    }
}
using System;
using System.Threading.Tasks;
using Courses.Domain.Models;

namespace Courses.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса импорта данных
    /// </summary>
    public interface IImportService
    {
        /// <summary>
        /// Формат импорта
        /// </summary>
        public enum ImportFormat
        {
            /// <summary>
            /// JSON
            /// </summary>
            Json,

            /// <summary>
            /// XML
            /// </summary>
            Xml,

            /// <summary>
            /// CSV
            /// </summary>
            Csv,

            /// <summary>
            /// Excel
            /// </summary>
            Excel
        }

        /// <summary>
        /// Импортировать курс
        /// </summary>
        /// <param name="filePath">Путь к файлу импорта</param>
        /// <param name="format">Формат импорта</param>
        /// <param name="authorId">Идентификатор автора</param>
        /// <returns>Идентификатор импортированного курса</returns>
        Task<Guid> ImportCourseAsync(string filePath, ImportFormat format, Guid authorId);

        /// <summary>
        /// Импортировать модуль
        /// </summary>
        /// <param name="filePath">Путь к файлу импорта</param>
        /// <param name="format">Формат импорта</param>
        /// <param name="courseId">Идентификатор курса</param>
        /// <returns>Идентификатор импортированного модуля</returns>
        Task<Guid> ImportModuleAsync(string filePath, ImportFormat format, Guid courseId);

        /// <summary>
        /// Импортировать урок
        /// </summary>
        /// <param name="filePath">Путь к файлу импорта</param>
        /// <param name="format">Формат импорта</param>
        /// <param name="moduleId">Идентификатор модуля</param>
        /// <returns>Идентификатор импортированного урока</returns>
        Task<Guid> ImportLessonAsync(string filePath, ImportFormat format, Guid moduleId);

        /// <summary>
        /// Импортировать материалы урока
        /// </summary>
        /// <param name="filePath">Путь к файлу импорта</param>
        /// <param name="format">Формат импорта</param>
        /// <param name="lessonId">Идентификатор урока</param>
        /// <returns>Количество импортированных материалов</returns>
        Task<int> ImportLessonMaterialsAsync(string filePath, ImportFormat format, Guid lessonId);

        /// <summary>
        /// Проверить файл импорта
        /// </summary>
        /// <param name="filePath">Путь к файлу импорта</param>
        /// <param name="format">Формат импорта</param>
        /// <returns>Результат проверки</returns>
        Task<ImportValidationResult> ValidateImportFileAsync(string filePath, ImportFormat format);

        /// <summary>
        /// Получить предварительный просмотр импорта
        /// </summary>
        /// <param name="filePath">Путь к файлу импорта</param>
        /// <param name="format">Формат импорта</param>
        /// <returns>Предварительный просмотр импорта</returns>
        Task<ImportPreviewModel> GetImportPreviewAsync(string filePath, ImportFormat format);
    }
}
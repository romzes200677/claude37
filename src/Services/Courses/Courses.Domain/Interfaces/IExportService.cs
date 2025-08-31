using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Courses.Domain.Models;

namespace Courses.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса экспорта данных
    /// </summary>
    public interface IExportService
    {
        /// <summary>
        /// Формат экспорта
        /// </summary>
        public enum ExportFormat
        {
            /// <summary>
            /// PDF
            /// </summary>
            Pdf,

            /// <summary>
            /// Excel
            /// </summary>
            Excel,

            /// <summary>
            /// CSV
            /// </summary>
            Csv,

            /// <summary>
            /// JSON
            /// </summary>
            Json,

            /// <summary>
            /// XML
            /// </summary>
            Xml
        }

        /// <summary>
        /// Экспортировать курс
        /// </summary>
        /// <param name="courseId">Идентификатор курса</param>
        /// <param name="format">Формат экспорта</param>
        /// <param name="includeModules">Включать модули</param>
        /// <param name="includeLessons">Включать уроки</param>
        /// <param name="includeMaterials">Включать материалы</param>
        /// <returns>Путь к файлу экспорта</returns>
        Task<string> ExportCourseAsync(Guid courseId, ExportFormat format, bool includeModules = true, bool includeLessons = true, bool includeMaterials = false);

        /// <summary>
        /// Экспортировать модуль
        /// </summary>
        /// <param name="moduleId">Идентификатор модуля</param>
        /// <param name="format">Формат экспорта</param>
        /// <param name="includeLessons">Включать уроки</param>
        /// <param name="includeMaterials">Включать материалы</param>
        /// <returns>Путь к файлу экспорта</returns>
        Task<string> ExportModuleAsync(Guid moduleId, ExportFormat format, bool includeLessons = true, bool includeMaterials = false);

        /// <summary>
        /// Экспортировать урок
        /// </summary>
        /// <param name="lessonId">Идентификатор урока</param>
        /// <param name="format">Формат экспорта</param>
        /// <param name="includeMaterials">Включать материалы</param>
        /// <returns>Путь к файлу экспорта</returns>
        Task<string> ExportLessonAsync(Guid lessonId, ExportFormat format, bool includeMaterials = true);

        /// <summary>
        /// Экспортировать список курсов
        /// </summary>
        /// <param name="courseIds">Список идентификаторов курсов</param>
        /// <param name="format">Формат экспорта</param>
        /// <returns>Путь к файлу экспорта</returns>
        Task<string> ExportCoursesAsync(IEnumerable<Guid> courseIds, ExportFormat format);

        /// <summary>
        /// Экспортировать прогресс студента
        /// </summary>
        /// <param name="studentId">Идентификатор студента</param>
        /// <param name="courseId">Идентификатор курса</param>
        /// <param name="format">Формат экспорта</param>
        /// <returns>Путь к файлу экспорта</returns>
        Task<string> ExportStudentProgressAsync(Guid studentId, Guid courseId, ExportFormat format);

        /// <summary>
        /// Экспортировать статистику курса
        /// </summary>
        /// <param name="courseId">Идентификатор курса</param>
        /// <param name="format">Формат экспорта</param>
        /// <param name="startDate">Дата начала</param>
        /// <param name="endDate">Дата окончания</param>
        /// <returns>Путь к файлу экспорта</returns>
        Task<string> ExportCourseStatisticsAsync(Guid courseId, ExportFormat format, DateTime? startDate = null, DateTime? endDate = null);
    }
}
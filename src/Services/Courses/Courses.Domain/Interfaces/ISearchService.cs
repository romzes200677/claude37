using System.Collections.Generic;
using System.Threading.Tasks;
using Courses.Domain.Models;

namespace Courses.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса поиска
    /// </summary>
    public interface ISearchService
    {
        /// <summary>
        /// Поиск курсов
        /// </summary>
        /// <param name="filterModel">Модель фильтрации</param>
        /// <returns>Результаты поиска</returns>
        Task<PaginationResponseModel<CourseViewModel>> SearchCoursesAsync(CourseFilterModel filterModel);

        /// <summary>
        /// Индексировать курс
        /// </summary>
        /// <param name="course">Модель курса</param>
        /// <returns>Задача</returns>
        Task IndexCourseAsync(CourseDetailViewModel course);

        /// <summary>
        /// Удалить курс из индекса
        /// </summary>
        /// <param name="courseId">Идентификатор курса</param>
        /// <returns>Задача</returns>
        Task RemoveCourseFromIndexAsync(string courseId);

        /// <summary>
        /// Получить рекомендации по курсам
        /// </summary>
        /// <param name="studentId">Идентификатор студента</param>
        /// <param name="count">Количество рекомендаций</param>
        /// <returns>Список рекомендованных курсов</returns>
        Task<List<CourseViewModel>> GetCourseRecommendationsAsync(string studentId, int count = 5);

        /// <summary>
        /// Получить похожие курсы
        /// </summary>
        /// <param name="courseId">Идентификатор курса</param>
        /// <param name="count">Количество похожих курсов</param>
        /// <returns>Список похожих курсов</returns>
        Task<List<CourseViewModel>> GetSimilarCoursesAsync(string courseId, int count = 5);
    }
}
using System.Collections.Generic;

namespace Courses.Domain.Models
{
    /// <summary>
    /// Модель запроса с пагинацией
    /// </summary>
    public class PaginationRequestModel
    {
        /// <summary>
        /// Номер страницы (начиная с 1)
        /// </summary>
        public int PageNumber { get; set; } = 1;

        /// <summary>
        /// Размер страницы
        /// </summary>
        public int PageSize { get; set; } = 10;

        /// <summary>
        /// Поле для сортировки
        /// </summary>
        public string SortBy { get; set; } = "CreatedAt";

        /// <summary>
        /// Направление сортировки (asc или desc)
        /// </summary>
        public string SortDirection { get; set; } = "desc";
    }

    /// <summary>
    /// Модель ответа с пагинацией
    /// </summary>
    /// <typeparam name="T">Тип данных</typeparam>
    public class PaginationResponseModel<T>
    {
        /// <summary>
        /// Элементы текущей страницы
        /// </summary>
        public IEnumerable<T> Items { get; set; }

        /// <summary>
        /// Номер текущей страницы
        /// </summary>
        public int PageNumber { get; set; }

        /// <summary>
        /// Размер страницы
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Общее количество элементов
        /// </summary>
        public int TotalCount { get; set; }

        /// <summary>
        /// Общее количество страниц
        /// </summary>
        public int TotalPages { get; set; }

        /// <summary>
        /// Есть ли предыдущая страница
        /// </summary>
        public bool HasPreviousPage { get; set; }

        /// <summary>
        /// Есть ли следующая страница
        /// </summary>
        public bool HasNextPage { get; set; }
    }

    /// <summary>
    /// Модель фильтрации курсов
    /// </summary>
    public class CourseFilterModel : PaginationRequestModel
    {
        /// <summary>
        /// Поисковый запрос
        /// </summary>
        public string SearchTerm { get; set; }

        /// <summary>
        /// Уровень сложности
        /// </summary>
        public string DifficultyLevel { get; set; }

        /// <summary>
        /// Только опубликованные курсы
        /// </summary>
        public bool? IsPublished { get; set; }

        /// <summary>
        /// Идентификатор автора
        /// </summary>
        public string AuthorId { get; set; }
    }
}
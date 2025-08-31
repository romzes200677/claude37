using System;
using System.Collections.Generic;
using System.Linq;

namespace Common.Models
{
    /// <summary>
    /// Пагинированный список
    /// </summary>
    /// <typeparam name="T">Тип элементов</typeparam>
    public class PaginatedList<T>
    {
        /// <summary>
        /// Элементы
        /// </summary>
        public IReadOnlyCollection<T> Items { get; }

        /// <summary>
        /// Номер страницы
        /// </summary>
        public int PageNumber { get; }

        /// <summary>
        /// Размер страницы
        /// </summary>
        public int PageSize { get; }

        /// <summary>
        /// Общее количество элементов
        /// </summary>
        public int TotalCount { get; }

        /// <summary>
        /// Общее количество страниц
        /// </summary>
        public int TotalPages { get; }

        /// <summary>
        /// Есть ли предыдущая страница
        /// </summary>
        public bool HasPreviousPage => PageNumber > 1;

        /// <summary>
        /// Есть ли следующая страница
        /// </summary>
        public bool HasNextPage => PageNumber < TotalPages;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="items">Элементы</param>
        /// <param name="count">Общее количество элементов</param>
        /// <param name="pageNumber">Номер страницы</param>
        /// <param name="pageSize">Размер страницы</param>
        public PaginatedList(IEnumerable<T> items, int count, int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = count;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Items = items.ToList().AsReadOnly();
        }

        /// <summary>
        /// Создать пагинированный список
        /// </summary>
        /// <param name="source">Источник данных</param>
        /// <param name="pageNumber">Номер страницы</param>
        /// <param name="pageSize">Размер страницы</param>
        /// <returns>Пагинированный список</returns>
        public static PaginatedList<T> Create(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedList<T>(items, count, pageNumber, pageSize);
        }

        /// <summary>
        /// Создать пагинированный список
        /// </summary>
        /// <param name="source">Источник данных</param>
        /// <param name="pageNumber">Номер страницы</param>
        /// <param name="pageSize">Размер страницы</param>
        /// <returns>Пагинированный список</returns>
        public static PaginatedList<T> Create(IEnumerable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
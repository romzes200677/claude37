using System;
using System.Collections.Generic;

namespace Courses.Domain.Models
{
    /// <summary>
    /// Модель создания модуля
    /// </summary>
    public class CreateModuleModel
    {
        /// <summary>
        /// Название модуля
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание модуля
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Порядковый номер
        /// </summary>
        public int OrderIndex { get; set; }

        /// <summary>
        /// Идентификатор курса
        /// </summary>
        public Guid CourseId { get; set; }
    }

    /// <summary>
    /// Модель обновления модуля
    /// </summary>
    public class UpdateModuleModel
    {
        /// <summary>
        /// Идентификатор модуля
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название модуля
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание модуля
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Порядковый номер
        /// </summary>
        public int OrderIndex { get; set; }
    }

    /// <summary>
    /// Модель представления модуля
    /// </summary>
    public class ModuleViewModel
    {
        /// <summary>
        /// Идентификатор модуля
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название модуля
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание модуля
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Порядковый номер
        /// </summary>
        public int OrderIndex { get; set; }

        /// <summary>
        /// Идентификатор курса
        /// </summary>
        public Guid CourseId { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Дата обновления
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    }

    /// <summary>
    /// Модель детального представления модуля
    /// </summary>
    public class ModuleDetailViewModel : ModuleViewModel
    {
        /// <summary>
        /// Уроки модуля
        /// </summary>
        public IEnumerable<LessonViewModel> Lessons { get; set; }
    }

    /// <summary>
    /// Модель обновления порядка модулей
    /// </summary>
    public class UpdateModulesOrderModel
    {
        /// <summary>
        /// Идентификатор курса
        /// </summary>
        public Guid CourseId { get; set; }

        /// <summary>
        /// Список идентификаторов модулей в новом порядке
        /// </summary>
        public List<Guid> ModuleIds { get; set; }
    }
}
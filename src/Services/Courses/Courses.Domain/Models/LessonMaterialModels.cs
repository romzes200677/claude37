using System;

namespace Courses.Domain.Models
{
    /// <summary>
    /// Модель создания материала урока
    /// </summary>
    public class CreateLessonMaterialModel
    {
        /// <summary>
        /// Название материала
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание материала
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Тип материала
        /// </summary>
        public string MaterialType { get; set; }

        /// <summary>
        /// URL ресурса
        /// </summary>
        public string ResourceUrl { get; set; }

        /// <summary>
        /// Порядковый номер
        /// </summary>
        public int OrderIndex { get; set; }

        /// <summary>
        /// Является ли обязательным
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// Идентификатор урока
        /// </summary>
        public Guid LessonId { get; set; }
    }

    /// <summary>
    /// Модель обновления материала урока
    /// </summary>
    public class UpdateLessonMaterialModel
    {
        /// <summary>
        /// Идентификатор материала
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название материала
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание материала
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Тип материала
        /// </summary>
        public string MaterialType { get; set; }

        /// <summary>
        /// URL ресурса
        /// </summary>
        public string ResourceUrl { get; set; }

        /// <summary>
        /// Порядковый номер
        /// </summary>
        public int OrderIndex { get; set; }

        /// <summary>
        /// Является ли обязательным
        /// </summary>
        public bool IsRequired { get; set; }
    }

    /// <summary>
    /// Модель представления материала урока
    /// </summary>
    public class LessonMaterialViewModel
    {
        /// <summary>
        /// Идентификатор материала
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Название материала
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание материала
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Тип материала
        /// </summary>
        public string MaterialType { get; set; }

        /// <summary>
        /// URL ресурса
        /// </summary>
        public string ResourceUrl { get; set; }

        /// <summary>
        /// Порядковый номер
        /// </summary>
        public int OrderIndex { get; set; }

        /// <summary>
        /// Является ли обязательным
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// Идентификатор урока
        /// </summary>
        public Guid LessonId { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Дата обновления
        /// </summary>
        public DateTime? UpdatedAt { get; set; }
    }
}
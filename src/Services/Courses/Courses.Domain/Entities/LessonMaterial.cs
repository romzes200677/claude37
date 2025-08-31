using System;

namespace Courses.Domain.Entities
{
    /// <summary>
    /// Материал урока
    /// </summary>
    public class LessonMaterial : BaseEntity
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
        /// Тип материала (pdf, видео, ссылка и т.д.)
        /// </summary>
        public string MaterialType { get; set; }

        /// <summary>
        /// URL или путь к материалу
        /// </summary>
        public string ResourceUrl { get; set; }

        /// <summary>
        /// Порядковый номер материала в уроке
        /// </summary>
        public int OrderIndex { get; set; }

        /// <summary>
        /// Обязательный ли материал
        /// </summary>
        public bool IsRequired { get; set; }

        /// <summary>
        /// Идентификатор урока
        /// </summary>
        public Guid LessonId { get; set; }

        /// <summary>
        /// Урок
        /// </summary>
        public virtual Lesson Lesson { get; set; }
    }
}
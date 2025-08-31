using System;
using System.Collections.Generic;

namespace Courses.Domain.Models
{
    /// <summary>
    /// Модель результата валидации импорта
    /// </summary>
    public class ImportValidationResult
    {
        /// <summary>
        /// Успешно ли прошла валидация
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// Список ошибок
        /// </summary>
        public List<string> Errors { get; set; } = new List<string>();

        /// <summary>
        /// Список предупреждений
        /// </summary>
        public List<string> Warnings { get; set; } = new List<string>();

        /// <summary>
        /// Тип импортируемых данных
        /// </summary>
        public string ImportType { get; set; }

        /// <summary>
        /// Количество записей
        /// </summary>
        public int RecordCount { get; set; }
    }

    /// <summary>
    /// Модель предварительного просмотра импорта
    /// </summary>
    public class ImportPreviewModel
    {
        /// <summary>
        /// Тип импортируемых данных
        /// </summary>
        public string ImportType { get; set; }

        /// <summary>
        /// Количество записей
        /// </summary>
        public int RecordCount { get; set; }

        /// <summary>
        /// Предварительный просмотр курса
        /// </summary>
        public CoursePreviewModel Course { get; set; }

        /// <summary>
        /// Предварительный просмотр модулей
        /// </summary>
        public List<ModulePreviewModel> Modules { get; set; } = new List<ModulePreviewModel>();

        /// <summary>
        /// Предварительный просмотр уроков
        /// </summary>
        public List<LessonPreviewModel> Lessons { get; set; } = new List<LessonPreviewModel>();

        /// <summary>
        /// Предварительный просмотр материалов
        /// </summary>
        public List<MaterialPreviewModel> Materials { get; set; } = new List<MaterialPreviewModel>();
    }

    /// <summary>
    /// Модель предварительного просмотра курса
    /// </summary>
    public class CoursePreviewModel
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Уровень сложности
        /// </summary>
        public string DifficultyLevel { get; set; }

        /// <summary>
        /// Продолжительность (в минутах)
        /// </summary>
        public int DurationMinutes { get; set; }

        /// <summary>
        /// Категория
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Теги
        /// </summary>
        public List<string> Tags { get; set; } = new List<string>();
    }

    /// <summary>
    /// Модель предварительного просмотра модуля
    /// </summary>
    public class ModulePreviewModel
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Порядковый номер
        /// </summary>
        public int OrderIndex { get; set; }

        /// <summary>
        /// Количество уроков
        /// </summary>
        public int LessonCount { get; set; }
    }

    /// <summary>
    /// Модель предварительного просмотра урока
    /// </summary>
    public class LessonPreviewModel
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Порядковый номер
        /// </summary>
        public int OrderIndex { get; set; }

        /// <summary>
        /// Продолжительность (в минутах)
        /// </summary>
        public int DurationMinutes { get; set; }

        /// <summary>
        /// Тип урока
        /// </summary>
        public string LessonType { get; set; }

        /// <summary>
        /// Количество материалов
        /// </summary>
        public int MaterialCount { get; set; }
    }

    /// <summary>
    /// Модель предварительного просмотра материала
    /// </summary>
    public class MaterialPreviewModel
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Тип материала
        /// </summary>
        public string MaterialType { get; set; }

        /// <summary>
        /// Размер файла (в байтах)
        /// </summary>
        public long? FileSize { get; set; }

        /// <summary>
        /// Тип файла
        /// </summary>
        public string FileType { get; set; }
    }
}
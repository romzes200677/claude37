using System;
using System.IO;

namespace Courses.Domain.Models
{
    /// <summary>
    /// Модель файла
    /// </summary>
    public class FileModel
    {
        /// <summary>
        /// Идентификатор файла
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Имя файла
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Оригинальное имя файла
        /// </summary>
        public string OriginalFileName { get; set; }

        /// <summary>
        /// URL файла
        /// </summary>
        public string FileUrl { get; set; }

        /// <summary>
        /// Тип содержимого
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Размер файла в байтах
        /// </summary>
        public long Size { get; set; }

        /// <summary>
        /// Дата загрузки
        /// </summary>
        public DateTime UploadedAt { get; set; }

        /// <summary>
        /// Идентификатор пользователя, загрузившего файл
        /// </summary>
        public Guid? UploadedById { get; set; }
    }

    /// <summary>
    /// Модель загрузки файла
    /// </summary>
    public class FileUploadModel
    {
        /// <summary>
        /// Имя файла
        /// </summary>
        public string FileName { get; set; }

        /// <summary>
        /// Тип содержимого
        /// </summary>
        public string ContentType { get; set; }

        /// <summary>
        /// Поток данных
        /// </summary>
        public Stream Stream { get; set; }

        /// <summary>
        /// Идентификатор пользователя, загружающего файл
        /// </summary>
        public Guid? UploadedById { get; set; }
    }

    /// <summary>
    /// Результат загрузки файла
    /// </summary>
    public class FileUploadResultModel
    {
        /// <summary>
        /// Успешность загрузки
        /// </summary>
        public bool IsSuccess { get; set; }

        /// <summary>
        /// Сообщение об ошибке
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Модель файла
        /// </summary>
        public FileModel File { get; set; }
    }

    /// <summary>
    /// Тип файла
    /// </summary>
    public enum FileType
    {
        /// <summary>
        /// Изображение
        /// </summary>
        Image,

        /// <summary>
        /// Документ
        /// </summary>
        Document,

        /// <summary>
        /// Видео
        /// </summary>
        Video,

        /// <summary>
        /// Аудио
        /// </summary>
        Audio,

        /// <summary>
        /// Архив
        /// </summary>
        Archive,

        /// <summary>
        /// Другое
        /// </summary>
        Other
    }

    /// <summary>
    /// Модель фильтра файлов
    /// </summary>
    public class FileFilterModel : PaginationRequestModel
    {
        /// <summary>
        /// Идентификатор пользователя, загрузившего файл
        /// </summary>
        public Guid? UploadedById { get; set; }

        /// <summary>
        /// Тип файла
        /// </summary>
        public FileType? FileType { get; set; }

        /// <summary>
        /// Поисковый запрос
        /// </summary>
        public string SearchQuery { get; set; }

        /// <summary>
        /// Дата начала периода
        /// </summary>
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Дата окончания периода
        /// </summary>
        public DateTime? EndDate { get; set; }

        /// <summary>
        /// Минимальный размер файла в байтах
        /// </summary>
        public long? MinSize { get; set; }

        /// <summary>
        /// Максимальный размер файла в байтах
        /// </summary>
        public long? MaxSize { get; set; }
    }
}
using System.IO;
using System.Threading.Tasks;

namespace Courses.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса файлового хранилища
    /// </summary>
    public interface IFileStorageService
    {
        /// <summary>
        /// Сохранить файл
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <param name="contentType">Тип содержимого</param>
        /// <param name="stream">Поток данных</param>
        /// <returns>URL файла</returns>
        Task<string> SaveFileAsync(string fileName, string contentType, Stream stream);

        /// <summary>
        /// Удалить файл
        /// </summary>
        /// <param name="fileUrl">URL файла</param>
        Task DeleteFileAsync(string fileUrl);

        /// <summary>
        /// Получить URL файла
        /// </summary>
        /// <param name="fileName">Имя файла</param>
        /// <returns>URL файла</returns>
        string GetFileUrl(string fileName);
    }
}
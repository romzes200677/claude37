using System.Collections.Generic;
using System.Threading.Tasks;

namespace Courses.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса валидации
    /// </summary>
    public interface IValidationService
    {
        /// <summary>
        /// Валидировать объект
        /// </summary>
        /// <typeparam name="T">Тип объекта</typeparam>
        /// <param name="obj">Объект</param>
        /// <returns>Результат валидации</returns>
        Task<ValidationResult> ValidateAsync<T>(T obj);
    }

    /// <summary>
    /// Результат валидации
    /// </summary>
    public class ValidationResult
    {
        /// <summary>
        /// Успешность валидации
        /// </summary>
        public bool IsValid { get; set; }

        /// <summary>
        /// Список ошибок
        /// </summary>
        public List<string> Errors { get; set; } = new List<string>();

        /// <summary>
        /// Добавить ошибку
        /// </summary>
        /// <param name="error">Ошибка</param>
        public void AddError(string error)
        {
            IsValid = false;
            Errors.Add(error);
        }

        /// <summary>
        /// Создать успешный результат
        /// </summary>
        /// <returns>Результат валидации</returns>
        public static ValidationResult Success()
        {
            return new ValidationResult { IsValid = true };
        }

        /// <summary>
        /// Создать результат с ошибкой
        /// </summary>
        /// <param name="error">Ошибка</param>
        /// <returns>Результат валидации</returns>
        public static ValidationResult Fail(string error)
        {
            return new ValidationResult
            {
                IsValid = false,
                Errors = new List<string> { error }
            };
        }

        /// <summary>
        /// Создать результат с ошибками
        /// </summary>
        /// <param name="errors">Список ошибок</param>
        /// <returns>Результат валидации</returns>
        public static ValidationResult Fail(List<string> errors)
        {
            return new ValidationResult
            {
                IsValid = false,
                Errors = errors
            };
        }
    }
}
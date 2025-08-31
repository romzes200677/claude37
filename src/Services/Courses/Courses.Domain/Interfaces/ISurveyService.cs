using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Courses.Domain.Models;

namespace Courses.Domain.Interfaces
{
    /// <summary>
    /// Интерфейс сервиса опросов
    /// </summary>
    public interface ISurveyService
    {
        /// <summary>
        /// Создать опрос
        /// </summary>
        /// <param name="survey">Модель создания опроса</param>
        /// <returns>Идентификатор опроса</returns>
        Task<Guid> CreateSurveyAsync(CreateSurveyModel survey);

        /// <summary>
        /// Обновить опрос
        /// </summary>
        /// <param name="surveyId">Идентификатор опроса</param>
        /// <param name="survey">Модель обновления опроса</param>
        /// <returns>Задача</returns>
        Task UpdateSurveyAsync(Guid surveyId, UpdateSurveyModel survey);

        /// <summary>
        /// Удалить опрос
        /// </summary>
        /// <param name="surveyId">Идентификатор опроса</param>
        /// <returns>Задача</returns>
        Task DeleteSurveyAsync(Guid surveyId);

        /// <summary>
        /// Получить опрос по идентификатору
        /// </summary>
        /// <param name="surveyId">Идентификатор опроса</param>
        /// <returns>Модель опроса</returns>
        Task<SurveyViewModel> GetSurveyByIdAsync(Guid surveyId);

        /// <summary>
        /// Получить список опросов
        /// </summary>
        /// <param name="filter">Фильтр</param>
        /// <returns>Список опросов</returns>
        Task<PaginationResponseModel<SurveyViewModel>> GetSurveysAsync(SurveyFilterModel filter);

        /// <summary>
        /// Опубликовать опрос
        /// </summary>
        /// <param name="surveyId">Идентификатор опроса</param>
        /// <returns>Задача</returns>
        Task PublishSurveyAsync(Guid surveyId);

        /// <summary>
        /// Закрыть опрос
        /// </summary>
        /// <param name="surveyId">Идентификатор опроса</param>
        /// <returns>Задача</returns>
        Task CloseSurveyAsync(Guid surveyId);

        /// <summary>
        /// Отправить ответы на опрос
        /// </summary>
        /// <param name="surveyId">Идентификатор опроса</param>
        /// <param name="response">Модель ответа на опрос</param>
        /// <returns>Идентификатор ответа</returns>
        Task<Guid> SubmitSurveyResponseAsync(Guid surveyId, SurveyResponseModel response);

        /// <summary>
        /// Получить ответ на опрос по идентификатору
        /// </summary>
        /// <param name="responseId">Идентификатор ответа</param>
        /// <returns>Модель ответа на опрос</returns>
        Task<SurveyResponseViewModel> GetSurveyResponseByIdAsync(Guid responseId);

        /// <summary>
        /// Получить список ответов на опрос
        /// </summary>
        /// <param name="surveyId">Идентификатор опроса</param>
        /// <param name="filter">Фильтр</param>
        /// <returns>Список ответов на опрос</returns>
        Task<PaginationResponseModel<SurveyResponseViewModel>> GetSurveyResponsesAsync(Guid surveyId, PaginationRequestModel filter);

        /// <summary>
        /// Получить результаты опроса
        /// </summary>
        /// <param name="surveyId">Идентификатор опроса</param>
        /// <returns>Результаты опроса</returns>
        Task<SurveyResultsModel> GetSurveyResultsAsync(Guid surveyId);

        /// <summary>
        /// Экспортировать результаты опроса
        /// </summary>
        /// <param name="surveyId">Идентификатор опроса</param>
        /// <param name="format">Формат экспорта</param>
        /// <returns>Путь к файлу экспорта</returns>
        Task<string> ExportSurveyResultsAsync(Guid surveyId, string format);
    }
}
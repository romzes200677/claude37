using System;

namespace Courses.Domain.Exceptions
{
    /// <summary>
    /// Базовое исключение домена
    /// </summary>
    public class DomainException : Exception
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="message">Сообщение</param>
        public DomainException(string message) : base(message)
        {
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="message">Сообщение</param>
        /// <param name="innerException">Внутреннее исключение</param>
        public DomainException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    /// <summary>
    /// Исключение при отсутствии сущности
    /// </summary>
    public class EntityNotFoundException : DomainException
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="entityName">Название сущности</param>
        /// <param name="id">Идентификатор</param>
        public EntityNotFoundException(string entityName, Guid id)
            : base($"Сущность {entityName} с идентификатором {id} не найдена")
        {
            EntityName = entityName;
            Id = id;
        }

        /// <summary>
        /// Название сущности
        /// </summary>
        public string EntityName { get; }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; }
    }

    /// <summary>
    /// Исключение при нарушении бизнес-правила
    /// </summary>
    public class BusinessRuleViolationException : DomainException
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="message">Сообщение</param>
        public BusinessRuleViolationException(string message) : base(message)
        {
        }
    }

    /// <summary>
    /// Исключение при недостаточных правах
    /// </summary>
    public class InsufficientPermissionsException : DomainException
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="message">Сообщение</param>
        public InsufficientPermissionsException(string message) : base(message)
        {
        }
    }

    /// <summary>
    /// Исключение при превышении лимита
    /// </summary>
    public class LimitExceededException : DomainException
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="message">Сообщение</param>
        public LimitExceededException(string message) : base(message)
        {
        }
    }

    /// <summary>
    /// Исключение при дублировании сущности
    /// </summary>
    public class DuplicateEntityException : DomainException
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="entityName">Название сущности</param>
        /// <param name="propertyName">Название свойства</param>
        /// <param name="propertyValue">Значение свойства</param>
        public DuplicateEntityException(string entityName, string propertyName, string propertyValue)
            : base($"Сущность {entityName} с {propertyName} = {propertyValue} уже существует")
        {
            EntityName = entityName;
            PropertyName = propertyName;
            PropertyValue = propertyValue;
        }

        /// <summary>
        /// Название сущности
        /// </summary>
        public string EntityName { get; }

        /// <summary>
        /// Название свойства
        /// </summary>
        public string PropertyName { get; }

        /// <summary>
        /// Значение свойства
        /// </summary>
        public string PropertyValue { get; }
    }

    /// <summary>
    /// Исключение при недопустимом состоянии
    /// </summary>
    public class InvalidStateException : DomainException
    {
        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="entityName">Название сущности</param>
        /// <param name="id">Идентификатор</param>
        /// <param name="currentState">Текущее состояние</param>
        /// <param name="expectedState">Ожидаемое состояние</param>
        public InvalidStateException(string entityName, Guid id, string currentState, string expectedState)
            : base($"Сущность {entityName} с идентификатором {id} находится в состоянии {currentState}, ожидалось {expectedState}")
        {
            EntityName = entityName;
            Id = id;
            CurrentState = currentState;
            ExpectedState = expectedState;
        }

        /// <summary>
        /// Название сущности
        /// </summary>
        public string EntityName { get; }

        /// <summary>
        /// Идентификатор
        /// </summary>
        public Guid Id { get; }

        /// <summary>
        /// Текущее состояние
        /// </summary>
        public string CurrentState { get; }

        /// <summary>
        /// Ожидаемое состояние
        /// </summary>
        public string ExpectedState { get; }
    }
}
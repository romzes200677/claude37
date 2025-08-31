# Взаимодействие микросервисов в AiTestPlatform

В этом документе описано взаимодействие между микросервисами в системе AiTestPlatform.

## Диаграмма взаимодействия микросервисов

```mermaid
flowchart TB
    Client["Клиент (Web/Mobile)"]
    ApiGateway["API Gateway"]
    
    subgraph Services["Микросервисы"]
        Identity["Identity"]
        Courses["Courses"]
        Testing["Testing"]
        CodeExecution["CodeExecution"]
    end
    
    EventBus["Event Bus (Kafka)"]
    
    Client --> ApiGateway
    ApiGateway --> Identity
    ApiGateway --> Courses
    ApiGateway --> Testing
    ApiGateway --> CodeExecution
    
    Identity <--> EventBus
    Courses <--> EventBus
    Testing <--> EventBus
    CodeExecution <--> EventBus
    
    Identity <-.-> Courses
    Courses <-.-> Testing
    Testing <-.-> CodeExecution
```

## Последовательность взаимодействия при создании курса

```mermaid
sequenceDiagram
    participant Client as Клиент
    participant Gateway as API Gateway
    participant Identity as Identity Service
    participant Courses as Courses Service
    participant EventBus as Event Bus
    
    Client->>Gateway: POST /api/courses
    Gateway->>Identity: Проверка аутентификации
    Identity-->>Gateway: Токен валиден, пользователь - преподаватель
    Gateway->>Courses: Создание курса
    Courses->>Courses: Сохранение в БД
    Courses->>EventBus: Публикация CourseCreatedEvent
    Courses-->>Gateway: Курс создан (ID)
    Gateway-->>Client: 201 Created, CourseDto
    
    Note over EventBus: Асинхронная обработка
    EventBus->>Identity: Обновление статистики преподавателя
```

## Последовательность взаимодействия при прохождении теста

```mermaid
sequenceDiagram
    participant Client as Клиент
    participant Gateway as API Gateway
    participant Identity as Identity Service
    participant Courses as Courses Service
    participant Testing as Testing Service
    participant CodeExec as CodeExecution Service
    participant AiService as AI Service
    participant EventBus as Event Bus
    
    Client->>Gateway: POST /api/tests/{testId}/attempts
    Gateway->>Identity: Проверка аутентификации
    Identity-->>Gateway: Токен валиден, пользователь - студент
    Gateway->>Testing: Создание попытки теста
    Testing->>Courses: Проверка записи на курс
    Courses-->>Testing: Студент записан на курс
    Testing->>Testing: Создание попытки в БД
    Testing-->>Gateway: Попытка создана (ID)
    Gateway-->>Client: 201 Created, TestAttemptDto
    
    Client->>Gateway: POST /api/tests/attempts/{attemptId}/questions/{questionId}/responses
    Gateway->>Testing: Сохранение ответа
    
    alt Вопрос с кодом
        Testing->>CodeExec: Выполнение кода
        CodeExec->>CodeExec: Запуск в изолированной среде
        CodeExec-->>Testing: Результаты выполнения
        Testing->>AiService: Оценка кода с помощью ИИ
        AiService-->>Testing: Результаты оценки кода
    else Вопрос с текстовым ответом
        Testing->>AiService: Оценка текстового ответа с помощью ИИ
        AiService-->>Testing: Результаты оценки текста
    end
    
    Testing->>Testing: Сохранение ответа и оценки в БД
    Testing-->>Gateway: Ответ сохранен
    Gateway-->>Client: 200 OK
    
    Client->>Gateway: POST /api/tests/attempts/{attemptId}/submit
    Gateway->>Testing: Завершение попытки
    Testing->>Testing: Финализация результатов
    Testing->>EventBus: Публикация TestCompletedEvent
    Testing-->>Gateway: Результаты теста
    Gateway-->>Client: 200 OK, TestResultsDto
    
    Note over EventBus: Асинхронная обработка
    EventBus->>Courses: Обновление прогресса студента
    EventBus->>Identity: Обновление статистики студента
```

## События интеграции (Integration Events)

### События микросервиса Identity

```mermaid
classDiagram
    class IntegrationEvent {
        <<abstract>>
        +Guid Id
        +DateTime CreationDate
    }
    
    class UserRegisteredEvent {
        +Guid UserId
        +string UserName
        +string Email
        +string Role
    }
    
    class UserUpdatedEvent {
        +Guid UserId
        +string UserName
        +string Email
    }
    
    class UserDeletedEvent {
        +Guid UserId
    }
    
    IntegrationEvent <|-- UserRegisteredEvent
    IntegrationEvent <|-- UserUpdatedEvent
    IntegrationEvent <|-- UserDeletedEvent
```

### События микросервиса Courses

```mermaid
classDiagram
    class IntegrationEvent {
        <<abstract>>
        +Guid Id
        +DateTime CreationDate
    }
    
    class CourseCreatedEvent {
        +Guid CourseId
        +string Title
        +Guid AuthorId
    }
    
    class CourseUpdatedEvent {
        +Guid CourseId
        +string Title
        +string Description
    }
    
    class CoursePublishedEvent {
        +Guid CourseId
        +string Title
        +DateTime PublishedAt
    }
    
    class CourseEnrollmentEvent {
        +Guid EnrollmentId
        +Guid CourseId
        +Guid StudentId
        +DateTime EnrolledAt
    }
    
    class CourseCompletedEvent {
        +Guid EnrollmentId
        +Guid CourseId
        +Guid StudentId
        +DateTime CompletedAt
        +int Score
    }
    
    IntegrationEvent <|-- CourseCreatedEvent
    IntegrationEvent <|-- CourseUpdatedEvent
    IntegrationEvent <|-- CoursePublishedEvent
    IntegrationEvent <|-- CourseEnrollmentEvent
    IntegrationEvent <|-- CourseCompletedEvent
```

### События микросервиса Testing

```mermaid
classDiagram
    class IntegrationEvent {
        <<abstract>>
        +Guid Id
        +DateTime CreationDate
    }
    
    class TestCreatedEvent {
        +Guid TestId
        +string Title
        +Guid AuthorId
        +Guid CourseId
    }
    
    class TestAttemptStartedEvent {
        +Guid AttemptId
        +Guid TestId
        +Guid StudentId
        +DateTime StartedAt
    }
    
    class TestCompletedEvent {
        +Guid AttemptId
        +Guid TestId
        +Guid StudentId
        +DateTime CompletedAt
        +int Score
        +bool IsPassed
    }
    
    class TestQuestionResponseEvaluatedEvent {
        +Guid ResponseId
        +Guid QuestionId
        +Guid StudentId
        +DateTime EvaluatedAt
        +int Score
        +bool IsCorrect
        +bool IsAiEvaluated
    }
    
    IntegrationEvent <|-- TestCreatedEvent
    IntegrationEvent <|-- TestAttemptStartedEvent
    IntegrationEvent <|-- TestCompletedEvent
    IntegrationEvent <|-- TestQuestionResponseEvaluatedEvent
```

### События микросервиса CodeExecution

```mermaid
classDiagram
    class IntegrationEvent {
        <<abstract>>
        +Guid Id
        +DateTime CreationDate
    }
    
    class CodeTaskCreatedEvent {
        +Guid TaskId
        +string Title
        +Guid AuthorId
        +string ProgrammingLanguage
    }
    
    class CodeSubmissionCompletedEvent {
        +Guid SubmissionId
        +Guid TaskId
        +Guid StudentId
        +DateTime CompletedAt
        +int Score
        +bool IsCorrect
    }
    
    IntegrationEvent <|-- CodeTaskCreatedEvent
    IntegrationEvent <|-- CodeSubmissionCompletedEvent
```

## Обработчики событий (Event Handlers)

### Обработчики в микросервисе Identity

```csharp
public class CourseCompletedEventHandler : IIntegrationEventHandler<CourseCompletedEvent>
{
    private readonly IUserRepository _userRepository;
    private readonly ILogService _logService;
    
    public CourseCompletedEventHandler(IUserRepository userRepository, ILogService logService)
    {
        _userRepository = userRepository;
        _logService = logService;
    }
    
    public async Task Handle(CourseCompletedEvent @event)
    {
        try
        {
            // Обновление статистики пользователя
            await _userRepository.UpdateUserStatisticsAsync(
                @event.StudentId,
                completedCourseId: @event.CourseId,
                score: @event.Score);
                
            // Добавление достижения, если применимо
            await _userRepository.AddAchievementIfEligibleAsync(
                @event.StudentId,
                "CourseCompletion",
                $"Completed course {@event.CourseId}");
                
            _logService.LogInformation(
                "Updated statistics for user {UserId} after completing course {CourseId}",
                @event.StudentId, @event.CourseId);
        }
        catch (Exception ex)
        {
            _logService.LogError(ex,
                "Error handling CourseCompletedEvent for user {UserId} and course {CourseId}",
                @event.StudentId, @event.CourseId);
        }
    }
}
```

### Обработчики в микросервисе Courses

```csharp
public class TestCompletedEventHandler : IIntegrationEventHandler<TestCompletedEvent>
{
    private readonly IEnrollmentRepository _enrollmentRepository;
    private readonly ILessonProgressRepository _lessonProgressRepository;
    private readonly ILogService _logService;
    
    public TestCompletedEventHandler(
        IEnrollmentRepository enrollmentRepository,
        ILessonProgressRepository lessonProgressRepository,
        ILogService logService)
    {
        _enrollmentRepository = enrollmentRepository;
        _lessonProgressRepository = lessonProgressRepository;
        _logService = logService;
    }
    
    public async Task Handle(TestCompletedEvent @event)
    {
        try
        {
            // Получение информации о тесте и связанном уроке
            var testInfo = await _enrollmentRepository.GetTestInfoAsync(@event.TestId);
            
            if (testInfo == null)
            {
                _logService.LogWarning(
                    "Test {TestId} not found when handling TestCompletedEvent",
                    @event.TestId);
                return;
            }
            
            // Обновление прогресса по уроку
            if (testInfo.LessonId.HasValue)
            {
                await _lessonProgressRepository.UpdateProgressAsync(
                    @event.StudentId,
                    testInfo.LessonId.Value,
                    testCompleted: true,
                    testScore: @event.Score,
                    testPassed: @event.IsPassed);
                    
                _logService.LogInformation(
                    "Updated lesson progress for student {StudentId}, lesson {LessonId}",
                    @event.StudentId, testInfo.LessonId.Value);
            }
            
            // Проверка завершения курса
            if (testInfo.CourseId.HasValue)
            {
                var isCompleted = await _enrollmentRepository.CheckCourseCompletionAsync(
                    @event.StudentId,
                    testInfo.CourseId.Value);
                    
                if (isCompleted)
                {
                    await _enrollmentRepository.CompleteCourseAsync(
                        @event.StudentId,
                        testInfo.CourseId.Value);
                        
                    _logService.LogInformation(
                        "Course {CourseId} completed by student {StudentId}",
                        testInfo.CourseId.Value, @event.StudentId);
                }
            }
        }
        catch (Exception ex)
        {
            _logService.LogError(ex,
                "Error handling TestCompletedEvent for student {StudentId} and test {TestId}",
                @event.StudentId, @event.TestId);
        }
    }
}
```

## Конфигурация Event Bus

### Регистрация Event Bus в Startup.cs

```csharp
public void ConfigureServices(IServiceCollection services)
{
    // Регистрация EventBus
    services.AddSingleton<IEventBus, EventBusKafka>(sp =>
    {
        var kafkaConnection = Configuration["EventBusConnection"];
        var kafkaGroupId = Configuration["EventBusGroupId"];
        var logService = sp.GetRequiredService<ILogService>();
        var serviceProvider = sp.GetRequiredService<IServiceProvider>();
        
        return new EventBusKafka(kafkaConnection, kafkaGroupId, logService, serviceProvider);
    });
    
    // Регистрация обработчиков событий
    services.AddTransient<UserRegisteredEventHandler>();
    services.AddTransient<CourseCreatedEventHandler>();
    services.AddTransient<TestCompletedEventHandler>();
    // Другие обработчики...
}

public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
{
    // Конфигурация подписок на события
    var eventBus = app.ApplicationServices.GetRequiredService<IEventBus>();
    
    eventBus.Subscribe<UserRegisteredEvent, UserRegisteredEventHandler>();
    eventBus.Subscribe<CourseCreatedEvent, CourseCreatedEventHandler>();
    eventBus.Subscribe<TestCompletedEvent, TestCompletedEventHandler>();
    // Другие подписки...
}
```

### Реализация EventBusKafka

```csharp
public class EventBusKafka : IEventBus
{
    private readonly IProducer<string, string> _producer;
    private readonly IConsumer<string, string> _consumer;
    private readonly IServiceProvider _serviceProvider;
    private readonly ILogService _logService;
    private readonly string _groupId;
    private readonly Dictionary<string, List<Type>> _eventHandlerTypes;
    
    public EventBusKafka(
        string connectionString,
        string groupId,
        ILogService logService,
        IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        _logService = logService;
        _groupId = groupId;
        _eventHandlerTypes = new Dictionary<string, List<Type>>();
        
        // Конфигурация Kafka Producer
        var producerConfig = new ProducerConfig
        {
            BootstrapServers = connectionString,
            Acks = Acks.All
        };
        _producer = new ProducerBuilder<string, string>(producerConfig).Build();
        
        // Конфигурация Kafka Consumer
        var consumerConfig = new ConsumerConfig
        {
            BootstrapServers = connectionString,
            GroupId = groupId,
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = false
        };
        _consumer = new ConsumerBuilder<string, string>(consumerConfig).Build();
        
        // Запуск обработки сообщений в фоновом режиме
        Task.Factory.StartNew(ProcessMessages, TaskCreationOptions.LongRunning);
    }
    
    public async Task PublishAsync<T>(T @event) where T : IntegrationEvent
    {
        var eventName = @event.GetType().Name;
        var message = JsonSerializer.Serialize(@event);
        
        try
        {
            await _producer.ProduceAsync(eventName, new Message<string, string>
            {
                Key = @event.Id.ToString(),
                Value = message
            });
            
            _logService.LogInformation(
                "Published event {EventName} with ID {EventId}",
                eventName, @event.Id);
        }
        catch (Exception ex)
        {
            _logService.LogError(ex,
                "Error publishing event {EventName} with ID {EventId}",
                eventName, @event.Id);
            throw;
        }
    }
    
    public void Subscribe<T, TH>()
        where T : IntegrationEvent
        where TH : IIntegrationEventHandler<T>
    {
        var eventName = typeof(T).Name;
        var handlerType = typeof(TH);
        
        if (!_eventHandlerTypes.ContainsKey(eventName))
        {
            _eventHandlerTypes[eventName] = new List<Type>();
            _consumer.Subscribe(eventName);
        }
        
        if (_eventHandlerTypes[eventName].Contains(handlerType))
        {
            _logService.LogWarning(
                "Handler {HandlerName} already registered for event {EventName}",
                handlerType.Name, eventName);
            return;
        }
        
        _eventHandlerTypes[eventName].Add(handlerType);
        
        _logService.LogInformation(
            "Subscribed to event {EventName} with handler {HandlerName}",
            eventName, handlerType.Name);
    }
    
    public void Unsubscribe<T, TH>()
        where T : IntegrationEvent
        where TH : IIntegrationEventHandler<T>
    {
        var eventName = typeof(T).Name;
        var handlerType = typeof(TH);
        
        if (!_eventHandlerTypes.ContainsKey(eventName))
            return;
            
        _eventHandlerTypes[eventName].Remove(handlerType);
        
        if (_eventHandlerTypes[eventName].Count == 0)
        {
            _eventHandlerTypes.Remove(eventName);
            _consumer.Unsubscribe(new[] { eventName });
        }
        
        _logService.LogInformation(
            "Unsubscribed from event {EventName} with handler {HandlerName}",
            eventName, handlerType.Name);
    }
    
    private async Task ProcessMessages()
    {
        while (true)
        {
            try
            {
                var consumeResult = _consumer.Consume(TimeSpan.FromMilliseconds(100));
                
                if (consumeResult == null)
                    continue;
                    
                var eventName = consumeResult.Topic;
                var message = consumeResult.Message.Value;
                
                if (_eventHandlerTypes.ContainsKey(eventName))
                {
                    await ProcessEvent(eventName, message);
                }
                
                _consumer.Commit(consumeResult);
            }
            catch (Exception ex)
            {
                _logService.LogError(ex, "Error processing message from Kafka");
            }
        }
    }
    
    private async Task ProcessEvent(string eventName, string message)
    {
        if (!_eventHandlerTypes.ContainsKey(eventName))
            return;
            
        using var scope = _serviceProvider.CreateScope();
        
        foreach (var handlerType in _eventHandlerTypes[eventName])
        {
            try
            {
                var handler = scope.ServiceProvider.GetService(handlerType);
                
                if (handler == null)
                {
                    _logService.LogWarning(
                        "Handler {HandlerName} not registered in DI container",
                        handlerType.Name);
                    continue;
                }
                
                var eventType = _eventHandlerTypes[eventName]
                    .Select(t => t.GetInterfaces())
                    .SelectMany(i => i)
                    .Where(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(IIntegrationEventHandler<>))
                    .Select(i => i.GetGenericArguments()[0])
                    .First(t => t.Name == eventName);
                    
                var @event = JsonSerializer.Deserialize(message, eventType);
                var concreteType = typeof(IIntegrationEventHandler<>).MakeGenericType(eventType);
                var handleMethod = concreteType.GetMethod("Handle");
                
                await (Task)handleMethod.Invoke(handler, new[] { @event });
            }
            catch (Exception ex)
            {
                _logService.LogError(ex,
                    "Error handling event {EventName} with handler {HandlerName}",
                    eventName, handlerType.Name);
            }
        }
    }
}
```

## Преимущества микросервисной архитектуры в AiTestPlatform

1. **Независимая разработка и развертывание** - каждый микросервис может разрабатываться, тестироваться и развертываться независимо от других.

2. **Масштабируемость** - микросервисы можно масштабировать независимо в зависимости от нагрузки. Например, CodeExecution может требовать больше ресурсов, чем другие сервисы.

3. **Технологическая гибкость** - каждый микросервис может использовать наиболее подходящие технологии и инструменты для своих задач.

4. **Устойчивость к сбоям** - сбой в одном микросервисе не приводит к отказу всей системы.

5. **Четкое разделение ответственности** - каждый микросервис отвечает за конкретную бизнес-функцию.

## Интеграция с ИИ сервисом для оценки тестов

В микросервисе Testing реализована интеграция с внешним ИИ сервисом для автоматической оценки ответов на вопросы с текстовым ответом и вопросы с кодом.

### Архитектура интеграции с ИИ

```mermaid
flowchart LR
    TestQuestionResponseService["TestQuestionResponseService"] --> TestEvaluationService["TestEvaluationService"]
    TestEvaluationService --> AiService["AiService"]
    AiService --> ExternalAiApi["Внешний ИИ API (OpenAI)"]    
```

### Последовательность оценки ответа с помощью ИИ

```mermaid
sequenceDiagram
    participant TQRService as TestQuestionResponseService
    participant TEService as TestEvaluationService
    participant AiService as AiService
    participant ExternalAI as Внешний ИИ API
    participant DB as База данных
    
    TQRService->>TEService: EvaluateTextAnswerAsync / EvaluateCodeAnswerAsync
    TEService->>AiService: EvaluateTextAnswerAsync / EvaluateCodeAnswerAsync
    AiService->>ExternalAI: HTTP запрос с ответом и эталонным ответом
    ExternalAI-->>AiService: Результат оценки (JSON)
    AiService-->>TEService: AiEvaluationResult
    TEService->>TEService: Расчет баллов на основе оценки ИИ
    TEService-->>TQRService: Обновленный TestQuestionResponse
    TQRService->>DB: Сохранение результатов оценки
```

### Конфигурация ИИ сервиса

Конфигурация для подключения к внешнему ИИ API хранится в appsettings.json:

```json
{
  "AiService": {
    "ApiKey": "your-api-key-here",
    "ApiEndpoint": "https://api.openai.com/v1/chat/completions"
  }
}
```

## Проблемы и решения

1. **Согласованность данных** - использование шаблона Event Sourcing и CQRS для обеспечения согласованности данных между микросервисами.

2. **Сложность распределенных транзакций** - использование шаблона Saga для управления распределенными транзакциями.

3. **Мониторинг и отладка** - централизованное логирование и трассировка запросов через все микросервисы.

4. **Аутентификация и авторизация** - использование JWT-токенов и централизованного сервиса Identity для управления доступом.

5. **Сетевые задержки** - оптимизация взаимодействия между микросервисами и использование кэширования.

6. **Оценка ответов на открытые вопросы** - использование ИИ для автоматической оценки текстовых ответов и кода.
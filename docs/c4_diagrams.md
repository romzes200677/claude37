# Диаграммы C4 для AiTestPlatform

В этом документе представлены диаграммы C4 для визуализации архитектуры проекта AiTestPlatform.

## Содержание

1. [Контекстная диаграмма (C1)](#контекстная-диаграмма-c1)
2. [Диаграмма контейнеров (C2)](#диаграмма-контейнеров-c2)
3. [Диаграмма компонентов (C3)](#диаграмма-компонентов-c3)
   - [Микросервис Identity](#микросервис-identity)
   - [Микросервис Courses](#микросервис-courses)
   - [Микросервис Testing](#микросервис-testing)
   - [Микросервис CodeExecution](#микросервис-codeexecution)

## Контекстная диаграмма (C1)

```mermaid
C4Context
    title Контекстная диаграмма AiTestPlatform
    
    Person(student, "Студент", "Пользователь, который проходит курсы и тесты")
    Person(teacher, "Преподаватель", "Пользователь, который создает курсы и тесты")
    Person(admin, "Администратор", "Пользователь, который управляет системой")
    
    System(aiTestPlatform, "AiTestPlatform", "Платформа для обучения программированию с использованием ИИ")
    
    System_Ext(emailSystem, "Система электронной почты", "Отправляет уведомления пользователям")
    System_Ext(paymentSystem, "Платежная система", "Обрабатывает платежи за курсы")
    System_Ext(aiService, "Сервис ИИ", "Предоставляет рекомендации и анализирует код")
    
    Rel(student, aiTestPlatform, "Проходит курсы, выполняет задания и тесты")
    Rel(teacher, aiTestPlatform, "Создает и управляет курсами, заданиями и тестами")
    Rel(admin, aiTestPlatform, "Управляет пользователями и системой")
    
    Rel(aiTestPlatform, emailSystem, "Отправляет уведомления")
    Rel(aiTestPlatform, paymentSystem, "Обрабатывает платежи")
    Rel(aiTestPlatform, aiService, "Получает рекомендации и анализ кода")
```

## Диаграмма контейнеров (C2)

```mermaid
C4Container
    title Диаграмма контейнеров AiTestPlatform
    
    Person(student, "Студент", "Пользователь, который проходит курсы и тесты")
    Person(teacher, "Преподаватель", "Пользователь, который создает курсы и тесты")
    Person(admin, "Администратор", "Пользователь, который управляет системой")
    
    System_Boundary(aiTestPlatform, "AiTestPlatform") {
        Container(webApp, "Web-приложение", "React, TypeScript", "Предоставляет пользовательский интерфейс для взаимодействия с системой")
        Container(mobileApp, "Мобильное приложение", "React Native", "Мобильный интерфейс для студентов")
        
        Container(apiGateway, "API Gateway", "ASP.NET Core", "Единая точка входа для всех клиентских запросов")
        
        Container(identityService, "Микросервис Identity", "ASP.NET Core", "Управление пользователями, аутентификация и авторизация")
        Container(coursesService, "Микросервис Courses", "ASP.NET Core", "Управление курсами, модулями и уроками")
        Container(testingService, "Микросервис Testing", "ASP.NET Core", "Управление тестами и оценками")
        Container(codeExecService, "Микросервис CodeExecution", "ASP.NET Core", "Выполнение и проверка кода")
        
        ContainerDb(identityDb, "База данных Identity", "PostgreSQL", "Хранит данные пользователей")
        ContainerDb(coursesDb, "База данных Courses", "PostgreSQL", "Хранит данные курсов")
        ContainerDb(testingDb, "База данных Testing", "PostgreSQL", "Хранит данные тестов")
        ContainerDb(codeExecDb, "База данных CodeExecution", "PostgreSQL", "Хранит данные задач и решений")
        
        Container(eventBus, "Event Bus", "Kafka", "Обеспечивает асинхронное взаимодействие между микросервисами")
        Container(fileStorage, "File Storage", "MinIO", "Хранит файлы и материалы курсов")
    }
    
    System_Ext(emailSystem, "Система электронной почты", "Отправляет уведомления пользователям")
    System_Ext(paymentSystem, "Платежная система", "Обрабатывает платежи за курсы")
    System_Ext(aiService, "Сервис ИИ", "Предоставляет рекомендации и анализирует код")
    
    Rel(student, webApp, "Использует", "HTTPS")
    Rel(student, mobileApp, "Использует", "HTTPS")
    Rel(teacher, webApp, "Использует", "HTTPS")
    Rel(admin, webApp, "Использует", "HTTPS")
    
    Rel(webApp, apiGateway, "Отправляет запросы", "HTTPS/JSON")
    Rel(mobileApp, apiGateway, "Отправляет запросы", "HTTPS/JSON")
    
    Rel(apiGateway, identityService, "Перенаправляет запросы", "HTTPS/JSON")
    Rel(apiGateway, coursesService, "Перенаправляет запросы", "HTTPS/JSON")
    Rel(apiGateway, testingService, "Перенаправляет запросы", "HTTPS/JSON")
    Rel(apiGateway, codeExecService, "Перенаправляет запросы", "HTTPS/JSON")
    
    Rel(identityService, identityDb, "Читает/пишет", "SQL/TCP")
    Rel(coursesService, coursesDb, "Читает/пишет", "SQL/TCP")
    Rel(testingService, testingDb, "Читает/пишет", "SQL/TCP")
    Rel(codeExecService, codeExecDb, "Читает/пишет", "SQL/TCP")
    
    Rel(identityService, eventBus, "Публикует/подписывается", "TCP")
    Rel(coursesService, eventBus, "Публикует/подписывается", "TCP")
    Rel(testingService, eventBus, "Публикует/подписывается", "TCP")
    Rel(codeExecService, eventBus, "Публикует/подписывается", "TCP")
    
    Rel(coursesService, fileStorage, "Хранит/получает файлы", "HTTPS")
    Rel(testingService, fileStorage, "Хранит/получает файлы", "HTTPS")
    
    Rel(identityService, emailSystem, "Отправляет уведомления", "SMTP")
    Rel(coursesService, paymentSystem, "Обрабатывает платежи", "HTTPS/JSON")
    Rel(codeExecService, aiService, "Получает рекомендации", "HTTPS/JSON")
```

## Диаграмма компонентов (C3)

### Микросервис Identity

```mermaid
C4Component
    title Диаграмма компонентов микросервиса Identity
    
    Container_Boundary(identityService, "Микросервис Identity") {
        Component(identityApi, "API Controller", "ASP.NET Core", "Обрабатывает HTTP-запросы")
        Component(authService, "Authentication Service", "C#", "Аутентификация пользователей и выдача токенов")
        Component(userService, "User Service", "C#", "Управление пользователями")
        Component(roleService, "Role Service", "C#", "Управление ролями и разрешениями")
        Component(profileService, "Profile Service", "C#", "Управление профилями пользователей")
        Component(achievementService, "Achievement Service", "C#", "Управление достижениями пользователей")
        Component(eventHandler, "Event Handler", "C#", "Обработка событий из других микросервисов")
        Component(identityRepository, "Repository", "Entity Framework Core", "Доступ к данным")
    }
    
    ContainerDb(identityDb, "База данных Identity", "PostgreSQL", "Хранит данные пользователей")
    Container(eventBus, "Event Bus", "Kafka", "Обеспечивает асинхронное взаимодействие между микросервисами")
    Container(apiGateway, "API Gateway", "ASP.NET Core", "Единая точка входа для всех клиентских запросов")
    
    Rel(apiGateway, identityApi, "Отправляет запросы", "HTTPS/JSON")
    
    Rel(identityApi, authService, "Использует")
    Rel(identityApi, userService, "Использует")
    Rel(identityApi, roleService, "Использует")
    Rel(identityApi, profileService, "Использует")
    
    Rel(userService, identityRepository, "Использует")
    Rel(roleService, identityRepository, "Использует")
    Rel(profileService, identityRepository, "Использует")
    Rel(achievementService, identityRepository, "Использует")
    Rel(authService, identityRepository, "Использует")
    
    Rel(identityRepository, identityDb, "Читает/пишет", "SQL/TCP")
    
    Rel(eventHandler, eventBus, "Подписывается", "TCP")
    Rel(userService, eventBus, "Публикует события", "TCP")
    Rel(eventHandler, achievementService, "Использует")
```

### Микросервис Courses

```mermaid
C4Component
    title Диаграмма компонентов микросервиса Courses
    
    Container_Boundary(coursesService, "Микросервис Courses") {
        Component(coursesApi, "API Controller", "ASP.NET Core", "Обрабатывает HTTP-запросы")
        Component(courseService, "Course Service", "C#", "Управление курсами")
        Component(moduleService, "Module Service", "C#", "Управление модулями")
        Component(lessonService, "Lesson Service", "C#", "Управление уроками")
        Component(enrollmentService, "Enrollment Service", "C#", "Управление записями на курсы")
        Component(progressService, "Progress Service", "C#", "Отслеживание прогресса студентов")
        Component(feedbackService, "Feedback Service", "C#", "Управление отзывами")
        Component(notificationService, "Notification Service", "C#", "Управление уведомлениями")
        Component(surveyService, "Survey Service", "C#", "Управление опросами")
        Component(certificateService, "Certificate Service", "C#", "Управление сертификатами")
        Component(coursesEventHandler, "Event Handler", "C#", "Обработка событий из других микросервисов")
        Component(coursesRepository, "Repository", "Entity Framework Core", "Доступ к данным")
    }
    
    ContainerDb(coursesDb, "База данных Courses", "PostgreSQL", "Хранит данные курсов")
    Container(eventBus, "Event Bus", "Kafka", "Обеспечивает асинхронное взаимодействие между микросервисами")
    Container(apiGateway, "API Gateway", "ASP.NET Core", "Единая точка входа для всех клиентских запросов")
    Container(fileStorage, "File Storage", "MinIO", "Хранит файлы и материалы курсов")
    
    Rel(apiGateway, coursesApi, "Отправляет запросы", "HTTPS/JSON")
    
    Rel(coursesApi, courseService, "Использует")
    Rel(coursesApi, moduleService, "Использует")
    Rel(coursesApi, lessonService, "Использует")
    Rel(coursesApi, enrollmentService, "Использует")
    Rel(coursesApi, progressService, "Использует")
    Rel(coursesApi, feedbackService, "Использует")
    Rel(coursesApi, notificationService, "Использует")
    Rel(coursesApi, surveyService, "Использует")
    Rel(coursesApi, certificateService, "Использует")
    
    Rel(courseService, coursesRepository, "Использует")
    Rel(moduleService, coursesRepository, "Использует")
    Rel(lessonService, coursesRepository, "Использует")
    Rel(enrollmentService, coursesRepository, "Использует")
    Rel(progressService, coursesRepository, "Использует")
    Rel(feedbackService, coursesRepository, "Использует")
    Rel(notificationService, coursesRepository, "Использует")
    Rel(surveyService, coursesRepository, "Использует")
    Rel(certificateService, coursesRepository, "Использует")
    
    Rel(coursesRepository, coursesDb, "Читает/пишет", "SQL/TCP")
    
    Rel(lessonService, fileStorage, "Хранит/получает файлы", "HTTPS")
    Rel(certificateService, fileStorage, "Хранит/получает файлы", "HTTPS")
    
    Rel(coursesEventHandler, eventBus, "Подписывается", "TCP")
    Rel(courseService, eventBus, "Публикует события", "TCP")
    Rel(enrollmentService, eventBus, "Публикует события", "TCP")
    Rel(progressService, eventBus, "Публикует события", "TCP")
    Rel(certificateService, eventBus, "Публикует события", "TCP")
    
    Rel(coursesEventHandler, progressService, "Использует")
    Rel(coursesEventHandler, enrollmentService, "Использует")
```

### Микросервис Testing

```mermaid
C4Component
    title Диаграмма компонентов микросервиса Testing
    
    Container_Boundary(testingService, "Микросервис Testing") {
        Component(testingApi, "API Controller", "ASP.NET Core", "Обрабатывает HTTP-запросы")
        Component(testTemplateService, "Test Template Service", "C#", "Управление шаблонами тестов")
        Component(testQuestionService, "Test Question Service", "C#", "Управление вопросами тестов")
        Component(testAttemptService, "Test Attempt Service", "C#", "Управление попытками прохождения тестов")
        Component(testEvaluationService, "Test Evaluation Service", "C#", "Оценка результатов тестов")
        Component(testCategoryService, "Test Category Service", "C#", "Управление категориями тестов")
        Component(testingEventHandler, "Event Handler", "C#", "Обработка событий из других микросервисов")
        Component(testingRepository, "Repository", "Entity Framework Core", "Доступ к данным")
    }
    
    ContainerDb(testingDb, "База данных Testing", "PostgreSQL", "Хранит данные тестов")
    Container(eventBus, "Event Bus", "Kafka", "Обеспечивает асинхронное взаимодействие между микросервисами")
    Container(apiGateway, "API Gateway", "ASP.NET Core", "Единая точка входа для всех клиентских запросов")
    Container(codeExecService, "Микросервис CodeExecution", "ASP.NET Core", "Выполнение и проверка кода")
    Container(fileStorage, "File Storage", "MinIO", "Хранит файлы и материалы тестов")
    
    Rel(apiGateway, testingApi, "Отправляет запросы", "HTTPS/JSON")
    
    Rel(testingApi, testTemplateService, "Использует")
    Rel(testingApi, testQuestionService, "Использует")
    Rel(testingApi, testAttemptService, "Использует")
    Rel(testingApi, testEvaluationService, "Использует")
    Rel(testingApi, testCategoryService, "Использует")
    
    Rel(testTemplateService, testingRepository, "Использует")
    Rel(testQuestionService, testingRepository, "Использует")
    Rel(testAttemptService, testingRepository, "Использует")
    Rel(testEvaluationService, testingRepository, "Использует")
    Rel(testCategoryService, testingRepository, "Использует")
    
    Rel(testingRepository, testingDb, "Читает/пишет", "SQL/TCP")
    
    Rel(testQuestionService, fileStorage, "Хранит/получает файлы", "HTTPS")
    
    Rel(testingEventHandler, eventBus, "Подписывается", "TCP")
    Rel(testAttemptService, eventBus, "Публикует события", "TCP")
    Rel(testEvaluationService, eventBus, "Публикует события", "TCP")
    
    Rel(testEvaluationService, codeExecService, "Запрашивает выполнение кода", "HTTPS/JSON")
```

### Микросервис CodeExecution

```mermaid
C4Component
    title Диаграмма компонентов микросервиса CodeExecution
    
    Container_Boundary(codeExecService, "Микросервис CodeExecution") {
        Component(codeExecApi, "API Controller", "ASP.NET Core", "Обрабатывает HTTP-запросы")
        Component(codeTaskService, "Code Task Service", "C#", "Управление задачами по программированию")
        Component(codeSubmissionService, "Code Submission Service", "C#", "Управление отправками кода")
        Component(codeExecutionService, "Code Execution Service", "C#", "Выполнение кода в изолированной среде")
        Component(codeTestCaseService, "Code Test Case Service", "C#", "Управление тестовыми случаями")
        Component(codeAnalysisService, "Code Analysis Service", "C#", "Анализ кода и предоставление рекомендаций")
        Component(codeExecEventHandler, "Event Handler", "C#", "Обработка событий из других микросервисов")
        Component(codeExecRepository, "Repository", "Entity Framework Core", "Доступ к данным")
    }
    
    ContainerDb(codeExecDb, "База данных CodeExecution", "PostgreSQL", "Хранит данные задач и решений")
    Container(eventBus, "Event Bus", "Kafka", "Обеспечивает асинхронное взаимодействие между микросервисами")
    Container(apiGateway, "API Gateway", "ASP.NET Core", "Единая точка входа для всех клиентских запросов")
    System_Ext(aiService, "Сервис ИИ", "Предоставляет рекомендации и анализирует код")
    
    Rel(apiGateway, codeExecApi, "Отправляет запросы", "HTTPS/JSON")
    
    Rel(codeExecApi, codeTaskService, "Использует")
    Rel(codeExecApi, codeSubmissionService, "Использует")
    Rel(codeExecApi, codeExecutionService, "Использует")
    Rel(codeExecApi, codeTestCaseService, "Использует")
    Rel(codeExecApi, codeAnalysisService, "Использует")
    
    Rel(codeTaskService, codeExecRepository, "Использует")
    Rel(codeSubmissionService, codeExecRepository, "Использует")
    Rel(codeTestCaseService, codeExecRepository, "Использует")
    
    Rel(codeExecRepository, codeExecDb, "Читает/пишет", "SQL/TCP")
    
    Rel(codeExecEventHandler, eventBus, "Подписывается", "TCP")
    Rel(codeSubmissionService, eventBus, "Публикует события", "TCP")
    
    Rel(codeAnalysisService, aiService, "Запрашивает анализ кода", "HTTPS/JSON")
```

## Диаграмма кода (C4)

Пример диаграммы кода для класса `Feedback` в микросервисе Courses:

```mermaid
classDiagram
    class BaseEntity {
        +Guid Id
        +DateTime CreatedAt
        +DateTime? UpdatedAt
        +bool IsDeleted
    }
    
    class Feedback {
        +Guid UserId
        +string UserName
        +string UserEmail
        +FeedbackType Type
        +string Subject
        +string Message
        +FeedbackStatus Status
        +FeedbackPriority Priority
        +Guid? AssigneeId
        +string ResponseMessage
        +DateTime? ResponseDate
        +Guid? RelatedEntityId
        +string RelatedEntityType
        +string AttachmentUrl
        +int? SatisfactionRating
        +List~FeedbackComment~ Comments
    }
    
    class FeedbackComment {
        +Guid FeedbackId
        +Guid UserId
        +string UserName
        +string Text
        +bool IsInternal
        +Feedback Feedback
    }
    
    class FeedbackType {
        <<enumeration>>
        BUG
        FEATURE_REQUEST
        GENERAL_INQUIRY
        CONTENT_ISSUE
        TECHNICAL_SUPPORT
    }
    
    class FeedbackStatus {
        <<enumeration>>
        NEW
        IN_PROGRESS
        RESOLVED
        CLOSED
        REOPENED
    }
    
    class FeedbackPriority {
        <<enumeration>>
        LOW
        MEDIUM
        HIGH
        CRITICAL
    }
    
    BaseEntity <|-- Feedback
    BaseEntity <|-- FeedbackComment
    Feedback "1" *-- "many" FeedbackComment
    Feedback -- FeedbackType
    Feedback -- FeedbackStatus
    Feedback -- FeedbackPriority
```

## Заключение

Диаграммы C4 предоставляют четкое представление об архитектуре системы AiTestPlatform на разных уровнях абстракции:

1. **Контекстная диаграмма (C1)** показывает систему в целом и ее взаимодействие с внешними системами и пользователями.
2. **Диаграмма контейнеров (C2)** детализирует систему до уровня контейнеров (микросервисов, баз данных, клиентских приложений).
3. **Диаграмма компонентов (C3)** показывает внутреннюю структуру каждого микросервиса.
4. **Диаграмма кода (C4)** представляет структуру классов и их взаимосвязи.

Эти диаграммы помогают разработчикам и заинтересованным сторонам понять архитектуру системы и принципы ее работы.
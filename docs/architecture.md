# Архитектура проекта AiTestPlatform

## Содержание

1. [Общее описание](#общее-описание)
2. [Системный контекст (C4 модель)](#системный-контекст-c4-модель)
3. [Контейнеры (C4 модель)](#контейнеры-c4-модель)
4. [Компоненты (C4 модель)](#компоненты-c4-модель)
5. [Взаимодействие микросервисов](#взаимодействие-микросервисов)
6. [Структура данных](#структура-данных)
7. [Технологический стек](#технологический-стек)

## Общее описание

AiTestPlatform - это комплексная платформа для тестирования с использованием искусственного интеллекта, построенная на микросервисной архитектуре. Система предназначена для создания, управления и прохождения образовательных курсов с интегрированной системой тестирования и выполнения кода.

## Системный контекст (C4 модель)

```mermaid
C4Context
    title Системный контекст AiTestPlatform
    
    Person(student, "Студент", "Пользователь, который проходит курсы и тесты")
    Person(instructor, "Преподаватель", "Пользователь, который создает и управляет курсами и тестами")
    Person(admin, "Администратор", "Пользователь, который управляет системой")
    
    System(aiTestPlatform, "AiTestPlatform", "Платформа для тестирования с использованием ИИ")
    
    System_Ext(emailSystem, "Email система", "Система для отправки электронных писем")
    System_Ext(paymentSystem, "Платежная система", "Система для обработки платежей")
    System_Ext(aiService, "ИИ сервис", "Внешний сервис ИИ для анализа и оценки")
    
    Rel(student, aiTestPlatform, "Проходит курсы и тесты")
    Rel(instructor, aiTestPlatform, "Создает и управляет курсами и тестами")
    Rel(admin, aiTestPlatform, "Управляет системой")
    
    Rel(aiTestPlatform, emailSystem, "Отправляет уведомления")
    Rel(aiTestPlatform, paymentSystem, "Обрабатывает платежи")
    Rel(aiTestPlatform, aiService, "Использует для анализа и оценки")
```

## Контейнеры (C4 модель)

```mermaid
C4Container
    title Контейнеры AiTestPlatform
    
    Person(student, "Студент", "Пользователь, который проходит курсы и тесты")
    Person(instructor, "Преподаватель", "Пользователь, который создает и управляет курсами и тестами")
    Person(admin, "Администратор", "Пользователь, который управляет системой")
    
    System_Boundary(aiTestPlatform, "AiTestPlatform") {
        Container(webApp, "Веб-приложение", "React", "Пользовательский интерфейс системы")
        
        Container(identityService, "Identity Service", ".NET Core", "Управление пользователями, аутентификация и авторизация")
        Container(coursesService, "Courses Service", ".NET Core", "Управление курсами, модулями и уроками")
        Container(testingService, "Testing Service", ".NET Core", "Создание и проведение тестов, оценка результатов")
        Container(codeExecutionService, "CodeExecution Service", ".NET Core", "Выполнение и проверка кода")
        
        Container(eventBus, "Event Bus", "Kafka", "Обмен сообщениями между микросервисами")
        
        ContainerDb(identityDb, "Identity DB", "PostgreSQL", "Хранение данных пользователей")
        ContainerDb(coursesDb, "Courses DB", "PostgreSQL", "Хранение данных курсов")
        ContainerDb(testingDb, "Testing DB", "PostgreSQL", "Хранение данных тестов")
        ContainerDb(codeExecutionDb, "CodeExecution DB", "PostgreSQL", "Хранение данных выполнения кода")
    }
    
    System_Ext(emailSystem, "Email система", "Система для отправки электронных писем")
    System_Ext(paymentSystem, "Платежная система", "Система для обработки платежей")
    System_Ext(aiService, "ИИ сервис", "Внешний сервис ИИ для анализа и оценки")
    
    Rel(student, webApp, "Использует", "HTTPS")
    Rel(instructor, webApp, "Использует", "HTTPS")
    Rel(admin, webApp, "Использует", "HTTPS")
    
    Rel(webApp, identityService, "Вызывает API", "HTTPS/JSON")
    Rel(webApp, coursesService, "Вызывает API", "HTTPS/JSON")
    Rel(webApp, testingService, "Вызывает API", "HTTPS/JSON")
    Rel(webApp, codeExecutionService, "Вызывает API", "HTTPS/JSON")
    
    Rel(identityService, identityDb, "Читает/Пишет", "SQL/TCP")
    Rel(coursesService, coursesDb, "Читает/Пишет", "SQL/TCP")
    Rel(testingService, testingDb, "Читает/Пишет", "SQL/TCP")
    Rel(codeExecutionService, codeExecutionDb, "Читает/Пишет", "SQL/TCP")
    
    Rel(identityService, eventBus, "Публикует/Подписывается", "TCP")
    Rel(coursesService, eventBus, "Публикует/Подписывается", "TCP")
    Rel(testingService, eventBus, "Публикует/Подписывается", "TCP")
    Rel(codeExecutionService, eventBus, "Публикует/Подписывается", "TCP")
    
    Rel(coursesService, emailSystem, "Отправляет уведомления", "SMTP")
    Rel(coursesService, paymentSystem, "Обрабатывает платежи", "HTTPS/JSON")
    Rel(testingService, aiService, "Использует для анализа и оценки", "HTTPS/JSON")
    Rel(codeExecutionService, aiService, "Использует для анализа и оценки", "HTTPS/JSON")
```

## Компоненты (C4 модель)

### Identity Service

```mermaid
C4Component
    title Компоненты Identity Service
    
    Container_Boundary(identityService, "Identity Service") {
        Component(authController, "Auth Controller", ".NET Core", "Обрабатывает запросы аутентификации и авторизации")
        Component(userController, "User Controller", ".NET Core", "Обрабатывает запросы управления пользователями")
        Component(roleController, "Role Controller", ".NET Core", "Обрабатывает запросы управления ролями")
        
        Component(authService, "Auth Service", ".NET Core", "Реализует логику аутентификации и авторизации")
        Component(userService, "User Service", ".NET Core", "Реализует логику управления пользователями")
        Component(roleService, "Role Service", ".NET Core", "Реализует логику управления ролями")
        
        Component(userRepository, "User Repository", ".NET Core", "Доступ к данным пользователей")
        Component(roleRepository, "Role Repository", ".NET Core", "Доступ к данным ролей")
        
        Component(eventPublisher, "Event Publisher", ".NET Core", "Публикация событий")
    }
    
    ContainerDb(identityDb, "Identity DB", "PostgreSQL", "Хранение данных пользователей")
    Container(eventBus, "Event Bus", "Kafka", "Обмен сообщениями между микросервисами")
    
    Rel(authController, authService, "Использует")
    Rel(userController, userService, "Использует")
    Rel(roleController, roleService, "Использует")
    
    Rel(authService, userRepository, "Использует")
    Rel(userService, userRepository, "Использует")
    Rel(roleService, roleRepository, "Использует")
    
    Rel(userRepository, identityDb, "Читает/Пишет", "SQL/TCP")
    Rel(roleRepository, identityDb, "Читает/Пишет", "SQL/TCP")
    
    Rel(authService, eventPublisher, "Публикует события")
    Rel(userService, eventPublisher, "Публикует события")
    Rel(roleService, eventPublisher, "Публикует события")
    
    Rel(eventPublisher, eventBus, "Публикует", "TCP")
```

### Courses Service

```mermaid
C4Component
    title Компоненты Courses Service
    
    Container_Boundary(coursesService, "Courses Service") {
        Component(courseController, "Course Controller", ".NET Core", "Обрабатывает запросы управления курсами")
        Component(moduleController, "Module Controller", ".NET Core", "Обрабатывает запросы управления модулями")
        Component(lessonController, "Lesson Controller", ".NET Core", "Обрабатывает запросы управления уроками")
        Component(enrollmentController, "Enrollment Controller", ".NET Core", "Обрабатывает запросы управления записями на курсы")
        Component(feedbackController, "Feedback Controller", ".NET Core", "Обрабатывает запросы управления обратной связью")
        Component(notificationController, "Notification Controller", ".NET Core", "Обрабатывает запросы управления уведомлениями")
        Component(certificateController, "Certificate Controller", ".NET Core", "Обрабатывает запросы управления сертификатами")
        
        Component(courseService, "Course Service", ".NET Core", "Реализует логику управления курсами")
        Component(moduleService, "Module Service", ".NET Core", "Реализует логику управления модулями")
        Component(lessonService, "Lesson Service", ".NET Core", "Реализует логику управления уроками")
        Component(enrollmentService, "Enrollment Service", ".NET Core", "Реализует логику управления записями на курсы")
        Component(feedbackService, "Feedback Service", ".NET Core", "Реализует логику управления обратной связью")
        Component(notificationService, "Notification Service", ".NET Core", "Реализует логику управления уведомлениями")
        Component(certificateService, "Certificate Service", ".NET Core", "Реализует логику управления сертификатами")
        
        Component(courseRepository, "Course Repository", ".NET Core", "Доступ к данным курсов")
        Component(moduleRepository, "Module Repository", ".NET Core", "Доступ к данным модулей")
        Component(lessonRepository, "Lesson Repository", ".NET Core", "Доступ к данным уроков")
        Component(enrollmentRepository, "Enrollment Repository", ".NET Core", "Доступ к данным записей на курсы")
        Component(feedbackRepository, "Feedback Repository", ".NET Core", "Доступ к данным обратной связи")
        Component(notificationRepository, "Notification Repository", ".NET Core", "Доступ к данным уведомлений")
        Component(certificateRepository, "Certificate Repository", ".NET Core", "Доступ к данным сертификатов")
        
        Component(eventPublisher, "Event Publisher", ".NET Core", "Публикация событий")
        Component(eventConsumer, "Event Consumer", ".NET Core", "Потребление событий")
        
        Component(fileStorageService, "File Storage Service", ".NET Core", "Управление файлами")
        Component(recommendationService, "Recommendation Service", ".NET Core", "Рекомендации курсов")
    }
    
    ContainerDb(coursesDb, "Courses DB", "PostgreSQL", "Хранение данных курсов")
    Container(eventBus, "Event Bus", "Kafka", "Обмен сообщениями между микросервисами")
    System_Ext(emailSystem, "Email система", "Система для отправки электронных писем")
    
    Rel(courseController, courseService, "Использует")
    Rel(moduleController, moduleService, "Использует")
    Rel(lessonController, lessonService, "Использует")
    Rel(enrollmentController, enrollmentService, "Использует")
    Rel(feedbackController, feedbackService, "Использует")
    Rel(notificationController, notificationService, "Использует")
    Rel(certificateController, certificateService, "Использует")
    
    Rel(courseService, courseRepository, "Использует")
    Rel(moduleService, moduleRepository, "Использует")
    Rel(lessonService, lessonRepository, "Использует")
    Rel(enrollmentService, enrollmentRepository, "Использует")
    Rel(feedbackService, feedbackRepository, "Использует")
    Rel(notificationService, notificationRepository, "Использует")
    Rel(certificateService, certificateRepository, "Использует")
    
    Rel(courseRepository, coursesDb, "Читает/Пишет", "SQL/TCP")
    Rel(moduleRepository, coursesDb, "Читает/Пишет", "SQL/TCP")
    Rel(lessonRepository, coursesDb, "Читает/Пишет", "SQL/TCP")
    Rel(enrollmentRepository, coursesDb, "Читает/Пишет", "SQL/TCP")
    Rel(feedbackRepository, coursesDb, "Читает/Пишет", "SQL/TCP")
    Rel(notificationRepository, coursesDb, "Читает/Пишет", "SQL/TCP")
    Rel(certificateRepository, coursesDb, "Читает/Пишет", "SQL/TCP")
    
    Rel(courseService, eventPublisher, "Публикует события")
    Rel(enrollmentService, eventPublisher, "Публикует события")
    Rel(certificateService, eventPublisher, "Публикует события")
    
    Rel(eventPublisher, eventBus, "Публикует", "TCP")
    Rel(eventBus, eventConsumer, "Потребляет", "TCP")
    
    Rel(notificationService, emailSystem, "Отправляет уведомления", "SMTP")
    
    Rel(courseService, fileStorageService, "Использует")
    Rel(lessonService, fileStorageService, "Использует")
    Rel(certificateService, fileStorageService, "Использует")
    
    Rel(courseService, recommendationService, "Использует")
```

### Testing Service

```mermaid
C4Component
    title Компоненты Testing Service
    
    Container_Boundary(testingService, "Testing Service") {
        Component(testController, "Test Controller", ".NET Core", "Обрабатывает запросы управления тестами")
        Component(testQuestionController, "Test Question Controller", ".NET Core", "Обрабатывает запросы управления вопросами тестов")
        Component(testAttemptController, "Test Attempt Controller", ".NET Core", "Обрабатывает запросы управления попытками прохождения тестов")
        
        Component(testService, "Test Service", ".NET Core", "Реализует логику управления тестами")
        Component(testQuestionService, "Test Question Service", ".NET Core", "Реализует логику управления вопросами тестов")
        Component(testAttemptService, "Test Attempt Service", ".NET Core", "Реализует логику управления попытками прохождения тестов")
        Component(testEvaluationService, "Test Evaluation Service", ".NET Core", "Реализует логику оценки тестов")
        
        Component(testRepository, "Test Repository", ".NET Core", "Доступ к данным тестов")
        Component(testQuestionRepository, "Test Question Repository", ".NET Core", "Доступ к данным вопросов тестов")
        Component(testAttemptRepository, "Test Attempt Repository", ".NET Core", "Доступ к данным попыток прохождения тестов")
        
        Component(eventPublisher, "Event Publisher", ".NET Core", "Публикация событий")
        Component(eventConsumer, "Event Consumer", ".NET Core", "Потребление событий")
    }
    
    ContainerDb(testingDb, "Testing DB", "PostgreSQL", "Хранение данных тестов")
    Container(eventBus, "Event Bus", "Kafka", "Обмен сообщениями между микросервисами")
    System_Ext(aiService, "ИИ сервис", "Внешний сервис ИИ для анализа и оценки")
    
    Rel(testController, testService, "Использует")
    Rel(testQuestionController, testQuestionService, "Использует")
    Rel(testAttemptController, testAttemptService, "Использует")
    
    Rel(testService, testRepository, "Использует")
    Rel(testQuestionService, testQuestionRepository, "Использует")
    Rel(testAttemptService, testAttemptRepository, "Использует")
    Rel(testAttemptService, testEvaluationService, "Использует")
    
    Rel(testRepository, testingDb, "Читает/Пишет", "SQL/TCP")
    Rel(testQuestionRepository, testingDb, "Читает/Пишет", "SQL/TCP")
    Rel(testAttemptRepository, testingDb, "Читает/Пишет", "SQL/TCP")
    
    Rel(testService, eventPublisher, "Публикует события")
    Rel(testAttemptService, eventPublisher, "Публикует события")
    
    Rel(eventPublisher, eventBus, "Публикует", "TCP")
    Rel(eventBus, eventConsumer, "Потребляет", "TCP")
    
    Rel(testEvaluationService, aiService, "Использует для анализа и оценки", "HTTPS/JSON")
```

### CodeExecution Service

```mermaid
C4Component
    title Компоненты CodeExecution Service
    
    Container_Boundary(codeExecutionService, "CodeExecution Service") {
        Component(codeTaskController, "Code Task Controller", ".NET Core", "Обрабатывает запросы управления заданиями на написание кода")
        Component(codeSubmissionController, "Code Submission Controller", ".NET Core", "Обрабатывает запросы управления отправленными решениями")
        
        Component(codeTaskService, "Code Task Service", ".NET Core", "Реализует логику управления заданиями на написание кода")
        Component(codeSubmissionService, "Code Submission Service", ".NET Core", "Реализует логику управления отправленными решениями")
        Component(codeExecutionService, "Code Execution Service", ".NET Core", "Реализует логику выполнения кода")
        Component(codeEvaluationService, "Code Evaluation Service", ".NET Core", "Реализует логику оценки кода")
        
        Component(codeTaskRepository, "Code Task Repository", ".NET Core", "Доступ к данным заданий на написание кода")
        Component(codeSubmissionRepository, "Code Submission Repository", ".NET Core", "Доступ к данным отправленных решений")
        
        Component(eventPublisher, "Event Publisher", ".NET Core", "Публикация событий")
        Component(eventConsumer, "Event Consumer", ".NET Core", "Потребление событий")
        
        Component(sandboxService, "Sandbox Service", ".NET Core", "Безопасное выполнение кода в изолированной среде")
    }
    
    ContainerDb(codeExecutionDb, "CodeExecution DB", "PostgreSQL", "Хранение данных выполнения кода")
    Container(eventBus, "Event Bus", "Kafka", "Обмен сообщениями между микросервисами")
    System_Ext(aiService, "ИИ сервис", "Внешний сервис ИИ для анализа и оценки")
    
    Rel(codeTaskController, codeTaskService, "Использует")
    Rel(codeSubmissionController, codeSubmissionService, "Использует")
    
    Rel(codeTaskService, codeTaskRepository, "Использует")
    Rel(codeSubmissionService, codeSubmissionRepository, "Использует")
    Rel(codeSubmissionService, codeExecutionService, "Использует")
    Rel(codeExecutionService, codeEvaluationService, "Использует")
    
    Rel(codeTaskRepository, codeExecutionDb, "Читает/Пишет", "SQL/TCP")
    Rel(codeSubmissionRepository, codeExecutionDb, "Читает/Пишет", "SQL/TCP")
    
    Rel(codeSubmissionService, eventPublisher, "Публикует события")
    
    Rel(eventPublisher, eventBus, "Публикует", "TCP")
    Rel(eventBus, eventConsumer, "Потребляет", "TCP")
    
    Rel(codeExecutionService, sandboxService, "Использует")
    Rel(codeEvaluationService, aiService, "Использует для анализа и оценки", "HTTPS/JSON")
```

## Взаимодействие микросервисов

```mermaid
sequenceDiagram
    participant User as Пользователь
    participant WebApp as Веб-приложение
    participant Identity as Identity Service
    participant Courses as Courses Service
    participant Testing as Testing Service
    participant CodeExecution as CodeExecution Service
    participant EventBus as Event Bus
    
    User->>WebApp: Вход в систему
    WebApp->>Identity: Аутентификация
    Identity-->>WebApp: Токен доступа
    
    User->>WebApp: Запрос списка курсов
    WebApp->>Courses: Получение списка курсов
    Courses-->>WebApp: Список курсов
    
    User->>WebApp: Запись на курс
    WebApp->>Courses: Запись на курс
    Courses->>EventBus: Публикация события CourseEnrollmentEvent
    Courses-->>WebApp: Подтверждение записи
    
    EventBus->>Identity: Уведомление о записи на курс
    EventBus->>Testing: Уведомление о записи на курс
    
    User->>WebApp: Прохождение теста
    WebApp->>Testing: Получение теста
    Testing-->>WebApp: Тест
    WebApp->>Testing: Отправка ответов
    Testing-->>WebApp: Результаты теста
    Testing->>EventBus: Публикация события TestCompletedEvent
    
    EventBus->>Courses: Уведомление о завершении теста
    
    User->>WebApp: Выполнение задания на написание кода
    WebApp->>CodeExecution: Получение задания
    CodeExecution-->>WebApp: Задание
    WebApp->>CodeExecution: Отправка решения
    CodeExecution-->>WebApp: Результаты выполнения
    CodeExecution->>EventBus: Публикация события CodeSubmissionEvent
    
    EventBus->>Courses: Уведомление о выполнении задания
    
    User->>WebApp: Завершение курса
    WebApp->>Courses: Проверка завершения курса
    Courses->>EventBus: Публикация события CourseCompletedEvent
    Courses-->>WebApp: Подтверждение завершения
    
    EventBus->>Identity: Уведомление о завершении курса
```

## Структура данных

```mermaid
erDiagram
    User ||--o{ Enrollment : "записывается"
    User ||--o{ Feedback : "оставляет"
    User ||--o{ Notification : "получает"
    User ||--o{ NotificationSettings : "настраивает"
    User ||--o{ TestAttempt : "выполняет"
    User ||--o{ CodeSubmission : "отправляет"
    
    Course ||--|{ Module : "содержит"
    Course ||--o{ Enrollment : "имеет"
    Course ||--o{ Review : "имеет"
    Course ||--o{ Survey : "имеет"
    
    Module ||--|{ Lesson : "содержит"
    
    Lesson ||--|{ LessonMaterial : "содержит"
    Lesson ||--o{ LessonProgress : "имеет"
    
    Enrollment ||--o{ LessonProgress : "отслеживает"
    Enrollment ||--o{ Certificate : "получает"
    
    Feedback ||--|{ FeedbackComment : "имеет"
    
    Survey ||--|{ SurveyQuestion : "содержит"
    Survey ||--o{ SurveyResponse : "имеет"
    
    SurveyQuestion ||--|{ SurveyQuestionOption : "имеет"
    SurveyQuestion ||--o{ SurveyQuestionResponse : "имеет"
    
    SurveyResponse ||--|{ SurveyQuestionResponse : "содержит"
    
    Test ||--|{ TestQuestion : "содержит"
    Test ||--o{ TestAttempt : "имеет"
    
    TestQuestion ||--|{ TestQuestionOption : "имеет"
    TestQuestion ||--o{ TestQuestionResponse : "имеет"
    
    TestAttempt ||--|{ TestQuestionResponse : "содержит"
    TestAttempt ||--o{ TestResult : "имеет"
    
    CodeTask ||--|{ CodeTestCase : "содержит"
    CodeTask ||--o{ CodeSubmission : "имеет"
    
    CodeSubmission ||--o{ CodeExecutionResult : "имеет"
```

## Технологический стек

```mermaid
mindmap
  root((AiTestPlatform))
    Backend
      .NET Core
        ASP.NET Core Web API
        Entity Framework Core
        Identity Server
        MediatR
        AutoMapper
        FluentValidation
    Frontend
      React
        Redux
        React Router
        Material UI
        Axios
        Formik
    Базы данных
      PostgreSQL
    Брокеры сообщений
      Kafka
    Контейнеризация
      Docker
      Docker Compose
    CI/CD
      GitHub Actions
    Мониторинг
      Prometheus
      Grafana
    Логирование
      Serilog
      Elasticsearch
      Kibana
```
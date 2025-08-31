# Диаграммы потоков данных AiTestPlatform

В этом документе представлены диаграммы потоков данных (DFD) для визуализации обработки информации в системе AiTestPlatform.

## Содержание

1. [Контекстная диаграмма (DFD уровня 0)](#контекстная-диаграмма-dfd-уровня-0)
2. [Диаграмма первого уровня (DFD уровня 1)](#диаграмма-первого-уровня-dfd-уровня-1)
3. [Детализированные диаграммы (DFD уровня 2)](#детализированные-диаграммы-dfd-уровня-2)
   - [Подсистема управления пользователями](#подсистема-управления-пользователями)
   - [Подсистема управления курсами](#подсистема-управления-курсами)
   - [Подсистема тестирования](#подсистема-тестирования)
   - [Подсистема выполнения кода](#подсистема-выполнения-кода)
4. [Диаграммы потоков данных для ключевых процессов](#диаграммы-потоков-данных-для-ключевых-процессов)
   - [Процесс регистрации и аутентификации](#процесс-регистрации-и-аутентификации)
   - [Процесс создания и публикации курса](#процесс-создания-и-публикации-курса)
   - [Процесс прохождения теста](#процесс-прохождения-теста)
   - [Процесс выполнения задания по программированию](#процесс-выполнения-задания-по-программированию)

## Контекстная диаграмма (DFD уровня 0)

```mermaid
flowchart TD
    User["Пользователи\n(Студенты, Преподаватели, Администраторы)"] <--> AiTestPlatform["AiTestPlatform\nСистема обучения программированию"] 
    AiTestPlatform <--> ExternalSystems["Внешние системы\n(Email, Платежи, ИИ-сервисы)"]    
```

## Диаграмма первого уровня (DFD уровня 1)

```mermaid
flowchart TD
    User["Пользователи\n(Студенты, Преподаватели, Администраторы)"] 
    
    subgraph AiTestPlatform["AiTestPlatform"]
        IdentitySystem["1.0\nПодсистема управления\nпользователями"] 
        CourseSystem["2.0\nПодсистема управления\nкурсами"] 
        TestingSystem["3.0\nПодсистема\nтестирования"] 
        CodeExecSystem["4.0\nПодсистема\nвыполнения кода"] 
    end
    
    EmailSystem["Система электронной почты"] 
    PaymentSystem["Платежная система"] 
    AIService["ИИ-сервисы"] 
    
    %% Потоки данных от пользователей
    User <-- "Регистрация/Вход\nУправление профилем" --> IdentitySystem
    User <-- "Просмотр/Создание курсов\nЗапись на курсы" --> CourseSystem
    User <-- "Прохождение тестов\nСоздание тестов" --> TestingSystem
    User <-- "Отправка кода\nПолучение результатов" --> CodeExecSystem
    
    %% Потоки данных между подсистемами
    IdentitySystem <-- "Данные пользователей\nАутентификация" --> CourseSystem
    IdentitySystem <-- "Данные пользователей\nАутентификация" --> TestingSystem
    IdentitySystem <-- "Данные пользователей\nАутентификация" --> CodeExecSystem
    
    CourseSystem <-- "Данные курсов\nМодули/Уроки" --> TestingSystem
    TestingSystem <-- "Задания по программированию" --> CodeExecSystem
    
    %% Потоки данных с внешними системами
    IdentitySystem <-- "Уведомления" --> EmailSystem
    CourseSystem <-- "Уведомления" --> EmailSystem
    CourseSystem <-- "Платежи за курсы" --> PaymentSystem
    CodeExecSystem <-- "Анализ кода\nРекомендации" --> AIService
```

## Детализированные диаграммы (DFD уровня 2)

### Подсистема управления пользователями

```mermaid
flowchart TD
    User["Пользователи"]
    
    subgraph IdentitySystem["1.0 Подсистема управления пользователями"]
        Registration["1.1\nРегистрация\nпользователей"]
        Authentication["1.2\nАутентификация\nи авторизация"]
        ProfileManagement["1.3\nУправление\nпрофилями"]
        RoleManagement["1.4\nУправление\nролями"]
        AchievementManagement["1.5\nУправление\nдостижениями"]
    end
    
    UserDB[("База данных\nпользователей")]
    EmailSystem["Система\nэлектронной почты"]
    OtherSystems["Другие\nподсистемы"]
    
    %% Потоки данных от пользователей
    User <-- "Регистрационные данные" --> Registration
    User <-- "Учетные данные" --> Authentication
    User <-- "Данные профиля" --> ProfileManagement
    User <-- "Управление ролями" --> RoleManagement
    
    %% Потоки данных с базой данных
    Registration -- "Сохранение данных\nпользователя" --> UserDB
    Authentication -- "Проверка учетных\nданных" --> UserDB
    ProfileManagement -- "Чтение/Запись\nданных профиля" --> UserDB
    RoleManagement -- "Чтение/Запись\nданных ролей" --> UserDB
    AchievementManagement -- "Чтение/Запись\nданных достижений" --> UserDB
    
    %% Потоки данных с внешними системами
    Registration -- "Отправка письма\nподтверждения" --> EmailSystem
    Authentication -- "Токены доступа" --> OtherSystems
    ProfileManagement -- "Уведомления\nоб изменениях" --> EmailSystem
    AchievementManagement -- "Уведомления\nо достижениях" --> EmailSystem
    
    %% Внутренние потоки
    Registration -- "Данные нового\nпользователя" --> Authentication
    OtherSystems -- "События для\nдостижений" --> AchievementManagement
```

### Подсистема управления курсами

```mermaid
flowchart TD
    User["Пользователи"]
    
    subgraph CourseSystem["2.0 Подсистема управления курсами"]
        CourseManagement["2.1\nУправление\nкурсами"]
        ModuleManagement["2.2\nУправление\nмодулями"]
        LessonManagement["2.3\nУправление\nуроками"]
        EnrollmentManagement["2.4\nУправление\nзаписями"]
        ProgressTracking["2.5\nОтслеживание\nпрогресса"]
        FeedbackManagement["2.6\nУправление\nотзывами"]
        NotificationManagement["2.7\nУправление\nуведомлениями"]
        CertificateManagement["2.8\nУправление\nсертификатами"]
    end
    
    CourseDB[("База данных\nкурсов")]
    FileStorage["Хранилище\nфайлов"]
    EmailSystem["Система\nэлектронной почты"]
    PaymentSystem["Платежная\nсистема"]
    TestingSystem["Подсистема\nтестирования"]
    IdentitySystem["Подсистема\nуправления пользователями"]
    
    %% Потоки данных от пользователей
    User <-- "Создание/Редактирование\nкурсов" --> CourseManagement
    User <-- "Создание/Редактирование\nмодулей" --> ModuleManagement
    User <-- "Создание/Редактирование\nуроков" --> LessonManagement
    User <-- "Запись на курсы" --> EnrollmentManagement
    User <-- "Просмотр прогресса" --> ProgressTracking
    User <-- "Отзывы и оценки" --> FeedbackManagement
    User <-- "Настройка уведомлений" --> NotificationManagement
    User <-- "Запрос сертификатов" --> CertificateManagement
    
    %% Потоки данных с базой данных
    CourseManagement -- "Чтение/Запись\nданных курсов" --> CourseDB
    ModuleManagement -- "Чтение/Запись\nданных модулей" --> CourseDB
    LessonManagement -- "Чтение/Запись\nданных уроков" --> CourseDB
    EnrollmentManagement -- "Чтение/Запись\nданных записей" --> CourseDB
    ProgressTracking -- "Чтение/Запись\nданных прогресса" --> CourseDB
    FeedbackManagement -- "Чтение/Запись\nданных отзывов" --> CourseDB
    NotificationManagement -- "Чтение/Запись\nданных уведомлений" --> CourseDB
    CertificateManagement -- "Чтение/Запись\nданных сертификатов" --> CourseDB
    
    %% Потоки данных с внешними системами
    LessonManagement -- "Загрузка/Получение\nматериалов" --> FileStorage
    CertificateManagement -- "Загрузка/Получение\nсертификатов" --> FileStorage
    NotificationManagement -- "Отправка\nуведомлений" --> EmailSystem
    EnrollmentManagement -- "Обработка\nплатежей" --> PaymentSystem
    LessonManagement -- "Данные тестов" --> TestingSystem
    EnrollmentManagement -- "Проверка\nпользователей" --> IdentitySystem
    ProgressTracking -- "Обновление\nстатистики" --> IdentitySystem
    CertificateManagement -- "Выдача\nсертификатов" --> IdentitySystem
    
    %% Внутренние потоки
    CourseManagement -- "Структура курса" --> ModuleManagement
    ModuleManagement -- "Структура модуля" --> LessonManagement
    EnrollmentManagement -- "Данные записи" --> ProgressTracking
    ProgressTracking -- "Завершение курса" --> CertificateManagement
    ProgressTracking -- "События прогресса" --> NotificationManagement
```

### Подсистема тестирования

```mermaid
flowchart TD
    User["Пользователи"]
    
    subgraph TestingSystem["3.0 Подсистема тестирования"]
        TestTemplateManagement["3.1\nУправление\nшаблонами тестов"]
        QuestionManagement["3.2\nУправление\nвопросами"]
        AttemptManagement["3.3\nУправление\nпопытками"]
        EvaluationSystem["3.4\nСистема\nоценивания"]
        CategoryManagement["3.5\nУправление\nкатегориями"]
    end
    
    TestingDB[("База данных\nтестирования")]
    FileStorage["Хранилище\nфайлов"]
    CourseSystem["Подсистема\nуправления курсами"]
    CodeExecSystem["Подсистема\nвыполнения кода"]
    IdentitySystem["Подсистема\nуправления пользователями"]
    
    %% Потоки данных от пользователей
    User <-- "Создание/Редактирование\nтестов" --> TestTemplateManagement
    User <-- "Создание/Редактирование\nвопросов" --> QuestionManagement
    User <-- "Прохождение тестов" --> AttemptManagement
    User <-- "Просмотр результатов" --> EvaluationSystem
    User <-- "Управление категориями" --> CategoryManagement
    
    %% Потоки данных с базой данных
    TestTemplateManagement -- "Чтение/Запись\nданных тестов" --> TestingDB
    QuestionManagement -- "Чтение/Запись\nданных вопросов" --> TestingDB
    AttemptManagement -- "Чтение/Запись\nданных попыток" --> TestingDB
    EvaluationSystem -- "Чтение/Запись\nданных оценок" --> TestingDB
    CategoryManagement -- "Чтение/Запись\nданных категорий" --> TestingDB
    
    %% Потоки данных с внешними системами
    QuestionManagement -- "Загрузка/Получение\nматериалов" --> FileStorage
    TestTemplateManagement -- "Привязка к курсам" --> CourseSystem
    AttemptManagement -- "Проверка доступа" --> CourseSystem
    QuestionManagement -- "Задания по\nпрограммированию" --> CodeExecSystem
    EvaluationSystem -- "Результаты\nвыполнения кода" --> CodeExecSystem
    AttemptManagement -- "Проверка\nпользователей" --> IdentitySystem
    EvaluationSystem -- "Обновление\nстатистики" --> IdentitySystem
    
    %% Внутренние потоки
    TestTemplateManagement -- "Структура теста" --> QuestionManagement
    AttemptManagement -- "Данные попытки" --> EvaluationSystem
    CategoryManagement -- "Категории" --> TestTemplateManagement
```

### Подсистема выполнения кода

```mermaid
flowchart TD
    User["Пользователи"]
    
    subgraph CodeExecSystem["4.0 Подсистема выполнения кода"]
        TaskManagement["4.1\nУправление\nзаданиями"]
        SubmissionManagement["4.2\nУправление\nотправками"]
        ExecutionEngine["4.3\nДвижок\nвыполнения кода"]
        TestCaseManagement["4.4\nУправление\nтестовыми случаями"]
        CodeAnalysis["4.5\nАнализ\nкода"]
    end
    
    CodeExecDB[("База данных\nвыполнения кода")]
    AIService["ИИ-сервисы"]
    TestingSystem["Подсистема\nтестирования"]
    IdentitySystem["Подсистема\nуправления пользователями"]
    
    %% Потоки данных от пользователей
    User <-- "Создание/Редактирование\nзаданий" --> TaskManagement
    User <-- "Отправка решений" --> SubmissionManagement
    User <-- "Создание/Редактирование\nтестовых случаев" --> TestCaseManagement
    User <-- "Запрос анализа кода" --> CodeAnalysis
    
    %% Потоки данных с базой данных
    TaskManagement -- "Чтение/Запись\nданных заданий" --> CodeExecDB
    SubmissionManagement -- "Чтение/Запись\nданных отправок" --> CodeExecDB
    TestCaseManagement -- "Чтение/Запись\nданных тестовых случаев" --> CodeExecDB
    
    %% Потоки данных с внешними системами
    CodeAnalysis -- "Запрос анализа" --> AIService
    TaskManagement -- "Интеграция с тестами" --> TestingSystem
    SubmissionManagement -- "Результаты выполнения" --> TestingSystem
    SubmissionManagement -- "Проверка\nпользователей" --> IdentitySystem
    
    %% Внутренние потоки
    TaskManagement -- "Задания" --> TestCaseManagement
    SubmissionManagement -- "Код для выполнения" --> ExecutionEngine
    TestCaseManagement -- "Тестовые случаи" --> ExecutionEngine
    ExecutionEngine -- "Результаты выполнения" --> SubmissionManagement
    SubmissionManagement -- "Код для анализа" --> CodeAnalysis
    CodeAnalysis -- "Рекомендации" --> SubmissionManagement
```

## Диаграммы потоков данных для ключевых процессов

### Процесс регистрации и аутентификации

```mermaid
sequenceDiagram
    participant User as Пользователь
    participant API as API Gateway
    participant Identity as Сервис Identity
    participant DB as База данных пользователей
    participant Email as Сервис Email
    
    User->>API: Отправка регистрационных данных
    API->>Identity: Перенаправление запроса
    Identity->>Identity: Валидация данных
    Identity->>DB: Проверка существования email
    DB-->>Identity: Результат проверки
    
    alt Email уже существует
        Identity-->>API: Ошибка: Email уже зарегистрирован
        API-->>User: Сообщение об ошибке
    else Email свободен
        Identity->>Identity: Хеширование пароля
        Identity->>DB: Сохранение данных пользователя
        Identity->>Email: Отправка письма подтверждения
        Identity-->>API: Успешная регистрация
        API-->>User: Сообщение об успешной регистрации
        
        User->>API: Подтверждение email (переход по ссылке)
        API->>Identity: Перенаправление запроса
        Identity->>DB: Подтверждение email пользователя
        Identity-->>API: Email подтвержден
        API-->>User: Перенаправление на страницу входа
        
        User->>API: Отправка учетных данных (логин)
        API->>Identity: Перенаправление запроса
        Identity->>DB: Проверка учетных данных
        DB-->>Identity: Результат проверки
        
        alt Неверные учетные данные
            Identity-->>API: Ошибка аутентификации
            API-->>User: Сообщение об ошибке
        else Верные учетные данные
            Identity->>Identity: Генерация JWT-токена
            Identity-->>API: JWT-токен
            API-->>User: JWT-токен и перенаправление в систему
        end
    end
```

### Процесс создания и публикации курса

```mermaid
sequenceDiagram
    participant Teacher as Преподаватель
    participant API as API Gateway
    participant Identity as Сервис Identity
    participant Courses as Сервис Courses
    participant DB as База данных курсов
    participant Storage as Хранилище файлов
    participant EventBus as Event Bus
    
    Teacher->>API: Запрос на создание курса
    API->>Identity: Проверка аутентификации и роли
    Identity-->>API: Подтверждение прав доступа
    API->>Courses: Перенаправление запроса
    Courses->>DB: Создание записи курса
    DB-->>Courses: ID нового курса
    Courses-->>API: Данные созданного курса
    API-->>Teacher: Перенаправление на редактор курса
    
    Teacher->>API: Добавление модулей и уроков
    API->>Courses: Перенаправление запроса
    Courses->>DB: Сохранение структуры курса
    DB-->>Courses: Подтверждение сохранения
    Courses-->>API: Обновленные данные курса
    API-->>Teacher: Обновление интерфейса
    
    Teacher->>API: Загрузка материалов урока
    API->>Courses: Перенаправление запроса
    Courses->>Storage: Сохранение файлов
    Storage-->>Courses: URL файлов
    Courses->>DB: Сохранение ссылок на материалы
    DB-->>Courses: Подтверждение сохранения
    Courses-->>API: Обновленные данные урока
    API-->>Teacher: Обновление интерфейса
    
    Teacher->>API: Запрос на публикацию курса
    API->>Courses: Перенаправление запроса
    Courses->>Courses: Валидация курса
    
    alt Курс не готов к публикации
        Courses-->>API: Ошибка: Курс не готов к публикации
        API-->>Teacher: Сообщение об ошибке
    else Курс готов к публикации
        Courses->>DB: Изменение статуса курса на "Опубликован"
        DB-->>Courses: Подтверждение публикации
        Courses->>EventBus: Публикация события CoursePublishedEvent
        Courses-->>API: Курс опубликован
        API-->>Teacher: Сообщение об успешной публикации
    end
```

### Процесс прохождения теста

```mermaid
sequenceDiagram
    participant Student as Студент
    participant API as API Gateway
    participant Identity as Сервис Identity
    participant Courses as Сервис Courses
    participant Testing as Сервис Testing
    participant CodeExec as Сервис CodeExecution
    participant TestDB as База данных тестирования
    participant EventBus as Event Bus
    
    Student->>API: Запрос на начало теста
    API->>Identity: Проверка аутентификации
    Identity-->>API: Подтверждение аутентификации
    API->>Testing: Перенаправление запроса
    Testing->>Courses: Проверка доступа к тесту
    Courses-->>Testing: Подтверждение доступа
    Testing->>TestDB: Создание попытки теста
    TestDB-->>Testing: ID попытки
    Testing->>TestDB: Получение вопросов теста
    TestDB-->>Testing: Вопросы теста
    Testing-->>API: Данные теста и вопросов
    API-->>Student: Отображение теста
    
    loop Для каждого вопроса
        Student->>API: Отправка ответа на вопрос
        API->>Testing: Перенаправление ответа
        
        alt Вопрос с кодом
            Testing->>CodeExec: Запрос на выполнение кода
            CodeExec->>CodeExec: Выполнение кода в изолированной среде
            CodeExec-->>Testing: Результаты выполнения
        end
        
        Testing->>TestDB: Сохранение ответа
        TestDB-->>Testing: Подтверждение сохранения
        Testing-->>API: Результат сохранения
        API-->>Student: Обновление интерфейса
    end
    
    Student->>API: Завершение теста
    API->>Testing: Перенаправление запроса
    Testing->>Testing: Оценка результатов
    Testing->>TestDB: Сохранение результатов
    TestDB-->>Testing: Подтверждение сохранения
    Testing->>EventBus: Публикация события TestCompletedEvent
    Testing-->>API: Результаты теста
    API-->>Student: Отображение результатов
    
    EventBus->>Courses: Обработка события TestCompletedEvent
    Courses->>Courses: Обновление прогресса студента
```

### Процесс выполнения задания по программированию

```mermaid
sequenceDiagram
    participant Student as Студент
    participant API as API Gateway
    participant Identity as Сервис Identity
    participant CodeExec as Сервис CodeExecution
    participant AI as ИИ-сервис
    participant CodeDB as База данных CodeExecution
    participant EventBus as Event Bus
    
    Student->>API: Запрос задания по программированию
    API->>Identity: Проверка аутентификации
    Identity-->>API: Подтверждение аутентификации
    API->>CodeExec: Перенаправление запроса
    CodeExec->>CodeDB: Получение задания и тестовых случаев
    CodeDB-->>CodeExec: Данные задания
    CodeExec-->>API: Описание задания
    API-->>Student: Отображение задания и редактора кода
    
    Student->>API: Отправка решения
    API->>CodeExec: Перенаправление решения
    CodeExec->>CodeDB: Сохранение отправки
    CodeDB-->>CodeExec: ID отправки
    CodeExec->>CodeExec: Выполнение кода в изолированной среде
    CodeExec->>CodeExec: Проверка на тестовых случаях
    
    alt Запрос анализа кода
        CodeExec->>AI: Запрос анализа кода
        AI-->>CodeExec: Рекомендации по улучшению кода
    end
    
    CodeExec->>CodeDB: Сохранение результатов
    CodeDB-->>CodeExec: Подтверждение сохранения
    CodeExec->>EventBus: Публикация события CodeSubmissionCompletedEvent
    CodeExec-->>API: Результаты выполнения
    API-->>Student: Отображение результатов и рекомендаций
```

## Заключение

Диаграммы потоков данных (DFD) предоставляют наглядное представление о том, как данные перемещаются внутри системы AiTestPlatform. Они помогают понять:

1. Как информация передается между различными компонентами системы
2. Какие процессы обрабатывают данные
3. Где данные хранятся
4. Как система взаимодействует с внешними сущностями

Эти диаграммы являются важным инструментом для разработчиков, аналитиков и других заинтересованных сторон, позволяя им лучше понять архитектуру системы и принципы ее работы.
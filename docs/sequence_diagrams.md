# Диаграммы последовательности AiTestPlatform

В этом документе представлены диаграммы последовательности (Sequence Diagrams) для визуализации взаимодействия компонентов системы AiTestPlatform.

## Содержание

1. [Регистрация и аутентификация пользователя](#регистрация-и-аутентификация-пользователя)
2. [Создание и публикация курса](#создание-и-публикация-курса)
3. [Запись студента на курс](#запись-студента-на-курс)
4. [Прохождение урока и тестирование](#прохождение-урока-и-тестирование)
5. [Выполнение задания по программированию](#выполнение-задания-по-программированию)
6. [Получение сертификата](#получение-сертификата)
7. [Обработка уведомлений](#обработка-уведомлений)
8. [Интеграция с внешними системами](#интеграция-с-внешними-системами)
9. [Оценка ответов на тесты с использованием AI](#оценка-ответов-на-тесты-с-использованием-ai)

## Регистрация и аутентификация пользователя

```mermaid
sequenceDiagram
    actor User as Пользователь
    participant Gateway as API Gateway
    participant Identity as Сервис Identity
    participant DB as База данных Identity
    participant Email as Сервис Email
    
    User->>Gateway: Запрос на регистрацию
    Gateway->>Identity: Перенаправление запроса
    Identity->>Identity: Валидация данных
    Identity->>DB: Проверка существования email
    DB-->>Identity: Результат проверки
    
    alt Email уже существует
        Identity-->>Gateway: Ошибка: Email уже зарегистрирован
        Gateway-->>User: Сообщение об ошибке
    else Email свободен
        Identity->>Identity: Хеширование пароля
        Identity->>DB: Сохранение данных пользователя
        DB-->>Identity: Успешное сохранение
        Identity->>Email: Отправка письма подтверждения
        Identity-->>Gateway: Успешная регистрация
        Gateway-->>User: Сообщение об успешной регистрации
        
        User->>Gateway: Подтверждение email (переход по ссылке)
        Gateway->>Identity: Перенаправление запроса
        Identity->>DB: Подтверждение email пользователя
        DB-->>Identity: Успешное подтверждение
        Identity-->>Gateway: Email подтвержден
        Gateway-->>User: Перенаправление на страницу входа
        
        User->>Gateway: Отправка учетных данных (логин)
        Gateway->>Identity: Перенаправление запроса
        Identity->>DB: Проверка учетных данных
        DB-->>Identity: Результат проверки
        
        alt Неверные учетные данные
            Identity-->>Gateway: Ошибка аутентификации
            Gateway-->>User: Сообщение об ошибке
        else Верные учетные данные
            Identity->>Identity: Генерация JWT-токена и refresh-токена
            Identity->>DB: Сохранение refresh-токена
            DB-->>Identity: Успешное сохранение
            Identity-->>Gateway: JWT-токен и refresh-токен
            Gateway-->>User: Токены и перенаправление в систему
        end
    end
```

## Создание и публикация курса

```mermaid
sequenceDiagram
    actor Teacher as Преподаватель
    participant Gateway as API Gateway
    participant Identity as Сервис Identity
    participant Courses as Сервис Courses
    participant DB as База данных Courses
    participant Storage as Хранилище файлов
    participant EventBus as Event Bus
    participant Notification as Сервис уведомлений
    
    Teacher->>Gateway: Запрос на создание курса
    Gateway->>Identity: Проверка аутентификации и роли
    Identity-->>Gateway: Подтверждение прав доступа
    Gateway->>Courses: Перенаправление запроса
    Courses->>DB: Создание записи курса
    DB-->>Courses: ID нового курса
    Courses-->>Gateway: Данные созданного курса
    Gateway-->>Teacher: Перенаправление на редактор курса
    
    Teacher->>Gateway: Добавление модулей
    Gateway->>Courses: Перенаправление запроса
    Courses->>DB: Сохранение модулей
    DB-->>Courses: Подтверждение сохранения
    Courses-->>Gateway: Обновленные данные курса
    Gateway-->>Teacher: Обновление интерфейса
    
    Teacher->>Gateway: Добавление уроков в модуль
    Gateway->>Courses: Перенаправление запроса
    Courses->>DB: Сохранение уроков
    DB-->>Courses: Подтверждение сохранения
    Courses-->>Gateway: Обновленные данные модуля
    Gateway-->>Teacher: Обновление интерфейса
    
    Teacher->>Gateway: Загрузка материалов урока
    Gateway->>Courses: Перенаправление запроса
    Courses->>Storage: Сохранение файлов
    Storage-->>Courses: URL файлов
    Courses->>DB: Сохранение ссылок на материалы
    DB-->>Courses: Подтверждение сохранения
    Courses-->>Gateway: Обновленные данные урока
    Gateway-->>Teacher: Обновление интерфейса
    
    Teacher->>Gateway: Запрос на публикацию курса
    Gateway->>Courses: Перенаправление запроса
    Courses->>Courses: Валидация курса
    
    alt Курс не готов к публикации
        Courses-->>Gateway: Ошибка: Курс не готов к публикации
        Gateway-->>Teacher: Сообщение об ошибке
    else Курс готов к публикации
        Courses->>DB: Изменение статуса курса на "Опубликован"
        DB-->>Courses: Подтверждение публикации
        Courses->>EventBus: Публикация события CoursePublishedEvent
        EventBus->>Notification: Передача события CoursePublishedEvent
        Notification->>Notification: Формирование уведомлений
        Courses-->>Gateway: Курс опубликован
        Gateway-->>Teacher: Сообщение об успешной публикации
    end
```

## Запись студента на курс

```mermaid
sequenceDiagram
    actor Student as Студент
    participant Gateway as API Gateway
    participant Identity as Сервис Identity
    participant Courses as Сервис Courses
    participant Payment as Сервис оплаты
    participant DB as База данных Courses
    participant EventBus as Event Bus
    participant Notification as Сервис уведомлений
    
    Student->>Gateway: Запрос на запись на курс
    Gateway->>Identity: Проверка аутентификации
    Identity-->>Gateway: Подтверждение аутентификации
    Gateway->>Courses: Перенаправление запроса
    Courses->>DB: Проверка доступности курса
    DB-->>Courses: Информация о курсе
    
    alt Курс платный
        Courses-->>Gateway: Требуется оплата
        Gateway-->>Student: Перенаправление на страницу оплаты
        Student->>Gateway: Отправка платежных данных
        Gateway->>Payment: Запрос на обработку платежа
        Payment->>Payment: Обработка платежа
        Payment-->>Gateway: Результат платежа
        
        alt Платеж не прошел
            Gateway-->>Student: Сообщение об ошибке платежа
        else Платеж успешен
            Gateway->>Courses: Подтверждение оплаты
            Courses->>DB: Создание записи о зачислении
            DB-->>Courses: ID зачисления
            Courses->>EventBus: Публикация события StudentEnrolledEvent
            EventBus->>Notification: Передача события StudentEnrolledEvent
            Notification->>Notification: Формирование уведомлений
            Courses-->>Gateway: Успешное зачисление
            Gateway-->>Student: Подтверждение зачисления и доступ к курсу
        end
    else Курс бесплатный
        Courses->>DB: Создание записи о зачислении
        DB-->>Courses: ID зачисления
        Courses->>EventBus: Публикация события StudentEnrolledEvent
        EventBus->>Notification: Передача события StudentEnrolledEvent
        Notification->>Notification: Формирование уведомлений
        Courses-->>Gateway: Успешное зачисление
        Gateway-->>Student: Подтверждение зачисления и доступ к курсу
    end
```

## Прохождение урока и тестирование

```mermaid
sequenceDiagram
    actor Student as Студент
    participant Gateway as API Gateway
    participant Identity as Сервис Identity
    participant Courses as Сервис Courses
    participant Testing as Сервис Testing
    participant AI as Сервис AI
    participant CoursesDB as База данных Courses
    participant TestingDB as База данных Testing
    participant EventBus as Event Bus
    
    Student->>Gateway: Запрос на доступ к уроку
    Gateway->>Identity: Проверка аутентификации
    Identity-->>Gateway: Подтверждение аутентификации
    Gateway->>Courses: Перенаправление запроса
    Courses->>CoursesDB: Проверка доступа к уроку
    CoursesDB-->>Courses: Подтверждение доступа
    Courses->>CoursesDB: Получение материалов урока
    CoursesDB-->>Courses: Материалы урока
    Courses-->>Gateway: Данные урока
    Gateway-->>Student: Отображение урока
    
    Student->>Gateway: Отметка о завершении изучения материала
    Gateway->>Courses: Перенаправление запроса
    Courses->>CoursesDB: Обновление прогресса
    CoursesDB-->>Courses: Подтверждение обновления
    
    alt Урок содержит тест
        Courses-->>Gateway: Информация о тесте
        Gateway-->>Student: Предложение пройти тест
        Student->>Gateway: Запрос на начало теста
        Gateway->>Testing: Перенаправление запроса
        Testing->>TestingDB: Создание попытки теста
        TestingDB-->>Testing: ID попытки
        Testing->>TestingDB: Получение вопросов теста
        TestingDB-->>Testing: Вопросы теста
        Testing-->>Gateway: Данные теста и вопросов
        Gateway-->>Student: Отображение теста
        
        loop Для каждого вопроса
            Student->>Gateway: Отправка ответа на вопрос
            Gateway->>Testing: Перенаправление ответа
            Testing->>TestingDB: Сохранение ответа
            TestingDB-->>Testing: Подтверждение сохранения
            
            alt Вопрос с открытым ответом или код
                Testing->>AI: Запрос на оценку ответа
                AI-->>Testing: Результат оценки (оценка, обратная связь)
                Testing->>TestingDB: Сохранение результатов AI-оценки
                TestingDB-->>Testing: Подтверждение сохранения
                Testing->>EventBus: Публикация события TestQuestionResponseEvaluatedEvent
            end
            
            Testing-->>Gateway: Результат сохранения
            Gateway-->>Student: Обновление интерфейса
        end
        
        Student->>Gateway: Завершение теста
        Gateway->>Testing: Перенаправление запроса
        Testing->>Testing: Оценка результатов
        Testing->>TestingDB: Сохранение результатов
        TestingDB-->>Testing: Подтверждение сохранения
        Testing->>EventBus: Публикация события TestCompletedEvent
        Testing-->>Gateway: Результаты теста
        Gateway-->>Student: Отображение результатов
        
        EventBus->>Courses: Обработка события TestCompletedEvent
        Courses->>CoursesDB: Обновление прогресса студента
        CoursesDB-->>Courses: Подтверждение обновления
    end
    
    Courses->>Courses: Проверка завершения модуля/курса
    
    alt Модуль/курс завершен
        Courses->>EventBus: Публикация события ModuleCompletedEvent/CourseCompletedEvent
        Courses-->>Gateway: Информация о завершении
        Gateway-->>Student: Поздравление с завершением модуля/курса
    end
```

## Выполнение задания по программированию

```mermaid
sequenceDiagram
    actor Student as Студент
    participant Gateway as API Gateway
    participant Identity as Сервис Identity
    participant Courses as Сервис Courses
    participant CodeExec as Сервис CodeExecution
    participant AI as ИИ-сервис
    participant CoursesDB as База данных Courses
    participant CodeDB as База данных CodeExecution
    participant EventBus as Event Bus
    
    Student->>Gateway: Запрос задания по программированию
    Gateway->>Identity: Проверка аутентификации
    Identity-->>Gateway: Подтверждение аутентификации
    Gateway->>Courses: Проверка доступа к заданию
    Courses->>CoursesDB: Проверка прогресса и доступа
    CoursesDB-->>Courses: Подтверждение доступа
    Courses-->>Gateway: Подтверждение доступа
    Gateway->>CodeExec: Запрос данных задания
    CodeExec->>CodeDB: Получение задания и тестовых случаев
    CodeDB-->>CodeExec: Данные задания
    CodeExec-->>Gateway: Описание задания
    Gateway-->>Student: Отображение задания и редактора кода
    
    Student->>Gateway: Отправка решения
    Gateway->>CodeExec: Перенаправление решения
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
    CodeExec-->>Gateway: Результаты выполнения
    Gateway-->>Student: Отображение результатов и рекомендаций
    
    EventBus->>Courses: Обработка события CodeSubmissionCompletedEvent
    
    alt Задание выполнено успешно
        Courses->>CoursesDB: Обновление прогресса студента
        CoursesDB-->>Courses: Подтверждение обновления
        
        Courses->>Courses: Проверка завершения модуля/курса
        alt Модуль/курс завершен
            Courses->>EventBus: Публикация события ModuleCompletedEvent/CourseCompletedEvent
            Courses-->>Gateway: Информация о завершении
            Gateway-->>Student: Поздравление с завершением модуля/курса
        end
    end
```

## Получение сертификата

```mermaid
sequenceDiagram
    actor Student as Студент
    participant Gateway as API Gateway
    participant Identity as Сервис Identity
    participant Courses as Сервис Courses
    participant DB as База данных Courses
    participant Storage as Хранилище файлов
    participant EventBus as Event Bus
    participant Email as Сервис Email
    
    alt Автоматическая выдача при завершении курса
        EventBus->>Courses: Событие CourseCompletedEvent
        Courses->>DB: Проверка завершения всех требований
        DB-->>Courses: Подтверждение завершения
        Courses->>Courses: Генерация сертификата
        Courses->>Storage: Сохранение сертификата
        Storage-->>Courses: URL сертификата
        Courses->>DB: Сохранение данных сертификата
        DB-->>Courses: Подтверждение сохранения
        Courses->>EventBus: Публикация события CertificateIssuedEvent
        EventBus->>Email: Обработка события CertificateIssuedEvent
        Email->>Email: Формирование письма с сертификатом
        Email->>Student: Отправка письма с сертификатом
    else Запрос сертификата студентом
        Student->>Gateway: Запрос на получение сертификата
        Gateway->>Identity: Проверка аутентификации
        Identity-->>Gateway: Подтверждение аутентификации
        Gateway->>Courses: Перенаправление запроса
        Courses->>DB: Проверка завершения курса
        DB-->>Courses: Статус завершения
        
        alt Курс не завершен
            Courses-->>Gateway: Ошибка: Курс не завершен
            Gateway-->>Student: Сообщение об ошибке
        else Курс завершен
            alt Сертификат уже существует
                Courses->>DB: Получение данных сертификата
                DB-->>Courses: Данные сертификата
                Courses-->>Gateway: Данные существующего сертификата
                Gateway-->>Student: Отображение сертификата
            else Сертификат не существует
                Courses->>Courses: Генерация сертификата
                Courses->>Storage: Сохранение сертификата
                Storage-->>Courses: URL сертификата
                Courses->>DB: Сохранение данных сертификата
                DB-->>Courses: Подтверждение сохранения
                Courses->>EventBus: Публикация события CertificateIssuedEvent
                Courses-->>Gateway: Данные нового сертификата
                Gateway-->>Student: Отображение сертификата
            end
        end
    end
    
    Student->>Gateway: Запрос на скачивание сертификата
    Gateway->>Courses: Перенаправление запроса
    Courses->>Storage: Запрос файла сертификата
    Storage-->>Courses: Файл сертификата
    Courses-->>Gateway: Файл сертификата
    Gateway-->>Student: Скачивание сертификата
```

## Обработка уведомлений

```mermaid
sequenceDiagram
    participant EventBus as Event Bus
    participant Notification as Сервис уведомлений
    participant DB as База данных Courses
    participant Email as Сервис Email
    participant Push as Сервис Push-уведомлений
    actor User as Пользователь
    participant Gateway as API Gateway
    participant Identity as Сервис Identity
    
    alt Создание уведомления через событие
        EventBus->>Notification: Событие (CoursePublished, LessonAdded, и т.д.)
        Notification->>DB: Получение настроек уведомлений пользователей
        DB-->>Notification: Настройки уведомлений
        Notification->>Notification: Формирование уведомлений
        Notification->>DB: Сохранение уведомлений
        DB-->>Notification: Подтверждение сохранения
        
        loop Для каждого пользователя с включенными уведомлениями
            alt Email уведомления включены
                Notification->>Email: Отправка email-уведомления
            end
            
            alt Push уведомления включены
                Notification->>Push: Отправка push-уведомления
            end
        end
    end
    
    User->>Gateway: Запрос на получение уведомлений
    Gateway->>Identity: Проверка аутентификации
    Identity-->>Gateway: Подтверждение аутентификации
    Gateway->>Notification: Перенаправление запроса
    Notification->>DB: Получение уведомлений пользователя
    DB-->>Notification: Список уведомлений
    Notification-->>Gateway: Список уведомлений
    Gateway-->>User: Отображение уведомлений
    
    User->>Gateway: Отметка уведомления как прочитанного
    Gateway->>Identity: Проверка аутентификации
    Identity-->>Gateway: Подтверждение аутентификации
    Gateway->>Notification: Перенаправление запроса
    Notification->>DB: Обновление статуса уведомления
    DB-->>Notification: Подтверждение обновления
    Notification-->>Gateway: Успешное обновление
    Gateway-->>User: Обновление интерфейса
    
    User->>Gateway: Изменение настроек уведомлений
    Gateway->>Identity: Проверка аутентификации
    Identity-->>Gateway: Подтверждение аутентификации
    Gateway->>Notification: Перенаправление запроса
    Notification->>DB: Обновление настроек уведомлений
    DB-->>Notification: Подтверждение обновления
    Notification-->>Gateway: Успешное обновление
    Gateway-->>User: Обновление интерфейса
```

## Интеграция с внешними системами

```mermaid
sequenceDiagram
    participant Gateway as API Gateway
    participant Identity as Сервис Identity
    participant Courses as Сервис Courses
    participant Testing as Сервис Testing
    participant CodeExec as Сервис CodeExecution
    participant EventBus as Event Bus
    participant Email as Сервис Email
    participant Payment as Платежная система
    participant AI as ИИ-сервисы
    participant Storage as Облачное хранилище
    
    %% Интеграция с Email-сервисом
    Identity->>Email: Отправка писем подтверждения
    Courses->>Email: Отправка уведомлений о курсах
    EventBus->>Email: События для email-уведомлений
    
    %% Интеграция с платежной системой
    Gateway->>Payment: Запросы на обработку платежей
    Payment-->>Gateway: Результаты платежей
    Payment->>EventBus: Уведомления о платежах
    
    %% Интеграция с ИИ-сервисами
    CodeExec->>AI: Запросы на анализ кода
    AI-->>CodeExec: Результаты анализа и рекомендации
    Testing->>AI: Запросы на оценку ответов и генерацию тестов
    AI-->>Testing: Результаты оценки и сгенерированные тесты
    
    %% Интеграция с облачным хранилищем
    Courses->>Storage: Загрузка материалов курсов
    Storage-->>Courses: URL загруженных файлов
    CodeExec->>Storage: Сохранение кода и результатов
    Storage-->>CodeExec: URL сохраненных файлов
    
    %% Интеграция между микросервисами через Event Bus
    Identity->>EventBus: События пользователей
    Courses->>EventBus: События курсов
    Testing->>EventBus: События тестирования
    CodeExec->>EventBus: События выполнения кода
    
    EventBus->>Identity: События для обработки
    EventBus->>Courses: События для обработки
    EventBus->>Testing: События для обработки
    EventBus->>CodeExec: События для обработки
```

## Оценка ответов на тесты с использованием AI

```mermaid
sequenceDiagram
    actor Student as Студент
    participant Gateway as API Gateway
    participant Testing as Сервис Testing
    participant TestQuestionResponseService as Сервис ответов на вопросы
    participant TestEvaluationService as Сервис оценки тестов
    participant AiService as AI сервис
    participant TestingDB as База данных Testing
    participant EventBus as Event Bus
    
    Student->>Gateway: Отправка ответа на вопрос теста
    Gateway->>Testing: Перенаправление ответа
    Testing->>TestQuestionResponseService: Сохранение ответа
    TestQuestionResponseService->>TestingDB: Запись ответа в базу данных
    TestingDB-->>TestQuestionResponseService: Подтверждение сохранения
    
    alt Вопрос с открытым ответом или код
        TestQuestionResponseService->>TestEvaluationService: Запрос на оценку ответа
        TestEvaluationService->>AiService: Отправка ответа и эталонного решения
        
        alt Ответ в виде кода
            AiService->>AiService: Анализ кода, проверка логики и эффективности
        else Текстовый ответ
            AiService->>AiService: Семантический анализ текста, сравнение с эталоном
        end
        
        AiService-->>TestEvaluationService: Результаты оценки (оценка, обратная связь)
        TestEvaluationService-->>TestQuestionResponseService: Возврат результатов оценки
        TestQuestionResponseService->>TestingDB: Сохранение результатов AI-оценки
        TestingDB-->>TestQuestionResponseService: Подтверждение сохранения
        TestQuestionResponseService->>EventBus: Публикация события TestQuestionResponseEvaluatedEvent
    else Вопрос с выбором варианта
        TestQuestionResponseService->>TestEvaluationService: Запрос на проверку ответа
        TestEvaluationService->>TestEvaluationService: Автоматическая проверка по ключу
        TestEvaluationService-->>TestQuestionResponseService: Результат проверки
    end
    
    TestQuestionResponseService-->>Testing: Результат обработки ответа
    Testing-->>Gateway: Результат сохранения и оценки
    Gateway-->>Student: Обновление интерфейса (при необходимости с обратной связью)
```

## Заключение

Диаграммы последовательности (Sequence Diagrams) предоставляют детальное представление о взаимодействии компонентов системы AiTestPlatform во времени. Они помогают понять:

1. Порядок взаимодействия между компонентами системы
2. Обмен сообщениями между участниками процесса
3. Альтернативные потоки выполнения в зависимости от условий
4. Временную последовательность операций

Эти диаграммы являются важным инструментом для разработчиков, позволяя им лучше понять динамическое поведение системы и взаимодействие между ее компонентами.
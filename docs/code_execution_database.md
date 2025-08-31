# Схема базы данных микросервиса CodeExecution

В этом документе описана структура базы данных для микросервиса CodeExecution в системе AiTestPlatform.

## ER-диаграмма

```mermaid
erDiagram
    CodeTask ||--o{ CodeSubmission : имеет
    CodeTask ||--o{ CodeTestCase : содержит
    CodeSubmission ||--o{ CodeTestResult : имеет
    CodeTask ||--o{ CodeTaskTag : имеет
    CodeTask ||--o{ CodeTaskCategory : относится
    CodeTask ||--o{ CodeTaskTemplate : имеет
    
    CodeTask {
        uuid id PK
        string title
        string description
        string difficultyLevel
        string programmingLanguage
        int timeLimit
        int memoryLimit
        uuid authorId
        uuid courseId FK
        uuid moduleId FK
        uuid lessonId FK
        boolean isActive
        boolean isPublished
        datetime createdAt
        datetime updatedAt
        string instructions
        string initialCode
        string solutionCode
        string validationStrategy
        string settings
    }
    
    CodeSubmission {
        uuid id PK
        uuid codeTaskId FK
        uuid userId
        string code
        string language
        string status
        int score
        string executionOutput
        int executionTime
        int memoryUsed
        string errorMessage
        datetime submittedAt
        datetime evaluatedAt
        string evaluationDetails
        boolean isCorrect
        datetime createdAt
        datetime updatedAt
    }
    
    CodeTestCase {
        uuid id PK
        uuid codeTaskId FK
        string input
        string expectedOutput
        boolean isHidden
        int orderIndex
        string description
        int points
        string validationMethod
        datetime createdAt
        datetime updatedAt
    }
    
    CodeTestResult {
        uuid id PK
        uuid submissionId FK
        uuid testCaseId FK
        boolean passed
        string actualOutput
        int executionTime
        int memoryUsed
        string errorMessage
        datetime createdAt
        datetime updatedAt
    }
    
    CodeTaskTag {
        uuid id PK
        string name
        datetime createdAt
        datetime updatedAt
    }
    
    CodeTaskCategory {
        uuid id PK
        string name
        string description
        uuid parentCategoryId FK
        datetime createdAt
        datetime updatedAt
    }
    
    CodeTaskTemplate {
        uuid id PK
        string name
        string description
        string programmingLanguage
        string templateCode
        string testCode
        string difficultyLevel
        uuid authorId
        datetime createdAt
        datetime updatedAt
    }
    
    ExecutionEnvironment {
        uuid id PK
        string name
        string description
        string language
        string version
        string containerImage
        string compileCommand
        string runCommand
        string testCommand
        boolean isActive
        string settings
        datetime createdAt
        datetime updatedAt
    }
```

## Описание таблиц

### CodeTask
Содержит задания по программированию.

| Поле | Тип | Описание |
|------|-----|----------|
| id | uuid | Первичный ключ |
| title | string | Название задания |
| description | string | Описание задания |
| difficultyLevel | string | Уровень сложности (Easy, Medium, Hard) |
| programmingLanguage | string | Язык программирования |
| timeLimit | int | Ограничение времени выполнения в миллисекундах |
| memoryLimit | int | Ограничение памяти в мегабайтах |
| authorId | uuid | ID автора задания |
| courseId | uuid | ID курса (может быть null) |
| moduleId | uuid | ID модуля (может быть null) |
| lessonId | uuid | ID урока (может быть null) |
| isActive | boolean | Активно ли задание |
| isPublished | boolean | Опубликовано ли задание |
| createdAt | datetime | Дата создания |
| updatedAt | datetime | Дата обновления |
| instructions | string | Инструкции для выполнения задания |
| initialCode | string | Начальный код для студента |
| solutionCode | string | Код решения (для преподавателя) |
| validationStrategy | string | Стратегия валидации (TestCases, UnitTests, OutputComparison) |
| settings | string | JSON с настройками задания |

### CodeSubmission
Содержит отправленные решения заданий.

| Поле | Тип | Описание |
|------|-----|----------|
| id | uuid | Первичный ключ |
| codeTaskId | uuid | ID задания |
| userId | uuid | ID пользователя |
| code | string | Отправленный код |
| language | string | Язык программирования |
| status | string | Статус (Pending, Running, Completed, Failed, TimeLimitExceeded, MemoryLimitExceeded) |
| score | int | Набранные баллы |
| executionOutput | string | Вывод выполнения |
| executionTime | int | Время выполнения в миллисекундах |
| memoryUsed | int | Использованная память в мегабайтах |
| errorMessage | string | Сообщение об ошибке (если есть) |
| submittedAt | datetime | Время отправки |
| evaluatedAt | datetime | Время оценки |
| evaluationDetails | string | JSON с деталями оценки |
| isCorrect | boolean | Правильное ли решение |
| createdAt | datetime | Дата создания |
| updatedAt | datetime | Дата обновления |

### CodeTestCase
Содержит тестовые случаи для заданий.

| Поле | Тип | Описание |
|------|-----|----------|
| id | uuid | Первичный ключ |
| codeTaskId | uuid | ID задания |
| input | string | Входные данные |
| expectedOutput | string | Ожидаемый вывод |
| isHidden | boolean | Скрытый ли тест от студентов |
| orderIndex | int | Порядковый номер теста |
| description | string | Описание теста |
| points | int | Количество баллов за тест |
| validationMethod | string | Метод валидации (ExactMatch, IgnoreWhitespace, Regex, Custom) |
| createdAt | datetime | Дата создания |
| updatedAt | datetime | Дата обновления |

### CodeTestResult
Содержит результаты выполнения тестовых случаев.

| Поле | Тип | Описание |
|------|-----|----------|
| id | uuid | Первичный ключ |
| submissionId | uuid | ID отправленного решения |
| testCaseId | uuid | ID тестового случая |
| passed | boolean | Пройден ли тест |
| actualOutput | string | Фактический вывод |
| executionTime | int | Время выполнения в миллисекундах |
| memoryUsed | int | Использованная память в мегабайтах |
| errorMessage | string | Сообщение об ошибке (если есть) |
| createdAt | datetime | Дата создания |
| updatedAt | datetime | Дата обновления |

### CodeTaskTag
Содержит теги для маркировки заданий.

| Поле | Тип | Описание |
|------|-----|----------|
| id | uuid | Первичный ключ |
| name | string | Название тега |
| createdAt | datetime | Дата создания |
| updatedAt | datetime | Дата обновления |

### CodeTaskCategory
Содержит категории заданий для их классификации.

| Поле | Тип | Описание |
|------|-----|----------|
| id | uuid | Первичный ключ |
| name | string | Название категории |
| description | string | Описание категории |
| parentCategoryId | uuid | ID родительской категории (для иерархии) |
| createdAt | datetime | Дата создания |
| updatedAt | datetime | Дата обновления |

### CodeTaskTemplate
Содержит шаблоны заданий для быстрого создания новых.

| Поле | Тип | Описание |
|------|-----|----------|
| id | uuid | Первичный ключ |
| name | string | Название шаблона |
| description | string | Описание шаблона |
| programmingLanguage | string | Язык программирования |
| templateCode | string | Шаблонный код |
| testCode | string | Код для тестирования |
| difficultyLevel | string | Уровень сложности |
| authorId | uuid | ID автора шаблона |
| createdAt | datetime | Дата создания |
| updatedAt | datetime | Дата обновления |

### ExecutionEnvironment
Содержит информацию о средах выполнения кода.

| Поле | Тип | Описание |
|------|-----|----------|
| id | uuid | Первичный ключ |
| name | string | Название среды |
| description | string | Описание среды |
| language | string | Язык программирования |
| version | string | Версия языка/среды |
| containerImage | string | Docker-образ для выполнения |
| compileCommand | string | Команда для компиляции |
| runCommand | string | Команда для запуска |
| testCommand | string | Команда для тестирования |
| isActive | boolean | Активна ли среда |
| settings | string | JSON с настройками среды |
| createdAt | datetime | Дата создания |
| updatedAt | datetime | Дата обновления |

## Индексы

- CodeTask: индексы по courseId, moduleId, lessonId, authorId, programmingLanguage
- CodeSubmission: индексы по codeTaskId, userId, status
- CodeTestCase: индекс по codeTaskId
- CodeTestResult: индексы по submissionId, testCaseId
- CodeTaskCategory: индекс по parentCategoryId
- ExecutionEnvironment: индексы по language, version

## Миграции Entity Framework Core

Для создания и обновления базы данных используйте следующие команды Entity Framework Core:

```bash
# Создание миграции
dotnet ef migrations add InitialCodeExecutionSchema --project src/Services/CodeExecution/CodeExecution.Infrastructure --startup-project src/Services/CodeExecution/CodeExecution.API

# Применение миграции
dotnet ef database update --project src/Services/CodeExecution/CodeExecution.Infrastructure --startup-project src/Services/CodeExecution/CodeExecution.API
```
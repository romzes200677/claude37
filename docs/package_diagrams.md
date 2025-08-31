# Диаграммы пакетов AiTestPlatform

В этом документе представлены диаграммы пакетов (Package Diagrams) для визуализации организации кода системы AiTestPlatform и зависимостей между пакетами.

## Содержание

1. [Общая структура решения](#общая-структура-решения)
2. [Микросервис Identity](#микросервис-identity)
3. [Микросервис Courses](#микросервис-courses)
4. [Микросервис Testing](#микросервис-testing)
5. [Микросервис CodeExecution](#микросервис-codeexecution)
6. [Общие компоненты (BuildingBlocks)](#общие-компоненты-buildingblocks)
7. [Клиентское приложение](#клиентское-приложение)

## Общая структура решения

```mermaid
packageDiagram
    title Общая структура решения AiTestPlatform

    package "AiTestPlatform" {
        package "BuildingBlocks" {
            package "Common" as Common
            package "EventBus" as EventBus
            package "Logging" as Logging
            package "FileStorage" as FileStorage
            package "Validation" as Validation
            package "Caching" as Caching
            package "Security" as Security
        }

        package "Services" {
            package "Identity" as Identity
            package "Courses" as Courses
            package "Testing" as Testing
            package "CodeExecution" as CodeExecution
        }

        package "ApiGateway" as ApiGateway

        package "Frontend" as Frontend

        package "Infrastructure" {
            package "Docker" as Docker
            package "Kubernetes" as Kubernetes
            package "Database" as Database
            package "Monitoring" as Monitoring
        }

        package "Tests" {
            package "UnitTests" as UnitTests
            package "IntegrationTests" as IntegrationTests
            package "E2ETests" as E2ETests
        }

        package "Docs" as Docs
    }

    Identity ..> Common
    Identity ..> EventBus
    Identity ..> Logging
    Identity ..> Validation
    Identity ..> Security

    Courses ..> Common
    Courses ..> EventBus
    Courses ..> Logging
    Courses ..> FileStorage
    Courses ..> Validation
    Courses ..> Caching
    Courses ..> Security

    Testing ..> Common
    Testing ..> EventBus
    Testing ..> Logging
    Testing ..> FileStorage
    Testing ..> Validation
    Testing ..> Caching
    Testing ..> Security

    CodeExecution ..> Common
    CodeExecution ..> EventBus
    CodeExecution ..> Logging
    CodeExecution ..> FileStorage
    CodeExecution ..> Validation
    CodeExecution ..> Security
    CodeExecution ..> Docker

    ApiGateway ..> Identity
    ApiGateway ..> Courses
    ApiGateway ..> Testing
    ApiGateway ..> CodeExecution

    Frontend ..> ApiGateway

    UnitTests ..> Identity
    UnitTests ..> Courses
    UnitTests ..> Testing
    UnitTests ..> CodeExecution
    UnitTests ..> Common

    IntegrationTests ..> Identity
    IntegrationTests ..> Courses
    IntegrationTests ..> Testing
    IntegrationTests ..> CodeExecution
    IntegrationTests ..> EventBus

    E2ETests ..> Frontend
    E2ETests ..> ApiGateway
```

## Микросервис Identity

```mermaid
packageDiagram
    title Структура пакетов микросервиса Identity

    package "Identity" {
        package "Identity.API" as API {
            package "Controllers" as Controllers
            package "Filters" as Filters
            package "Middleware" as Middleware
            package "Configuration" as Configuration
        }

        package "Identity.Application" as Application {
            package "Commands" as Commands
            package "Queries" as Queries
            package "Behaviors" as Behaviors
            package "Validators" as Validators
            package "DTOs" as DTOs
            package "Mappings" as Mappings
            package "Services" as Services
            package "IntegrationEvents" as IntegrationEvents
        }

        package "Identity.Domain" as Domain {
            package "Entities" as Entities
            package "ValueObjects" as ValueObjects
            package "Enums" as Enums
            package "Exceptions" as Exceptions
            package "Interfaces" as Interfaces
            package "Events" as Events
        }

        package "Identity.Infrastructure" as Infrastructure {
            package "Persistence" as Persistence {
                package "Context" as Context
                package "Repositories" as Repositories
                package "Migrations" as Migrations
            }
            package "Services" as InfraServices
            package "EventBus" as EventBusImpl
            package "Security" as SecurityImpl
        }
    }

    package "BuildingBlocks" {
        package "Common" as Common
        package "EventBus" as EventBus
        package "Logging" as Logging
        package "Validation" as Validation
        package "Security" as Security
    }

    API ..> Application
    Application ..> Domain
    Application ..> Infrastructure
    Infrastructure ..> Domain

    Controllers ..> Commands
    Controllers ..> Queries
    Controllers ..> DTOs

    Commands ..> Domain
    Commands ..> Validators
    Queries ..> Domain
    Queries ..> Validators

    Services ..> Domain
    Services ..> IntegrationEvents

    Repositories ..> Entities
    Context ..> Entities

    Application ..> Common
    Application ..> EventBus
    Application ..> Validation

    Infrastructure ..> Common
    Infrastructure ..> EventBus
    Infrastructure ..> Logging
    Infrastructure ..> Security

    EventBusImpl ..> IntegrationEvents
    EventBusImpl ..> EventBus

    SecurityImpl ..> Security
```

## Микросервис Courses

```mermaid
packageDiagram
    title Структура пакетов микросервиса Courses

    package "Courses" {
        package "Courses.API" as API {
            package "Controllers" as Controllers
            package "Filters" as Filters
            package "Middleware" as Middleware
            package "Configuration" as Configuration
        }

        package "Courses.Application" as Application {
            package "Commands" as Commands
            package "Queries" as Queries
            package "Behaviors" as Behaviors
            package "Validators" as Validators
            package "DTOs" as DTOs
            package "Mappings" as Mappings
            package "Services" as Services
            package "IntegrationEvents" as IntegrationEvents
            package "EventHandlers" as EventHandlers
        }

        package "Courses.Domain" as Domain {
            package "Entities" as Entities
            package "ValueObjects" as ValueObjects
            package "Enums" as Enums
            package "Exceptions" as Exceptions
            package "Interfaces" as Interfaces
            package "Events" as Events
        }

        package "Courses.Infrastructure" as Infrastructure {
            package "Persistence" as Persistence {
                package "Context" as Context
                package "Repositories" as Repositories
                package "Migrations" as Migrations
            }
            package "Services" as InfraServices
            package "EventBus" as EventBusImpl
            package "FileStorage" as FileStorageImpl
        }
    }

    package "BuildingBlocks" {
        package "Common" as Common
        package "EventBus" as EventBus
        package "Logging" as Logging
        package "FileStorage" as FileStorage
        package "Validation" as Validation
        package "Caching" as Caching
        package "Security" as Security
    }

    API ..> Application
    Application ..> Domain
    Application ..> Infrastructure
    Infrastructure ..> Domain

    Controllers ..> Commands
    Controllers ..> Queries
    Controllers ..> DTOs

    Commands ..> Domain
    Commands ..> Validators
    Queries ..> Domain
    Queries ..> Validators

    Services ..> Domain
    Services ..> IntegrationEvents

    EventHandlers ..> Domain
    EventHandlers ..> Commands

    Repositories ..> Entities
    Context ..> Entities

    Application ..> Common
    Application ..> EventBus
    Application ..> Validation
    Application ..> Caching

    Infrastructure ..> Common
    Infrastructure ..> EventBus
    Infrastructure ..> Logging
    Infrastructure ..> FileStorage
    Infrastructure ..> Security

    EventBusImpl ..> IntegrationEvents
    EventBusImpl ..> EventHandlers
    EventBusImpl ..> EventBus

    FileStorageImpl ..> FileStorage
```

## Микросервис Testing

```mermaid
packageDiagram
    title Структура пакетов микросервиса Testing

    package "Testing" {
        package "Testing.API" as API {
            package "Controllers" as Controllers
            package "Filters" as Filters
            package "Middleware" as Middleware
            package "Configuration" as Configuration
        }

        package "Testing.Application" as Application {
            package "Commands" as Commands
            package "Queries" as Queries
            package "Behaviors" as Behaviors
            package "Validators" as Validators
            package "DTOs" as DTOs
            package "Mappings" as Mappings
            package "Services" as Services
            package "IntegrationEvents" as IntegrationEvents
            package "EventHandlers" as EventHandlers
        }

        package "Testing.Domain" as Domain {
            package "Entities" as Entities
            package "ValueObjects" as ValueObjects
            package "Enums" as Enums
            package "Exceptions" as Exceptions
            package "Interfaces" as Interfaces
            package "Events" as Events
        }

        package "Testing.Infrastructure" as Infrastructure {
            package "Persistence" as Persistence {
                package "Context" as Context
                package "Repositories" as Repositories
                package "Migrations" as Migrations
            }
            package "Services" as InfraServices
            package "EventBus" as EventBusImpl
            package "FileStorage" as FileStorageImpl
        }
    }

    package "BuildingBlocks" {
        package "Common" as Common
        package "EventBus" as EventBus
        package "Logging" as Logging
        package "FileStorage" as FileStorage
        package "Validation" as Validation
        package "Caching" as Caching
        package "Security" as Security
    }

    API ..> Application
    Application ..> Domain
    Application ..> Infrastructure
    Infrastructure ..> Domain

    Controllers ..> Commands
    Controllers ..> Queries
    Controllers ..> DTOs

    Commands ..> Domain
    Commands ..> Validators
    Queries ..> Domain
    Queries ..> Validators

    Services ..> Domain
    Services ..> IntegrationEvents

    EventHandlers ..> Domain
    EventHandlers ..> Commands

    Repositories ..> Entities
    Context ..> Entities

    Application ..> Common
    Application ..> EventBus
    Application ..> Validation
    Application ..> Caching

    Infrastructure ..> Common
    Infrastructure ..> EventBus
    Infrastructure ..> Logging
    Infrastructure ..> FileStorage
    Infrastructure ..> Security

    EventBusImpl ..> IntegrationEvents
    EventBusImpl ..> EventHandlers
    EventBusImpl ..> EventBus

    FileStorageImpl ..> FileStorage
```

## Микросервис CodeExecution

```mermaid
packageDiagram
    title Структура пакетов микросервиса CodeExecution

    package "CodeExecution" {
        package "CodeExecution.API" as API {
            package "Controllers" as Controllers
            package "Filters" as Filters
            package "Middleware" as Middleware
            package "Configuration" as Configuration
        }

        package "CodeExecution.Application" as Application {
            package "Commands" as Commands
            package "Queries" as Queries
            package "Behaviors" as Behaviors
            package "Validators" as Validators
            package "DTOs" as DTOs
            package "Mappings" as Mappings
            package "Services" as Services
            package "IntegrationEvents" as IntegrationEvents
            package "EventHandlers" as EventHandlers
        }

        package "CodeExecution.Domain" as Domain {
            package "Entities" as Entities
            package "ValueObjects" as ValueObjects
            package "Enums" as Enums
            package "Exceptions" as Exceptions
            package "Interfaces" as Interfaces
            package "Events" as Events
        }

        package "CodeExecution.Infrastructure" as Infrastructure {
            package "Persistence" as Persistence {
                package "Context" as Context
                package "Repositories" as Repositories
                package "Migrations" as Migrations
            }
            package "Services" as InfraServices
            package "EventBus" as EventBusImpl
            package "FileStorage" as FileStorageImpl
            package "Docker" as DockerService
        }

        package "CodeExecution.Runner" as Runner {
            package "Containers" as Containers
            package "Execution" as Execution
            package "Security" as RunnerSecurity
        }
    }

    package "BuildingBlocks" {
        package "Common" as Common
        package "EventBus" as EventBus
        package "Logging" as Logging
        package "FileStorage" as FileStorage
        package "Validation" as Validation
        package "Security" as Security
    }

    API ..> Application
    Application ..> Domain
    Application ..> Infrastructure
    Infrastructure ..> Domain
    Infrastructure ..> Runner

    Controllers ..> Commands
    Controllers ..> Queries
    Controllers ..> DTOs

    Commands ..> Domain
    Commands ..> Validators
    Queries ..> Domain
    Queries ..> Validators

    Services ..> Domain
    Services ..> IntegrationEvents
    Services ..> DockerService

    EventHandlers ..> Domain
    EventHandlers ..> Commands

    Repositories ..> Entities
    Context ..> Entities

    DockerService ..> Containers
    DockerService ..> Execution

    Application ..> Common
    Application ..> EventBus
    Application ..> Validation

    Infrastructure ..> Common
    Infrastructure ..> EventBus
    Infrastructure ..> Logging
    Infrastructure ..> FileStorage
    Infrastructure ..> Security

    Runner ..> Common
    Runner ..> Logging
    Runner ..> Security

    EventBusImpl ..> IntegrationEvents
    EventBusImpl ..> EventHandlers
    EventBusImpl ..> EventBus

    FileStorageImpl ..> FileStorage
```

## Общие компоненты (BuildingBlocks)

```mermaid
packageDiagram
    title Структура пакетов общих компонентов (BuildingBlocks)

    package "BuildingBlocks" {
        package "Common" as Common {
            package "Models" as CommonModels
            package "Extensions" as CommonExtensions
            package "Helpers" as CommonHelpers
            package "Constants" as CommonConstants
        }

        package "EventBus" as EventBus {
            package "Abstractions" as EventBusAbstractions
            package "Events" as EventBusEvents
            package "Kafka" as EventBusKafka
            package "InMemory" as EventBusInMemory
        }

        package "Logging" as Logging {
            package "Abstractions" as LoggingAbstractions
            package "Serilog" as LoggingSerilog
        }

        package "FileStorage" as FileStorage {
            package "Abstractions" as FileStorageAbstractions
            package "MinIO" as FileStorageMinIO
            package "Local" as FileStorageLocal
        }

        package "Validation" as Validation {
            package "Behaviors" as ValidationBehaviors
            package "Extensions" as ValidationExtensions
        }

        package "Caching" as Caching {
            package "Abstractions" as CachingAbstractions
            package "Redis" as CachingRedis
            package "InMemory" as CachingInMemory
        }

        package "Security" as Security {
            package "Abstractions" as SecurityAbstractions
            package "JWT" as SecurityJWT
            package "CurrentUser" as SecurityCurrentUser
        }
    }

    CommonModels ..> CommonExtensions
    CommonModels ..> CommonHelpers

    EventBusKafka ..> EventBusAbstractions
    EventBusKafka ..> EventBusEvents
    EventBusInMemory ..> EventBusAbstractions
    EventBusInMemory ..> EventBusEvents

    LoggingSerilog ..> LoggingAbstractions

    FileStorageMinIO ..> FileStorageAbstractions
    FileStorageLocal ..> FileStorageAbstractions

    ValidationBehaviors ..> ValidationExtensions

    CachingRedis ..> CachingAbstractions
    CachingInMemory ..> CachingAbstractions

    SecurityJWT ..> SecurityAbstractions
    SecurityCurrentUser ..> SecurityAbstractions

    EventBus ..> Common
    Logging ..> Common
    FileStorage ..> Common
    Validation ..> Common
    Caching ..> Common
    Security ..> Common
```

## Клиентское приложение

```mermaid
packageDiagram
    title Структура пакетов клиентского приложения

    package "Frontend" {
        package "src" as Src {
            package "components" as Components {
                package "common" as CommonComponents
                package "auth" as AuthComponents
                package "courses" as CoursesComponents
                package "testing" as TestingComponents
                package "codeExecution" as CodeExecutionComponents
                package "profile" as ProfileComponents
                package "admin" as AdminComponents
            }

            package "pages" as Pages {
                package "auth" as AuthPages
                package "courses" as CoursesPages
                package "testing" as TestingPages
                package "codeExecution" as CodeExecutionPages
                package "profile" as ProfilePages
                package "admin" as AdminPages
            }

            package "store" as Store {
                package "slices" as Slices
                package "actions" as Actions
                package "selectors" as Selectors
                package "middleware" as Middleware
            }

            package "services" as Services {
                package "api" as ApiServices
                package "auth" as AuthServices
                package "courses" as CoursesServices
                package "testing" as TestingServices
                package "codeExecution" as CodeExecutionServices
                package "profile" as ProfileServices
                package "admin" as AdminServices
            }

            package "hooks" as Hooks
            package "utils" as Utils
            package "styles" as Styles
            package "assets" as Assets
            package "config" as Config
            package "types" as Types
        }

        package "public" as Public
        package "node_modules" as NodeModules
    }

    AuthPages ..> AuthComponents
    CoursesPages ..> CoursesComponents
    TestingPages ..> TestingComponents
    CodeExecutionPages ..> CodeExecutionComponents
    ProfilePages ..> ProfileComponents
    AdminPages ..> AdminComponents

    AuthPages ..> Store
    CoursesPages ..> Store
    TestingPages ..> Store
    CodeExecutionPages ..> Store
    ProfilePages ..> Store
    AdminPages ..> Store

    AuthComponents ..> Hooks
    CoursesComponents ..> Hooks
    TestingComponents ..> Hooks
    CodeExecutionComponents ..> Hooks
    ProfileComponents ..> Hooks
    AdminComponents ..> Hooks

    AuthComponents ..> Utils
    CoursesComponents ..> Utils
    TestingComponents ..> Utils
    CodeExecutionComponents ..> Utils
    ProfileComponents ..> Utils
    AdminComponents ..> Utils

    AuthComponents ..> Styles
    CoursesComponents ..> Styles
    TestingComponents ..> Styles
    CodeExecutionComponents ..> Styles
    ProfileComponents ..> Styles
    AdminComponents ..> Styles

    Slices ..> Actions
    Slices ..> Types
    Actions ..> ApiServices
    Selectors ..> Types

    ApiServices ..> AuthServices
    ApiServices ..> CoursesServices
    ApiServices ..> TestingServices
    ApiServices ..> CodeExecutionServices
    ApiServices ..> ProfileServices
    ApiServices ..> AdminServices

    AuthServices ..> Config
    CoursesServices ..> Config
    TestingServices ..> Config
    CodeExecutionServices ..> Config
    ProfileServices ..> Config
    AdminServices ..> Config

    Hooks ..> Store
    Hooks ..> Services
```

## Заключение

Диаграммы пакетов (Package Diagrams) предоставляют детальное представление об организации кода системы AiTestPlatform и зависимостях между пакетами. Они помогают понять:

1. Общую структуру решения и взаимосвязи между проектами
2. Организацию кода внутри каждого микросервиса
3. Структуру общих компонентов (BuildingBlocks) и их использование в микросервисах
4. Организацию кода клиентского приложения

Эти диаграммы являются важным инструментом для разработчиков, позволяя им лучше понять структуру кода и принципы его организации.
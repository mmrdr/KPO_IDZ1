# Finance Tracker App

## 1. Общая идея проекта

**Finance Tracker App** — это консольное приложение для управления личными финансами. Оно позволяет пользователям:

- Вести учет банковских счетов.
- Классифицировать операции по категориям.
- Выполнять аналитические операции для получения статистики по расходам и доходам.
- Импортировать и экспортировать финансовые данные в различных форматах (JSON, YAML, CSV).

Приложение разработано с использованием принципов **SOLID** и **GRASP**, а также применяет паттерны проектирования **GoF**.

## 2. SOLID

- **Single Responsibility Principle (SRP)** – Каждый класс отвечает за одну конкретную задачу:
  - `BankAccountFacade` управляет банковскими счетами.
  - `DataTransferFacade` отвечает за импорт и экспорт данных.
  - `AnalyticFacade` выполняет анализ финансовых операций.
- **Open/Closed Principle (OCP)** – Приложение легко расширяемо без изменения существующего кода:
  - Добавлены классы `JsonExporter`, `YamlExporter`, `CsvExporter`, которые реализуют интерфейс `DataExporter`.
  - Импортеры `JsonDataImporter`, `YamlDataImporter`, `CsvDataImporter` реализуют общий интерфейс `DataImporter`.
- **Liskov Substitution Principle (LSP)** – Подклассы корректно заменяют базовые классы:
  - `BankRepositoryProxy` расширяет `IBankAccountRepository`, но не изменяет его поведение.
- **Interface Segregation Principle (ISP)** – Интерфейсы разделены на более мелкие:
  - `IBankAccountRepository`, `ICategoryRepository`, `IOperationRepository` разделяют логику работы с разными сущностями.
- **Dependency Inversion Principle (DIP)** – Используются абстракции вместо конкретных классов:
  - Взаимодействие с базой данных идет через интерфейсы репозиториев(прокси), а не через конкретные классы.

## 3. Используемые паттерны GoF

### 1. **Фабричный метод**

Используется для создания объектов банковских счетов, категорий и операций:

- Классы: `BankAccountFactory`, `CategoryFactory`, `OperationFactory`.
- Позволяет легко добавлять новые типы счетов, категорий и операций.

### 2. **Фасад**

Обеспечивает унифицированный интерфейс к сложной системе управления финансами:

- Классы: `BankAccountFacade`, `CategoryFacade`, `OperationFacade`, `AnalyticFacade`, `DataTransferFacade`.
- Упрощает использование системы, скрывая сложную логику за удобным API.

### 3. **Прокси (Proxy)**

Применяется для добавления дополнительной логики к репозиториям:

- Классы: `BankRepositoryProxy`, `CategoryRepositoryProxy`, `OperationRepositoryProxy`.
- Позволяет, например, логировать доступ к данным без изменения оригинального репозитория.

### 4. **Стратегия**

Используется для выбора метода экспорта и импорта данных:

- Классы: `JsonExporter`, `YamlExporter`, `CsvExporter`, `JsonDataImporter`, `YamlDataImporter`, `CsvDataImporter`.
- Позволяет динамически выбирать формат работы с данными.

### 5. **Декоратор**

Реализован в одной папке с патерном команда

### 6. **Команда**

Применяется для инкапсуляции операций в отдельные объекты, что позволяет удобно их отменять, логировать и выполнять повторно.

- Классы: `CreateTransactionCommand`, `DeleteTransactionCommand`, `UpdateTransactionCommand`.
- Упрощает работу с операциями, позволяя управлять их выполнением единообразно.

## 4. Запуск проекта в Docker

### 1. Сборка и запуск контейнера

```bash
docker-compose up --build
```

### 2. Зайти в контейнер

```bash
 docker exec -it kpo_idz1-db-1 bash
```
### 3. Посмотреть базу данных 

```bash
 psql -U postgres -d financedb
```

### 4. Миграции базы данных

```bash
dotnet ef migrations add InitialMigration
```


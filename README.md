# HomeAccounting

Этот проект представляет собой веб-приложение для ведения домашней бухгалтерии, разработанное в качестве пет проекта. Приложение позволяет пользователям регистрироваться, входить в систему, создавать категории, добавлять транзакции и просматривать их в удобном формате.

## Описание выполненной работы

1. **Функционал приложения**:
   - Регистрация и авторизация пользователей.
   - Создание, удаление и отображение категорий.
   - Создание, удаление и отображение транзакций.
   - Форматирование суммы и даты транзакций в соответствии с заданными требованиями.

2. **Используемые технологии**:

   **Frontend**:
   - React
   - React Router
   - Bootstrap для стилизации компонентов

   **Backend**:
   - ASP.NET Core Web API
   - JWT для управления доступом
   - База данных PostgreSQL
   - EntityFramework Core в качестве ORM

## Запуск проекта

### Предварительные требования

- Node.js (рекомендуемая версия: 14.x или выше)
- npm (Node Package Manager)
- .NET SDK (рекомендуемая версия: 5.0 или выше)
- PostgreSQL

### Установка

Клонируйте репозиторий на свой локальный компьютер:
   ```sh
   git clone https://github.com/Aybek-Dev/Home-Accounting.git
   ```

#### Backend

1. Настройте подключение к базе данных PostgreSQL в файле `appsettings.json`:
   ```json
   "JwtOptions": {
   "SecretKey": "YOUR_SECRET_KEY",
   "ExpiresHours": "12"
   }
   "ConnectionStrings": {
   "HomeAccountingDbContext": "Host=your_host;Port=ypur_port;Database=your_database;UserId=your_user_id;Password=your_password;"
   }
   ```

2. Примените миграции для создания базы данных:
   ```sh
   dotnet ef database update
   ```

### Запуск

#### Frontend

1. Запустите приложение:
   ```sh
   npm start
   ```

2. Откройте браузер и перейдите по адресу:
   ```
   http://localhost:YOUR
   ```

#### Backend

1. Запустите API сервер:
   ```sh
   dotnet run
   ```

2. API сервер будет доступен по адресу:
   ```
   http://localhost:YOUR
   ```

## Структура проекта

- `frontend/`
  - `src/`
    - `api/`: функции для взаимодействия с сервером (login, register, getCategories, createCategory, deleteCategory, getTransactions, createTransaction, deleteTransaction).
    - `components/`: компоненты приложения (CategoryForm, TransactionsTable).
    - `pages/`: страницы приложения (Home, Login, Register, Categories, Transactions).
    - `App.js`: основной компонент приложения с маршрутизацией.
    - `index.js`: точка входа в приложение.

- `backend/`
  - `Endpoint/`: эндпоинты для обработки запросов.
  - `Models/`: модели данных.
  - `Migrations/`: контекст базы данных и миграции.
  - `Services/`: сервисы для бизнес-логики.
  - `Program.cs`: конфигурация и запуск приложения.
```
- Автор: Teor
```

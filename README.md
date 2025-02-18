
# JCom

Этот проект является частью моего портфолио. Он демонстрирует мои навыки в разработке веб-приложений с использованием современных технологий.

## Стек технологий

- .NET 9.0
- ASP.NET Core
- Entity Framework Core
- MediatR
- PostgreSQL
- Humanizer
- Microsoft.Extensions.DependencyInjection

## Структура проекта

- **Domain**: Содержит основные доменные модели и логику.
- **Persistence**: Реализация доступа к данным и конфигурация базы данных.
- **Web.Application**: Содержит бизнес-логику и сервисы.
- **Web.Application.Interfaces**: Интерфейсы для сервисов и репозиториев.
- **Web**: Веб-интерфейс и API.

## Установка и запуск

1. Клонируйте репозиторий:
    ```sh
    git clone https://github.com/RedYara/JCom.git
    ```

2. Перейдите в директорию проекта:
    ```sh
    cd JCom
    ```

3. Восстановите зависимости:
    ```sh
    dotnet restore
    ```

4. Постройте проект:
    ```sh
    dotnet build
    ```

5. Запустите проект:
    ```sh
    dotnet run --project Web/Web.csproj
    ```

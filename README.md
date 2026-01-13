# Feedback API - Clean Architecture

This is a .NET Core 9 Web API built following the **Clean Architecture** principles.

## Project Structure

The solution consists of four main projects (layers):

1.  **Feedback.Domain** (Class Library)
    *   Contains the core business entities and interfaces.
    *   No dependencies on other projects.
    *   *Key Files*: `FeedbackEntity.cs`, `IRepository.cs`, `IFeedbackRepository.cs`.

2.  **Feedback.Application** (Class Library)
    *   Contains business logic, DTOs, and Service definitions.
    *   Depends only on `Feedback.Domain`.
    *   *Key Files*: `FeedbackService.cs`, `IFeedbackService.cs`, `CreateFeedbackDto.cs`.

3.  **Feedback.Infrastructure** (Class Library)
    *   Contains implementation of data access (Entity Framework Core).
    *   Depends on `Feedback.Application` and `Feedback.Domain`.
    *   *Key Files*: `FeedbackDbContext.cs`, `FeedbackRepository.cs`.

4.  **Feedback.WebAPI** (ASP.NET Core Web API)
    *   The entry point of the application. Handles HTTP requests.
    *   Depends on `Feedback.Application` and `Feedback.Infrastructure`.
    *   *Key Files*: `FeedbackController.cs`, `Program.cs`.

## Getting Started

### Prerequisites
*   .NET 9.0 SDK
*   SQL Server (LocalDB or full instance)

### Running the Application

1.  **Navigate to the project directory:**
    ```bash
    cd src/Feedback.WebAPI
    ```

2.  **Update Database Connection (if needed):**
    Check `appsettings.Development.json` for the connection string. Default is `(localdb)\mssqllocaldb`.

3.  **Create Database Migrations:**
    ```bash
    dotnet ef migrations add InitialCreate --project ../Feedback.Infrastructure --startup-project .
    dotnet ef database update --project ../Feedback.Infrastructure --startup-project .
    ```

4.  **Run the API:**
    ```bash
    dotnet run
    ```

5.  **Explore API:**
    Open your browser to `http://localhost:5000/swagger` (or the port shown in the console) to test the endpoints via Swagger UI.

## Features implemented
*   **Clean Architecture**: Separation of concerns.
*   **Repository Pattern**: Generic and specific repositories.
*   **Service Layer**: Business logic encapsulation.
*   **Entity Framework Core**: Code-first data access.
*   **Swagger/OpenAPI**: API documentation.
*   **Result Pattern**: Standardized operation results handling.

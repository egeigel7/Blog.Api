# Blog.Api

## Endpoints

### GET /blogs
- **Description:** Returns all published blog content.
- **Method:** GET
- **Route:** /blogs
- **Response:** 200 OK, JSON array of published blog posts. Each post includes:
  - `id` (GUID)
  - `title` (string)
  - `body` (string)
  - `status` (string, should be 'Published')
  - `version` (integer)

Example response:
```json
[
  {
    "id": "b1a2c3d4-e5f6-7890-abcd-1234567890ef",
    "title": "My First Blog Post",
    "body": "This is the content of the post.",
    "status": "Published",
    "version": 1
  }
]
```

### POST /createDraft
- **Description:** Creates a new draft blog post.
- **Method:** POST
- **Route:** /createDraft
- **Request Body:**
```json
{
  "title": "string (required, max 200 chars)",
  "body": "string (required)"
}
```
- **Response:** 200 OK, GUID of the created draft post.

### POST /saveContent
- **Description:** Saves content for an existing draft blog post.
- **Method:** POST
- **Route:** /saveContent
- **Request Body:**
```json
{
  "id": "GUID (required)",
  "content": "string (required)"
}
```
- **Response:** 200 OK (no content)

---

## Overview
Blog.Api is a modern, event-sourced .NET API designed to showcase advanced software engineering practices, including Domain-Driven Design (DDD), SOLID principles, CQRS, and robust validation and error handling. The project is structured to demonstrate clean architecture, testability, and maintainability—qualities expected of a senior software engineer.

## Purpose
This repository serves as a demonstration of:
- **Domain-Driven Design (DDD)**: Clear separation of domain, application, and infrastructure concerns.
- **SOLID Principles**: Code is modular, testable, and adheres to best practices for maintainability and extensibility.
- **Event Sourcing**: Uses Marten to persist domain events and aggregate state.
- **CQRS**: Command handlers encapsulate business logic and coordinate persistence.
- **Validation & Error Handling**: FluentValidation and ProblemDetails middleware ensure robust, user-friendly API boundaries.
- **Comprehensive Testing**: Unit tests for domain logic, command handlers, repositories, and validators.

## Key Architectural Details
- **Aggregates**: The core domain concept is the `Content` aggregate, representing a blog post draft or published content. All state changes are event-sourced.
- **Repository Pattern**: Abstracts persistence logic for aggregates, enabling testability and separation of concerns.
- **Command Handlers**: Encapsulate business logic for creating and saving content, using dependency injection and repository abstractions.
- **Validation**: All API requests are validated using FluentValidation, ensuring data integrity and clear error messages.
- **Global Error Handling**: ProblemDetails middleware provides consistent, industry-standard error responses.

## Domain Model
### Content Aggregate
- **Properties**: `Id`, `Title`, `Body`, `Status`, `Version`
- **Events**: `ContentCreated`, `ContentSaved`
- **Behavior**: State changes are applied via event application methods (`Apply`).

### Example: Creating a Draft
1. `CreateDraftCommandHandler` receives a command.
2. Validates the request.
3. Creates a new `Content` aggregate and applies `ContentCreated` and `ContentSaved` events.
4. Persists via the repository (event-sourced with Marten).

### Example: Saving Content
1. `SaveContentCommandHandler` loads the aggregate from the repository.
2. Validates business rules (status, content changes).
3. Applies a `ContentSaved` event and persists changes.

## Validation & Error Handling
- **FluentValidation**: All request DTOs have validators with comprehensive unit tests.
- **ProblemDetails**: Global error handling middleware returns standardized error responses for all exceptions and validation failures.

## Testing
- **Unit Tests**: Cover command handlers, repository, aggregate logic, and validators.
- **Mocking**: Infrastructure dependencies (e.g., Marten session) are mocked for isolation.
- **Test Coverage**: Both happy-path and error scenarios are tested.

## How to Run
1. Restore dependencies:
   ```sh
   dotnet restore src/Blog/Blog.Api/Blog.Api/Blog.Api.csproj
   ```
2. Build and run the API:
   ```sh
   dotnet run --project src/Blog/Blog.Api/Blog.Api/Blog.Api.csproj
   ```
3. Run tests:
   ```sh
   dotnet test src/Blog/Blog.Api/Blog.Api.Tests/Blog.Api.Tests.csproj
   ```
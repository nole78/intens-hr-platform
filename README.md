# intens-hr-platform

HR platform for adding and monitoring job candidates and their skills, developed as part of the Intens internship task.

## Table of Contents

- [Features](#features)
- [Tech Stack](#tech-stack)
- [Project Structure](#project-structure)
- [Running the Project](#running-the-project)
- [Database Design](#database-design)
- [Architectural Decisions](#architectural-decisions)
- [Seed Data](#seed-data)
- [Testing](#testing)
- [Most Interesting Part of the Task](#most-interesting-part-of-the-task)

## Features

- Add, update and remove job candidates
- Add skills
- Assign and remove skills from candidates
- Search candidates by:
  - full name
  - one or multiple skills
- Seed data for quick testing
- Unit tests for service layer
- Result pattern based error handling

---

## Tech Stack

- ASP.NET Core Web API (.NET 8)
- Entity Framework Core
- PostgreSQL
- Docker
- NUnit
- Moq

---

## Project Structure

```text
project/
└── Server/
    ├── Tests/
    │   └── Services/                 # Service unit tests
    │
    └── Server/
        ├── Common/                   # Result pattern implementation
        ├── Controllers/              # API controllers
        ├── Domain/
        │   └── Models/               # Database entities
        ├── DTOs/                     # Request/response DTOs
        ├── Migrations/               # EF Core migrations
        ├── Persistence/
        │   ├── Configurations/       # EF configurations and seed data
        │   ├── Context/              # Database context
        │   └── Repositories/         # Repository implementations
        │       ├── Candidates/
        │       ├── CandidateSkills/
        │       └── Skills/
        └── Services/                 # Business logic layer
            ├── CandidateService/
            └── SkillService/
```

---

## Running the Project

### 1. Clone repository

```bash
git clone https://github.com/nole78/intens-hr-platform.git
cd intens-hr-platform
```

---

### 2. Start PostgreSQL container

From the root folder run:

```bash
docker compose up -d
```

---

### 3. Install EF Core CLI

```bash
dotnet tool install --global dotnet-ef
```

---

### 4. Restore dependencies

Inside `Server/Server/` run:

```bash
cd Server/Server
dotnet restore
```

---

### 5. Apply database migrations

Inside `Server/Server/` run:

```bash
dotnet ef database update
```

---

### 6. Start the application

```bash
dotnet run
```

After starting the application, check the console output to find the correct URL (including the port).  
**Swagger** will be available at:
http://localhost:{port}/swagger

### 7. Run tests

```bash
dotnet test ../Tests/Tests.csproj
```
---

## Database Design

The database is modeled as a PostgreSQL docker container with three tables:

- `Candidates`
- `Skills`
- `CandidateSkills`

`CandidateSkills` is used as a junction table for the many-to-many relationship between candidates and skills, and contains only their primary keys.

The `Skill` entity contains only:
- `Id`
- `Name`

while the `Candidate` entity contains:
- Id
- Full name
- Date of birth
- Contact number
- Email
- Related skills

`DateOnly` was used for storing candidate birth dates.

---

## Architectural Decisions

### Repository Pattern

Repositories are used to separate data access logic from the business layer and simplify unit testing.

### DTO Usage

DTOs are used instead of returning EF Core entities directly in order to:
- avoid serialization cycles
- reduce coupling between API and database models
- expose only required response data

### Result Pattern

A custom `Result` / `Result<T>` implementation is used for consistent error handling across the application.

The pattern includes:
- error types (`Internal`, `NotFound`, `Conflict`, `Validation`)
- success/failure states
- extension methods for controller responses

This keeps controllers cleaner and prevents business logic from leaking into the API layer.

---

## Seed Data

The application includes predefined seed data for:
- candidates
- skills
- candidate-skill relations

This allows the application to be tested immediately after migration.

---

## Testing

Unit tests were implemented for the service layer using:
- NUnit
- Moq

The tests cover:
- successful operations
- repository interaction
- failure cases

---

## Most Interesting Part of the Task

The most interesting part of the task was designing the communication between the service layer and controllers using the custom `Result` pattern.

Instead of throwing exceptions for expected business cases, services return results containing success states and error information. This approach simplified controller logic by using extesnion methods to handle Api response based on error type and made the application easier to test.

Another interesting challenge was handling many-to-many relationships and avoiding JSON serialization cycles when returning EF Core entities. This was solved by introducing DTOs and separating API responses from persistence models.

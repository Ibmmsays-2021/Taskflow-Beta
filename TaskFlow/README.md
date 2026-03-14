# TaskFlow - Ticket Management System

TaskFlow is a robust, scalable ticket management system built using **Clean Architecture (Onion Architecture)** and **Domain-Driven Design (DDD)** principles. This project serves as a professional POC demonstrating best practices in .NET development.

## 🏗️ Architecture Overview
The project is divided into four distinct layers to ensure separation of concerns and maintainability:

* **Domain:** Contains enterprise logic, entities, and value objects. No dependencies.
* **Application:** Contains business logic, DTOs, Mapping, and CQRS handlers.
* **Infrastructure:** Implementation of data persistence (EF Core & Dapper), Identity, and external services.
* **API (TaskFlow):** The entry point, handling HTTP requests and Dependency Injection.



## 🛠️ Tech Stack
* **Backend:** .NET 10 (C# 14)
* **Database:** MS SQL Server
* **ORM:** Entity Framework Core (Commands/Writes) & Dapper (Queries/Reads)
* **Security:** ASP.NET Core Identity with JWT Authentication
* **Validation:** FluentValidation
* **Mapping:** AutoMapper

## 🚀 Features implemented
- [x] **Global Query Filters:** Automatic Soft Delete handling.
- [x] **Automated Auditing:** `TrackedEntity` base class for automatic timestamps.
- [x] **Generic Repository Pattern:** Combined with Dapper for high-performance reads.
- [x] **Fluent API Configurations:** Decoupled database schema mapping.

## 🏁 Getting Started

### Prerequisites
* .NET 10 SDK
* SQL Server (LocalDB or Express)

### Installation
1. Clone the repository:
   ```bash
   git clone [https://github.com/](https://github.com/)[Your-Username]/TaskFlow.git
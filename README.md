

# Fullstack Ecommerce:  Eshop

![TypeScript](https://img.shields.io/badge/TypeScript-v.4-green)
![SASS](https://img.shields.io/badge/SASS-v.4-hotpink)
![React](https://img.shields.io/badge/React-v.18-blue)
![Redux toolkit](https://img.shields.io/badge/Redux-v.1.9-brown)
![.NET Core](https://img.shields.io/badge/.NET%20Core-v.8-purple)
![EF Core](https://img.shields.io/badge/EF%20Core-v.8-cyan)
![PostgreSQL](https://img.shields.io/badge/PostgreSQL-v.16-darkblue)

This project involves creating a Fullstack e-commerce platform with a cutting-edge frontend built on TypeScript, React, and Redux Toolkit. The backend is powered by ASP.NET Core 8, utilizing Entity Framework Core for database operations with PostgreSQL. The goal is to deliver a seamless shopping experience for users and provide a robust management system for administrators.

- **Frontend:** TypeScript, React, Redux Toolkit, React Router, Material UI, Jest, React-Hook-Form
  - **Repository:** You can find the frontend project repository [here](https://github.com/moscuet/fs17-Frontend)
  - **Live Demo:** Experience the innovation firsthand by exploring our live demo at [here](https://virtuous-motivation-production.up.railway.app/index.html).

- **Backend:** ASP.NET Core 8, Entity Framework Core, PostgreSQL

## Table of Contents

1. [Technologies and Libraries](#technologies-and-libraries)
2. [Getting Started](#getting-started)
3. [Relational Database Design](#relational-database-design)
4. [Folder Structure](#folder-structure)
5. [Clean Architecture Overview](#clean-architecture-overview)
6. [API Documentation](#api-documentation)
7. [Features](#features)
8. [Testing](#testing)

## Technologies and Libraries

This section outlines the core technologies and essential libraries used in the backend of this e-commerce application, explaining their functions and significance in the overall architecture.

| Technology            | Function                                                                                | Version       |
| --------------------- | --------------------------------------------------------------------------------------- | ------------- |
| ASP.NET Core          | Primary framework for server-side logic, routing, middleware, and dependency management | .NET Core 8.0 |
| Entity Framework Core | ORM (Object-Relational Mapper) for database operations, simplifying SQL queries         | 8.0.4         |
| PostgreSQL            | Relational database management system for storing application data                      | 16            |

| Library                       | Function                                                                     | Version |
| ----------------------------- | ---------------------------------------------------------------------------- | ------- |
| AutoMapper                    | Automates mapping of data entities to DTOs, reducing manual coding           | 12.0.1  |
| Microsoft.AspNetCore.Identity | Manages user authentication, security, password hashing, and role management | 6.0.0   |
| JWT Bearer Authentication     | Implements token-based authentication for securing API endpoints             | 7.5.1   |
| xUnit                         | Framework for unit testing, ensuring components work correctly in isolation  | 2.4.1   |
| Moq                           | Mocking library used with xUnit to simulate dependencies during testing      | 4.16.1  |

## Getting Started

### Prerequisites

Before you begin, ensure you have the following prerequisites installed on your development environment:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [PostgreSQL](https://www.postgresql.org/download/)
- [Visual Studio](https://visualstudio.microsoft.com/downloads/) or [Visual Studio Code](https://code.visualstudio.com/download) (optional but recommended)

### Clone the Repository

Clone the backend repository to your local machine:

```
git clone https://github.com/adhanif/fs17_CSharp_FullStack
cd CSharp_FullStack
cd Eshop:WebApi
dotnet restore
dotnet run
```

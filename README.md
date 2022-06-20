
# DotNet6TestApp

> The education projects for the ASP.NET 6 with CRUD operations to SQL Server and ASP.NET Identity

The application is written in the **Asp.Net Web API - using .NET 6**

## Requirements

- [Install](https://www.microsoft.com/net/download/windows#/current) the latest .NET 6 SDK (using older versions may lead to 502.5 errors when hosted on IIS or application exiting immediately after starting when self-hosted)

### What are projects containing:

- TestApp:
  - IoC with Autofac
  - Global using statesments in separate file
  - CQRS pattern
  - EF Core 6 (MediatR)
  - ASP.NET Custom filters
  - Fluent validation
  - Middleware
  - Serilog (console/file)
  - Send e-mails service by MailKit
  - Map models by AutoMapper
  - Swagger UI

- AspIdentityApp:
  - IoC with Autofac
  - Authenticate actions with ASP.NET 6 Identity
  - Database initialization on project run
  - Global using statesments in separate file
  - Fluent validation
  - Middleware
  - Serilog (console/file)
  - Swagger UI  
# ElGuerre.Items.Api
Is a SAMPLE ASP.NET Core API Rest to show and use it as a template to create new API Rest Proyects.

**API project**:
- Think controllers
- Application
    - Models
    - 
- Infrastructure
    - Middlewares
    - Filters
    ...
- ...

**Application project** group by Domain (Customers and Accounts):
- [Serilog](https://serilog.net/)
- [Swagger](https://docs.microsoft.com/es-es/aspnet/core/tutorials/getting-started-with-swashbuckle?view=aspnetcore-2.2&tabs=visual-studio)
- Entity Framework Core for Commands.
- [Dapper](https://github.com/StackExchange/Dapper/tree/master/Dapper) for Queries
- [Automapper](https://automapper.org/). Optional to map Model/Requests and Responses/ViewModels.
- ...

# Next steps
All those new Patterns, Tools and Platforms, will be Blog Post entries in [elGuerre.com](https://elguerre.com)
## Learning
1. Everything using Docker
2. Everything using Kubernetes


# Runnng the App
1. Using Kestrel from Visual Studio:
`
xxx
`
2. Using Docker from Visual Studio or Docker Command Line:
`
xxx
`
3. Run Entity Framework Migration: 
```
cd ElGuerre.Items.Api
dotnet ef migrations add Init --startup-project ..\src\ElGuerre.Items.Api.csproj
dotnet ef database update --startup-project ..\src\ElGuerre.Items.Api.csproj
``` 
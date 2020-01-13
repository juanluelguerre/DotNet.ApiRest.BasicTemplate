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

# Generate and publish Template

## Generate a new token to access Azure DevOs - Artifacts:
- %APPDATA%\NuGet\CredentialProvider.VSS.exe -U https://pkgs.dev.azure.com/MdAbogacia/_packaging/MdAbogacia/nuget/v3/index.json

## Update Nuget.config
- Update ```%APPDATA%\NuGet\NuGet.config``` and delete properties "elguerre".
- Set or updated Nuget.config properties with the password already got in the previous steps
- `nuget.exe sources Add -Name MdAbogacia -Source https://pkgs.dev.azure.com/MdAbogacia/_packaging/MdAbogacia/nuget/v3/index.json -username unused -password ### TOKEN ###`


## Package and publish a new template version
- Nuget.exe pack ElGuerre.Items.API.nuspec -Version 1.0.n
- nuget.exe push -Source ElGuerre -ApiKey AzureDevOps ElGuerre.Items.API.1.0.n.nupkg

## Install and uninstall templates

Clear cache: ```dotnet nuget locals http-cache --clear```
Add new templates ```dotnet new -i ElGuerre.Items.API::*```

**Nota:** List of templeates cached: ```dotnet nuget locals all --list```
**Nota:** Remove cached of templatess Http and Temps: ```dotnet nuget locals http-cache --clear``` y ```dotnet nuget locals temp --clear```

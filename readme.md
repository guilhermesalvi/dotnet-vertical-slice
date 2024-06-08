# dotnet-vertical-slice

Template designed to create a vertical slice architecture in .NET projects.

> 🚧 This project is currently under development and subject to change. Please check back later for more updates.

## Projects structure 🏗️

```
dotnet-vertical-slice/
│
├── src/
│   ├── Api/
│
├── tests/
│   ├── IntegrationTests/
│   ├── UnitTests/
│
└── VerticalSlice.sln
```

## Main technologies 🚀

- [C#](https://docs.microsoft.com/pt-br/dotnet/csharp/)
- [ASP.NET Core](https://learn.microsoft.com/pt-br/aspnet/core/?view=aspnetcore-8.0&viewFallbackFrom=aspnetcore-8)
- [Swagger](https://learn.microsoft.com/en-us/aspnet/core/tutorials/web-api-help-pages-using-swagger?view=aspnetcore-8.0)
- [Serilog](https://github.com/serilog/serilog)
- [FluentValidation](https://github.com/FluentValidation/FluentValidation)
- [MediatR](https://github.com/jbogard/MediatR)
- [Entity Framework Core](https://docs.microsoft.com/pt-br/ef/core/)
- [Docker](https://www.docker.com/)
- [Docker Compose](https://docs.docker.com/compose/)
- [xUnit](https://xunit.net/)
- [FluentAssertions](https://github.com/fluentassertions/fluentassertions)

## Generating Nuget package locally 📦

To generate the Nuget package, run the command below in the root directory of the project:

```bash
dotnet pack .\nuget.csproj -c Release
```

## Installing package ⚗️

To install the package, run the command below (note package version, in this case 0.0.1):

```bash
dotnet new -i .\VerticalSlice.0.0.1.nupkg
```

## Creating new project based on this template ✨

To create a new project based on this template, run the command below:

```bash
dotnet new vertical-slice -n ProjectName
```
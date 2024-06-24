# orangotango-core
![Logo](https://github.com/wesleycosta/orangotango/raw/main/images/logos/logo_full.png)

[![CodeFactor](https://www.codefactor.io/repository/github/wesleycosta/orangotango-core/badge)](https://www.codefactor.io/repository/github/wesleycosta/orangotango-core)
[![.NET](https://github.com/wesleycosta/orangotango-core/actions/workflows/dotnet.yml/badge.svg)](https://github.com/wesleycosta/orangotango-core/actions/workflows/dotnet.yml)

**orangotango-core** contains essential core packages intended for sharing among microservices in the Orangotango ecosystem.

### Packages Overview:

- **Orangotango.Api**: This package provides APIs centralizing Swagger configuration, standardized response patterns, middleware for logging incoming requests and outgoing responses. [![NuGet](https://img.shields.io/nuget/v/Orangotango.Api.svg)](https://www.nuget.org/packages/Orangotango.Api)

- **Orangotango.Core**: The kernel package for microservices, encompassing abstractions for messaging, events, repositories, services, aggregations, and more. [![NuGet](https://img.shields.io/nuget/v/Orangotango.Core.svg)](https://www.nuget.org/packages/Orangotango.Core)

- **Orangotango.Events**: Contains all solution events, establishing contracts for easy integration via messaging topology, facilitating data replication across microservices. [![NuGet](https://img.shields.io/nuget/v/Orangotango.Events.svg)](https://www.nuget.org/packages/Orangotango.Events)

- **Orangotango.Infra**: Includes infrastructure-related configurations such as Entity Framework contexts, ELK-based logging configuration, Messaging using Mass Transit and RabbitMQ, among others. [![NuGet](https://img.shields.io/nuget/v/Orangotango.Infra.svg)](https://www.nuget.org/packages/Orangotango.Infra)

### Purpose:

The project aims to provide a structured foundation for developing and scaling microservices within the Orangotango environment. By centralizing core functionalities into reusable packages, it promotes consistency, scalability, and ease of maintenance across different services.

### Execution:

To utilize the packages within your microservices:

1. Clone the repository:

   ```bash
   git clone https://github.com/wesleycosta/orangotango-core.git
   ```

2. Open the solution file (`Orangotango.sln`) in your preferred IDE.

### Contributing:

Contributions are welcome! If you have ideas for improvements or new features, feel free to submit issues and pull requests.

### License:

This project is licensed under the [MIT License](LICENSE).

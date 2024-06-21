# pixel-hotel-core

[![CodeFactor](https://www.codefactor.io/repository/github/wesleycosta/pixel-hotel-core/badge)](https://www.codefactor.io/repository/github/wesleycosta/pixel-hotel-core)
[![.NET](https://github.com/wesleycosta/pixel-hotel-core/actions/workflows/dotnet.yml/badge.svg)](https://github.com/wesleycosta/pixel-hotel-core/actions/workflows/dotnet.yml)

**pixel-hotel-core** contains essential core packages intended for sharing among microservices in the PixelHotel ecosystem.

### Packages Overview:

- **PixelHotel.Api**: This package provides APIs centralizing Swagger configuration, standardized response patterns, middleware for logging incoming requests and outgoing responses. [![NuGet](https://img.shields.io/nuget/v/PixelHotel.Api.svg)](https://www.nuget.org/packages/PixelHotel.Api)

- **PixelHotel.Core**: The kernel package for microservices, encompassing abstractions for messaging, events, repositories, services, aggregations, and more. [![NuGet](https://img.shields.io/nuget/v/PixelHotel.Core.svg)](https://www.nuget.org/packages/PixelHotel.Core)

- **PixelHotel.Events**: Contains all solution events, establishing contracts for easy integration via messaging topology, facilitating data replication across microservices. [![NuGet](https://img.shields.io/nuget/v/PixelHotel.Events.svg)](https://www.nuget.org/packages/PixelHotel.Events)

- **PixelHotel.Infra**: Includes infrastructure-related configurations such as Entity Framework contexts, ELK-based logging configuration, Messaging using Mass Transit and RabbitMQ, among others. [![NuGet](https://img.shields.io/nuget/v/PixelHotel.Infra.svg)](https://www.nuget.org/packages/PixelHotel.Infra)

### Purpose:

The project aims to provide a structured foundation for developing and scaling microservices within the PixelHotel environment. By centralizing core functionalities into reusable packages, it promotes consistency, scalability, and ease of maintenance across different services.

### Installation:

To utilize the packages within your microservices:

1. Clone the repository:

   ```bash
   git clone https://github.com/wesleycosta/pixel-hotel-core.git
   ```

2. Open the solution file (`PixelHotel.sln`) in your preferred IDE.

### Contributing:

Contributions are welcome! If you have ideas for improvements or new features, feel free to submit issues and pull requests.

### License:

This project is licensed under the [MIT License](LICENSE).

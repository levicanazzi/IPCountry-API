# IPCountry API

A **.NET 8** REST API that identifies the country of an IP address, keeps that data up to date in the background, and generates aggregated reports by country. Built as a technical challenge, focusing on layered architecture, CQRS and clean code practices.

![.NET](https://img.shields.io/badge/.NET-8.0-512BD4?logo=dotnet&logoColor=white)
![C#](https://img.shields.io/badge/C%23-12-239120?logo=csharp&logoColor=white)
![SQL Server](https://img.shields.io/badge/SQL%20Server-EF%20Core%208-CC2927?logo=microsoftsqlserver&logoColor=white)
![Swagger](https://img.shields.io/badge/OpenAPI-Swagger-85EA2D?logo=swagger&logoColor=black)

## Features

- **Country lookup by IP**: returns the country associated with an IP, with an in-memory cache to avoid repeated database hits.
- **Automatic refresh**: a cron-scheduled `HostedService` walks through the stored IPs, queries the public [ip2c.org](https://ip2c.org) service and updates each IP's country, in batches of 100.
- **Report by country**: number of addresses per country and the date of the last update, with an optional filter by two-letter country codes.
- **Validation and domain notifications**: requests are validated with FluentValidation inside a MediatR pipeline, and errors are returned in a consistent format.

## Endpoints

| Method | Route                  | Description                                        |
| ------ | ---------------------- | -------------------------------------------------- |
| `GET`  | `/v1/countries/{ip}`   | Returns the country associated with the given IP   |
| `POST` | `/v1/countries/report` | Generates the report of addresses grouped by country |

**Example: filtered report**

```http
POST /v1/countries/report
Content-Type: application/json

{
  "twoLetterCodes": ["GR", "US"]
}
```

```json
[
  { "countryName": "Greece", "addressesCount": 12, "lastAddressUpdated": "2024-11-04T10:00:00" },
  { "countryName": "United States", "addressesCount": 7, "lastAddressUpdated": "2024-11-04T10:00:00" }
]
```

Sending an empty `twoLetterCodes` (or omitting it) returns every country. The values above are illustrative.

## Architecture

The solution is split into layers, with dependencies pointing inward:

```
IpCountry.API                  → Controllers, hosted service, DI, Swagger
IpCountry.Application          → Commands/handlers (MediatR), validators, aggregates, contracts
IpCountry.Domain               → Domain notifications
IpCountry.Infrastructure.Core  → Base handler, validation pipeline, hosted services, error models
IpCountry.Repository           → EF Core, mappings, migrations, repositories, Unit of Work, ip2c.org integration
```

Highlighted patterns and decisions:

- **CQRS with MediatR**: each use case is a command with its own handler and validator.
- **Result pattern** (`CSharpFunctionalExtensions`) instead of exceptions for expected failures.
- **ValidationBehavior** in the MediatR pipeline, running FluentValidation validators before the handlers.
- **Unit of Work + Repository** on top of EF Core; the report uses raw SQL (Dapper) since it is a read-only aggregation.
- **In-memory cache** (`IMemoryCache`) on the IP lookup, invalidated when the background refresh detects a country change.
- **Cron-based background job** (`NCrontab`), configurable in `appsettings.json`.

## Tech Stack

.NET 8 · ASP.NET Core · Entity Framework Core 8 · SQL Server · MediatR · FluentValidation · CSharpFunctionalExtensions · Dapper · NCrontab · Swagger/Swashbuckle

## Getting Started

**Prerequisites:** [.NET 8 SDK](https://dotnet.microsoft.com/download), SQL Server (local or Docker) and the `dotnet-ef` tool.

1. Clone the repository

   ```bash
   git clone https://github.com/levicanazzi/IPCountry-API.git
   cd IPCountry-API
   ```

2. Configure the connection string. Avoid storing the password in a file; use *user secrets*:

   ```bash
   cd IpCountry.API
   dotnet user-secrets init
   dotnet user-secrets set "ConnectionStrings:DefaultConnection" "Server=localhost;Database=IpCountry;User Id=sa;Password=<YOUR_PASSWORD>;Encrypt=True;TrustServerCertificate=True;"
   ```

3. Apply the migrations

   ```bash
   dotnet tool install --global dotnet-ef
   dotnet ef database update --project ../IpCountry.Repository --startup-project .
   ```

4. Run the API

   ```bash
   dotnet run --launch-profile https
   ```

   Swagger opens at `https://localhost:7165/swagger`.

> The database is not seeded: to see the lookup and the report working, insert a few rows into the `Countries` and `IPAddresses` tables. The background refresh then corrects each IP's country.

### Scheduling

The job frequency is defined in `appsettings.json` (cron expression with seconds). The default runs at the top of every hour:

```json
"HostedServiceSettings": {
  "CronExpression": "0 0 * * * *"
}
```

## Roadmap

- Unit tests for the handlers and integration tests for the endpoints
- Use `IHttpClientFactory` with a retry policy for the ip2c.org integration
- Dockerfile and `docker-compose` with SQL Server to start everything with one command
- Endpoint to register IPs and an initial data seed
- Distributed cache (Redis) for multi-instance deployments

## Author

**Levi Canazzi** · [GitHub](https://github.com/levicanazzi)

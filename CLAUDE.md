# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Project Overview

Arex388.Carmine is a .NET Standard 2.0 library that provides a client for the Carmine.io API v2, used for GPS fleet tracking. The library supports both single-account usage via `ICarmineClient` and multi-account usage via `ICarmineClientFactory`.

## Build and Development Commands

```bash
# Build the solution
dotnet build

# Run tests
dotnet test

# Run tests with code coverage (PowerShell)
cd Arex388.Carmine.Tests
./Coverage.ps1

# Run benchmarks
cd Arex388.Carmine.Benchmarks
dotnet run -c Release
```

## Architecture and Key Components

### Core Structure
- **Arex388.Carmine/**: Main library implementing the Carmine.io API client
  - `ICarmineClient`: Interface for single-account operations
  - `ICarmineClientFactory`: Factory for multi-account scenarios
  - `CarmineClient`: Main implementation using HttpClient
  
### Request/Response Pattern
- **Slices/**: Contains vertical slice architecture with request/response pairs for each API operation:
  - `GetTrip`, `GetUser`, `GetVehicle`: Single entity retrieval
  - `ListTrips`, `ListUsers`, `ListVehicles`: Collection retrieval
- Each slice follows a consistent pattern with nested `Request` and `Response` classes

### Type System
- **ValueObjects/**: Strongly-typed IDs using StronglyTypedId source generator
  - `TripId`, `UserId`, `VehicleId`
- **Enums/**: Domain enums using NetEscapades.EnumGenerators with `[EnumExtensions]` attribute
- **Models/**: Core domain models (`Trip`, `TripExpanded`, `User`, `Vehicle`, `Location`, `Event`, `Waypoint`)
- **Converters/**: Custom JSON converters for deserializing domain models from the API

### Dependency Injection
- Configured via `ServiceCollectionExtensions.AddCarmine()`
- Supports both single-account (with options) and multi-account (without options) registration

### Testing Structure
- **Arex388.Carmine.Tests/**: xUnit integration tests
  - Uses User Secrets for API credentials
  - Tests organized by domain (Trips, Users, Vehicles)
- **Arex388.Carmine.Benchmarks/**: BenchmarkDotNet performance tests

## Key Technical Details

- Targets .NET 10.0 (via Directory.Build.props) but library itself is .NET Standard 2.0
- Uses FluentValidation for request validation
- Implements caching via IMemoryCache
- Custom JSON converters for domain model deserialization
- Uses PolySharp package for .NET Standard 2.0 compatibility polyfills
#### 3.0.2 (2023-07-20)

- **Revised:** `Event.EndAtUtc`, `Event.StartAtUtc`, `Location.LastActivityAtUtc`, `Trip.EndAtUtc`, `Trip.StartAtUtc`, `User.LastActivityAtUtc`, and`Vehicle.LastActivityAtUtc` to be non-UTC. Turns out they're not in UTC. Created timestamps still are.
- **Removed:** `Event.TripId` and `Waypoint.TripId` because the only way to get them is through the trip anyway so they're redundant properties.
- **Removed:** `WaypointId` because it turns out the `Waypoint` doesn't have an id.
- **Removed:** `EventId` and `LocationId` to match `Event` and `Location` with `Waypoint`.

#### 3.0.1 (2023-07-20)

- **Revised:** `EventId`, `LocationId`, `TripId`, `UserId`, `VehicleId`, `WaypointId` by copying the generated code from StronglyTypedId directly.
- **Removed:** StronglyTypedId because it was causing NuGet installation issues when attempting to consume the library.

#### 3.0.0 (2023-07-19)

- **Added:** `ICarmineClient` interface for dependency injection.
- **Added:** `ICarmineClient` extensions for common operations.
- **Added:** `ICarmineClientFactory` interface for dependency injection.
- **Added:** `CarmineClientFactory` implementation.
- **Added:** `GetTripRequestValidator`, `GetUserRequestValidator`, and `GetVehicleRequestValidator` validators using FluentValidation.
- **Added:** `EventId`, `LocationId`, `TripId`, `UserId`, `VehicleId`, `WaypointId` value objects using StronglyTypedId.
- **Added:** Basic unit tests.
- **Added:** Basic benchmarks.
- **Added:** Built-in dependency injection extensions for Microsoft.Extensions.DependencyInjection.
- **Revised:** `CarmineClient` implementation.
  - Simplified `await/async` implementations.

- **Revised:** `EventType`, `Language`, `LocationCategory`, `LocationType`, `ResponseStatus`, `UserRole`, `UserStatus`, and `VehicleStatus` enums.
- **Revised:** `Event`, `Location`, `Trip`, `User`, `Vehicle`, and `Waypoint` objects.
- **Merged:** `GetTripRequest` and `GetTripResponse` into a single `GetTrip` slice.
- **Merged:** `GetUserRequest` and `GetUserResponse` into a single `GetUser` slice.
- **Merged:** `GetVehicleRequest` and `GetVehicleResponse` into a single `GetVehicle` slice.
- **Merged:** `ListTripsRequest` and `ListTripsResponse` into a single `ListTrips` slice.
- **Merged:** `ListUsersRequest` and `ListUsersResponse` into a single `ListUsers` slice.
- **Merged:** `ListVehicleRequest` and `ListVehiclesResponse` into a single `ListVehicles` slice.
- **Removed:** `ListWaypointsRequest` and `ListWaypointsResponse`.
  - When getting a `Trip` with `GetTrip`, it already contains the waypoints in it, so listing waypoints separately is redundant.

- **Removed:** Redundant or non-sensical object properties.
- **Replaced:** Json.NET with System.Text.Json.
- Other internal implementation changes.
- "Upgraded" to C# 11 as much as possible using PolySharp.
- Revised documentation.

#### 2.0.1 (2022-06-23)

- Minor code cleanup.

#### 2.0.0 (2021-04-09)

- Lots of breaking changes.
- Lots of improvements.
- Lots of normalizations.
- Has complete XML documentation.
- Properly asynchronous.

#### 1.0.8 (2021-04-02)

- Changed `VehicleResponse.FuelRemaining` to a `byte` data type since its value range is only 0-100.

#### 1.0.7 (2020-05-27)

- Internal code clean up.
- Improved `debug` flag handling.

#### 1.0.6 (2020-05-07)

- Targeting .NET Standard 2.0 now.
- Internal code clean up and rearrangement. Hopefully some performance optimizations by adding `ConfigureAwait(false)` to all `await` calls.
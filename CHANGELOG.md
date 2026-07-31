# CHANGELOG

#### 4.2.0 (2026-07-30)

Correctness and robustness release driven by a full code audit. Returned values change where the previous behavior was wrong.

- **Fixed:** `Trip.MaxSpeedInKilometersPerHour` / `MaxSpeedInMilesPerHour` never applied their conversion factors due to an operator-precedence bug and returned the raw m/s value; a `null` max speed now returns `null` instead of `0.00`.
- **Fixed:** Mile conversions used a truncated `1609` divisor (now `1609.344`); `Vehicle` odometer conversions now round instead of truncating.
- **Fixed:** `ListTrips` sent `start_time` / `end_time` with a 12-hour clock (afternoon filters silently queried the wrong window) and culture-sensitive separators; now `HH` with the invariant culture.
- **Fixed:** `ListRecentlyActiveVehiclesAsync` compared the API's UTC timestamps against local time; now uses `DateTime.UtcNow`.
- **Fixed:** All JSON converters parse number-as-string fallbacks with the invariant culture (comma-decimal hosts previously corrupted values), and unexpected enum-value tokens fall back to `None` instead of failing the whole response.
- **Fixed:** `Cancelled` / `Failed` responses are fresh instances per call instead of shared mutable singletons, and cancellation observed mid-request is reported as `Cancelled` instead of `Failed`.
- **Added:** `harsh_braking` / `extreme_braking` event spellings are matched alongside the existing `_breaking` spellings.
- **Fixed:** Single-account `AddCarmine(options)` registered an unresolvable typed-client `ICarmineClient`; now uses the named registration, and the client factory cache is atomic.
- **Optimized:** Converters skip unknown JSON properties with `Utf8JsonReader.Skip()` instead of allocating a throwaway `JsonElement`, and base-`Trip` parsing is shared between the trip converters.
- **Updated:** FluentValidation pinned to `[11.12.0,12.0.0)` (v12 dropped .NET Standard 2.0); NetEscapades.EnumGenerators to 1.0.0-beta21 (build is now warning-free); PolySharp to 1.16.0.
- **Refactored:** Tests are offline-first — unit tests and benchmarks share JSON fixtures with mocked HTTP; live integration tests run only with an explicit `CARMINE_LIVE_TESTS=1` opt-in.



#### 4.1.1 (2026-01-09)

- **Updated:** NuGet packages.
- **Updated:** Target framework to .NET 10.0.
- **Replaced:** Custom polyfill files with PolySharp package.
- **Refactored:** JSON converters from enum-level to model-level converters for improved maintainability.
- **Optimized:** JSON deserialization using `ValueTextEquals` with UTF-8 spans for better performance.
- **Refactored:** Dependency injection and removed `HttpClientHelper` class.
- **Refactored:** Benchmarks to use mock HTTP responses instead of real API calls.



#### 4.1.0 (2025-08-18)

- **Updated:** NuGet packages.
- Internal cleanup and refactoring.



#### 4.0.1 (2024-10-13)

- **Updated:** NuGet packages.



#### 4.0.0 (2024-09-25)

- Internal cleanup and refactoring.
- Implemented an options object for client instancing.
- Presumably enabled Source Link.



#### 3.0.6 (2024-03-27)

- Internal cleanup.



#### 3.0.5 (2023-08-21)

- Internal cleanup.



#### 3.0.4 (2023-07-26)

- **Revised:** Dependency Injection extensions to not register an `HttpClient` prior to registering the interfaces and concrete classes.
- **Revised:** Exception handling for failed response.



#### 3.0.3 (2023-07-25)

- **Revised:** Invalid responses to return the validation errors.
- Implemented global usings.
- Implemented global nullable reference types.



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
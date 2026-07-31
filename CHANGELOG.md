# CHANGELOG

#### 4.2.0 (2026-07-30)

Correctness and robustness release driven by a full code audit. Returned values change where the previous behavior was wrong.

- **Breaking:** The response surface is read-only. `ResponseBase<TResponse>.Errors`, `ListTrips.Response.Trips`, `ListUsers.Response.Users`, `ListVehicles.Response.Vehicles`, `TripExpanded.Events`, and `TripExpanded.Waypoints` are now `IReadOnlyList<T>` (previously `IList<T>`); `Vehicle.Faults` is now `IReadOnlyDictionary<string, string>?` (previously `IDictionary<string, string>?`); and the `ListActiveVehiclesAsync` / `ListRecentlyActiveVehiclesAsync` extensions return `IReadOnlyList<Vehicle>`. Consumers declaring variables as `IList<T>` / `IDictionary<K,V>` against these members, or mutating response collections, must retype to the read-only interfaces. Empty payloads no longer allocate fresh lists.
- **Fixed:** `Trip.MaxSpeedInKilometersPerHour` / `MaxSpeedInMilesPerHour` never applied their conversion factors due to an operator-precedence bug and returned the raw m/s value; a `null` max speed now returns `null` instead of `0.00`.
- **Fixed:** Mile conversions used a truncated `1609` divisor (now `1609.344`); `Vehicle` odometer conversions now round instead of truncating; the m/s→mph conversions (`Trip.MaxSpeedInMilesPerHour`, `Waypoint.SpeedInMilesPerHour`) use the full-precision `2.2369362920544` factor instead of a truncated `2.237` — returned values can differ by 0.01.
- **Fixed:** `ListTrips` sent `start_time` / `end_time` with a 12-hour clock (afternoon filters silently queried the wrong window) and culture-sensitive separators; now `HH` with the invariant culture.
- **Fixed:** `ListRecentlyActiveVehiclesAsync` compared the API's UTC timestamps against local time; now uses `DateTime.UtcNow`.
- **Fixed:** All JSON converters parse number-as-string fallbacks with the invariant culture (comma-decimal hosts previously corrupted values), and unexpected enum-value tokens — including object or array values — fall back to `None` instead of failing the whole response.
- **Fixed:** `User.Phone` number-as-string parsing accepts formatted values (`"+1 (214) 555-0123"`) and degrades to `null` on unparseable input instead of failing the whole response.
- **Fixed:** `StronglyTypedId` no longer flows to consumers as a package dependency (it is a compile-time source generator, and its prerelease pin broke feeds that reject prerelease packages).
- **Added:** NuGet package metadata — MIT license expression, embedded README, repository type, and a SourceLink `.snupkg` symbol package.
- **Added:** .NET SDK analyzers (`latest-recommended`) with warnings-as-errors across the solution.
- **Fixed:** `Cancelled` / `Failed` responses are fresh instances per call instead of shared mutable singletons, and cancellation observed mid-request is reported as `Cancelled` instead of `Failed`.
- **Breaking:** `EventType.ExtremeBraking` replaces the misspelled `ExtremeBreaking`, which is removed outright — rename references to `ExtremeBraking` (same value; the remaining `EventType` values are numerically unchanged). The `harsh_braking` / `extreme_braking` event spellings are matched alongside the existing `_breaking` spellings.
- **Breaking:** `UserStatus` gains `None = 0` as the unparsed fallback, renumbering `Active` to 1 and `Inactive` to 2 — consumers persisting the raw values or relying on `default(UserStatus) == Active` must migrate. A user payload missing the `active` boolean now reports `None` instead of `Inactive`, and a `None` status filter on `ListUsers` / `ListVehicles` is treated as no filter (`ListVehicles` previously sent a literal `&status=None`).
- **Added:** Failed responses append a human-readable detail entry to `Errors` — the HTTP status (`"HTTP 401 Unauthorized"`) or the exception type and message. The first entry remains exactly `"The request has failed."`, but consumers asserting on a single-element `Errors` list will see a second entry.
- **Fixed:** Single-account `AddCarmine(options)` registered an unresolvable typed-client `ICarmineClient`; now uses the named registration, and the client factory cache is atomic.
- **Added:** `ListTrips.Request.Take` is validated (`> 0`) and returns `Invalid` locally instead of sending a bad `per_page` to the API, and `ListRecentlyActiveVehiclesAsync` accepts either sign for `minutes` (a positive value previously returned an empty list).
- **Optimized:** Converters skip unknown JSON properties with `Utf8JsonReader.Skip()` instead of allocating a throwaway `JsonElement`, and base-`Trip` parsing is shared between the trip converters.
- **Updated:** FluentValidation pinned to `[11.12.0,12.0.0)` (v12 dropped .NET Standard 2.0); NetEscapades.EnumGenerators to 1.0.0-beta21 (build is now warning-free); PolySharp to 1.16.0; Microsoft.Extensions.* / System.Net.Http.Json / System.Text.Json to 10.0.10 — consumers on the 9.x Microsoft.Extensions stack should stay on 4.1.2.
- **Refactored:** Tests are offline-first — unit tests and benchmarks share JSON fixtures with mocked HTTP; live integration tests run only with an explicit `CARMINE_LIVE_TESTS=1` opt-in.



#### 4.1.2 (2026-01-12)

- **Updated:** Enums to use `MetadataSource.DisplayAttribute` with the matching `ToStringFast(true)` overload for endpoint values.
- **Refactored:** `List*` request static instances renamed to `Default`.
- **Added:** Integration tests for the `ICarmineClient` extension methods.



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
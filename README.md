# Arex388.Carmine
Arex388.Carmine is a highly opinionated .NET Standard 2.0 library for the [Carmine.io](https://carmine.io) API. It's intended to be an easy, well structured, and highly performant client for interacting with Carmine.io's API for retrieving GPS fleet tracking information. It can be used in applications interacting with a single account using `ICarmineClient`, or with applications interacting with multiple accounts using `ICarmineClientFactory`. The library has gone through three major, and breaking, revisions because I never quite liked the first two implementations.

As noted above, it is highly opinionated. The [API documentation](https://api.carmine.io/v2/docs) is not very clear, and there's redundancies, ambiguities, and object properties that I have no idea why they're there or what they stand for. I've attempted to normalize the ambiguities, and to ignore the redundancies.

> **NOTE**
>
> The Carmine.io API is a read-only API, unfortunately.

The library has built-in dependency injections to simplify usage. By default a singleton `ICarmineClient` or `ICarmineClientFactory` instance will be created. If using `ICarmineClientFactory`, it will cache `ICarmineClient` instances by their API key.

> **V3.0.0**
>
> The latest version, 3.0.0, is a complete overhaul with many breaking changes. Mostly has to do with request/response object structures. Fingers crossed, this will be my last revision as I quite like how it ended up this time.

- [Changelog](CHANGELOG.md)
- [Benchmarks](BENCHMARKS.md)



#### Dependency Injection

To configure dependency injection with ASP.NET and ASP.NET Core, use `AddCarmine()` extensions on `IServiceCollection`. There are two signatures, with and without `apiKey` parameter. If `apiKey` is passed to the extension, it will register `ICarmineClient` for use with a single account, otherwise it will register `ICarmineClientFactory` for use with multiple accounts.



#### How to Use

You can get a single `Trip`, `User`, or `Vehicle` using:

```c#
//	Trip
await carmine.GetTripAsync("id");
await carmine.GetTripAsync(new GetTrip.Request {
    ...
});

//	User
await carmine.GetUserAsync("id");
await carmine.GetUserAsync(new GetUser.Request {
    ...
});

//	Vehicle
await carmine.GetVehicleAsync("id");
await carmine.GetVehicleAsync(new GetVehicle.Request {
    ...
});
```

Or you can list multiple `Trip`s, `User`s, `Vehicle`s using:

```c#
//	Trips
await carmine.ListTripsAsync();
await carmine.ListTripsAsync(new ListTrips.Request {
    ...
});

//	Users
await carmine.ListUsersAsync();
await carmine.ListUsersAsync(new ListUsers.Request {
    ...
});

//	Vehicles
await carmine.ListVehiclesAsync();
await carmine.ListVehiclesAsync(new ListVehicles.Request {
    ...
});
```



#### Extensions

There are some extension methods on `ICarmineClient` to help with what I consider to be common tasks:

- `GetActiveVehicleAsync("vin")` - Returns an active vehicle with a specified VIN.
- `ListActiveVehiclesAsync()` - Returns a list of all active vehicles.
- `ListRecentlyActiveVehiclesAsync()` - Returns a list of recently active vehicles. Unfortunately, this pulls all vehicles from the server and performs that activity filter client-side.
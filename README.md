# Arex388.Carmine


Arex388.Carmine is a highly opinionated .NET Standard 2.0 library for the [Carmine.io](https://api.carmine.io/v2/docs) API. It's intended to be an easy, well structured, and highly performant client for interacting with the Carmine.io API for retrieving GPS fleet tracking information. It can be used in applications interacting with a single account using `ICarmineClient`, or with applications interacting with multiple accounts using `ICarmineClientFactory`.

- [Changelog](CHANGELOG.md)
- [Benchmarks](BENCHMARKS.md)



#### Dependency Injection

To configure dependency injection use `AddCarmine()` extensions on `IServiceCollection`. There are two signatures, with and without passing in a `CarmineClientOptions` object. If the options object is passed to the extension, it will register `ICarmineClient` for use with a single account, otherwise it will register `ICarmineClientFactory` for use with multiple accounts.



#### How to Use

For a single account, inject the `ICarmineClient`.

```c#
private readonly ICarmineClient _carmine;

_ = await _carmine.GetTripAsync(new TripId(Guid.Parse("...")));
_ = await _carmine.GetUserAsync(new UserId(Guid.Parse("...")));
_ = await _carmine.GetVehicleAsync(new VehicleId(Guid.Parse("...")));
```



For multiple accounts, inject the `ICarmineClientFactory` to create an instance per account.

```c#
private readonly ICarmineClientFactory _carmineFactory;

var carmine = _carmineFactory.CreateClient(new CarmineClientOptions {
    Key = "Your key from Carmine.io"
});

_ = await carmine.GetTripAsync(new TripId(Guid.Parse("...")));
_ = await carmine.GetUserAsync(new UserId(Guid.Parse("...")));
_ = await carmine.GetVehicleAsync(new VehicleId(Guid.Parse("...")));
```



#### Extensions

There are some extension methods on `ICarmineClient` to help with what I consider to be common tasks:

- `GetActiveVehicleAsync("vin")` - Returns an active vehicle with a specified VIN.
- `ListActiveVehiclesAsync()` - Returns a list of all active vehicles.
- `ListRecentlyActiveVehiclesAsync()` - Returns a list of recently active vehicles. Unfortunately, this pulls all vehicles from the server and performs that activity filter client-side.
# Arex388.Carmine
A C# client for the Carmine.io API.

To use, create a new instance of `CarmineClient` and pass in an instance of `HttpClient` and your API key. The original API documentation can be found [here](https://api.carmine.io/v2/docs). Please note that this is a *read-only* API. For more information, please visit [arex388.com](https://arex388.com).

Available as a NuGet package [here](https://www.nuget.org/packages/Arex388.Carmine).



## v2.0.0

Version 2.0.0 is a complete rewrite and overhaul of the client. There's many breaking changes, many improvements and internal simplifications, and lots of normalizations to make interacting with the API more consistent. Overall, it should be faster and easier to interact with Carmine's API now with the re-written client.



## v1.0.6

Version 1.0.6 targets .NET Standard 2.0 now. There's really no specific reason not to. To quote Immo Landwerth from his [.NET Standard 2.1 announcement post](https://devblogs.microsoft.com/dotnet/announcing-net-standard-2-1/):

> *Library authors who need to support .NET Framework customers should stay on .NET Standard 2.0.*



## How to Use

First start by creating an instance of the client by passing in an instance of `HttpClient` and your API key. Optionally, you can enable debugging which will include the raw JSON response as well.

```C#
var carmine = new CarmineClient(httpClient, apiKey, debug: true || false);
```

Then call the methods you need to list trips, users, vehicles or waypoints, or to get a trip, user or vehicle.



### Trips

To list trips, call the `ListTripsAsync()` method.

```c#
var trips = await carmine.ListTripsAsync().ConfigureAwait(false);
```



To get a specific trip, call the `GetTripAsync()` method.

```c#
var trip = await carming.GetTripAsync(tripId).ConfigureAwait(false);
```



### Users

To list users, call the `ListUsersAsync()` method.

```c#
var users = await carmine.ListUsersAsync().ConfigureAwait(false);
```



To get a specific user, call the `GetUserAsync()` method.

```c#
var user = await carmine.GetUserAsync(userId).ConfigureAwait(false);
```



### Vehicles

To list vehicles, call the `ListVehiclesAsync()` method.

```c#
var vehicles = await carmine.ListVehiclesAsync().ConfigureAwait(false);
```



To get a specific vehicle, call the `GetVehicleAsync()` method.

```c#
var vehicle = await carmine.GetVehicleAsync(vehicleId).ConfigureAwait(false);
```



### Waypoints

To list waypoints for a trip, call the `ListWaypointsAsync()` method.

```C#
var waypoints = await carmine.GetWaypointsAsync(tripId).ConfigureAwait(false);
```

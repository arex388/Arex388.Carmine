# Arex388.Carmine
Carmine.io API Implementation in C#

To use, create a new instance of `CarmineClient` and pass in an instance of `HttpClient` and your API key. The original API documentation can be found [here][0]. For more information, please visit [arex388.com][1].

Available as a NuGet package [here][2].

    var carmine = new CarmineClient(httpClient, "{key}");

**Get a List of Vehicles**

    var vehicles = await carmine.GetVehiclesAsync();

**Get a Vehicle**

    var vehicle = await carmine.GetVehicleAsync("{vehicleId}");

**Get a List of Users**

    var users = await carmine.GetUsersAsync();

**Get a User**

    var user = await carmine.GetUserAsync("{userId}");

**Get a List of Trips**

    var trips = await carmine.GetTripsAsync();

**Get a Trip**

    var trip = await carmine.GetTripAsync("{tripId}");

**Get Waypoints for a Trip**

    var waypoints = await carmine.GetWaypointsAsync("{tripId}");

[0]:https://api.carmine.io/v2/docs
[1]:https://arex388.com
[2]:https://www.nuget.org/packages/Arex388.Carmine
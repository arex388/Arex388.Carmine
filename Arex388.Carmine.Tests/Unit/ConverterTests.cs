using FluentAssertions;
using System.Globalization;

namespace Arex388.Carmine.Tests.Unit;

public sealed class ConverterTests {
	[Fact]
	public async Task GetTrip_ParsesExpandedTripFixture() {
		var carmine = TestClients.CreateWithFixtures();

		var response = await carmine.GetTripAsync(new TripId(Guid.Parse("a1a1dee1-fdb6-4a55-9e48-1fd9882bf4f7")));

		response.Success.Should().BeTrue();

		var trip = response.Trip!;

		trip.Id.Value.Should().Be(Guid.Parse("a1a1dee1-fdb6-4a55-9e48-1fd9882bf4f7"));
		trip.DistanceTraveledInMeters.Should().Be(12);
		trip.MaxSpeedInMetersPerSecond.Should().Be(2.5M);
		trip.ParkedSeconds.Should().Be(8861);
		trip.Driver!.Name.Should().Be("Test Driver");
		trip.Driver.Role.Should().Be(UserRole.Driver);
		trip.Vehicle.Status.Should().Be(VehicleStatus.Active);
		trip.Vehicle.OdometerInMeters.Should().Be(41516888);
		trip.StartLocation.Category.Should().Be(LocationCategory.CompanyOffice);
		trip.StartLocation.Type.Should().Be(LocationType.PointOfInterest);
		trip.StartLocation.Latitude.Should().Be(32.780000M);
		trip.StartLocation.VisitedCount.Should().Be(12);
		trip.EndLocation!.Category.Should().Be(LocationCategory.Customer);
		trip.EndLocation.Type.Should().Be(LocationType.Geofence);
		trip.Events.Should().HaveCount(2);
		trip.Events[0].Type.Should().Be(EventType.HarshAcceleration);
		trip.Events[1].Type.Should().Be(EventType.HarshBraking);
		trip.Waypoints.Should().HaveCount(3);
		trip.Waypoints[1].DistanceTraveledInMeters.Should().Be(9);
		trip.Waypoints[1].SpeedInMetersPerSecond.Should().Be(2.5M);
		trip.Waypoints[1].Latitude.Should().Be(32.780043M);
		trip.Waypoints[1].EngineRpm.Should().Be(1800);
	}

	[Fact]
	public async Task ListTrips_ParsesTripsFixture() {
		var carmine = TestClients.CreateWithFixtures();

		var response = await carmine.ListTripsAsync();

		response.Success.Should().BeTrue();
		response.Trips.Should().HaveCount(3);

		//	String-encoded numbers parse via the invariant-culture fallback.
		response.Trips[1].DistanceTraveledInMeters.Should().Be(108);
		response.Trips[1].MaxSpeedInMetersPerSecond.Should().Be(20.277778M);

		//	Null end_time and max_speed propagate as null.
		response.Trips[2].EndAt.Should().BeNull();
		response.Trips[2].MaxSpeedInMetersPerSecond.Should().BeNull();
	}

	[Fact]
	public async Task GetUser_ParsesUserFixture() {
		var carmine = TestClients.CreateWithFixtures();

		var response = await carmine.GetUserAsync(new UserId(Guid.Parse("f42c7580-e66f-4daa-869e-44a2cd9ce6a7")));

		response.Success.Should().BeTrue();

		var user = response.User!;

		user.Name.Should().Be("Test Administrator");
		user.Role.Should().Be(UserRole.Administrator);
		user.Status.Should().Be(UserStatus.Active);
		user.Phone.Should().Be(2145550123);
		user.IsEmailValidated.Should().BeTrue();
	}

	[Fact]
	public async Task ListUsers_ParsesUsersFixture() {
		var carmine = TestClients.CreateWithFixtures();

		var response = await carmine.ListUsersAsync();

		response.Success.Should().BeTrue();
		response.Users.Should().HaveCount(2);
		response.Users[1].Role.Should().Be(UserRole.Driver);
		response.Users[1].Status.Should().Be(UserStatus.Inactive);
		response.Users[1].Phone.Should().BeNull();
	}

	[Fact]
	public async Task GetVehicle_ParsesVehicleFixture() {
		var carmine = TestClients.CreateWithFixtures();

		var response = await carmine.GetVehicleAsync(new VehicleId(Guid.Parse("8b320390-0f25-466a-81f6-f757a5891f36")));

		response.Success.Should().BeTrue();

		var vehicle = response.Vehicle!;

		vehicle.Status.Should().Be(VehicleStatus.Active);
		vehicle.OdometerInMeters.Should().Be(10752466);
		vehicle.FuelConsumptionInMetersPerLiter.Should().Be(1500);
		vehicle.FuelRemaining.Should().Be(58);
		vehicle.Year.Should().Be(2023);
		vehicle.Faults.Should().ContainKey("U0402").WhoseValue.Should().Be("Unknown");
		vehicle.Latitude.Should().Be(32.780952M);
	}

	[Fact]
	public async Task ListVehicles_ParsesVehiclesFixture() {
		var carmine = TestClients.CreateWithFixtures();

		var response = await carmine.ListVehiclesAsync();

		response.Success.Should().BeTrue();
		response.Vehicles.Should().HaveCount(2);
		response.Vehicles[1].Status.Should().Be(VehicleStatus.Inactive);
		response.Vehicles[1].OdometerInMeters.Should().BeNull();
		response.Vehicles[1].Faults.Should().BeNull();
	}

	[Fact]
	public async Task StringEncodedDecimals_ParseInvariantly_UnderCommaDecimalCulture() {
		var previous = CultureInfo.CurrentCulture;

		CultureInfo.CurrentCulture = new CultureInfo("de-DE");

		try {
			const string json = """
				[{
					"id": "a1a1dee1-fdb6-4a55-9e48-1fd9882bf4f7",
					"distance": "108",
					"max_speed": "12.5",
					"start_time": "2026-01-07T08:10:58-07:00",
					"time_parked": "60"
				}]
				""";

			var carmine = TestClients.CreateWithJson(json, out _);

			var response = await carmine.ListTripsAsync();

			response.Success.Should().BeTrue();
			response.Trips[0].MaxSpeedInMetersPerSecond.Should().Be(12.5M, "decimal.Parse must not honor the comma-decimal current culture");
			response.Trips[0].DistanceTraveledInMeters.Should().Be(108);
			response.Trips[0].ParkedSeconds.Should().Be(60);
		} finally {
			CultureInfo.CurrentCulture = previous;
		}
	}

	[Fact]
	public async Task NullEnumTokens_FallBackToNone() {
		const string json = """
			[{
				"id": "8b320390-0f25-466a-81f6-f757a5891f36",
				"created": "2023-09-19T13:19:08-07:00",
				"status": null,
				"fuel_level": 0,
				"fuel_economy": 0
			}]
			""";

		var carmine = TestClients.CreateWithJson(json, out _);

		var response = await carmine.ListVehiclesAsync();

		response.Success.Should().BeTrue("an unexpected token must degrade to the enum fallback, not a Failed response");
		response.Vehicles[0].Status.Should().Be(VehicleStatus.None);
	}

	[Fact]
	public async Task ContainerEnumTokens_DegradeToNone_InExpandedTrip() {
		const string json = """
			{
				"id": "a1a1dee1-fdb6-4a55-9e48-1fd9882bf4f7",
				"start_time": "2026-01-07T08:10:58-07:00",
				"events": [{
					"event_type": { "code": 3 },
					"start_time": "2026-01-07T08:15:00-07:00",
					"end_time": "2026-01-07T08:16:00-07:00"
				}],
				"start_location": {
					"category": { "code": 1 },
					"type": ["poi"],
					"address": "123 Test St",
					"popularity": 4
				},
				"distance": 12
			}
			""";

		var carmine = TestClients.CreateWithJson(json, out _);

		var response = await carmine.GetTripAsync(new TripId(Guid.Parse("a1a1dee1-fdb6-4a55-9e48-1fd9882bf4f7")));

		response.Success.Should().BeTrue("a container token on an enum property must degrade, not fail the response");

		var trip = response.Trip!;

		trip.Events[0].Type.Should().Be(EventType.None);
		trip.StartLocation.Category.Should().Be(LocationCategory.None);
		trip.StartLocation.Type.Should().Be(LocationType.None);
		trip.StartLocation.VisitedCount.Should().Be(4, "properties after the degraded containers must still parse");
		trip.DistanceTraveledInMeters.Should().Be(12, "properties after the degraded containers must still parse");
	}

	[Fact]
	public async Task ContainerEnumTokens_DegradeToDefault_ForUserRoleAndVehicleStatus() {
		const string usersJson = """
			[{
				"id": "f42c7580-e66f-4daa-869e-44a2cd9ce6a7",
				"role": { "code": 1 },
				"name": "Test User"
			}]
			""";

		var users = await TestClients.CreateWithJson(usersJson, out _).ListUsersAsync();

		users.Success.Should().BeTrue();
		users.Users[0].Role.Should().Be(UserRole.None);
		users.Users[0].Name.Should().Be("Test User");

		const string vehiclesJson = """
			[{
				"id": "8b320390-0f25-466a-81f6-f757a5891f36",
				"created": "2023-09-19T13:19:08-07:00",
				"status": ["active"],
				"vin": "TESTVIN0123456789"
			}]
			""";

		var vehicles = await TestClients.CreateWithJson(vehiclesJson, out _).ListVehiclesAsync();

		vehicles.Success.Should().BeTrue();
		vehicles.Vehicles[0].Status.Should().Be(VehicleStatus.None);
		vehicles.Vehicles[0].Vin.Should().Be("TESTVIN0123456789");
	}

	[Theory]
	[InlineData("\"2145550123\"", 2145550123L)]
	[InlineData("\"+1 (214) 555-0123\"", 12145550123L)]
	[InlineData("\"n/a\"", null)]
	[InlineData("\"1234567890123456789\"", null)]
	[InlineData("2145550123", 2145550123L)]
	[InlineData("null", null)]
	public async Task UserPhone_DegradesInsteadOfFailing(
		string smsJson,
		long? expected) {
		var json = $$"""
			[{
				"id": "f42c7580-e66f-4daa-869e-44a2cd9ce6a7",
				"sms": {{smsJson}},
				"name": "Test User"
			}]
			""";

		var response = await TestClients.CreateWithJson(json, out _).ListUsersAsync();

		response.Success.Should().BeTrue("an unparseable phone must degrade to null, never fail the response");
		response.Users[0].Phone.Should().Be(expected);
		response.Users[0].Name.Should().Be("Test User");
	}

	[Fact]
	public void EventType_ByteValues_AreStable() {
		((byte)EventType.None).Should().Be(0);
		((byte)EventType.ExtremeAcceleration).Should().Be(3);
		((byte)EventType.ExtremeBraking).Should().Be(4);
#pragma warning disable CS0618 // the obsolete alias must share the value it replaced
		((byte)EventType.ExtremeBreaking).Should().Be(4);
#pragma warning restore CS0618
		((byte)EventType.HarshAcceleration).Should().Be(5);
		((byte)EventType.Speeding).Should().Be(10);
	}

	[Theory]
	[InlineData("extreme_braking")]
	[InlineData("extreme_breaking")]
	public async Task EventType_BothBrakingSpellings_Parse(
		string spelling) {
		var json = $$"""
			{
				"id": "a1a1dee1-fdb6-4a55-9e48-1fd9882bf4f7",
				"start_time": "2026-01-07T08:10:58-07:00",
				"events": [{
					"event_type": "{{spelling}}",
					"start_time": "2026-01-07T08:15:00-07:00",
					"end_time": "2026-01-07T08:16:00-07:00"
				}]
			}
			""";

		var response = await TestClients.CreateWithJson(json, out _).GetTripAsync(new TripId(Guid.Parse("a1a1dee1-fdb6-4a55-9e48-1fd9882bf4f7")));

		response.Success.Should().BeTrue();
		response.Trip!.Events[0].Type.Should().Be(EventType.ExtremeBraking);
	}

	[Fact]
	public async Task UnknownProperties_AreSkipped() {
		const string json = """
			[{
				"id": "a1a1dee1-fdb6-4a55-9e48-1fd9882bf4f7",
				"unknown_object": { "nested": { "deep": [1, 2, 3] } },
				"unknown_array": [{ "x": 1 }],
				"distance": 12,
				"start_time": "2026-01-07T08:10:58-07:00",
				"unknown_scalar": 42.5
			}]
			""";

		var carmine = TestClients.CreateWithJson(json, out _);

		var response = await carmine.ListTripsAsync();

		response.Success.Should().BeTrue();
		response.Trips[0].DistanceTraveledInMeters.Should().Be(12);
	}
}

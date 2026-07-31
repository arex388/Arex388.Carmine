using FluentAssertions;

namespace Arex388.Carmine.Tests.Unit;

public sealed class EndpointTests {
	private static readonly Guid _id = Guid.Parse("a1a1dee1-fdb6-4a55-9e48-1fd9882bf4f7");

	private static async Task<string> CaptureAsync(
		Func<ICarmineClient, Task> act,
		string json = "[]") {
		var carmine = TestClients.CreateWithJson(json, out var handler);

		await act(carmine);

		return Uri.UnescapeDataString(handler.Requests.Single().ToString());
	}

	[Fact]
	public async Task GetTrip_BuildsEndpoint() {
		var uri = await CaptureAsync(c => c.GetTripAsync(new TripId(_id)), "null");

		uri.Should().Be($"https://localhost:9/v2/trips/{_id}?lang=en&api_key={TestClients.ApiKey}");
	}

	[Fact]
	public async Task GetUser_BuildsEndpoint() {
		var uri = await CaptureAsync(c => c.GetUserAsync(new UserId(_id)), "null");

		uri.Should().Be($"https://localhost:9/v2/users/{_id}?lang=en&api_key={TestClients.ApiKey}");
	}

	[Fact]
	public async Task GetVehicle_BuildsEndpoint() {
		var uri = await CaptureAsync(c => c.GetVehicleAsync(new VehicleId(_id)), "null");

		uri.Should().Be($"https://localhost:9/v2/vehicles/{_id}?lang=en&api_key={TestClients.ApiKey}");
	}

	[Fact]
	public async Task ListTrips_BuildsEndpoint_WithAfternoonTimestamps() {
		var uri = await CaptureAsync(c => c.ListTripsAsync(new ListTrips.Request {
			DriverId = new UserId(_id),
			EndAtUtc = new DateTime(2026, 1, 16, 17, 45, 30),
			StartAtUtc = new DateTime(2026, 1, 15, 14, 30, 0),
			Take = 25,
			VehicleId = new VehicleId(_id)
		}));

		uri.Should().Be($"https://localhost:9/v2/trips?lang=en&per_page=25&driver[]={_id}&end_time=2026-01-16T17:45:30&start_time=2026-01-15T14:30:00&vehicle[]={_id}&api_key={TestClients.ApiKey}");
	}

	[Fact]
	public async Task ListTrips_BuildsDefaultEndpoint() {
		var uri = await CaptureAsync(c => c.ListTripsAsync());

		uri.Should().Be($"https://localhost:9/v2/trips?lang=en&per_page=100&api_key={TestClients.ApiKey}");
	}

	[Fact]
	public async Task ListUsers_BuildsEndpoint() {
		var uri = await CaptureAsync(c => c.ListUsersAsync());

		uri.Should().Be($"https://localhost:9/v2/users?lang=en&api_key={TestClients.ApiKey}");
	}

	[Theory]
	[InlineData(UserStatus.Active, "true")]
	[InlineData(UserStatus.Inactive, "false")]
	public async Task ListUsers_BuildsEndpoint_WithEncodedSearchAndStatus(
		UserStatus status,
		string active) {
		var uri = await CaptureAsync(c => c.ListUsersAsync(new ListUsers.Request {
			Search = "John Smith",
			Status = status
		}));

		uri.Should().Be($"https://localhost:9/v2/users?lang=en&search=John+Smith&active={active}&api_key={TestClients.ApiKey}");
	}

	[Fact]
	public async Task GetTrip_BuildsEndpoint_WithNonDefaultLanguage() {
		var uri = await CaptureAsync(c => c.GetTripAsync(new GetTrip.Request {
			Id = new TripId(_id),
			Language = Language.German
		}), "null");

		uri.Should().Be($"https://localhost:9/v2/trips/{_id}?lang=de&api_key={TestClients.ApiKey}");
	}

	[Fact]
	public async Task ListVehicles_BuildsEndpoint_WithEncodedSearch() {
		var uri = await CaptureAsync(c => c.ListVehiclesAsync(new ListVehicles.Request {
			Search = "ProMaster 3500",
			Status = VehicleStatus.Active
		}));

		uri.Should().Be($"https://localhost:9/v2/vehicles?lang=en&search=ProMaster+3500&status=active&api_key={TestClients.ApiKey}");
	}
}

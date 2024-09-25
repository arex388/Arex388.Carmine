using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace Arex388.Carmine.Tests;

public sealed class TripsTest {
	private readonly ICarmineClient _carmine;
	private readonly ITestOutputHelper _console;

	public TripsTest(
		ITestOutputHelper console) {
		var services = new ServiceCollection().AddCarmine(new CarmineClientOptions {
			Key = Config.Key1
		}).BuildServiceProvider();

		_carmine = services.GetRequiredService<ICarmineClient>();
		_console = console;
	}

	[Fact]
	public async Task Get_Succeeds() {
		//	========================================================================
		//	Arrange
		//	========================================================================

		var list = await _carmine.ListTripsAsync();
		var trip = list.Trips.First();

		//	========================================================================
		//	Act
		//	========================================================================

		var getTrip = await _carmine.GetTripAsync(trip.Id);

		_console.WriteLineWithHeader(nameof(getTrip), getTrip);

		//	========================================================================
		//	Assert
		//	========================================================================

		getTrip.Errors.Should().BeEmpty();
		getTrip.Success.Should().BeTrue();
		getTrip.Trip.Should().NotBeNull();
	}

	[Fact]
	public async Task List_Succeeds() {
		//	========================================================================
		//	Arrange
		//	========================================================================

		//	========================================================================
		//	Act
		//	========================================================================

		var listTrips = await _carmine.ListTripsAsync();

		_console.WriteLineWithHeader(nameof(listTrips), listTrips);

		//	========================================================================
		//	Assert
		//	========================================================================

		listTrips.Errors.Should().BeEmpty();
		listTrips.Success.Should().BeTrue();
		listTrips.Trips.Should().NotBeEmpty();
	}
}
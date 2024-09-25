using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace Arex388.Carmine.Tests;

public sealed class VehiclesTests {
	private readonly ICarmineClient _carmine;
	private readonly ITestOutputHelper _console;

	public VehiclesTests(
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

		var list = await _carmine.ListVehiclesAsync();
		var vehicle = list.Vehicles.First();

		//	========================================================================
		//	Act
		//	========================================================================

		var getVehicle = await _carmine.GetVehicleAsync(vehicle.Id);

		_console.WriteLineWithHeader(nameof(getVehicle), getVehicle);

		//	========================================================================
		//	Assert
		//	========================================================================

		getVehicle.Errors.Should().BeEmpty();
		getVehicle.Success.Should().BeTrue();
		getVehicle.Vehicle.Should().NotBeNull();
	}

	[Fact]
	public async Task List_Succeeds() {
		//	========================================================================
		//	Arrange
		//	========================================================================

		//	========================================================================
		//	Act
		//	========================================================================

		var listVehicles = await _carmine.ListVehiclesAsync();

		//	========================================================================
		//	Assert
		//	========================================================================

		listVehicles.Errors.Should().BeEmpty();
		listVehicles.Success.Should().BeTrue();
		listVehicles.Vehicles.Should().NotBeEmpty();
	}
}
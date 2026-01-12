using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace Arex388.Carmine.Tests;

public sealed class CarmineClientExtensionsTests {
	private readonly ICarmineClient _carmine;
	private readonly ITestOutputHelper _console;

	public CarmineClientExtensionsTests(
		ITestOutputHelper console) {
		var services = new ServiceCollection().AddCarmine(new CarmineClientOptions {
			Key = Config.Key1
		}).BuildServiceProvider();

		_carmine = services.GetRequiredService<ICarmineClient>();
		_console = console;
	}

	[Fact]
	public async Task GetActiveVehicleAsync_ReturnsMatchingVehicle() {
		//	========================================================================
		//	Arrange
		//	========================================================================

		var listResponse = await _carmine.ListVehiclesAsync(new ListVehicles.Request {
			Status = VehicleStatus.Active
		});

		listResponse.Vehicles.Should().NotBeEmpty("need at least one active vehicle for this test");

		var expectedVehicle = listResponse.Vehicles.First();

		//	========================================================================
		//	Act
		//	========================================================================

		var actualVehicle = await _carmine.GetActiveVehicleAsync(expectedVehicle.Vin!, default);

		_console.WriteLineWithHeader(nameof(expectedVehicle), expectedVehicle);
		_console.WriteLineWithHeader(nameof(actualVehicle), actualVehicle);

		//	========================================================================
		//	Assert
		//	========================================================================

		actualVehicle.Should().NotBeNull();
		actualVehicle!.Id.Should().Be(expectedVehicle.Id);
		actualVehicle.Vin.Should().Be(expectedVehicle.Vin);
	}

	[Fact]
	public async Task ListActiveVehiclesAsync_ReturnsOnlyActiveVehicles() {
		//	========================================================================
		//	Arrange
		//	========================================================================

		var listResponse = await _carmine.ListVehiclesAsync(new ListVehicles.Request {
			Status = VehicleStatus.Active
		});

		var expectedVehicles = listResponse.Vehicles;

		//	========================================================================
		//	Act
		//	========================================================================

		var actualVehicles = await _carmine.ListActiveVehiclesAsync(default);

		_console.WriteLineWithHeader("Expected Count", expectedVehicles.Count);
		_console.WriteLineWithHeader("Actual Count", actualVehicles.Count);

		//	========================================================================
		//	Assert
		//	========================================================================

		actualVehicles.Should().HaveCount(expectedVehicles.Count);
		actualVehicles.Select(v => v.Id).Should().BeEquivalentTo(expectedVehicles.Select(v => v.Id));
	}

	[Fact]
	public async Task ListRecentlyActiveVehiclesAsync_ReturnsRecentlyActiveVehicles() {
		//	========================================================================
		//	Arrange
		//	========================================================================

		var minutes = -60;
		var cutoff = DateTime.Now.AddMinutes(minutes);

		var listResponse = await _carmine.ListVehiclesAsync();
		var expectedVehicles = listResponse.Vehicles.Where(
			v => v.LastActivityAt >= cutoff).ToList();

		//	========================================================================
		//	Act
		//	========================================================================

		var actualVehicles = await _carmine.ListRecentlyActiveVehiclesAsync(minutes, default);

		_console.WriteLineWithHeader("Cutoff", cutoff);
		_console.WriteLineWithHeader("Expected Count", expectedVehicles.Count);
		_console.WriteLineWithHeader("Actual Count", actualVehicles.Count);

		//	========================================================================
		//	Assert
		//	========================================================================

		actualVehicles.Should().HaveCount(expectedVehicles.Count);
		actualVehicles.Select(v => v.Id).Should().BeEquivalentTo(expectedVehicles.Select(v => v.Id));
		actualVehicles.Should().AllSatisfy(v => v.LastActivityAt.Should().BeOnOrAfter(cutoff));
	}
}

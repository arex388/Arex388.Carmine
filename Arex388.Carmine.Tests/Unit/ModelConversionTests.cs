using FluentAssertions;

namespace Arex388.Carmine.Tests.Unit;

public sealed class ModelConversionTests {
	[Fact]
	public void Trip_MaxSpeed_ConvertsFromMetersPerSecond() {
		var trip = new Trip {
			MaxSpeedInMetersPerSecond = 10M
		};

		trip.MaxSpeedInKilometersPerHour.Should().Be(36.00M);
		trip.MaxSpeedInMilesPerHour.Should().Be(22.37M);
	}

	[Fact]
	public void Trip_MaxSpeed_UsesFullPrecisionMphFactor() {
		var trip = new Trip {
			MaxSpeedInMetersPerSecond = 15M
		};

		//	15 × 2.2369362920544 = 33.554… → 33.55; the old truncated 2.237
		//	factor gave 33.555 → 33.56.
		trip.MaxSpeedInMilesPerHour.Should().Be(33.55M);
	}

	[Fact]
	public void Trip_MaxSpeed_PropagatesNull() {
		var trip = new Trip();

		trip.MaxSpeedInKilometersPerHour.Should().BeNull();
		trip.MaxSpeedInMilesPerHour.Should().BeNull();
	}

	[Fact]
	public void Trip_Distance_Converts() {
		var trip = new Trip {
			DistanceTraveledInMeters = 1609344
		};

		trip.DistanceTraveledInKilometers.Should().Be(1609.34M);
		trip.DistanceTraveledInMiles.Should().Be(1000.00M);
	}

	[Fact]
	public void Waypoint_Conversions() {
		var waypoint = new Waypoint {
			DistanceTraveledInMeters = 1609344,
			ElevationInMeters = 1609.344M,
			SpeedInMetersPerSecond = 10M
		};

		waypoint.DistanceTraveledInKilometers.Should().Be(1609.34M);
		waypoint.DistanceTraveledInMiles.Should().Be(1000.00M);
		waypoint.ElevationInKilometers.Should().Be(1.61M);
		waypoint.ElevationInMiles.Should().Be(1.00M);
		waypoint.SpeedInKilometersPerHour.Should().Be(36.00M);
		waypoint.SpeedInMilesPerHour.Should().Be(22.37M);
	}

	[Fact]
	public void Vehicle_Odometer_RoundsInsteadOfTruncating() {
		var vehicle = new Vehicle {
			OdometerInMeters = 10752466
		};

		vehicle.OdometerInKilometers.Should().Be(10752);
		vehicle.OdometerInMiles.Should().Be(6681);
	}

	[Fact]
	public void Vehicle_Odometer_PropagatesNull() {
		var vehicle = new Vehicle();

		vehicle.OdometerInKilometers.Should().BeNull();
		vehicle.OdometerInMiles.Should().BeNull();
	}

	[Fact]
	public void Vehicle_FuelAndEmissions_Convert() {
		var vehicle = new Vehicle {
			CarbonEmissionsInTonsPerGallon = 3.785M,
			FuelConsumptionInMetersPerLiter = 1500
		};

		vehicle.CarbonEmissionsInTonsPerLiter.Should().Be(1M);
		vehicle.FuelConsumptionInKilometersPerLiter.Should().Be(1.50M);
		vehicle.FuelConsumptionInMilesPerGallon.Should().Be(3.53M);
	}
}

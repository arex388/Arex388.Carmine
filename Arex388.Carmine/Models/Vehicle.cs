using Arex388.Carmine.Converters;
using System.Text.Json.Serialization;

namespace Arex388.Carmine;

/// <summary>
/// Vehicle object.
/// </summary>
public sealed class Vehicle {
	private decimal? _carbonEmissionsInTonsPerLiter;
	private decimal? _fuelConsumptionInKilometersPerLiter;
	private decimal? _fuelConsumptionInMilesPerGallon;
	private int? _odometerInKilometers;
	private int? _odometerInMiles;

	/// <summary>
	/// The vehicle's CO2 emissions in tons per gallon of fuel.
	/// </summary>
	[JsonPropertyName("carbon_footprint")]
	public decimal? CarbonEmissionsInTonsPerGallon { get; init; }

	/// <summary>
	/// The vehicle's CO2 emissions in tons per liter of fuel.
	/// </summary>
	public decimal? CarbonEmissionsInTonsPerLiter => _carbonEmissionsInTonsPerLiter ??= CarbonEmissionsInTonsPerGallon / 3.785M;

	/// <summary>
	/// The vehicle's color.
	/// </summary>
	public string? Color { get; init; }

	/// <summary>
	/// The vehicle's created timestamp in UTC.
	/// </summary>
	[JsonPropertyName("created")]
	public DateTime CreatedAtUtc { get; init; }

	/// <summary>
	/// The vehicle's current driver id.
	/// </summary>
	[JsonPropertyName("current_driver")]
	public UserId? DriverId { get; init; }

	/// <summary>
	/// Diagnostic fault codes.
	/// </summary>
	[JsonPropertyName("fault_codes")]
	public IDictionary<string, string>? Faults { get; init; }

	/// <summary>
	/// The vehicle's fuel consumption in kilometers per liter, rounded to two decimal places.
	/// </summary>
	public decimal? FuelConsumptionInKilometersPerLiter => _fuelConsumptionInKilometersPerLiter ??= Math.Round(FuelConsumptionInMetersPerLiter * .001M, 2) + 0.00M;

	/// <summary>
	/// The vehicle's fuel consumption in meters per liter.
	/// </summary>
	[JsonPropertyName("fuel_economy")]
	public int FuelConsumptionInMetersPerLiter { get; init; }

	/// <summary>
	/// The vehicle's fuel consumption in miles per galon, rounded to two decimal places.
	/// </summary>
	public decimal? FuelConsumptionInMilesPerGallon => _fuelConsumptionInMilesPerGallon ??= Math.Round(FuelConsumptionInMetersPerLiter * .0023521442146661M, 2) + 0.00M;

	/// <summary>
	/// The vehicle's current remaining fuel.
	/// </summary>
	[JsonPropertyName("fuel_level")]
	public byte FuelRemaining { get; init; }

	/// <summary>
	/// The vehicle's id.
	/// </summary>
	public VehicleId Id { get; init; }

	/// <summary>
	/// The vehicle's last activity timestamp.
	/// </summary>
	[JsonPropertyName("last_activity")]
	public DateTime? LastActivityAt { get; init; }

	/// <summary>
	/// The vehicle's last trip id.
	/// </summary>
	[JsonPropertyName("last_trip")]
	public TripId? LastTripId { get; init; }

	/// <summary>
	/// The vehicle's current latitude.
	/// </summary>
	[JsonPropertyName("current_latitude")]
	public decimal? Latitude { get; init; }

	/// <summary>
	/// The vehicle's license plate number.
	/// </summary>
	[JsonPropertyName("license_plate")]
	public string? LicensePlateNumber { get; init; }

	/// <summary>
	/// The vehicle's current longitude.
	/// </summary>
	[JsonPropertyName("current_longitude")]
	public decimal? Longitude { get; init; }

	/// <summary>
	/// The vehicle's make.
	/// </summary>
	public string? Make { get; init; }

	/// <summary>
	/// The vehicle's model.
	/// </summary>
	public string? Model { get; init; }

	/// <summary>
	/// Notes for the vehicle.
	/// </summary>
	public string? Notes { get; init; }

	/// <summary>
	/// The vehicle's odometer in kilometers.
	/// </summary>
	public int? OdometerInKilometers => _odometerInKilometers ??= OdometerInMeters / 1000;

	/// <summary>
	/// The vehicle's odometer in meters.
	/// </summary>
	[JsonPropertyName("odometer")]
	public int? OdometerInMeters { get; init; }

	/// <summary>
	/// The vehicle's odometer in miles.
	/// </summary>
	public int? OdometerInMiles => _odometerInMiles ??= OdometerInMeters / 1609;

	/// <summary>
	/// The vehicle's status.
	/// </summary>
	public VehicleStatus Status { get; init; }

	/// <summary>
	/// The vehicle's VIN.
	/// </summary>
	public string? Vin { get; init; }

	/// <summary>
	/// The vehicle's year.
	/// </summary>
	public short? Year { get; init; }
}
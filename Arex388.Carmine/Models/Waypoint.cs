using System.Text.Json.Serialization;

namespace Arex388.Carmine;

/// <summary>
/// Waypoint object.
/// </summary>
//	Unlike Trip, the unit conversions here are deliberately uncached: backing
//	fields would add ~20 bytes to each of the thousands of waypoints a trip can
//	carry, and recomputing is cheaper than that memory.
public sealed class Waypoint {
	/// <summary>
	/// The waypoint's timestamp in UTC.
	/// </summary>
	[JsonPropertyName("time")]
	public DateTime AtUtc { get; init; }

	/// <summary>
	/// The trip's distance traveled at this waypoint in kilometers.
	/// </summary>
	public decimal DistanceTraveledInKilometers => Math.Round(DistanceTraveledInMeters / UnitConversions.MetersPerKilometer, 2) + 0.00M;

	/// <summary>
	/// The trip's distance traveled at this waypoint in meters.
	/// </summary>
	[JsonPropertyName("distance")]
	public int DistanceTraveledInMeters { get; init; }

	/// <summary>
	/// The trip's distance traveled at this waypoint in miles.
	/// </summary>
	public decimal DistanceTraveledInMiles => Math.Round(DistanceTraveledInMeters / UnitConversions.MetersPerMile, 2) + 0.00M;

	/// <summary>
	/// The vehicle's elevation above sea level in kilometers.
	/// </summary>
	public decimal ElevationInKilometers => Math.Round(ElevationInMeters / UnitConversions.MetersPerKilometer, 2) + 0.00M;

	/// <summary>
	/// The vehicle's elevation above sea level in meters.
	/// </summary>
	[JsonPropertyName("altitude")]
	public decimal ElevationInMeters { get; init; }

	/// <summary>
	/// The vehicle's elevation above sea level in miles.
	/// </summary>
	public decimal ElevationInMiles => Math.Round(ElevationInMeters / UnitConversions.MetersPerMile, 2) + 0.00M;

	/// <summary>
	/// The vehicle's engine's RPM at this waypoint.
	/// </summary>
	[JsonPropertyName("rpm")]
	public int? EngineRpm { get; init; }
	
	/// <summary>
	/// The waypoint's latitude.
	/// </summary>
	public decimal Latitude { get; init; }

	/// <summary>
	/// The waypoint's longitude.
	/// </summary>
	public decimal Longitude { get; init; }

	/// <summary>
	/// The vehicle's speed at this waypoint in kilometers per hour, rounded to two decimal places.
	/// </summary>
	public decimal SpeedInKilometersPerHour => Math.Round(SpeedInMetersPerSecond * UnitConversions.MetersPerSecondToKilometersPerHour, 2) + 0.00M;

	/// <summary>
	/// The vehicle's speed at this waypoint in meters per second.
	/// </summary>
	[JsonPropertyName("speed")]
	public decimal SpeedInMetersPerSecond { get; init; }

	/// <summary>
	/// The vehicle's speed at this waypoint in miles per hour, rounded to two decimal places.
	/// </summary>
	public decimal SpeedInMilesPerHour => Math.Round(SpeedInMetersPerSecond * UnitConversions.MetersPerSecondToMilesPerHour, 2) + 0.00M;
}
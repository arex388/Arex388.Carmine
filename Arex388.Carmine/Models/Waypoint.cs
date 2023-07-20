using System;
using System.Text.Json.Serialization;

#nullable enable

namespace Arex388.Carmine;

/// <summary>
/// Waypoint object.
/// </summary>
public sealed class Waypoint {
	private decimal? _distanceTraveledInKilometers;
	private decimal? _distanceTraveledInMiles;
	private decimal? _elevationInKilometers;
	private decimal? _elevationInMiles;
	private decimal? _speedInKilometersPerHour;
	private decimal? _speedInMilesPerHour;

	/// <summary>
	/// The waypoint's timestamp in UTC.
	/// </summary>
	[JsonPropertyName("time")]
	public DateTime AtUtc { get; init; }

	/// <summary>
	/// The trip's distance traveled at this waypoint in kilometers.
	/// </summary>
	public decimal DistanceTraveledInKilometers => _distanceTraveledInKilometers ??= Math.Round(DistanceTraveledInMeters / 1000M, 2) + 0.00M;

	/// <summary>
	/// The trip's distance traveled at this waypoint in meters.
	/// </summary>
	[JsonPropertyName("distance")]
	public int DistanceTraveledInMeters { get; init; }

	/// <summary>
	/// The trip's distance traveled at this waypoint in miles.
	/// </summary>
	public decimal DistanceTraveledInMiles => _distanceTraveledInMiles ??= Math.Round(DistanceTraveledInMeters / 1609M, 2) + 0.00M;

	/// <summary>
	/// The vehicle's elevation above sea level in kilometers.
	/// </summary>
	public decimal ElevationInKilometers => _elevationInKilometers ??= Math.Round(ElevationInMeters / 1000M, 2) + 0.00M;

	/// <summary>
	/// The vehicle's elevation above sea level in meters.
	/// </summary>
	[JsonPropertyName("altitude")]
	public decimal ElevationInMeters { get; init; }

	/// <summary>
	/// The vehicle's elevation above sea level in miles.
	/// </summary>
	public decimal ElevationInMiles => _elevationInMiles ??= Math.Round(ElevationInMeters / 1609M, 2) + 0.00M;

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
	public decimal SpeedInKilometersPerHour => _speedInKilometersPerHour ??= Math.Round(SpeedInMetersPerSecond * 3.6M, 2) + 0.00M;

	/// <summary>
	/// The vehicle's speed at this waypoint in meters per second.
	/// </summary>
	[JsonPropertyName("speed")]
	public decimal SpeedInMetersPerSecond { get; init; }

	/// <summary>
	/// The vehicle's speed at this waypoint in miles per hour, rounded to two decimal places.
	/// </summary>
	public decimal SpeedInMilesPerHour => _speedInMilesPerHour ??= Math.Round(SpeedInMetersPerSecond * 2.237M, 2) + 0.00M;
}
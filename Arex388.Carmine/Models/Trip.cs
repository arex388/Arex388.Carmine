using System.Text.Json.Serialization;

namespace Arex388.Carmine;

/// <summary>
/// Trip object.
/// </summary>
public class Trip {
    /// <summary>
	/// The trip's distance traveled in kilometers, rounded to two decimal places.
	/// </summary>
	public decimal DistanceTraveledInKilometers => Math.Round(DistanceTraveledInMeters / 1000M, 2) + 0.00M;

	/// <summary>
	/// The trip's distance traveled in meters.
	/// </summary>
	[JsonPropertyName("distance")]
	public int DistanceTraveledInMeters { get; init; }

	/// <summary>
	/// The trip's distance traveled in miles, rounded to two decimal places.
	/// </summary>
	public decimal DistanceTraveledInMiles => Math.Round(DistanceTraveledInMeters / 1609.344M, 2) + 0.00M;

	/// <summary>
	/// The trip's end timestamp.
	/// </summary>
	[JsonPropertyName("end_time")]
	public DateTime? EndAt { get; init; }

	/// <summary>
	/// The trip's id.
	/// </summary>
	public TripId Id { get; init; }

	/// <summary>
	/// Flag indicating if the trip was after hours (outside of work hours, but not explicitly for personal use).
	/// </summary>
	public bool IsAfterHours { get; init; }

	/// <summary>
	/// Flag indicating if the trip is hidden.
	/// </summary>
	public bool IsHidden { get; init; }

	/// <summary>
	/// Flag indicating if the trip was for personal use (outside of work hours).
	/// </summary>
	public bool IsPersonal { get; init; }

	/// <summary>
	/// Flag indicating if the vehicle did not move during the trip.
	/// </summary>
	public bool IsStationary { get; init; }

	/// <summary>
	/// The trip's maximum speed in kilometers per hour, rounded to two decimal places.
	/// </summary>
	public decimal? MaxSpeedInKilometersPerHour => MaxSpeedInMetersPerSecond is null
		? null
		: field ??= Math.Round(MaxSpeedInMetersPerSecond.Value * 3.6M, 2) + 0.00M;

	/// <summary>
	/// The trip's maximum speed in meters per second.
	/// </summary>
	[JsonPropertyName("max_speed")]
	public decimal? MaxSpeedInMetersPerSecond { get; init; }

	/// <summary>
	/// The trip's maximum speed in miles per hour, rounded to two decimal places.
	/// </summary>
	public decimal? MaxSpeedInMilesPerHour => MaxSpeedInMetersPerSecond is null
		? null
		: field ??= Math.Round(MaxSpeedInMetersPerSecond.Value * 2.237M, 2) + 0.00M;

	/// <summary>
	/// The amount of time, in seconds, the vehicle was parked before the trip's start.
	/// </summary>
	[JsonPropertyName("time_parked")]
	public int ParkedSeconds { get; init; }

	/// <summary>
	/// The trip's start timestamp.
	/// </summary>
	[JsonPropertyName("start_time")]
	public DateTime StartAt { get; init; }
}
namespace Arex388.Carmine;

/// <summary>
/// Trip object.
/// </summary>
public class Trip {
	//	The distance conversions cache with explicit nullable backing fields
	//	because the properties are non-nullable — the field keyword's backing
	//	field takes the property's type, so `field ??=` only works for the
	//	nullable speed conversions below.
	private decimal? _distanceTraveledInKilometers;
	private decimal? _distanceTraveledInMiles;

    /// <summary>
	/// The trip's distance traveled in kilometers, rounded to two decimal places.
	/// </summary>
	public decimal DistanceTraveledInKilometers => _distanceTraveledInKilometers ??= Math.Round(DistanceTraveledInMeters / UnitConversions.MetersPerKilometer, 2) + 0.00M;

	/// <summary>
	/// The trip's distance traveled in meters.
	/// </summary>
	public int DistanceTraveledInMeters { get; init; }

	/// <summary>
	/// The trip's distance traveled in miles, rounded to two decimal places.
	/// </summary>
	public decimal DistanceTraveledInMiles => _distanceTraveledInMiles ??= Math.Round(DistanceTraveledInMeters / UnitConversions.MetersPerMile, 2) + 0.00M;

	/// <summary>
	/// The trip's end timestamp.
	/// </summary>
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
		: field ??= Math.Round(MaxSpeedInMetersPerSecond.Value * UnitConversions.MetersPerSecondToKilometersPerHour, 2) + 0.00M;

	/// <summary>
	/// The trip's maximum speed in meters per second.
	/// </summary>
	public decimal? MaxSpeedInMetersPerSecond { get; init; }

	/// <summary>
	/// The trip's maximum speed in miles per hour, rounded to two decimal places.
	/// </summary>
	public decimal? MaxSpeedInMilesPerHour => MaxSpeedInMetersPerSecond is null
		? null
		: field ??= Math.Round(MaxSpeedInMetersPerSecond.Value * UnitConversions.MetersPerSecondToMilesPerHour, 2) + 0.00M;

	/// <summary>
	/// The amount of time, in seconds, the vehicle was parked before the trip's start.
	/// </summary>
	public int ParkedSeconds { get; init; }

	/// <summary>
	/// The trip's start timestamp.
	/// </summary>
	public DateTime StartAt { get; init; }
}
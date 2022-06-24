using Newtonsoft.Json;
using System;

namespace Arex388.Carmine; 

/// <summary>
/// Waypoint object.
/// </summary>
public sealed class Waypoint {
    /// <summary>
    /// The vehicle's elevation above sea level in kilometers, rounded to two decimal places.
    /// </summary>
    public decimal ElevationInKilomenters => Math.Round(ElevationInMeters / 1000.0M, 2);

    /// <summary>
    /// The vehicle's elevation above sea level in meters.
    /// </summary>
    [JsonProperty("altitude")]
    public decimal ElevationInMeters { get; set; }

    /// <summary>
    /// The vehicle's elevation above sea level in miles, rounded to two decimal places.
    /// </summary>
    public decimal ElevationInMiles => Math.Round(ElevationInMeters / 1609.0M, 2);

    /// <summary>
    /// The trip's distance traveled in kilometers, rounded to two decimal places.
    /// </summary>
    public decimal DistanceTraveledInKilometers => Math.Round(DistanceTraveledInMeters / 1000.0M, 2);

    /// <summary>
    /// The trip's distance traveled in meters.
    /// </summary>
    [JsonProperty("distance")]
    public decimal DistanceTraveledInMeters { get; set; }

    /// <summary>
    /// The trip's distance traveled in miles, rounded to two decimal places.
    /// </summary>
    public decimal DistanceTraveledInMiles => Math.Round(DistanceTraveledInMeters / 1609.0M, 2);

    /// <summary>
    /// The vehicle's engine's RPM.
    /// </summary>
    [JsonProperty("rpm")]
    public int? EngineRpm { get; set; }

    /// <summary>
    /// The waypoint's latitude.
    /// </summary>
    public decimal Latitude { get; set; }

    /// <summary>
    /// The waypoint's longitude.
    /// </summary>
    public decimal Longitude { get; set; }

    /// <summary>
    /// The waypoint's timestamp in UTC.
    /// </summary>
    [JsonProperty("time")]
    public DateTime AtUtc { get; set; }

    /// <summary>
    /// The vehicle's speed in kilometers per hour, rounded to two decimal places.
    /// </summary>
    public decimal SpeedInKilometersPerHour => Math.Round(SpeedInMetersPerSecond * 3.6M, 2);

    /// <summary>
    /// The vehicle's speed in meters per second.
    /// </summary>
    [JsonProperty("speed")]
    public decimal SpeedInMetersPerSecond { get; set; }

    /// <summary>
    /// The vehicle's speed in miles per hour, rounded to two decimal places.
    /// </summary>
    public decimal SpeedInMilesPerHour => Math.Round(SpeedInMetersPerSecond * 2.237M, 2);

    /// <summary>
    /// The waypoint's trip id.
    /// </summary>
    [JsonProperty("trip_id")]
    public Guid TripId { get; set; }
}
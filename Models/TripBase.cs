using Newtonsoft.Json;
using System;

namespace Arex388.Carmine {
    /// <summary>
    /// Trip object base.
    /// </summary>
    public abstract class TripBase {
        /// <summary>
        /// Unknown.
        /// </summary>
        [JsonProperty("trip_cost")]
        public decimal? Cost { get; set; }

        /// <summary>
        /// The trip's distance traveled in kilometers, rounded to two decimal places.
        /// </summary>
        public decimal DistanceInKilometers => Math.Round(DistanceInMeters / 1000.0M, 2);

        /// <summary>
        /// The trip's distance traveled in meters.
        /// </summary>
        [JsonProperty("distance")]
        public int DistanceInMeters { get; set; }

        /// <summary>
        /// The trip's distance traveled in miles, rounded to two decimal places.
        /// </summary>
        public decimal DistanceInMiles => Math.Round(DistanceInMeters / 1609.0M, 2);

        /// <summary>
        /// The trip's end timestamp in UTC.
        /// </summary>
        [JsonProperty("end_time")]
        public DateTime? EndedAtUtc { get; set; }

        /// <summary>
        /// The trip's end location.
        /// </summary>
        [JsonProperty("end_location")]
        public Location EndingLocation { get; set; }

        /// <summary>
        /// Have any events occured during the trip.
        /// </summary>
        [JsonProperty("has_events")]
        public bool HasEvents { get; set; }

        /// <summary>
        /// The trip's id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Is teh trip after hours (outside of work hours, but not for personal use).
        /// </summary>
        [JsonProperty("is_after_hours")]
        public bool IsAfterHours { get; set; }

        /// <summary>
        /// Is the trip hidden.
        /// </summary>
        [JsonProperty("is_hidden")]
        public bool IsHidden { get; set; }

        /// <summary>
        /// Is the trip for personal use (outside of work hours).
        /// </summary>
        [JsonProperty("is_personal")]
        public bool IsPersonal { get; set; }

        /// <summary>
        /// Was there any movement during the trip or was the vehicle stationary the whole time.
        /// </summary>
        [JsonProperty("is_stationary")]
        public bool IsStationary { get; set; }

        /// <summary>
        /// The trip's maximum speed in kilometers per hour, rounded to two decimal places.
        /// </summary>
        public decimal? MaxSpeedInKilometersPerHour =>
            MaxSpeedInMetersPerSecond.HasValue
                ? Math.Round(MaxSpeedInMetersPerSecond.Value * 3.6M, 2)
                : null;

        /// <summary>
        /// The trip's maximum speed in meters per second.
        /// </summary>
        [JsonProperty("max_speed")]
        public decimal? MaxSpeedInMetersPerSecond { get; set; }

        /// <summary>
        /// The trip's maximum speed in miles per hour, rounded to two decimal places.
        /// </summary>
        public decimal? MaxSpeedInMilesPerHour =>
            MaxSpeedInMetersPerSecond.HasValue
                ? Math.Round(MaxSpeedInMetersPerSecond.Value * 2.237M, 2)
                : null;

        [JsonProperty("time_parked")]
        private int ParkedTimeInSecondsInternal { get; set; }

        /// <summary>
        /// The amount of time the vehicle was parked before the trip.
        /// </summary>
        public TimeSpan ParkedTime => TimeSpan.FromSeconds(ParkedTimeInSecondsInternal);

        /// <summary>
        /// The trip's start timestamp in UTC.
        /// </summary>
        [JsonProperty("start_time")]
        public DateTime StartedAtUtc { get; set; }

        /// <summary>
        /// The trip's start location.
        /// </summary>
        [JsonProperty("start_location")]
        public Location StartingLocation { get; set; }
    }
}
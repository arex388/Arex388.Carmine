using Newtonsoft.Json;
using System;

namespace Arex388.Carmine {
    /// <summary>
    /// Event object.
    /// </summary>
    public sealed class Event {
        /// <summary>
        /// The event's end timestamp in UTC.
        /// </summary>
        [JsonProperty("end_time")]
        public DateTime EndedAtUtc { get; set; }

        /// <summary>
        /// The event's data in JSON.
        /// </summary>
        [JsonProperty("event_data")]
        public string Data { get; set; }

        /// <summary>
        /// The event's id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The event's start timestamp in UTC.
        /// </summary>
        [JsonProperty("start_time")]
        public DateTime StartedAtUtc { get; set; }

        /// <summary>
        /// The event's trip id.
        /// </summary>
        [JsonProperty("trip")]
        public Guid TripId { get; set; }

        [JsonProperty("event_type")]
        private string TypeInternal { get; set; }

        /// <summary>
        /// The event's type.
        /// </summary>
        public EventType Type => GetType();

        //  ====================================================================
        //  Utilities
        //  ====================================================================

        private new EventType GetType() => TypeInternal switch {
            "after_hours_driving" => EventType.AfterHoursDriving,
            "excessive_idling" => EventType.ExcessiveIdling,
            "extreme_acceleration" => EventType.ExtremeAcceleration,
            "extreme_braking" => EventType.ExtremeBraking,
            "harsh_acceleration" => EventType.HarshAcceleration,
            "harsh_braking" => EventType.HarshBraking,
            "harsh_cornering" => EventType.HarshCornering,
            "high_rpm" => EventType.HighRpm,
            "max_trip_duration" => EventType.MaxTripDuration,
            "speeding" => EventType.Speeding,
            _ => throw new ArgumentException(nameof(TypeInternal))
        };
    }
}
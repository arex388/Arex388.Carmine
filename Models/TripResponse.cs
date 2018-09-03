using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Arex388.Carmine {
	public sealed class TripResponse :
		ResponseBase {
		[JsonProperty("distance")]
		public int DistanceInMeters { get; set; }
		[JsonProperty("end_time")]
		public DateTime? EndedAtUtc { get; set; }
		[JsonProperty("end_location")]
		public LocationResponse EndingLocation { get; set; }
		public IEnumerable<EventResponse> Events { get; set; }
		[JsonProperty("is_after_hours")]
		public bool IsAfterHours { get; set; }
		[JsonProperty("is_hidden")]
		public bool IsHidden { get; set; }
		[JsonProperty("is_personal")]
		public bool IsPersonal { get; set; }
		[JsonProperty("is_stationary")]
		public bool IsStationary { get; set; }
		[JsonProperty("max_speed")]
		public decimal? MaxSpeedInMetersPerSecond { get; set; }
		[JsonProperty("time_parked")]
		public int ParkedTimeInSeconds { get; set; }
		[JsonProperty("start_time")]
		public DateTime StartedAtUtc { get; set; }
		[JsonProperty("start_location")]
		public LocationResponse StartingLocation { get; set; }
		[JsonProperty("trip_cost")]
		public decimal? TripCost { get; set; }
		[JsonProperty("driver"), JsonConverter(typeof(UserConverter))]
		public object Driver { get; set; }
		[JsonConverter(typeof(VehicleConverter))]
		public object Vehicle { get; set; }
		public IEnumerable<WaypointResponse> Waypoints { get; set; }
	}
}
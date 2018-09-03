using System;
using Newtonsoft.Json;

namespace Arex388.Carmine {
	public sealed class WaypointResponse :
		ResponseBase {
		public decimal Altitude { get; set; }
		[JsonProperty("distance")]
		public int DistanceInMeters { get; set; }
		[JsonProperty("rpm")]
		public int? EngineRpm { get; set; }
		public decimal Latitude { get; set; }
		public decimal Longitude { get; set; }
		[JsonProperty("time")]
		public DateTime RecordedAtUtc { get; set; }
		public decimal Speed { get; set; }
		[JsonProperty("trip_id")]
		public string TripId { get; set; }
	}
}
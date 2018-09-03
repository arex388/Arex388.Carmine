using System;
using Newtonsoft.Json;

namespace Arex388.Carmine {
	public sealed class EventResponse :
		ResponseBase {
		[JsonProperty("end_time")]
		public DateTime EndedAtUtc { get; set; }
		[JsonProperty("event_data")]
		public string Data { get; set; }
		[JsonProperty("start_time")]
		public DateTime StartedAtUtc { get; set; }
		[JsonProperty("trip")]
		public string TripId { get; set; }
		[JsonProperty("event_type")]
		public string Type { get; set; }
	}
}
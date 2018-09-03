using System;
using Newtonsoft.Json;

namespace Arex388.Carmine {
	public sealed class LocationResponse :
		ResponseBase {
		public string Address { get; set; }
		public string Category { get; set; }
		[JsonProperty("created")]
		public DateTime CratedAtUtc { get; set; }
		[JsonProperty("creator")]
		public string CreatorId { get; set; }
		[JsonProperty("geometry")]
		public string Geofence { get; set; }
		[JsonProperty("last_activity_type")]
		public DateTime? LastActivityAtUtc { get; set; }
		[JsonProperty("last_activity_driver")]
		public string LastDriverId { get; set; }
		public decimal Latitude { get; set; }
		public decimal Longitude { get; set; }
		public string Name { get; set; }
		public string Notes { get; set; }
		public string Type { get; set; }
		[JsonProperty("Popularity")]
		public int VisitsCount { get; set; }
	}
}
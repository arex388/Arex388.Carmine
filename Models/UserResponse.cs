using System;
using Newtonsoft.Json;

namespace Arex388.Carmine {
	public sealed class UserResponse :
		ResponseBase {
		[JsonProperty("created")]
		public DateTime CreatedAtUtc { get; set; }
		public string Email { get; set; }
		[JsonProperty("active")]
		public bool IsActive { get; set; }
		[JsonProperty("validated")]
		public bool IsValidated { get; set; }
		[JsonProperty("last_activity")]
		public DateTime? LastActivityAtUtc { get; set; }
		[JsonProperty("last_trip")]
		public string LastTripId { get; set; }
		public string Name { get; set; }
		public string Role { get; set; }
		[JsonProperty("current_vehicle")]
		public string VehicleId { get; set; }
	}
}
using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Arex388.Carmine {
	public sealed class VehicleResponse :
		ResponseBase {
		[JsonProperty("created")]
		public DateTime CreatedAtUtc { get; set; }
		[JsonProperty("current_driver")]
		public string DriverId { get; set; }
		[JsonProperty("fault_codes")]
		public IDictionary<string, string> Faults { get; set; }
		[JsonProperty("fuel_economy")]
		public int FuelConsumptionInMetersPerLiter { get; set; }
		[JsonProperty("fuel_level")]
		public int FuelRemaining { get; set; }
		[JsonProperty("last_activity")]
		public DateTime? LastActivityAtUtc { get; set; }
		[JsonProperty("last_trip")]
		public string LastTripId { get; set; }
		[JsonProperty("current_latitude")]
		public decimal? Latitude { get; set; }
		[JsonProperty("current_longitude")]
		public decimal? Longitude { get; set; }
		public string Make { get; set; }
		public string Model { get; set; }
		[JsonProperty("odometer")]
		public int OdometerInMeters { get; set; }
		[JsonProperty("license_plate")]
		public string RegistrationNumber { get; set; }
		public string Status { get; set; }
		public string Vin { get; set; }
		public short Year { get; set; }
	}
}
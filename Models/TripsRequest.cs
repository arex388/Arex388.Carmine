using System;
using System.Collections.Generic;
using System.Linq;

namespace Arex388.Carmine {
	public sealed class TripsRequest :
		RequestBase {
		public IEnumerable<string> DriverIds { get; set; } = Enumerable.Empty<string>();
		public DateTime? EndedAtUtc { get; set; }
		public override string Endpoint {
			get {
				var parameters = new HashSet<string> {
					$"lang={Language}",
					$"per_page={Limit}"
				};

				if (DriverIds.Any()) {
					var driverIds = string.Join(",", DriverIds);

					parameters.Add($"driver={driverIds}");
				}

				if (EndedAtUtc.HasValue) {
					var endedAtUtc = EndedAtUtc.Value.ToString("O");

					parameters.Add($"end_time={endedAtUtc}");
				}

				if (StartedAtUtc.HasValue) {
					var startedAtUtc = StartedAtUtc.Value.ToString("O");

					parameters.Add($"start_time={startedAtUtc}");
				}

				if (VehicleIds.Any()) {
					var vehicleIds = string.Join(",", VehicleIds);

					parameters.Add($"vehicle={vehicleIds}");
				}

				var query = string.Join("&", parameters);

				return $"{EndpointRoot}/trips?{query}";
			}
		}
		public string Language { get; set; } = Languages.English;
		public int Limit { get; set; } = 100;
		public DateTime? StartedAtUtc { get; set; }
		public IEnumerable<string> VehicleIds { get; set; } = Enumerable.Empty<string>();
	}
}

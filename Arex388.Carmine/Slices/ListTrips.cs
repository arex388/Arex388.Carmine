namespace Arex388.Carmine;

/// <summary>
/// ListTrips request and response containers.
/// </summary>
public static class ListTrips {
	/// <summary>
	/// ListTrips request container.
	/// </summary>
	public sealed class Request :
		RequestBase {
		internal static Request Instance = new();

		/// <summary>
		/// The trip's driver id.
		/// </summary>
		public UserId? DriverId { get; init; }

		/// <summary>
		/// The trip's end timestamp in UTC.
		/// </summary>
		public DateTime? EndAtUtc { get; init; }

		internal override string Endpoint => GetEndpoint(this);

		/// <summary>
		/// The language the results should be returned in. <c>English</c> by default.
		/// </summary>
		public Language Language { get; init; } = Language.English;

		/// <summary>
		/// The trip's start timestamp in UTC.
		/// </summary>
		public DateTime? StartAtUtc { get; init; }

		/// <summary>
		/// The number of results to return. <c>100</c> by default.
		/// </summary>
		public int Take { get; init; } = 100;

		/// <summary>
		/// The trip's vehicle id.
		/// </summary>
		public VehicleId? VehicleId { get; init; }

		//	========================================================================
		//	Utilities
		//	========================================================================

		private static string GetEndpoint(
			Request request) {
			var parameters = new HashSet<string> {
				$"lang={request.Language.ToStringFast()}",
				$"per_page={request.Take}"
			};

			if (request.DriverId.HasValue) {
				parameters.Add($"driver[]={request.DriverId}");
			}

			if (request.EndAtUtc.HasValue) {
				parameters.Add($"end_time={request.EndAtUtc:yyyy-MM-ddThh:mm:ss}");
			}

			if (request.StartAtUtc.HasValue) {
				parameters.Add($"start_time={request.StartAtUtc:yyyy-MM-ddThh:mm:ss}");
			}

			if (request.VehicleId.HasValue) {
				parameters.Add($"vehicle[]={request.VehicleId}");
			}

			return $"trips?{parameters.StringJoin("&")}";
		}
	}

	/// <summary>
	/// ListTrips response container.
	/// </summary>
	public sealed class Response :
		ResponseBase<Response> {
		/// <summary>
		/// The matched trips.
		/// </summary>
		public IList<Trip> Trips { get; init; } = [];
	}
}
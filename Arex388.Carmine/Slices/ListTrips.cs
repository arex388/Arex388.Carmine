namespace Arex388.Carmine;

/// <summary>
/// ListTrips request and response containers.
/// </summary>
public static class ListTrips {
	private static Response? _cancelled;
	private static Response? _failed;
	private static Response? _timedOut;

	internal static Response Cancelled => _cancelled ??= new Response {
		Status = ResponseStatus.Cancelled
	};
	internal static Response Failed => _failed ??= new Response {
		Status = ResponseStatus.Failed
	};
	internal static Response TimedOut => _timedOut ??= new Response {
		Status = ResponseStatus.TimedOut
	};

	/// <summary>
	/// ListTrips request container.
	/// </summary>
	public sealed class Request {
		/// <summary>
		/// The trip's driver id.
		/// </summary>
		public UserId? DriverId { get; init; }

		/// <summary>
		/// The trip's end timestamp in UTC.
		/// </summary>
		public DateTime? EndAtUtc { get; init; }

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
	}

	/// <summary>
	/// ListTrips response container.
	/// </summary>
	public sealed class Response {
		/// <summary>
		/// The response's status.
		/// </summary>
		public ResponseStatus Status { get; init; }

		/// <summary>
		/// The matched trips.
		/// </summary>
		public IList<Trip> Trips { get; init; } = [];
	}
}
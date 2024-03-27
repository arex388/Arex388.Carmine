namespace Arex388.Carmine;

/// <summary>
/// ListVehicles request and response containers.
/// </summary>
public static class ListVehicles {
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
	/// ListVehicles request container.
	/// </summary>
	public sealed class Request {
		/// <summary>
		/// The language the results should be returned in. <c>English</c> by default.
		/// </summary>
		public Language Language { get; init; } = Language.English;

		/// <summary>
		/// Filter by the vehicle's VIN, license plate number, year, make, or model.
		/// </summary>
		public string? Search { get; init; }

		/// <summary>
		/// Filter by the vehicle's status. <c>All</c> by default.
		/// </summary>
		public VehicleStatus? Status { get; init; }
	}

	/// <summary>
	/// ListVehicles response container.
	/// </summary>
	public sealed class Response {
		/// <summary>
		/// The response's status.
		/// </summary>
		public ResponseStatus Status { get; init; }

		/// <summary>
		/// The matched vehicles.
		/// </summary>
		public IList<Vehicle> Vehicles { get; init; } = [];
	}
}
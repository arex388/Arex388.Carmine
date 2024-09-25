using System.Net;

namespace Arex388.Carmine;

/// <summary>
/// ListVehicles request and response containers.
/// </summary>
public static class ListVehicles {
	/// <summary>
	/// ListVehicles request container.
	/// </summary>
	public sealed class Request :
		RequestBase {
		internal static Request Instance = new();

		internal override string Endpoint => GetEndpoint(this);

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

		//	========================================================================
		//	Utilities
		//	========================================================================

		private static string GetEndpoint(
			Request request) {
			var parameters = new HashSet<string> {
				$"lang={request.Language.ToStringFast()}"
			};

			if (request.Search.HasValue()) {
				var search = WebUtility.UrlEncode(request.Search);

				parameters.Add($"search={search}");
			}

			if (request.Status.HasValue) {
				parameters.Add($"status={request.Status?.ToStringFast()}");
			}

			return $"vehicles?{parameters.StringJoin("&")}";
		}
	}

	/// <summary>
	/// ListVehicles response container.
	/// </summary>
	public sealed class Response :
		ResponseBase<Response> {
		/// <summary>
		/// The matched vehicles.
		/// </summary>
		public IList<Vehicle> Vehicles { get; init; } = [];
	}
}
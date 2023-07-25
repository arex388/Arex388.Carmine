namespace Arex388.Carmine.Extensions;

internal static class ListTripsRequestExtensions {
	public static string GetEndpoint(
		this ListTrips.Request request) {
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
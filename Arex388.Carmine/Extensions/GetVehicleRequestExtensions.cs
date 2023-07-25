namespace Arex388.Carmine.Extensions;

internal static class GetVehicleRequestExtensions {
	public static string GetEndpoint(
		this GetVehicle.Request request) => $"vehicles/{request.Id}?lang={request.Language.ToStringFast()}";
}
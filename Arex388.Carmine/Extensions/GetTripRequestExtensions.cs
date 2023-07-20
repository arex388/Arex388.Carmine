#nullable enable

namespace Arex388.Carmine.Extensions;

internal static class GetTripRequestExtensions {
	public static string GetEndpoint(
		this GetTrip.Request request) => $"trips/{request.Id}?lang={request.Language.ToStringFast()}";
}
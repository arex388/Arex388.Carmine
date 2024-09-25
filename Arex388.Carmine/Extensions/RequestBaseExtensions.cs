namespace Arex388.Carmine;

internal static class RequestBaseExtensions {
	public static string GetEndpoint(
		this RequestBase request,
		CarmineClientOptions options) => $"{request.Endpoint}&api_key={options.Key}";
}
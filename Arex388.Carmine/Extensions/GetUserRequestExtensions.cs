#nullable enable

namespace Arex388.Carmine.Extensions;

internal static class GetUserRequestExtensions {
	public static string GetEndpoint(
		this GetUser.Request request) => $"users/{request.Id}?lang={request.Language.ToStringFast()}";
}
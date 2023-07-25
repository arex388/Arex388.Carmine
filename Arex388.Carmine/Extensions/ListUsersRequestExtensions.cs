using System.Net;

namespace Arex388.Carmine.Extensions;

internal static class ListUsersRequestExtensions {
	public static string GetEndpoint(
		this ListUsers.Request request) {
		var parameters = new HashSet<string> {
			$"lang={request.Language.ToStringFast()}"
		};

		if (request.Search.HasValue()) {
			var search = WebUtility.UrlEncode(request.Search);

			parameters.Add($"search={search}");
		}

		if (request.Status.HasValue) {
			var active = request.Status == UserStatus.Active
				? "true"
				: "false";

			parameters.Add($"active={active}");
		}

		return $"users?{parameters.StringJoin("&")}";
	}
}
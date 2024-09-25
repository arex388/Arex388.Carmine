using System.Net;

namespace Arex388.Carmine;

/// <summary>
/// ListUsers request and response containers.
/// </summary>
public static class ListUsers {
	/// <summary>
	/// ListUsers request container.
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
		/// Filter by the user's name or email. Supports SQL's LIKE syntax.
		/// </summary>
		public string? Search { get; init; }

		/// <summary>
		/// Filter by the user's status.
		/// </summary>
		public UserStatus? Status { get; init; }

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
				var active = request.Status == UserStatus.Active
					? "true"
					: "false";

				parameters.Add($"active={active}");
			}

			return $"users?{parameters.StringJoin("&")}";
		}
	}

	/// <summary>
	/// ListUsers response container.
	/// </summary>
	public sealed class Response :
		ResponseBase<Response> {
		/// <summary>
		/// The matched users.
		/// </summary>
		public IList<User> Users { get; init; } = [];
	}
}
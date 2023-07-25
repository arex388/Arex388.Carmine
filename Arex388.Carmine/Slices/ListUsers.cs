namespace Arex388.Carmine;

/// <summary>
/// ListUsers request and response containers.
/// </summary>
public sealed class ListUsers {
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
	/// ListUsers request container.
	/// </summary>
	public sealed class Request {
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
	}

	/// <summary>
	/// ListUsers response container.
	/// </summary>
	public sealed class Response {
		/// <summary>
		/// The response's status.
		/// </summary>
		public ResponseStatus Status { get; init; }

		/// <summary>
		/// The matched users.
		/// </summary>
		public IList<User> Users { get; init; } = new List<User>(0);
	}
}
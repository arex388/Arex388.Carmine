﻿using System;

#nullable enable

namespace Arex388.Carmine;

/// <summary>
/// GetUser request and response containers.
/// </summary>
public sealed class GetUser {
	private static Response? _cancelled;
	private static Response? _failed;
	private static Response? _invalid;
	private static Response? _timedOut;

	internal static Response Cancelled => _cancelled ??= new Response {
		Status = ResponseStatus.Cancelled
	};
	internal static Response Failed => _failed ??= new Response {
		Status = ResponseStatus.Failed
	};
	internal static Response Invalid => _invalid ??= new Response {
		Status = ResponseStatus.Invalid
	};
	internal static Response TimedOut => _timedOut ??= new Response {
		Status = ResponseStatus.TimedOut
	};

	/// <summary>
	/// GetUser request container.
	/// </summary>
	public sealed class Request {
		/// <summary>
		/// The user's id.
		/// </summary>
		public UserId Id { get; init; }

		/// <summary>
		/// The language the results should be returned in. <c>English</c> by default.
		/// </summary>
		public Language Language { get; init; } = Language.English;
	}

	/// <summary>
	/// GetUser response container.
	/// </summary>
	public sealed class Response {
		/// <summary>
		/// The response's status.
		/// </summary>
		public ResponseStatus Status { get; init; }

		/// <summary>
		/// The matched user.
		/// </summary>
		public User? User { get; init; }
	}
}
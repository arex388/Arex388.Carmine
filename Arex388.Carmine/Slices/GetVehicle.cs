﻿using FluentValidation.Results;

namespace Arex388.Carmine;

/// <summary>
/// GetVehicle request and response containers.
/// </summary>
public static class GetVehicle {
	private static Response? _cancelled;
	private static Response? _failed;
	private static Response? _timedOut;

	internal static Response Cancelled => _cancelled ??= new Response {
		Status = ResponseStatus.Cancelled
	};
	internal static Response Failed(
		Exception? exception) => exception is null
		? _failed ??= new Response {
			Status = ResponseStatus.Failed
		}
		: Failed($"[{exception.GetType().Name}] {exception.Message}");
	internal static Response Failed(
		string error) => new() {
		Errors = [
			error
		],
		Status = ResponseStatus.Failed
	};
	internal static Response Invalid(
		ValidationResult validation) => new() {
			Errors = validation.GetErrors(),
			Status = ResponseStatus.Invalid
		};
	internal static Response TimedOut => _timedOut ??= new Response {
		Status = ResponseStatus.TimedOut
	};

	/// <summary>
	/// GetVehicle request container.
	/// </summary>
	public sealed class Request {
		/// <summary>
		/// The vehicle's id.
		/// </summary>
		public VehicleId Id { get; init; }

		/// <summary>
		/// The language the results should be returned in. <c>English</c> by default.
		/// </summary>
		public Language Language { get; init; } = Language.English;
	}

	/// <summary>
	/// GetVehicle response container.
	/// </summary>
	public sealed class Response {
		/// <summary>
		/// The response's errors, if any.
		/// </summary>
		public IEnumerable<string> Errors { get; init; } = [];

		/// <summary>
		/// The response's status.
		/// </summary>
		public ResponseStatus Status { get; init; }

		/// <summary>
		/// The matched vehicle.
		/// </summary>
		public Vehicle? Vehicle { get; init; }
	}
}
using FluentValidation.Results;

namespace Arex388.Carmine;

/// <summary>
/// The response's base details.
/// </summary>
/// <typeparam name="TResponse">The response's type.</typeparam>
public abstract class ResponseBase<TResponse>
	where TResponse : ResponseBase<TResponse>, new() {
	/// <summary>
	/// The request's errors, if any.
	/// </summary>
	public IReadOnlyList<string> Errors { get; internal set; } = [];

	/// <summary>
	/// The request's status.
	/// </summary>
	public bool Success => Errors.Count == 0;

	//	============================================================================
	//	Responses
	//	============================================================================

	internal static TResponse Cancelled => new() {
		Errors = [
			"The request was cancelled."
		]
	};
	internal static TResponse Failed => new() {
		Errors = [
			"The request has failed."
		]
	};
	internal static TResponse FailedWith(
		string detail) => new() {
			Errors = [
				"The request has failed.",
				detail
			]
		};
	internal static TResponse Invalid(
		ValidationResult validationResult) => new() {
			Errors = validationResult.ToErrors()
		};
}
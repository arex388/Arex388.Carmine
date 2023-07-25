namespace FluentValidation.Results;

internal static class ValidationResultExtensions {
	public static IEnumerable<string> GetErrors(
		this ValidationResult result) => result.Errors.Select(
		_ => _.ErrorMessage);
}
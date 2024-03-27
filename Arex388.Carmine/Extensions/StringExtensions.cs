namespace System;

internal static class StringExtensions {
	private const string _separator = "&";

	public static bool HasValue(
		this string? value) => !string.IsNullOrEmpty(value);

	public static string StringJoin(
		this IEnumerable<string> values,
		string? separator = _separator) => string.Join(separator, values);
}
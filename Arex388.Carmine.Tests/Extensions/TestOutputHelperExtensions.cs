using System.Text.Json;

namespace Xunit.Abstractions;

internal static class TestOutputHelperExtensions {
	private static readonly JsonSerializerOptions _jsonSerializerOptions = new() {
		WriteIndented = true
	};

	private const char _equals = '=';
	private const string _na = "N/A";

	/// <summary>
	/// Write an empty line to the console.
	/// </summary>
	private static void WriteLine(
		this ITestOutputHelper console) => console.WriteLine(string.Empty);

	/// <summary>
	/// Write a header and object, serialized as JSON, to the console and wrap the header with "=[ HEADER ]=".
	/// </summary>
	public static void WriteLineWithHeader(
		this ITestOutputHelper console,
		string header,
		object? obj) {
		var json = obj is null
			? _na
			: JsonSerializer.Serialize(obj, _jsonSerializerOptions);

		console.WriteLineWithHeader(header, json);
	}

	/// <summary>
	/// Write a header and JSON to the console output and wrap the header with "=[ HEADER ]=".
	/// </summary>
	public static void WriteLineWithHeader(
		this ITestOutputHelper console,
		string header,
		string? json = _na) {
		console.WriteLine($"=[ {header} ]{new string(_equals, 75 - header.Length)}");
		console.WriteLine();
		console.WriteLine(json);
		console.WriteLine();
	}
}

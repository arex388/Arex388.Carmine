using System.Collections.Generic;

namespace System;

internal static class StringExtensions {
    public static bool HasValue(
        this string s) => !string.IsNullOrEmpty(s);

    public static string StringJoin(
        this IEnumerable<string> values,
        string separator) => string.Join(separator, values);
}
using System;
using System.Collections.Generic;
using System.Net;

#nullable enable

namespace Arex388.Carmine.Extensions;

internal static class ListVehiclesRequestExtensions {
	public static string GetEndpoint(
		this ListVehicles.Request request) {
		var parameters = new HashSet<string> {
			$"lang={request.Language.ToStringFast()}"
		};

		if (request.Search.HasValue()) {
			var search = WebUtility.UrlEncode(request.Search);

			parameters.Add($"search={search}");
		}

		if (request.Status.HasValue) {
			parameters.Add($"status={request.Status?.ToStringFast()}");
		}

		return $"vehicles?{parameters.StringJoin("&")}";
	}
}
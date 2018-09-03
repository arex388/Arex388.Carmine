using System.Collections.Generic;
using System.Web;

namespace Arex388.Carmine {
	public sealed class VehiclesRequest :
		RequestBase {
		public override string Endpoint {
			get {
				var parameters = new HashSet<string> {
					$"lang={Language}"
				};

				if (!string.IsNullOrEmpty(Search)) {
					var search = HttpUtility.UrlEncode(Search);

					parameters.Add($"search={search}");
				}

				if (!string.IsNullOrEmpty(Status)) {
					parameters.Add($"status={Status}");
				}

				var query = string.Join("&", parameters);

				return $"{EndpointRoot}/vehicles?{query}";
			}
		}
		public string Language { get; set; } = Languages.English;
		public string Search { get; set; }
		public string Status { get; set; }
	}
}
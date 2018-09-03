using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Arex388.Carmine {
	public sealed class UsersRequest :
		RequestBase {
		public bool? Active { get; set; }
		public override string Endpoint {
			get {
				var parameters = new HashSet<string> {
					$"lang={Language}"
				};

				if (Active.HasValue) {
					parameters.Add($"active={Active}");
				}

				if (!string.IsNullOrEmpty(Name)) {
					var name = HttpUtility.UrlEncode(Name);

					parameters.Add($"name={name}");
				}

				if (Roles.Any()) {
					var roles = string.Join(",", Roles);

					parameters.Add($"roles={roles}");
				}

				if (!string.IsNullOrEmpty(Search)) {
					var search = HttpUtility.UrlEncode(Search);

					parameters.Add($"search={search}");
				}

				var query = string.Join("&", parameters);

				return $"{EndpointRoot}/users?{query}";
			}
		}
		public string Language { get; set; } = Languages.English;
		public string Name { get; set; }
		public IEnumerable<string> Roles { get; set; } = Enumerable.Empty<string>();
		public string Search { get; set; }
	}
}
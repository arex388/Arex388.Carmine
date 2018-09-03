using System.Net.Http;

namespace Arex388.Carmine {
	public abstract class RequestBase {
		protected string EndpointRoot => "https://api.carmine.io/v2";

		public abstract string Endpoint { get; }
		public virtual HttpMethod Method { get; } = HttpMethod.Get;
	}
}
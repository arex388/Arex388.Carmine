namespace Arex388.Carmine {
	public sealed class UserRequest :
		RequestBase {
		public override string Endpoint => $"{EndpointRoot}/users/{Id}?lang={Language}";
		public string Id { get; set; }
		public string Language { get; set; } = Languages.English;
	}
}
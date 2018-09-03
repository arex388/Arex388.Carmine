namespace Arex388.Carmine {
	public sealed class VehicleRequest :
		RequestBase {
		public override string Endpoint => $"{EndpointRoot}/vehicles/{Id}?lang={Language}";
		public string Id { get; set; }
		public string Language { get; set; } = Languages.English;
	}
}
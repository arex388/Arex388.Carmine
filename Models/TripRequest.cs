namespace Arex388.Carmine {
	public sealed class TripRequest :
		RequestBase {
		public override string Endpoint => $"{EndpointRoot}/trips/{Id}?lang={Language}";
		public string Id { get; set; }
		public string Language { get; set; } = Languages.English;
	}
}
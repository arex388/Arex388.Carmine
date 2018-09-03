namespace Arex388.Carmine {
	public sealed class WaypointsRequest :
		RequestBase {
		public override string Endpoint => $"{EndpointRoot}/waypoints?trip={TripId}&lang={Language}";
		public string TripId { get; set; }
		public string Language { get; set; } = Languages.English;
	}
}
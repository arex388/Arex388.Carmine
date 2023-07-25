namespace Arex388.Carmine;

/// <summary>
/// The <c>Location</c>'s type.
/// </summary>
public enum LocationType :
	byte {
	/// <summary>
	/// The default type. If this is the value, then the response value wasn't parsed and this was used as a fallback.
	/// </summary>
	None,

	/// <summary>
	/// The location was retrieved from cache.
	/// </summary>
	Cache,

	/// <summary>
	/// The location was a geofence.
	/// </summary>
	Geofence,

	/// <summary>
	/// The location was retrieved from Google cache.
	/// </summary>
	GoogleCache,

	/// <summary>
	/// The location is a point of interest.
	/// </summary>
	PointOfInterest
}
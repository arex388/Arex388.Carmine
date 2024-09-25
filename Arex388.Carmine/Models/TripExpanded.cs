using System.Text.Json.Serialization;

namespace Arex388.Carmine;

/// <summary>
/// Trip object (expanded).
/// </summary>
public sealed class TripExpanded :
	Trip {
	/// <summary>
	/// The trip's driver.
	/// </summary>
	public User? Driver { get; init; }

	/// <summary>
	/// The trip's end location.
	/// </summary>
	public Location? EndLocation { get; init; } = null!;

	/// <summary>
	/// The trip's events.
	/// </summary>
	public IList<Event> Events { get; init; } = [];

	/// <summary>
	/// The trip's vehicle.
	/// </summary>
	public Vehicle Vehicle { get; init; } = null!;

	/// <summary>
	/// The trip's start location.
	/// </summary>
	public Location StartLocation { get; init; } = null!;

	/// <summary>
	/// The trip's waypoints.
	/// </summary>
	public IList<Waypoint> Waypoints { get; init; } = [];
}
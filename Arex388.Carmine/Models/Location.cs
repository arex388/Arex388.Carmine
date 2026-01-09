using System.Text.Json.Serialization;

namespace Arex388.Carmine;

/// <summary>
/// Location object.
/// </summary>
public sealed class Location {
	/// <summary>
	/// The location's address.
	/// </summary>
	public string Address { get; init; } = null!;

	/// <summary>
	/// The location's category.
	/// </summary>
	public LocationCategory Category { get; init; }

	/// <summary>
	/// The location's created timestamp in UTC.
	/// </summary>
	[JsonPropertyName("created")]
	public DateTime CreatedAtUtc { get; init; }

	/// <summary>
	/// The location's creator id.
	/// </summary>
	public UserId? CreatedById { get; init; }

	/// <summary>
	/// The location's driver id.
	/// </summary>
	public UserId? DriverId { get; init; }

	/// <summary>
	/// The location's bounds.
	/// </summary>
	public string Geometry { get; init; } = null!;
	
	/// <summary>
	/// The location's last activity timestamp.
	/// </summary>
	[JsonPropertyName("last_activity_time")]
	public DateTime? LastActivityAt { get; init; }

	/// <summary>
	/// The location's center's latitude.
	/// </summary>
	public decimal Latitude { get; init; }

	/// <summary>
	/// The location's center's longitude.
	/// </summary>
	public decimal Longitude { get; init; }

	/// <summary>
	/// The location's name.
	/// </summary>
	public string? Name { get; init; }

	/// <summary>
	/// Notes about the location.
	/// </summary>
	public string? Notes { get; init; }

	/// <summary>
	/// The location's visit count.
	/// </summary>
	[JsonPropertyName("popularity")]
	public int VisitedCount { get; init; }

	/// <summary>
	/// The location's type.
	/// </summary>
	public LocationType Type { get; init; }
}
using System.Text.Json.Serialization;

namespace Arex388.Carmine;

/// <summary>
/// User object.
/// </summary>
public sealed class User {
	/// <summary>
	/// Flag indicating if the user can share their vehicle's ETA.
	/// </summary>
	[JsonPropertyName("can_share_eta")]
	public bool CanShareVehiclesEta { get; init; }

	/// <summary>
	/// The user's color.
	/// </summary>
	public string? Color { get; init; }

	/// <summary>
	/// The user's created timestamp in UTC.
	/// </summary>
	[JsonPropertyName("created")]
	public DateTime CreatedAtUtc { get; init; }

	/// <summary>
	/// The user's email.
	/// </summary>
	public string? Email { get; init; }

	/// <summary>
	/// The user's id.
	/// </summary>
	public UserId Id { get; init; }

	/// <summary>
	/// Flag indicating if the user's email has been validated.
	/// </summary>
	[JsonPropertyName("validated")]
	public bool IsEmailValidated { get; init; }

	/// <summary>
	/// The user's last activity timestamp.
	/// </summary>
	[JsonPropertyName("last_activity")]
	public DateTime? LastActivityAt { get; init; }

	/// <summary>
	/// The user's last trip id.
	/// </summary>
	[JsonPropertyName("last_trip")]
	public TripId? LastTripId { get; init; }

	/// <summary>
	/// The user's name.
	/// </summary>
	public string Name { get; init; } = null!;

	/// <summary>
	/// The user's phone.
	/// </summary>
	[JsonPropertyName("sms")]
	public long? Phone { get; init; }

	/// <summary>
	/// The user's photo URL.
	/// </summary>
	public string? PhotoUrl { get; init; }

	/// <summary>
	/// The user's role.
	/// </summary>
	public UserRole Role { get; init; }

	/// <summary>
	/// The user's status.
	/// </summary>
	[JsonPropertyName("active")]
	public UserStatus Status { get; init; }

	/// <summary>
	/// The user's current vehicle id.
	/// </summary>
	[JsonPropertyName("current_vehicle")]
	public VehicleId? VehicleId { get; init; }
}
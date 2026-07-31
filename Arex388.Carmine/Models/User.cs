namespace Arex388.Carmine;

/// <summary>
/// User object.
/// </summary>
public sealed class User {
	/// <summary>
	/// Flag indicating if the user can share their vehicle's ETA.
	/// </summary>
	public bool CanShareVehiclesEta { get; init; }

	/// <summary>
	/// The user's color.
	/// </summary>
	public string? Color { get; init; }

	/// <summary>
	/// The user's created timestamp in UTC.
	/// </summary>
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
	public bool IsEmailValidated { get; init; }

	/// <summary>
	/// The user's last activity timestamp.
	/// </summary>
	public DateTime? LastActivityAt { get; init; }

	/// <summary>
	/// The user's last trip id.
	/// </summary>
	public TripId? LastTripId { get; init; }

	/// <summary>
	/// The user's name.
	/// </summary>
	public string Name { get; init; } = null!;

	/// <summary>
	/// The user's phone.
	/// </summary>
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
	public UserStatus Status { get; init; }

	/// <summary>
	/// The user's current vehicle id.
	/// </summary>
	public VehicleId? VehicleId { get; init; }
}
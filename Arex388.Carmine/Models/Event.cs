namespace Arex388.Carmine;

/// <summary>
/// Event object.
/// </summary>
public sealed class Event {
	/// <summary>
	/// Additional data about the event.
	/// </summary>
	public string? Data { get; init; }

	/// <summary>
	/// The event's end timestamp.
	/// </summary>
	public DateTime EndAt { get; init; }

	/// <summary>
	/// The event's type.
	/// </summary>
	public EventType Type { get; init; }
	
	/// <summary>
	/// The event's start timestamp.
	/// </summary>
	public DateTime StartAt { get; init; }
}
using Arex388.Carmine.Converters;
using System.Text.Json.Serialization;

namespace Arex388.Carmine;

/// <summary>
/// Event object.
/// </summary>
public sealed class Event {
	/// <summary>
	/// Additional data about the event.
	/// </summary>
	[JsonPropertyName("event_data")]
	public string? Data { get; init; }

	/// <summary>
	/// The event's end timestamp.
	/// </summary>
	[JsonPropertyName("end_time")]
	public DateTime EndAt { get; init; }

	/// <summary>
	/// The event's type.
	/// </summary>
	[JsonConverter(typeof(EventTypeJsonConverter)), JsonPropertyName("event_type")]
	public EventType Type { get; init; }
	
	/// <summary>
	/// The event's start timestamp.
	/// </summary>
	[JsonPropertyName("start_time")]
	public DateTime StartAt { get; init; }
}
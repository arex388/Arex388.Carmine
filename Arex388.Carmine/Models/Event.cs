using Arex388.Carmine.Converters;
using System;
using System.Text.Json.Serialization;

#nullable enable

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
	/// The event's end timestamp in UTC.
	/// </summary>
	[JsonPropertyName("end_time")]
	public DateTime EndAtUtc { get; init; }

	/// <summary>
	/// The event's type.
	/// </summary>
	[JsonConverter(typeof(EventTypeJsonConverter)), JsonPropertyName("event_type")]
	public EventType Type { get; init; }

	/// <summary>
	/// The event's id.
	/// </summary>
	public EventId Id { get; init; }

	/// <summary>
	/// The event's start timestamp in UTC.
	/// </summary>
	[JsonPropertyName("start_time")]
	public DateTime StartAtUtc { get; init; }

	/// <summary>
	/// The event's trip id.
	/// </summary>
	public TripId TripId { get; init; }
}
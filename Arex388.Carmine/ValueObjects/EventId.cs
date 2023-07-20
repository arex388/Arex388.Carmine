using System;
using System.Text.Json;
using System.Text.Json.Serialization;

#pragma warning disable CS1591
#nullable enable

namespace Arex388.Carmine;

/// <summary>
/// Event id struct.
/// </summary>
[JsonConverter(typeof(EventIdSystemTextJsonConverter))]
public readonly struct EventId :
	IComparable<EventId>,
	IEquatable<EventId> {
	public Guid Value { get; }

	public EventId(
		Guid value) => Value = value;

	public int CompareTo(
		EventId other) => Value.CompareTo(other.Value);

	public static readonly EventId Empty = new(Guid.Empty);

	public bool Equals(
		EventId other) => Value.Equals(other.Value);

	public override bool Equals(
		object? obj) {
		if (obj is null) {
			return false;
		}

		return obj is EventId other && Equals(other);
	}

	public override int GetHashCode() => Value.GetHashCode();

	public static EventId New() => new(Guid.NewGuid());

	public override string ToString() => Value.ToString();

	public static bool operator ==(EventId a, EventId b) => a.Equals(b);
	public static bool operator !=(EventId a, EventId b) => !(a == b);

	public sealed class EventIdSystemTextJsonConverter :
		JsonConverter<EventId> {
		public override EventId Read(
			ref Utf8JsonReader reader,
			Type typeToConvert,
			JsonSerializerOptions options) => new(Guid.Parse(reader.GetString()!));

		public override void Write(
			Utf8JsonWriter writer,
			EventId value,
			JsonSerializerOptions options) => writer.WriteStringValue(value.Value);
	}
}
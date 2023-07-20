using System;
using System.Text.Json;
using System.Text.Json.Serialization;

#pragma warning disable CS1591
#nullable enable

namespace Arex388.Carmine;

/// <summary>
/// Waypoint id struct.
/// </summary>
[JsonConverter(typeof(WaypointIdSystemTextJsonConverter))]
public readonly partial struct WaypointId :
	IComparable<WaypointId>,
	IEquatable<WaypointId> {
	public Guid Value { get; }

	public WaypointId(
		Guid value) => Value = value;

	public int CompareTo(
		WaypointId other) => Value.CompareTo(other.Value);

	public static readonly WaypointId Empty = new(Guid.Empty);

	public bool Equals(
		WaypointId other) => Value.Equals(other.Value);

	public override bool Equals(
		object? obj) {
		if (obj is null) {
			return false;
		}

		return obj is WaypointId other && Equals(other);
	}

	public override int GetHashCode() => Value.GetHashCode();

	public static WaypointId New() => new(Guid.NewGuid());

	public override string ToString() => Value.ToString();

	public static bool operator ==(WaypointId a, WaypointId b) => a.Equals(b);
	public static bool operator !=(WaypointId a, WaypointId b) => !(a == b);

	public sealed class WaypointIdSystemTextJsonConverter :
		JsonConverter<WaypointId> {
		public override WaypointId Read(
			ref Utf8JsonReader reader,
			Type typeToConvert,
			JsonSerializerOptions options) => new(Guid.Parse(reader.GetString()!));

		public override void Write(
			Utf8JsonWriter writer,
			WaypointId value,
			JsonSerializerOptions options) => writer.WriteStringValue(value.Value);
	}
}
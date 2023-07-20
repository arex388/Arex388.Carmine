using System;
using System.Text.Json;
using System.Text.Json.Serialization;

#pragma warning disable CS1591
#nullable enable

namespace Arex388.Carmine;

/// <summary>
/// Location id struct.
/// </summary>
[JsonConverter(typeof(LocationIdSystemTextJsonConverter))]
public readonly struct LocationId :
	IComparable<LocationId>,
	IEquatable<LocationId> {
	public Guid Value { get; }

	public LocationId(
		Guid value) => Value = value;

	public int CompareTo(
		LocationId other) => Value.CompareTo(other.Value);

	public static readonly LocationId Empty = new(Guid.Empty);

	public bool Equals(
		LocationId other) => Value.Equals(other.Value);

	public override bool Equals(
		object? obj) {
		if (obj is null) {
			return false;
		}

		return obj is LocationId other && Equals(other);
	}

	public override int GetHashCode() => Value.GetHashCode();

	public static LocationId New() => new(Guid.NewGuid());

	public override string ToString() => Value.ToString();

	public static bool operator ==(LocationId a, LocationId b) => a.Equals(b);
	public static bool operator !=(LocationId a, LocationId b) => !(a == b);

	public sealed class LocationIdSystemTextJsonConverter :
		JsonConverter<LocationId> {
		public override LocationId Read(
			ref Utf8JsonReader reader,
			Type typeToConvert,
			JsonSerializerOptions options) => new(Guid.Parse(reader.GetString()!));

		public override void Write(
			Utf8JsonWriter writer,
			LocationId value,
			JsonSerializerOptions options) => writer.WriteStringValue(value.Value);
	}
}
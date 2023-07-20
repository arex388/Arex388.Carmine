using System;
using System.Text.Json;
using System.Text.Json.Serialization;

#pragma warning disable CS1591
#nullable enable

namespace Arex388.Carmine;

/// <summary>
/// Trip id struct.
/// </summary>
[JsonConverter(typeof(TripIdSystemTextJsonConverter))]
public readonly struct TripId : IComparable<TripId>, IEquatable<TripId> {
	public Guid Value { get; }

	public TripId(
		Guid value) => Value = value;

	public int CompareTo(
		TripId other) => Value.CompareTo(other.Value);

	public static readonly TripId Empty = new(Guid.Empty);

	public bool Equals(
		TripId other) => Value.Equals(other.Value);

	public override bool Equals(
		object? obj) {
		if (obj is null) {
			return false;
		}

		return obj is TripId other && Equals(other);
	}

	public override int GetHashCode() => Value.GetHashCode();

	public static TripId New() => new(Guid.NewGuid());

	public override string ToString() => Value.ToString();

	public static bool operator ==(TripId a, TripId b) => a.Equals(b);
	public static bool operator !=(TripId a, TripId b) => !(a == b);

	public sealed class TripIdSystemTextJsonConverter :
		JsonConverter<TripId> {
		public override TripId Read(
			ref Utf8JsonReader reader,
			Type typeToConvert,
			JsonSerializerOptions options) => new(Guid.Parse(reader.GetString()!));

		public override void Write(
			Utf8JsonWriter writer,
			TripId value,
			JsonSerializerOptions options) => writer.WriteStringValue(value.Value);
	}
}
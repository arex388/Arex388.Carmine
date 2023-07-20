using System;
using System.Text.Json;
using System.Text.Json.Serialization;

#pragma warning disable CS1591
#nullable enable

namespace Arex388.Carmine;

/// <summary>
/// Vehicle id struct.
/// </summary>
[JsonConverter(typeof(VehicleIdSystemTextJsonConverter))]
public readonly struct VehicleId :
	IComparable<VehicleId>,
	IEquatable<VehicleId> {
	public Guid Value { get; }

	public VehicleId(
		Guid value) => Value = value;

	public int CompareTo(
		VehicleId other) => Value.CompareTo(other.Value);

	public static readonly VehicleId Empty = new(Guid.Empty);

	public bool Equals(
		VehicleId other) => Value.Equals(other.Value);

	public override bool Equals(
		object? obj) {
		if (obj is null) {
			return false;
		}

		return obj is VehicleId other && Equals(other);
	}

	public override int GetHashCode() => Value.GetHashCode();

	public static VehicleId New() => new(Guid.NewGuid());

	public override string ToString() => Value.ToString();

	public static bool operator ==(VehicleId a, VehicleId b) => a.Equals(b);
	public static bool operator !=(VehicleId a, VehicleId b) => !(a == b);

	public sealed class VehicleIdSystemTextJsonConverter :
		JsonConverter<VehicleId> {
		public override VehicleId Read(
			ref Utf8JsonReader reader,
			Type typeToConvert,
			JsonSerializerOptions options) => new(Guid.Parse(reader.GetString()!));

		public override void Write(
			Utf8JsonWriter writer,
			VehicleId value,
			JsonSerializerOptions options) => writer.WriteStringValue(value.Value);
	}
}
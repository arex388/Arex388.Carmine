using System.Text.Json;
using System.Text.Json.Serialization;

#pragma warning disable CS1591

namespace Arex388.Carmine;

/// <summary>
/// User id struct.
/// </summary>
[JsonConverter(typeof(UserIdSystemTextJsonConverter))]
public readonly struct UserId(
	Guid value) :
	IComparable<UserId>,
	IEquatable<UserId> {
	public Guid Value { get; } = value;

	public int CompareTo(
		UserId other) => Value.CompareTo(other.Value);

	public static readonly UserId Empty = new(Guid.Empty);

	public bool Equals(
		UserId other) => Value.Equals(other.Value);

	public override bool Equals(
		object? obj) => obj is UserId other && Equals(other);

	public override int GetHashCode() => Value.GetHashCode();

	public static UserId New() => new(Guid.NewGuid());

	public override string ToString() => Value.ToString();

	public static bool operator ==(UserId a, UserId b) => a.Equals(b);
	public static bool operator !=(UserId a, UserId b) => !(a == b);

	internal sealed class UserIdSystemTextJsonConverter :
		JsonConverter<UserId> {
		public override UserId Read(
			ref Utf8JsonReader reader,
			Type typeToConvert,
			JsonSerializerOptions options) => new(Guid.Parse(reader.GetString()!));

		public override void Write(
			Utf8JsonWriter writer,
			UserId value,
			JsonSerializerOptions options) => writer.WriteStringValue(value.Value);
	}
}
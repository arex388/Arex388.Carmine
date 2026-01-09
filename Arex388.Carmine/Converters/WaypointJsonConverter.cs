using System.Text.Json;
using System.Text.Json.Serialization;

namespace Arex388.Carmine.Converters;

internal sealed class WaypointJsonConverter :
    JsonConverter<Waypoint> {
    // Property names
    private static ReadOnlySpan<byte> _altitude => "altitude"u8;
    private static ReadOnlySpan<byte> _distance => "distance"u8;
    private static ReadOnlySpan<byte> _latitude => "latitude"u8;
    private static ReadOnlySpan<byte> _longitude => "longitude"u8;
    private static ReadOnlySpan<byte> _rpm => "rpm"u8;
    private static ReadOnlySpan<byte> _speed => "speed"u8;
    private static ReadOnlySpan<byte> _time => "time"u8;

    public override Waypoint? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options) {
        if (reader.TokenType == JsonTokenType.Null) {
            return null;
        }

        if (reader.TokenType != JsonTokenType.StartObject) {
            throw new JsonException("Expected StartObject token");
        }

        DateTime atUtc = default;
        var distanceTraveledInMeters = 0;
        decimal elevationInMeters = 0;
        int? engineRpm = null;
        decimal latitude = 0;
        decimal longitude = 0;
        decimal speedInMetersPerSecond = 0;

        while (reader.Read()) {
            if (reader.TokenType == JsonTokenType.EndObject) {
                break;
            }

            if (reader.TokenType != JsonTokenType.PropertyName) {
                continue;
            }

            if (reader.ValueTextEquals(_time)) {
                reader.Read();

                atUtc = reader.GetDateTime();
            } else if (reader.ValueTextEquals(_altitude)) {
                reader.Read();

                elevationInMeters = reader.TokenType == JsonTokenType.String
                    ? decimal.Parse(reader.GetString()!)
                    : reader.GetDecimal();
            } else if (reader.ValueTextEquals(_distance)) {
                reader.Read();

                distanceTraveledInMeters = reader.TokenType == JsonTokenType.String
                    ? int.Parse(reader.GetString()!)
                    : reader.GetInt32();
            } else if (reader.ValueTextEquals(_latitude)) {
                reader.Read();

                latitude = reader.TokenType == JsonTokenType.String
                    ? decimal.Parse(reader.GetString()!)
                    : reader.GetDecimal();
            } else if (reader.ValueTextEquals(_longitude)) {
                reader.Read();

                longitude = reader.TokenType == JsonTokenType.String
                    ? decimal.Parse(reader.GetString()!)
                    : reader.GetDecimal();
            } else if (reader.ValueTextEquals(_rpm)) {
                reader.Read();

                if (reader.TokenType != JsonTokenType.Null) {
                    engineRpm = reader.TokenType == JsonTokenType.String
                        ? int.Parse(reader.GetString()!)
                        : reader.GetInt32();
                }
            } else if (reader.ValueTextEquals(_speed)) {
                reader.Read();

                speedInMetersPerSecond = reader.TokenType == JsonTokenType.String
                    ? decimal.Parse(reader.GetString()!)
                    : reader.GetDecimal();
            } else {
                JsonSerializer.Deserialize<JsonElement>(ref reader, options);
            }
        }

        return new Waypoint {
            AtUtc = atUtc,
            DistanceTraveledInMeters = distanceTraveledInMeters,
            ElevationInMeters = elevationInMeters,
            EngineRpm = engineRpm,
            Latitude = latitude,
            Longitude = longitude,
            SpeedInMetersPerSecond = speedInMetersPerSecond
        };
    }

    public override void Write(
        Utf8JsonWriter writer,
        Waypoint value,
        JsonSerializerOptions options) => throw new NotImplementedException();
}
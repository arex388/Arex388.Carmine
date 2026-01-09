using System.Text.Json;
using System.Text.Json.Serialization;

namespace Arex388.Carmine.Converters;

internal sealed class TripJsonConverter :
    JsonConverter<Trip> {
    // Property names
    private static ReadOnlySpan<byte> _distance => "distance"u8;
    private static ReadOnlySpan<byte> _endTime => "end_time"u8;
    private static ReadOnlySpan<byte> _id => "id"u8;
    private static ReadOnlySpan<byte> _isAfterHours => "is_after_hours"u8;
    private static ReadOnlySpan<byte> _isHidden => "is_hidden"u8;
    private static ReadOnlySpan<byte> _isPersonal => "is_personal"u8;
    private static ReadOnlySpan<byte> _isStationary => "is_stationary"u8;
    private static ReadOnlySpan<byte> _maxSpeed => "max_speed"u8;
    private static ReadOnlySpan<byte> _startTime => "start_time"u8;
    private static ReadOnlySpan<byte> _timeParked => "time_parked"u8;

    public override Trip? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options) {
        if (reader.TokenType == JsonTokenType.Null) {
            return null;
        }

        if (reader.TokenType != JsonTokenType.StartObject) {
            throw new JsonException("Expected StartObject token");
        }

        var distanceTraveledInMeters = 0;
        DateTime? endAt = null;
        TripId id = default;
        var isAfterHours = false;
        var isHidden = false;
        var isPersonal = false;
        var isStationary = false;
        decimal? maxSpeedInMetersPerSecond = null;
        var parkedSeconds = 0;
        DateTime startAt = default;

        while (reader.Read()) {
            if (reader.TokenType == JsonTokenType.EndObject) {
                break;
            }

            if (reader.TokenType != JsonTokenType.PropertyName) {
                continue;
            }

            if (reader.ValueTextEquals(_id)) {
                reader.Read();

                id = new TripId(reader.GetGuid());
            } else if (reader.ValueTextEquals(_distance)) {
                reader.Read();

                distanceTraveledInMeters = reader.TokenType == JsonTokenType.String
                    ? int.Parse(reader.GetString()!)
                    : reader.GetInt32();
            } else if (reader.ValueTextEquals(_endTime)) {
                reader.Read();

                if (reader.TokenType != JsonTokenType.Null) {
                    endAt = reader.GetDateTime();
                }
            } else if (reader.ValueTextEquals(_isAfterHours)) {
                reader.Read();

                isAfterHours = reader.GetBoolean();
            } else if (reader.ValueTextEquals(_isHidden)) {
                reader.Read();

                isHidden = reader.GetBoolean();
            } else if (reader.ValueTextEquals(_isPersonal)) {
                reader.Read();

                isPersonal = reader.GetBoolean();
            } else if (reader.ValueTextEquals(_isStationary)) {
                reader.Read();

                isStationary = reader.GetBoolean();
            } else if (reader.ValueTextEquals(_maxSpeed)) {
                reader.Read();

                if (reader.TokenType != JsonTokenType.Null) {
                    maxSpeedInMetersPerSecond = reader.TokenType == JsonTokenType.String
                        ? decimal.Parse(reader.GetString()!)
                        : reader.GetDecimal();
                }
            } else if (reader.ValueTextEquals(_startTime)) {
                reader.Read();

                startAt = reader.GetDateTime();
            } else if (reader.ValueTextEquals(_timeParked)) {
                reader.Read();

                parkedSeconds = reader.TokenType == JsonTokenType.String
                    ? int.Parse(reader.GetString()!)
                    : reader.GetInt32();
            } else {
                JsonSerializer.Deserialize<JsonElement>(ref reader, options);
            }
        }

        return new Trip {
            DistanceTraveledInMeters = distanceTraveledInMeters,
            EndAt = endAt,
            Id = id,
            IsAfterHours = isAfterHours,
            IsHidden = isHidden,
            IsPersonal = isPersonal,
            IsStationary = isStationary,
            MaxSpeedInMetersPerSecond = maxSpeedInMetersPerSecond,
            ParkedSeconds = parkedSeconds,
            StartAt = startAt
        };
    }

    public override void Write(
        Utf8JsonWriter writer,
        Trip value,
        JsonSerializerOptions options) => throw new NotImplementedException();
}
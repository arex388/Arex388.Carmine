using System.Text.Json;
using System.Text.Json.Serialization;

namespace Arex388.Carmine.Converters;

internal sealed class EventJsonConverter :
    JsonConverter<Event> {
    // Property names
    private static ReadOnlySpan<byte> _endTime => "end_time"u8;
    private static ReadOnlySpan<byte> _eventData => "event_data"u8;
    private static ReadOnlySpan<byte> _eventType => "event_type"u8;
    private static ReadOnlySpan<byte> _startTime => "start_time"u8;

    // Event type values
    private static ReadOnlySpan<byte> _afterHoursDriving => "after_hours_driving"u8;
    private static ReadOnlySpan<byte> _excessiveIdling => "excessive_idling"u8;
    private static ReadOnlySpan<byte> _extremeAcceleration => "extreme_acceleration"u8;
    private static ReadOnlySpan<byte> _extremeBraking => "extreme_braking"u8;
    private static ReadOnlySpan<byte> _harshAcceleration => "harsh_acceleration"u8;
    private static ReadOnlySpan<byte> _harshBreaking => "harsh_breaking"u8;
    private static ReadOnlySpan<byte> _harshCornering => "harsh_cornering"u8;
    private static ReadOnlySpan<byte> _highRpm => "high_rpm"u8;
    private static ReadOnlySpan<byte> _maxTripDuration => "max_trip_duration"u8;
    private static ReadOnlySpan<byte> _speeding => "speeding"u8;

    public override Event? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options) {
        if (reader.TokenType == JsonTokenType.Null) {
            return null;
        }

        if (reader.TokenType != JsonTokenType.StartObject) {
            throw new JsonException("Expected StartObject token");
        }

        string? data = null;
        DateTime endAt = default;
        DateTime startAt = default;
        var type = EventType.None;

        while (reader.Read()) {
            if (reader.TokenType == JsonTokenType.EndObject) {
                break;
            }

            if (reader.TokenType != JsonTokenType.PropertyName) {
                continue;
            }

            if (reader.ValueTextEquals(_eventData)) {
                reader.Read();
                data = reader.GetString();
            } else if (reader.ValueTextEquals(_endTime)) {
                reader.Read();
                endAt = reader.GetDateTime();
            } else if (reader.ValueTextEquals(_startTime)) {
                reader.Read();
                startAt = reader.GetDateTime();
            } else if (reader.ValueTextEquals(_eventType)) {
                reader.Read();
                type = ParseEventType(ref reader);
            } else {
                // Use JsonElement deserialization to skip unknown properties (streaming-compatible)
                JsonSerializer.Deserialize<JsonElement>(ref reader, options);
            }
        }

        return new Event {
            Data = data,
            EndAt = endAt,
            StartAt = startAt,
            Type = type
        };
    }

    public override void Write(
        Utf8JsonWriter writer,
        Event value,
        JsonSerializerOptions options) => throw new NotImplementedException();

    //  ============================================================================
    //  Utilities
    //  ============================================================================

    private static EventType ParseEventType(
        ref Utf8JsonReader reader) {
        if (reader.ValueTextEquals(_afterHoursDriving)) {
            return EventType.AfterHoursDriving;
        }

        if (reader.ValueTextEquals(_excessiveIdling)) {
            return EventType.ExcessiveIdling;
        }

        if (reader.ValueTextEquals(_extremeAcceleration)) {
            return EventType.ExtremeAcceleration;
        }

        if (reader.ValueTextEquals(_extremeBraking)) {
            return EventType.ExtremeBreaking;
        }

        if (reader.ValueTextEquals(_harshAcceleration)) {
            return EventType.HarshAcceleration;
        }

        if (reader.ValueTextEquals(_harshBreaking)) {
            return EventType.HarshBraking;
        }

        if (reader.ValueTextEquals(_harshCornering)) {
            return EventType.HarshCornering;
        }

        if (reader.ValueTextEquals(_highRpm)) {
            return EventType.HighRpm;
        }

        if (reader.ValueTextEquals(_maxTripDuration)) {
            return EventType.MaxTripDuration;
        }

        if (reader.ValueTextEquals(_speeding)) {
            return EventType.Speeding;
        }

        return EventType.None;
    }
}
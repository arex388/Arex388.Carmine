using System.Text.Json;
using System.Text.Json.Serialization;

namespace Arex388.Carmine.Converters;

internal sealed class EventTypeJsonConverter :
    JsonConverter<EventType> {
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

    public override EventType Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options) {
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

    public override void Write(
        Utf8JsonWriter writer,
        EventType value,
        JsonSerializerOptions options) => throw new NotImplementedException();
}
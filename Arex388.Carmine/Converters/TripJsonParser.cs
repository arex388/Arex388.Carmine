using System.Globalization;
using System.Text.Json;

namespace Arex388.Carmine.Converters;

/// <summary>
/// Shared base-Trip property parsing used by both TripJsonConverter and
/// TripExpandedJsonConverter so a new Trip field is added in exactly one place.
/// </summary>
internal static class TripJsonParser {
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

    internal struct State {
        public int DistanceTraveledInMeters;
        public DateTime? EndAt;
        public TripId Id;
        public bool IsAfterHours;
        public bool IsHidden;
        public bool IsPersonal;
        public bool IsStationary;
        public decimal? MaxSpeedInMetersPerSecond;
        public int ParkedSeconds;
        public DateTime StartAt;
    }

    /// <summary>
    /// If the reader is positioned on a base-Trip property name, consumes the
    /// property's value into <paramref name="state"/> and returns true;
    /// otherwise leaves the reader on the property name and returns false.
    /// </summary>
    public static bool TryReadProperty(
        ref Utf8JsonReader reader,
        ref State state) {
        if (reader.ValueTextEquals(_id)) {
            reader.Read();

            state.Id = new TripId(reader.GetGuid());
        } else if (reader.ValueTextEquals(_distance)) {
            reader.Read();

            state.DistanceTraveledInMeters = reader.TokenType == JsonTokenType.String
                ? int.Parse(reader.GetString()!, NumberStyles.Integer, CultureInfo.InvariantCulture)
                : reader.GetInt32();
        } else if (reader.ValueTextEquals(_endTime)) {
            reader.Read();

            if (reader.TokenType != JsonTokenType.Null) {
                state.EndAt = reader.GetDateTime();
            }
        } else if (reader.ValueTextEquals(_isAfterHours)) {
            reader.Read();

            state.IsAfterHours = reader.GetBoolean();
        } else if (reader.ValueTextEquals(_isHidden)) {
            reader.Read();

            state.IsHidden = reader.GetBoolean();
        } else if (reader.ValueTextEquals(_isPersonal)) {
            reader.Read();

            state.IsPersonal = reader.GetBoolean();
        } else if (reader.ValueTextEquals(_isStationary)) {
            reader.Read();

            state.IsStationary = reader.GetBoolean();
        } else if (reader.ValueTextEquals(_maxSpeed)) {
            reader.Read();

            if (reader.TokenType != JsonTokenType.Null) {
                state.MaxSpeedInMetersPerSecond = reader.TokenType == JsonTokenType.String
                    ? decimal.Parse(reader.GetString()!, NumberStyles.Number, CultureInfo.InvariantCulture)
                    : reader.GetDecimal();
            }
        } else if (reader.ValueTextEquals(_startTime)) {
            reader.Read();

            state.StartAt = reader.GetDateTime();
        } else if (reader.ValueTextEquals(_timeParked)) {
            reader.Read();

            state.ParkedSeconds = reader.TokenType == JsonTokenType.String
                ? int.Parse(reader.GetString()!, NumberStyles.Integer, CultureInfo.InvariantCulture)
                : reader.GetInt32();
        } else {
            return false;
        }

        return true;
    }
}

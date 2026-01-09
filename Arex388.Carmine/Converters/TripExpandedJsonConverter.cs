using System.Text.Json;
using System.Text.Json.Serialization;

namespace Arex388.Carmine.Converters;

internal sealed class TripExpandedJsonConverter :
    JsonConverter<TripExpanded> {
    // Nested converters (reused for parsing nested objects)
    private static readonly EventJsonConverter _eventConverter = new();
    private static readonly LocationJsonConverter _locationConverter = new();
    private static readonly UserJsonConverter _userConverter = new();
    private static readonly VehicleJsonConverter _vehicleConverter = new();
    private static readonly WaypointJsonConverter _waypointConverter = new();

    // Property names (base Trip properties)
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

    // Property names (TripExpanded properties)
    private static ReadOnlySpan<byte> _driver => "driver"u8;
    private static ReadOnlySpan<byte> _endLocation => "end_location"u8;
    private static ReadOnlySpan<byte> _events => "events"u8;
    private static ReadOnlySpan<byte> _startLocation => "start_location"u8;
    private static ReadOnlySpan<byte> _vehicle => "vehicle"u8;
    private static ReadOnlySpan<byte> _waypoints => "waypoints"u8;

    public override TripExpanded? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options) {
        if (reader.TokenType == JsonTokenType.Null) {
            return null;
        }

        if (reader.TokenType != JsonTokenType.StartObject) {
            throw new JsonException("Expected StartObject token");
        }

        // Base Trip properties
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

        // TripExpanded properties
        User? driver = null;
        Location? endLocation = null;
        IList<Event> events = [];
        Location startLocation = null!;
        Vehicle vehicle = null!;
        IList<Waypoint> waypoints = [];

        while (reader.Read()) {
            if (reader.TokenType == JsonTokenType.EndObject) {
                break;
            }

            if (reader.TokenType != JsonTokenType.PropertyName) {
                continue;
            }

            // Base Trip properties
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
            }
            // TripExpanded properties
            else if (reader.ValueTextEquals(_driver)) {
                reader.Read();

                driver = _userConverter.Read(ref reader, typeof(User), options);
            } else if (reader.ValueTextEquals(_endLocation)) {
                reader.Read();

                endLocation = _locationConverter.Read(ref reader, typeof(Location), options);
            } else if (reader.ValueTextEquals(_events)) {
                reader.Read();

                if (reader.TokenType == JsonTokenType.StartArray) {
                    var eventList = new List<Event>();

                    while (reader.Read() && reader.TokenType != JsonTokenType.EndArray) {
                        var evt = _eventConverter.Read(ref reader, typeof(Event), options);

                        if (evt is not null) {
                            eventList.Add(evt);
                        }
                    }

                    events = eventList;
                }
            } else if (reader.ValueTextEquals(_startLocation)) {
                reader.Read();

                startLocation = _locationConverter.Read(ref reader, typeof(Location), options)!;
            } else if (reader.ValueTextEquals(_vehicle)) {
                reader.Read();

                vehicle = _vehicleConverter.Read(ref reader, typeof(Vehicle), options)!;
            } else if (reader.ValueTextEquals(_waypoints)) {
                reader.Read();

                if (reader.TokenType != JsonTokenType.StartArray) {
                    continue;
                }

                var waypointList = new List<Waypoint>();

                while (reader.Read() && reader.TokenType != JsonTokenType.EndArray) {
                    var waypoint = _waypointConverter.Read(ref reader, typeof(Waypoint), options);

                    if (waypoint is not null) {
                        waypointList.Add(waypoint);
                    }
                }

                waypoints = waypointList;
            } else {
                JsonSerializer.Deserialize<JsonElement>(ref reader, options);
            }
        }

        return new TripExpanded {
            DistanceTraveledInMeters = distanceTraveledInMeters,
            Driver = driver,
            EndAt = endAt,
            EndLocation = endLocation,
            Events = events,
            Id = id,
            IsAfterHours = isAfterHours,
            IsHidden = isHidden,
            IsPersonal = isPersonal,
            IsStationary = isStationary,
            MaxSpeedInMetersPerSecond = maxSpeedInMetersPerSecond,
            ParkedSeconds = parkedSeconds,
            StartAt = startAt,
            StartLocation = startLocation,
            Vehicle = vehicle,
            Waypoints = waypoints
        };
    }

    public override void Write(
        Utf8JsonWriter writer,
        TripExpanded value,
        JsonSerializerOptions options) => throw new NotImplementedException();
}
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

    // Property names (TripExpanded properties; base Trip properties parse via TripJsonParser)
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
        var state = new TripJsonParser.State();

        // TripExpanded properties
        User? driver = null;
        Location? endLocation = null;
        //  Lists start null — the payload usually carries the arrays, and fresh
        //  instances here would be discarded; coalesced once at construction.
        List<Event>? events = null;
        Location startLocation = null!;
        Vehicle vehicle = null!;
        List<Waypoint>? waypoints = null;

        while (reader.Read()) {
            if (reader.TokenType == JsonTokenType.EndObject) {
                break;
            }

            if (reader.TokenType != JsonTokenType.PropertyName) {
                continue;
            }

            // Base Trip properties
            if (TripJsonParser.TryReadProperty(ref reader, ref state)) {
                continue;
            }

            // TripExpanded properties
            if (reader.ValueTextEquals(_driver)) {
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
                reader.Skip();
            }
        }

        return new TripExpanded {
            DistanceTraveledInMeters = state.DistanceTraveledInMeters,
            Driver = driver,
            EndAt = state.EndAt,
            EndLocation = endLocation,
            Events = events ?? [],
            Id = state.Id,
            IsAfterHours = state.IsAfterHours,
            IsHidden = state.IsHidden,
            IsPersonal = state.IsPersonal,
            IsStationary = state.IsStationary,
            MaxSpeedInMetersPerSecond = state.MaxSpeedInMetersPerSecond,
            ParkedSeconds = state.ParkedSeconds,
            StartAt = state.StartAt,
            StartLocation = startLocation,
            Vehicle = vehicle,
            Waypoints = waypoints ?? []
        };
    }

    public override void Write(
        Utf8JsonWriter writer,
        TripExpanded value,
        JsonSerializerOptions options) => throw new NotImplementedException();
}

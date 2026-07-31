using System.Text.Json;
using System.Text.Json.Serialization;

namespace Arex388.Carmine.Converters;

internal sealed class TripJsonConverter :
    JsonConverter<Trip> {
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

        var state = new TripJsonParser.State();

        while (reader.Read()) {
            if (reader.TokenType == JsonTokenType.EndObject) {
                break;
            }

            if (reader.TokenType != JsonTokenType.PropertyName) {
                continue;
            }

            if (!TripJsonParser.TryReadProperty(ref reader, ref state)) {
                reader.Skip();
            }
        }

        return new Trip {
            DistanceTraveledInMeters = state.DistanceTraveledInMeters,
            EndAt = state.EndAt,
            Id = state.Id,
            IsAfterHours = state.IsAfterHours,
            IsHidden = state.IsHidden,
            IsPersonal = state.IsPersonal,
            IsStationary = state.IsStationary,
            MaxSpeedInMetersPerSecond = state.MaxSpeedInMetersPerSecond,
            ParkedSeconds = state.ParkedSeconds,
            StartAt = state.StartAt
        };
    }

    public override void Write(
        Utf8JsonWriter writer,
        Trip value,
        JsonSerializerOptions options) => throw new NotImplementedException();
}

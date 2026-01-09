using System.Text.Json;
using System.Text.Json.Serialization;

namespace Arex388.Carmine.Converters;

internal sealed class LocationTypeJsonConverter :
    JsonConverter<LocationType> {
    private static ReadOnlySpan<byte> _cache => "cache"u8;
    private static ReadOnlySpan<byte> _geofence => "geofence"u8;
    private static ReadOnlySpan<byte> _googleCache => "google_cache"u8;
    private static ReadOnlySpan<byte> _pointOfInterest => "poi"u8;

    public override LocationType Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options) {
        if (reader.ValueTextEquals(_cache)) {
            return LocationType.Cache;
        }

        if (reader.ValueTextEquals(_geofence)) {
            return LocationType.Geofence;
        }

        if (reader.ValueTextEquals(_googleCache)) {
            return LocationType.GoogleCache;
        }

        if (reader.ValueTextEquals(_pointOfInterest)) {
            return LocationType.PointOfInterest;
        }

        return LocationType.None;
    }

    public override void Write(
        Utf8JsonWriter writer,
        LocationType value,
        JsonSerializerOptions options) => throw new NotImplementedException();
}
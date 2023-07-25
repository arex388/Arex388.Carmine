using System.Text.Json;
using System.Text.Json.Serialization;

namespace Arex388.Carmine.Converters;

internal sealed class LocationTypeJsonConverter :
	JsonConverter<LocationType> {
	public override LocationType Read(
		ref Utf8JsonReader reader,
		Type typeToConvert,
		JsonSerializerOptions options) => reader.GetString() switch {
			"cache" => LocationType.Cache,
			"geofence" => LocationType.Geofence,
			"google_cache" => LocationType.GoogleCache,
			"poi" => LocationType.PointOfInterest,
			_ => LocationType.None
		};

	public override void Write(
		Utf8JsonWriter writer,
		LocationType value,
		JsonSerializerOptions options) {
	}
}
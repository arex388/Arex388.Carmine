using System.Text.Json;
using System.Text.Json.Serialization;

namespace Arex388.Carmine.Converters;

internal sealed class EventTypeJsonConverter :
	JsonConverter<EventType> {
	public override EventType Read(
		ref Utf8JsonReader reader,
		Type typeToConvert,
		JsonSerializerOptions options) => reader.GetString() switch {
			"after_hours_driving" => EventType.AfterHoursDriving,
			"excessive_idling" => EventType.ExcessiveIdling,
			"extreme_acceleration" => EventType.ExtremeAcceleration,
			"extreme_braking" => EventType.ExtremeBreaking,
			"harsh_acceleration" => EventType.HarshAcceleration,
			"harsh_breaking" => EventType.HarshBraking,
			"harsh_cornering" => EventType.HarshCornering,
			"high_rpm" => EventType.HighRpm,
			"max_trip_duration" => EventType.MaxTripDuration,
			"speeding" => EventType.Speeding,
			_ => EventType.None
		};

	public override void Write(
		Utf8JsonWriter writer,
		EventType value,
		JsonSerializerOptions options) => throw new NotImplementedException();
}
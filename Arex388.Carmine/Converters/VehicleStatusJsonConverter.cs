using System.Text.Json;
using System.Text.Json.Serialization;

namespace Arex388.Carmine.Converters;

internal sealed class VehicleStatusJsonConverter :
	JsonConverter<VehicleStatus> {
	public override VehicleStatus Read(
		ref Utf8JsonReader reader,
		Type typeToConvert,
		JsonSerializerOptions options) => reader.GetString() switch {
			"active" => VehicleStatus.Active,
			"inactive" => VehicleStatus.Inactive,
			_ => VehicleStatus.None
		};

	public override void Write(
		Utf8JsonWriter writer,
		VehicleStatus value,
		JsonSerializerOptions options) {
	}
}
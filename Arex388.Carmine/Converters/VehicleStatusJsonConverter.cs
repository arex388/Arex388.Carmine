using System.Text.Json;
using System.Text.Json.Serialization;

namespace Arex388.Carmine.Converters;

internal sealed class VehicleStatusJsonConverter :
    JsonConverter<VehicleStatus> {
    private static ReadOnlySpan<byte> _active => "active"u8;
    private static ReadOnlySpan<byte> _inactive => "inactive"u8;

    public override VehicleStatus Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options) {
        if (reader.ValueTextEquals(_active)) {
            return VehicleStatus.Active;
        }

        if (reader.ValueTextEquals(_inactive)) {
            return VehicleStatus.Inactive;
        }

        return VehicleStatus.None;
    }

    public override void Write(
        Utf8JsonWriter writer,
        VehicleStatus value,
        JsonSerializerOptions options) => throw new NotImplementedException();
}
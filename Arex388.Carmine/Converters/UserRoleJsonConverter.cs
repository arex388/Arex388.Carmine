using System.Text.Json;
using System.Text.Json.Serialization;

namespace Arex388.Carmine.Converters;

internal sealed class UserRoleJsonConverter :
    JsonConverter<UserRole> {
    private static ReadOnlySpan<byte> _administrator => "company_admin"u8;
    private static ReadOnlySpan<byte> _driver => "driver"u8;

    public override UserRole Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options) {
        if (reader.ValueTextEquals(_administrator)) {
            return UserRole.Administrator;
        }

        if (reader.ValueTextEquals(_driver)) {
            return UserRole.Driver;
        }

        return UserRole.None;
    }

    public override void Write(
        Utf8JsonWriter writer,
        UserRole value,
        JsonSerializerOptions options) => throw new NotImplementedException();
}
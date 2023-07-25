using System.Text.Json;
using System.Text.Json.Serialization;

namespace Arex388.Carmine.Converters;

internal sealed class UserRoleJsonConverter :
	JsonConverter<UserRole> {
	public override UserRole Read(
		ref Utf8JsonReader reader,
		Type typeToConvert,
		JsonSerializerOptions options) => reader.GetString() switch {
			"company_admin" => UserRole.Administrator,
			"driver" => UserRole.Driver,
			_ => UserRole.None
		};

	public override void Write(
		Utf8JsonWriter writer,
		UserRole value,
		JsonSerializerOptions options) {
	}
}
using System;
using System.Text.Json;
using System.Text.Json.Serialization;

#nullable enable

namespace Arex388.Carmine.Converters;

internal sealed class UserStatusJsonConverter :
	JsonConverter<UserStatus> {
	public override UserStatus Read(
		ref Utf8JsonReader reader,
		Type typeToConvert,
		JsonSerializerOptions options) {
		var value = reader.GetBoolean();

		return value
			? UserStatus.Active
			: UserStatus.Inactive;
	}

	public override void Write(
		Utf8JsonWriter writer,
		UserStatus value,
		JsonSerializerOptions options) {
	}
}
using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Arex388.Carmine {
	public sealed class UserConverter :
		JsonConverter {
		public override bool CanConvert(
			Type objectType) => objectType == typeof(UserResponse);

		public override object ReadJson(
			JsonReader reader,
			Type objectType,
			object existingValue,
			JsonSerializer serializer) {
			var token = JToken.Load(reader);

			if (token.Type == JTokenType.Object) {
				return token.ToObject<UserResponse>();
			}

			return token.Value<string>();
		}

		public override void WriteJson(
			JsonWriter writer,
			object value,
			JsonSerializer serializer) => throw new NotImplementedException();
	}
}

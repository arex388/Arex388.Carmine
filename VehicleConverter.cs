using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Arex388.Carmine {
	public sealed class VehicleConverter :
		JsonConverter {
		public override bool CanConvert(
			Type objectType) => objectType == typeof(VehicleResponse);

		public override object ReadJson(
			JsonReader reader,
			Type objectType,
			object existingValue,
			JsonSerializer serializer) {
			var token = JToken.Load(reader);

			if (token.Type == JTokenType.Object) {
				return token.ToObject<VehicleResponse>();
			}

			return token.Value<string>();
		}

		public override void WriteJson(
			JsonWriter writer,
			object value,
			JsonSerializer serializer) => throw new NotImplementedException();
	}
}
using FluentAssertions;
using System.Reflection;
using System.Text.Json;

namespace Arex388.Carmine.Tests.Unit;

//	The models carry no serialization metadata — every model type deserializes
//	exclusively through a hand-written converter, and a converter file that is
//	added but never registered in CarmineClient's serializer options fails
//	silently. This guard makes that trap loud.
public sealed class ConverterRegistrationTests {
	[Theory]
	[InlineData(typeof(Event))]
	[InlineData(typeof(Location))]
	[InlineData(typeof(Trip))]
	[InlineData(typeof(TripExpanded))]
	[InlineData(typeof(User))]
	[InlineData(typeof(Vehicle))]
	[InlineData(typeof(Waypoint))]
	public void EveryModelType_HasARegisteredConverter(
		Type modelType) {
		var libraryAssembly = typeof(ICarmineClient).Assembly;
		var clientType = libraryAssembly.GetType("Arex388.Carmine.CarmineClient")!;
		var options = (JsonSerializerOptions)clientType.GetField("_jsonSerializerOptions", BindingFlags.NonPublic | BindingFlags.Static)!.GetValue(null)!;

		var converter = options.GetConverter(modelType);

		//	An unregistered type resolves to System.Text.Json's reflection-based
		//	fallback converter; a registered one resolves to the library's own.
		converter.GetType().Assembly.Should().BeSameAs(libraryAssembly, $"{modelType.Name} must have its hand-written converter registered in CarmineClient's serializer options");
	}
}

using FluentAssertions;
using System.Reflection;

namespace Arex388.Carmine.Tests.Unit;

//	Dutch is Language value 0, so a slice Request that forgets its
//	`= Language.English` initializer silently flips the API language for that
//	operation. This reflection sweep fences the trap for every current and
//	future Request type.
public sealed class RequestLanguageDefaultTests {
	[Fact]
	public void EveryRequestType_DefaultsLanguageToEnglish() {
		var requestTypes = typeof(ICarmineClient).Assembly.GetTypes().Where(
			t => t is {
				IsClass: true,
				IsAbstract: false
			} && typeof(RequestBase).IsAssignableFrom(t)).ToList();

		requestTypes.Should().NotBeEmpty("the sweep must actually find the slice Request types");

		foreach (var requestType in requestTypes) {
			var language = requestType.GetProperty("Language", BindingFlags.Public | BindingFlags.Instance);

			if (language is null) {
				continue;
			}

			var request = Activator.CreateInstance(requestType)!;

			language.GetValue(request).Should().Be(Language.English, $"{requestType.FullName} must default Language to English — Dutch is enum value 0 and a missing initializer silently flips the API language");
		}
	}
}

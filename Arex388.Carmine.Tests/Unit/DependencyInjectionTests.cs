using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace Arex388.Carmine.Tests.Unit;

public sealed class DependencyInjectionTests {
	[Fact]
	public void AddCarmine_WithOptions_ResolvesSingleClient() {
		var services = new ServiceCollection().AddCarmine(new CarmineClientOptions {
			Key = "unit-test-key"
		}).BuildServiceProvider();

		services.GetRequiredService<ICarmineClient>().Should().NotBeNull();
		services.GetServices<ICarmineClient>().Should().ContainSingle();
	}

	[Fact]
	public void AddCarmine_WithoutOptions_ResolvesFactory() {
		var services = new ServiceCollection().AddCarmine().BuildServiceProvider();

		services.GetRequiredService<ICarmineClientFactory>().Should().NotBeNull();
	}
}

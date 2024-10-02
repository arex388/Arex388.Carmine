using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace Arex388.Carmine.Tests;

public sealed class CarmineClientFactory {
	private readonly ICarmineClientFactory _carmineFactory;
	private readonly ITestOutputHelper _console;

	public CarmineClientFactory(
		ITestOutputHelper console) {
		var services = new ServiceCollection().AddCarmine().BuildServiceProvider();

		_carmineFactory = services.GetRequiredService<ICarmineClientFactory>();
		_console = console;
	}

	[Fact]
	public void CreateAndCacheClient() {
		//	========================================================================
		//	Arrange
		//	========================================================================

		//	========================================================================
		//	Act
		//	========================================================================

		var created = _carmineFactory.CreateClient(new CarmineClientOptions {
			Key = Config.Key1
		});
		var cached = _carmineFactory.CreateClient(new CarmineClientOptions {
			Key = Config.Key1
		});

		_console.WriteLineWithHeader(nameof(created), created);
		_console.WriteLineWithHeader(nameof(cached), cached);

		//	========================================================================
		//	Assert
		//	========================================================================

		created.Should().BeSameAs(cached);
	}

	[Fact]
	public void CreateClients() {
		//	========================================================================
		//	Arrange
		//	========================================================================

		//	========================================================================
		//	Act
		//	========================================================================

		var client1 = _carmineFactory.CreateClient(new CarmineClientOptions {
			Key = Config.Key1
		});
		var client2 = _carmineFactory.CreateClient(new CarmineClientOptions {
			Key = string.Empty
		});

		_console.WriteLineWithHeader(nameof(client1), client1);
		_console.WriteLineWithHeader(nameof(client2), client2);

		//	========================================================================
		//	Assert
		//	========================================================================

		client1.Should().NotBeSameAs(client2);
	}
}
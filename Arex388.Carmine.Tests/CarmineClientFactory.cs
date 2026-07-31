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
			Key = "factory-test-key"
		});
		var cached = _carmineFactory.CreateClient(new CarmineClientOptions {
			Key = "factory-test-key"
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
			Key = "factory-test-key"
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

	[Fact]
	public async Task CreateClient_IsAtomicUnderConcurrency() {
		//	========================================================================
		//	Arrange
		//	========================================================================

		var options = new CarmineClientOptions {
			Key = "factory-concurrency-key"
		};

		//	========================================================================
		//	Act
		//	========================================================================

		var clients = await Task.WhenAll(Enumerable.Range(0, 64).Select(
			_ => Task.Run(() => _carmineFactory.CreateClient(options))));

		//	========================================================================
		//	Assert
		//	========================================================================

		clients.Should().AllSatisfy(c => c.Should().BeSameAs(clients[0]));
	}
}
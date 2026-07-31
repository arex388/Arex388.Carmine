using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace Arex388.Carmine.Tests;

public sealed class CarmineClientFactoryTests {
	private readonly ICarmineClientFactory _carmineFactory;
	private readonly ITestOutputHelper _console;

	public CarmineClientFactoryTests(
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

		//	Synchronized start so the first touches genuinely race the miss path.
		var gate = new TaskCompletionSource();
		var tasks = Enumerable.Range(0, 128).Select(
			_ => Task.Run(async () => {
				await gate.Task;

				return _carmineFactory.CreateClient(options);
			})).ToArray();

		gate.SetResult();

		var clients = await Task.WhenAll(tasks);

		//	========================================================================
		//	Assert
		//	========================================================================

		clients.Should().AllSatisfy(c => c.Should().BeSameAs(clients[0]));
	}
}
using Microsoft.Extensions.Caching.Memory;
using Xunit;

namespace Arex388.Carmine.Tests;

public sealed class CarmineClientFactory {
	private readonly ICarmineClientFactory _carmineFactory = new Carmine.CarmineClientFactory(new HttpClient(), new MemoryCache(new MemoryCacheOptions()));

	[Fact]
	public void CreateAndCacheClient() {
		var created = _carmineFactory.CreateClient(Config.CarmineKey1);
		var cached = _carmineFactory.CreateClient(Config.CarmineKey1);

		Assert.Equal(created, cached);
	}

	[Fact]
	public void CreateClients() {
		var client1 = _carmineFactory.CreateClient(Config.CarmineKey1);
		var client2 = _carmineFactory.CreateClient(Config.CarmineKey2);
		var client3 = _carmineFactory.CreateClient(Config.CarmineKey3);

		Assert.NotNull(client1);
		Assert.NotNull(client2);
		Assert.NotNull(client3);
	}
}
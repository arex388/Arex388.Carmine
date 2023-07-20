using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace Arex388.Carmine.Tests;

public sealed class CarmineClientFactory {
	private readonly ICarmineClientFactory _carmineFactory;
	private readonly IConfigurationRoot _configuration;

	public CarmineClientFactory() {
		_carmineFactory = new Carmine.CarmineClientFactory(new HttpClient(), new MemoryCache(new MemoryCacheOptions()));
		_configuration = new ConfigurationBuilder().AddUserSecrets<CarmineClientFactory>().Build();
	}

	[Fact]
	public void CreateAndCacheClient() {
		var created = _carmineFactory.CreateClient(_configuration["CarmineKey-1"]!);
		var cached = _carmineFactory.CreateClient(_configuration["CarmineKey-1"]!);

		Assert.Equal(created, cached);
	}

	[Fact]
	public void CreateClients() {
		var client1 = _carmineFactory.CreateClient(_configuration["CarmineKey-1"]!);
		var client2 = _carmineFactory.CreateClient(_configuration["CarmineKey-2"]!);
		var client3 = _carmineFactory.CreateClient(_configuration["CarmineKey-3"]!);

		Assert.NotNull(client1);
		Assert.NotNull(client2);
		Assert.NotNull(client3);
	}
}
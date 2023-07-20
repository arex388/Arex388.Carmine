using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

namespace Arex388.Carmine.Benchmarks;

[MemoryDiagnoser]
public class CarmineClientFactory {
	private readonly ICarmineClientFactory _carmineFactory;
	private readonly IConfigurationRoot _configuration;

	public CarmineClientFactory() {
		_carmineFactory = new Carmine.CarmineClientFactory(new HttpClient(), new MemoryCache(new MemoryCacheOptions()));
		_configuration = new ConfigurationBuilder().AddUserSecrets<CarmineClientFactory>().Build();
	}

	[Benchmark]
	public void CreateAndCacheClient() {
		var client = _carmineFactory.CreateClient(_configuration["CarmineKey"]!);

		_ = client.ToString();
	}
}
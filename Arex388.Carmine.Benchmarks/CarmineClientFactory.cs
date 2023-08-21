using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.Caching.Memory;

namespace Arex388.Carmine.Benchmarks;

[MemoryDiagnoser]
public class CarmineClientFactory {
	private readonly ICarmineClientFactory _carmineFactory = new Carmine.CarmineClientFactory(new HttpClient(), new MemoryCache(new MemoryCacheOptions()));

	[Benchmark]
	public void CreateAndCacheClient() {
		var client = _carmineFactory.CreateClient(Config.CarmineKey);

		_ = client.ToString();
	}
}
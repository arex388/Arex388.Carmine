using Microsoft.Extensions.Caching.Memory;

namespace Arex388.Carmine;

/// <summary>
/// Carmine.io API client factory for applications integrating with more than one Carmine.io account.
/// </summary>
/// <remarks>
/// Create an instance of the Carmine.io client factory.
/// </remarks>
/// <param name="httpClient">An instance of <c>HttpClient</c>.</param>
/// <param name="memoryCache">An instance of <c>MemoryCache</c>.</param>
public sealed class CarmineClientFactory(
	HttpClient httpClient,
	IMemoryCache memoryCache) :
	ICarmineClientFactory {
	private static readonly MemoryCacheEntryOptions _memoryCacheEntryOptions = new() {
		SlidingExpiration = TimeSpan.MaxValue
	};

	private readonly HttpClient _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
	private readonly IMemoryCache _memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));

	/// <summary>
	/// Create and cache an instance of the Carmine.io API client.
	/// </summary>
	/// <param name="apiKey">Your Carmine.io API key. The value will be used as the cache identifier.</param>
	/// <returns>A new or cached instance of <c>CarmineClient</c>.</returns>
	public ICarmineClient CreateClient(
		string apiKey) {
		var key = $"{nameof(Arex388)}.{nameof(Carmine)}.Key[{apiKey}]";

		if (_memoryCache.TryGetValue(key, out ICarmineClient? carmineClient)
			&& carmineClient is not null) {
			return carmineClient;
		}

		carmineClient = new CarmineClient(apiKey, _httpClient);

		_memoryCache.Set(key, carmineClient, _memoryCacheEntryOptions);

		return carmineClient;
	}
}
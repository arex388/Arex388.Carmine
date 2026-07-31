using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;

namespace Arex388.Carmine;

internal sealed class CarmineClientFactory(
    IServiceProvider services,
    IMemoryCache cache) :
    ICarmineClientFactory {
    private static readonly MemoryCacheEntryOptions _memoryCacheEntryOptions = new() {
        SlidingExpiration = TimeSpan.FromHours(1),
        AbsoluteExpirationRelativeToNow = TimeSpan.FromHours(24),
    };

    private readonly IServiceProvider _services = services;
    private readonly IMemoryCache _cache = cache;

    /// <inheritdoc />
    public ICarmineClient CreateClient(
        CarmineClientOptions options) {
        var key = $"{nameof(Arex388)}.{nameof(Carmine)}.Key[{options.Key}]";

        //  Fast path: cache hits skip GetOrCreate's closure and entry-options work.
        if (_cache.TryGetValue<Lazy<ICarmineClient>>(key, out var cached)) {
            return cached!.Value;
        }

        //  GetOrCreate's value factory is not synchronized — caching a Lazy with
        //  ExecutionAndPublication guarantees one client per key under concurrency.
        return _cache.GetOrCreate(key, entry => {
            entry.SetOptions(_memoryCacheEntryOptions);

            return new Lazy<ICarmineClient>(() => {
                var httpClientFactory = _services.GetRequiredService<IHttpClientFactory>();
                var httpClient = httpClientFactory.CreateClient(nameof(ICarmineClient));

                return new CarmineClient(_services, httpClient, options);
            }, LazyThreadSafetyMode.ExecutionAndPublication);
        })!.Value;
    }
}
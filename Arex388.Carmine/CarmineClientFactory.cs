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

        return _cache.GetOrCreate(key, entry => {
            entry.SetOptions(_memoryCacheEntryOptions);

            var httpClientFactory = _services.GetRequiredService<IHttpClientFactory>();
            var httpClient = httpClientFactory.CreateClient(nameof(ICarmineClient));

            return new CarmineClient(_services, httpClient, options);
        })!;
    }
}
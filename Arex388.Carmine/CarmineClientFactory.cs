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

        if (_cache.TryGetValue(key, out ICarmineClient? carmineClient)
            && carmineClient is not null) {
            return carmineClient;
        }

        var httpClientFactory = _services.GetRequiredService<IHttpClientFactory>();
        var httpClient = httpClientFactory.CreateClient(nameof(ICarmineClient));

        carmineClient = new CarmineClient(_services, httpClient, options);

        _cache.Set(key, carmineClient, _memoryCacheEntryOptions);

        return carmineClient;
    }
}
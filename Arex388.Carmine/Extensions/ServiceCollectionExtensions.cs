using Arex388.Carmine;
using FluentValidation;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// <see cref="IServiceCollection"/> extensions.
/// </summary>
public static class ServiceCollectionExtensions {
    private static readonly Uri _baseAddress = new("https://api.carmine.io/v2/");

    /// <summary>
    /// Add the Carmine.io API client factory for interacting with multiple accounts.
    /// </summary>
    /// <param name="services">The services collection.</param>
    /// <returns>The services collection.</returns>
    public static IServiceCollection AddCarmine(
        this IServiceCollection services) {
        services.AddHttpClient(nameof(ICarmineClient), hc => {
            hc.BaseAddress = _baseAddress;
        });

        return services.AddMemoryCache()
                       .AddValidatorsFromAssemblyContaining<ICarmineClient>(includeInternalTypes: true, lifetime: ServiceLifetime.Singleton)
                       .AddSingleton<ICarmineClientFactory, CarmineClientFactory>();
    }

    /// <summary>
    /// Add the Carmine.io API client for interacting with a single account.
    /// </summary>
    /// <param name="services">The services collection.</param>
    /// <param name="options">The client's configuration options.</param>
    /// <returns>The services collection.</returns>
    public static IServiceCollection AddCarmine(
        this IServiceCollection services,
        CarmineClientOptions options) {
        services.AddHttpClient<ICarmineClient>(
            hc => hc.BaseAddress = _baseAddress);

        return services.AddValidatorsFromAssemblyContaining<ICarmineClient>(includeInternalTypes: true, lifetime: ServiceLifetime.Singleton)
                       .AddSingleton(options)
                       .AddSingleton<ICarmineClient>(
                           sp => new CarmineClient(sp));
    }
}
using Arex388.Carmine.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace Arex388.Carmine.Benchmarks;

/// <summary>
/// Creates a service provider configured for benchmarking with mocked HTTP responses.
/// </summary>
internal static class BenchmarkServiceProvider {
    public static IServiceProvider Create() {
        var services = new ServiceCollection();

        //  The unroutable base address is a fail-safe: if the mock handler wiring
        //  ever regresses, benchmarks fail instantly instead of draining live quota.
        services.AddCarmine()
                .AddHttpClient(nameof(ICarmineClient), hc => hc.BaseAddress = new Uri("https://localhost:9/v2/"))
                .ConfigurePrimaryHttpMessageHandler(() => new MockHttpMessageHandler());

        return services.BuildServiceProvider();
    }

    public static ICarmineClient CreateClient(
        IServiceProvider services) {
        var factory = services.GetRequiredService<ICarmineClientFactory>();

        return factory.CreateClient(new CarmineClientOptions {
            Key = "benchmark-mock-key"
        });
    }
}

using Microsoft.Extensions.DependencyInjection;

namespace Arex388.Carmine.Benchmarks;

/// <summary>
/// Creates a service provider configured for benchmarking with mocked HTTP responses.
/// </summary>
internal static class BenchmarkServiceProvider {
    public static IServiceProvider Create() {
        var services = new ServiceCollection();

        services.AddCarmine()
                .AddHttpClient(nameof(ICarmineClient))
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
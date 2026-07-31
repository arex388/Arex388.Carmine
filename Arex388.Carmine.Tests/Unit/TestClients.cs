using Arex388.Carmine.Testing;
using Microsoft.Extensions.DependencyInjection;
using System.Net;
using System.Text;

namespace Arex388.Carmine.Tests.Unit;

/// <summary>
/// Builds ICarmineClient instances backed by mock HTTP handlers. The named
/// client's base address is unroutable so a wiring regression fails fast
/// instead of hitting the live API.
/// </summary>
internal static class TestClients {
	public const string ApiKey = "unit-test-key";

	public static ICarmineClient Create(
		HttpMessageHandler handler) {
		var services = new ServiceCollection();

		services.AddCarmine(new CarmineClientOptions {
			Key = ApiKey
		}).AddHttpClient(nameof(ICarmineClient), hc => hc.BaseAddress = new Uri("https://localhost:9/v2/"))
		  .ConfigurePrimaryHttpMessageHandler(() => handler);

		return services.BuildServiceProvider().GetRequiredService<ICarmineClient>();
	}

	/// <summary>Serves the shared TestData/Responses fixtures by route.</summary>
	public static ICarmineClient CreateWithFixtures() => Create(new MockHttpMessageHandler());

	/// <summary>Serves the same JSON body for every request, recording each request URI.</summary>
	public static ICarmineClient CreateWithJson(
		string json,
		out CapturingHandler handler) {
		handler = new CapturingHandler(json);

		return Create(handler);
	}
}

internal sealed class CapturingHandler(
	string json,
	HttpStatusCode statusCode = HttpStatusCode.OK) :
	HttpMessageHandler {
	public IList<Uri> Requests { get; } = [];

	protected override Task<HttpResponseMessage> SendAsync(
		HttpRequestMessage request,
		CancellationToken cancellationToken) {
		Requests.Add(request.RequestUri!);

		return Task.FromResult(new HttpResponseMessage(statusCode) {
			Content = new StringContent(json, Encoding.UTF8, "application/json")
		});
	}
}

internal sealed class DelayingHandler :
	HttpMessageHandler {
	protected override async Task<HttpResponseMessage> SendAsync(
		HttpRequestMessage request,
		CancellationToken cancellationToken) {
		await Task.Delay(TimeSpan.FromSeconds(30), cancellationToken);

		return new HttpResponseMessage(HttpStatusCode.OK) {
			Content = new StringContent("[]", Encoding.UTF8, "application/json")
		};
	}
}

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

/// <summary>
/// Succeeds immediately with OK headers, then stalls the body until the read
/// is cancelled — a transfer that dies mid-download.
/// </summary>
internal sealed class DelayedBodyHandler :
	HttpMessageHandler {
	protected override Task<HttpResponseMessage> SendAsync(
		HttpRequestMessage request,
		CancellationToken cancellationToken) => Task.FromResult(new HttpResponseMessage(HttpStatusCode.OK) {
			Content = new DelayedContent()
		});

	private sealed class DelayedContent :
		HttpContent {
		protected override Task SerializeToStreamAsync(
			Stream stream,
			TransportContext? context) => SerializeToStreamAsync(stream, context, CancellationToken.None);

		protected override Task SerializeToStreamAsync(
			Stream stream,
			TransportContext? context,
			CancellationToken cancellationToken) => Task.Delay(TimeSpan.FromSeconds(30), cancellationToken);

		protected override bool TryComputeLength(
			out long length) {
			length = 0;

			return false;
		}
	}
}

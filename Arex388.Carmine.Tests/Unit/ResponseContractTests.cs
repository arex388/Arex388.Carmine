using FluentAssertions;
using System.Net;

namespace Arex388.Carmine.Tests.Unit;

public sealed class ResponseContractTests {
	[Fact]
	public async Task FailedResponses_AreDistinctInstances() {
		var carmine = TestClients.Create(new CapturingHandler("oops", HttpStatusCode.InternalServerError));

		var a = await carmine.ListTripsAsync();
		var b = await carmine.ListTripsAsync();

		a.Success.Should().BeFalse();
		b.Success.Should().BeFalse();
		a.Should().NotBeSameAs(b);

		//	Consumer mutation of one response must not corrupt later responses.
		a.Errors.Clear();

		var c = await carmine.ListTripsAsync();

		c.Success.Should().BeFalse();
	}

	[Theory]
	[InlineData(HttpStatusCode.Unauthorized, "HTTP 401 Unauthorized")]
	[InlineData(HttpStatusCode.InternalServerError, "HTTP 500 Internal Server Error")]
	public async Task FailedResponses_CarryStatusDetail(
		HttpStatusCode statusCode,
		string expectedDetail) {
		var carmine = TestClients.Create(new CapturingHandler("{}", statusCode));

		var response = await carmine.ListTripsAsync();

		response.Success.Should().BeFalse();
		response.Errors[0].Should().Be("The request has failed.", "the first entry must stay stable for existing consumer string-matches");
		response.Errors[1].Should().Be(expectedDetail);
	}

	[Fact]
	public async Task FailedResponses_CarryExceptionDetail() {
		var carmine = TestClients.CreateWithJson("{ broken", out _);

		var response = await carmine.ListTripsAsync();

		response.Success.Should().BeFalse();
		response.Errors[0].Should().Be("The request has failed.");
		response.Errors[1].Should().StartWith("JsonException: ");
	}

	[Fact]
	public async Task PreCancelledToken_ReturnsCancelled() {
		var carmine = TestClients.CreateWithFixtures();

		using var cts = new CancellationTokenSource();

		cts.Cancel();

		var response = await carmine.ListTripsAsync(cts.Token);

		response.Success.Should().BeFalse();
		response.Errors.Should().ContainSingle().Which.Should().Be("The request was cancelled.");
	}

	[Fact]
	public async Task MidFlightCancellation_ReturnsCancelled() {
		var carmine = TestClients.Create(new DelayingHandler());

		using var cts = new CancellationTokenSource(TimeSpan.FromMilliseconds(100));

		var response = await carmine.ListTripsAsync(cts.Token);

		response.Success.Should().BeFalse();
		response.Errors.Should().ContainSingle().Which.Should().Be("The request was cancelled.");
	}

	[Fact]
	public async Task InvalidRequest_ReturnsInvalid_WithoutHttpCall() {
		var carmine = TestClients.CreateWithJson("null", out var handler);

		var response = await carmine.GetTripAsync(default(TripId));

		response.Success.Should().BeFalse();
		response.Errors.Should().NotBeEmpty();
		handler.Requests.Should().BeEmpty("validation failures must not reach the API");
	}
}

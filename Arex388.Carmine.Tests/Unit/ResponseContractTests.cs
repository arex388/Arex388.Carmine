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

		//	Each failed response carries its own Errors instance — the surface is
		//	read-only, so shared state between responses would be unobservable but
		//	still wrong.
		a.Errors.Should().NotBeSameAs(b.Errors);
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

	[Theory]
	[InlineData(0)]
	[InlineData(-5)]
	public async Task ListTrips_NonPositiveTake_ReturnsInvalid_WithoutHttpCall(
		int take) {
		var carmine = TestClients.CreateWithJson("[]", out var handler);

		var response = await carmine.ListTripsAsync(new ListTrips.Request {
			Take = take
		});

		response.Success.Should().BeFalse();
		response.Errors.Should().NotBeEmpty();
		handler.Requests.Should().BeEmpty("validation failures must not reach the API");
	}

	[Theory]
	[InlineData(-60)]
	[InlineData(60)]
	public async Task ListRecentlyActiveVehicles_AcceptsEitherSign(
		int minutes) {
		var json = $$"""
			[{
				"id": "8b320390-0f25-466a-81f6-f757a5891f36",
				"created": "2023-09-19T13:19:08-07:00",
				"last_activity": "{{DateTime.UtcNow.AddMinutes(-5):yyyy-MM-ddTHH:mm:ssZ}}"
			}]
			""";

		var vehicles = await TestClients.CreateWithJson(json, out _).ListRecentlyActiveVehiclesAsync(minutes);

		vehicles.Should().ContainSingle("a vehicle active 5 minutes ago is within the last 60 minutes for either input sign");
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

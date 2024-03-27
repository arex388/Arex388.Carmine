using Xunit;

namespace Arex388.Carmine.Tests;

public sealed class Trips {
	private readonly ICarmineClient _carmine = new CarmineClient(Config.CarmineKey1, new HttpClient());

	[Fact]
	public async Task GetAsync() {
		var list = await _carmine.ListTripsAsync();
		var trip = list.Trips.First();

		var response = await _carmine.GetTripAsync(trip.Id);

		Assert.Equal(ResponseStatus.Succeeded, response.Status);
		Assert.NotNull(response.Trip);
	}

	[Fact]
	public async Task ListAsync() {
		var response = await _carmine.ListTripsAsync();

		Assert.Equal(ResponseStatus.Succeeded, response.Status);
		Assert.NotEmpty(response.Trips);
	}
}
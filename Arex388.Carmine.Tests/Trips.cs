using Microsoft.Extensions.Configuration;
using Xunit;

namespace Arex388.Carmine.Tests;

public sealed class Trips {
	private readonly ICarmineClient _carmine;

	public Trips() {
		var configuration = new ConfigurationBuilder().AddUserSecrets<Trips>().Build();

		_carmine = new CarmineClient(configuration["CarmineKey-1"]!, new HttpClient());
	}

	[Fact]
	public async Task GetAsync() {
		var list = await _carmine.ListTripsAsync().ConfigureAwait(false);
		var trip = list.Trips.First();

		var response = await _carmine.GetTripAsync(trip.Id).ConfigureAwait(false);

		Assert.Equal(ResponseStatus.Succeeded, response.Status);
		Assert.NotNull(response.Trip);
	}

	[Fact]
	public async Task ListAsync() {
		var response = await _carmine.ListTripsAsync().ConfigureAwait(false);

		Assert.Equal(ResponseStatus.Succeeded, response.Status);
		Assert.NotEmpty(response.Trips);
	}
}
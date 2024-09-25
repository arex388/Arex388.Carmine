using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace Arex388.Carmine.Benchmarks;

[MemoryDiagnoser]
public class TripsBenchmarks {
	private readonly ICarmineClient _carmine;

	public TripsBenchmarks() {
		var services = new ServiceCollection().AddCarmine(new CarmineClientOptions {
			Key = Config.Key
		}).BuildServiceProvider();

		_carmine = services.GetRequiredService<ICarmineClient>();
	}

	[Benchmark]
	public async Task<GetTrip.Response?> GetAsync() {
		var list = await _carmine.ListTripsAsync().ConfigureAwait(false);
		var trip = list.Trips.FirstOrDefault();

		if (trip is null) {
			return null;
		}

		return await _carmine.GetTripAsync(trip.Id).ConfigureAwait(false);
	}

	[Benchmark]
	public Task<ListTrips.Response> ListAsync() => _carmine.ListTripsAsync();
}
﻿using BenchmarkDotNet.Attributes;

namespace Arex388.Carmine.Benchmarks;

[MemoryDiagnoser]
public class Trips {
	private readonly ICarmineClient _carmine = new CarmineClient(Config.CarmineKey, new HttpClient());

	[Benchmark]
	public async Task GetAsync() {
		var list = await _carmine.ListTripsAsync().ConfigureAwait(false);
		var trip = list.Trips.FirstOrDefault();

		if (trip is null) {
			return;
		}

		_ = await _carmine.GetTripAsync(trip.Id).ConfigureAwait(false);
	}

	[Benchmark]
	public async Task ListAsync() {
		var response = await _carmine.ListTripsAsync().ConfigureAwait(false);

		_ = response.ToString();
	}
}
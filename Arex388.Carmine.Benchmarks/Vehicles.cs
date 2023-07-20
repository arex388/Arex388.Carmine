using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.Configuration;

namespace Arex388.Carmine.Benchmarks;

[MemoryDiagnoser]
public class Vehicles {
	private readonly ICarmineClient _carmine;

	public Vehicles() {
		var configuration = new ConfigurationBuilder().AddUserSecrets<Trips>().Build();

		_carmine = new CarmineClient(configuration["CarmineKey"]!, new HttpClient());
	}

	[Benchmark]
	public async Task GetAsync() {
		var list = await _carmine.ListVehiclesAsync().ConfigureAwait(false);
		var vehicle = list.Vehicles.FirstOrDefault();

		if (vehicle is null) {
			return;
		}

		_ = await _carmine.GetVehicleAsync(vehicle.Id).ConfigureAwait(false);
	}

	[Benchmark]
	public async Task ListAsync() {
		var response = await _carmine.ListVehiclesAsync().ConfigureAwait(false);

		_ = response.ToString();
	}
}
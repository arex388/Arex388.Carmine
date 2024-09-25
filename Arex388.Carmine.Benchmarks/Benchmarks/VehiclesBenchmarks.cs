using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace Arex388.Carmine.Benchmarks;

[MemoryDiagnoser]
public class VehiclesBenchmarks {
	private readonly ICarmineClient _carmine;

	public VehiclesBenchmarks() {
		var services = new ServiceCollection().AddCarmine(new CarmineClientOptions {
			Key = Config.Key
		}).BuildServiceProvider();

		_carmine = services.GetRequiredService<ICarmineClient>();
	}

	[Benchmark]
	public async Task<GetVehicle.Response?> GetAsync() {
		var list = await _carmine.ListVehiclesAsync().ConfigureAwait(false);
		var vehicle = list.Vehicles.FirstOrDefault();

		if (vehicle is null) {
			return null;
		}

		return await _carmine.GetVehicleAsync(vehicle.Id).ConfigureAwait(false);
	}

	[Benchmark]
	public Task<ListVehicles.Response> ListAsync() => _carmine.ListVehiclesAsync();
}
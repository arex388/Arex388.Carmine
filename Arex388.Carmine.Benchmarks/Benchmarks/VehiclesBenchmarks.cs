using BenchmarkDotNet.Attributes;

namespace Arex388.Carmine.Benchmarks;

[MemoryDiagnoser]
public class VehiclesBenchmarks {
    private readonly ICarmineClient _carmine;
    private readonly VehicleId _vehicleId;

    public VehiclesBenchmarks() {
        var services = BenchmarkServiceProvider.Create();

        _carmine = BenchmarkServiceProvider.CreateClient(services);

        // Get a vehicle ID from the cached list response for the Get benchmark
        var list = _carmine.ListVehiclesAsync().GetAwaiter().GetResult();

        _vehicleId = list.Vehicles.FirstOrDefault()?.Id ?? default;
    }

    [Benchmark]
    public Task<GetVehicle.Response> GetAsync() => _carmine.GetVehicleAsync(_vehicleId);

    [Benchmark]
    public Task<ListVehicles.Response> ListAsync() => _carmine.ListVehiclesAsync();
}
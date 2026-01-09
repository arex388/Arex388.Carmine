using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace Arex388.Carmine.Benchmarks;

[MemoryDiagnoser]
//[SimpleJob(RuntimeMoniker.Net80, id: ".NET 8")]
//[SimpleJob(RuntimeMoniker.Net90, id: ".NET 9")]
[SimpleJob(RuntimeMoniker.Net10_0, id: ".NET 10")]
public class VehiclesBenchmarks {
    private ICarmineClient _carmine = null!;
    private VehicleId _vehicleId;

    [GlobalSetup]
    public void Setup() {
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
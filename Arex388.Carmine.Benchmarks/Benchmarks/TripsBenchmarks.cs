using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace Arex388.Carmine.Benchmarks;

[MemoryDiagnoser]
//[SimpleJob(RuntimeMoniker.Net80, id: ".NET 8")]
//[SimpleJob(RuntimeMoniker.Net90, id: ".NET 9")]
[SimpleJob(RuntimeMoniker.Net10_0, id: ".NET 10")]
public class TripsBenchmarks {
    private ICarmineClient _carmine = null!;
    private TripId _tripId;

    [GlobalSetup]
    public void Setup() {
        var services = BenchmarkServiceProvider.Create();

        _carmine = BenchmarkServiceProvider.CreateClient(services);

        // Get a trip ID from the cached list response for the Get benchmark
        var list = _carmine.ListTripsAsync().GetAwaiter().GetResult();

        _tripId = list.Trips.FirstOrDefault()?.Id ?? default;
    }

    [Benchmark]
    public Task<GetTrip.Response> GetAsync() => _carmine.GetTripAsync(_tripId);

    [Benchmark]
    public Task<ListTrips.Response> ListAsync() => _carmine.ListTripsAsync();
}
using BenchmarkDotNet.Attributes;

namespace Arex388.Carmine.Benchmarks;

[MemoryDiagnoser]
public class TripsBenchmarks {
    private readonly ICarmineClient _carmine;
    private readonly TripId _tripId;

    public TripsBenchmarks() {
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
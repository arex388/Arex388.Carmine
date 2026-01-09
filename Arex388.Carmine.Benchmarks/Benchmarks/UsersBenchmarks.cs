using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Jobs;

namespace Arex388.Carmine.Benchmarks;

[MemoryDiagnoser]
//[SimpleJob(RuntimeMoniker.Net80, id: ".NET 8")]
//[SimpleJob(RuntimeMoniker.Net90, id: ".NET 9")]
[SimpleJob(RuntimeMoniker.Net10_0, id: ".NET 10")]
public class UsersBenchmarks {
    private ICarmineClient _carmine = null!;
    private UserId _userId;

    [GlobalSetup]
    public void Setup() {
        var services = BenchmarkServiceProvider.Create();

        _carmine = BenchmarkServiceProvider.CreateClient(services);

        // Get a user ID from the cached list response for the Get benchmark
        var list = _carmine.ListUsersAsync().GetAwaiter().GetResult();

        _userId = list.Users.FirstOrDefault()?.Id ?? default;
    }

    [Benchmark]
    public Task<GetUser.Response> GetAsync() => _carmine.GetUserAsync(_userId);

    [Benchmark]
    public Task<ListUsers.Response> ListAsync() => _carmine.ListUsersAsync();
}
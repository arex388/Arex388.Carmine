using BenchmarkDotNet.Attributes;

namespace Arex388.Carmine.Benchmarks;

[MemoryDiagnoser]
public class UsersBenchmarks {
    private readonly ICarmineClient _carmine;
    private readonly UserId _userId;

    public UsersBenchmarks() {
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
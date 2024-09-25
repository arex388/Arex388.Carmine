using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace Arex388.Carmine.Benchmarks;

[MemoryDiagnoser]
public class UsersBenchmarks {
	private readonly ICarmineClient _carmine;

	public UsersBenchmarks() {
		var services = new ServiceCollection().AddCarmine(new CarmineClientOptions {
			Key = Config.Key
		}).BuildServiceProvider();

		_carmine = services.GetRequiredService<ICarmineClient>();
	}

	[Benchmark]
	public async Task<GetUser.Response?> GetAsync() {
		var list = await _carmine.ListUsersAsync().ConfigureAwait(false);
		var user = list.Users.FirstOrDefault();

		if (user is null) {
			return null;
		}

		return await _carmine.GetUserAsync(user.Id).ConfigureAwait(false);
	}

	[Benchmark]
	public Task<ListUsers.Response> ListAsync() => _carmine.ListUsersAsync();
}
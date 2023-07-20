using BenchmarkDotNet.Attributes;
using Microsoft.Extensions.Configuration;

namespace Arex388.Carmine.Benchmarks;

[MemoryDiagnoser]
public class Users {
	private readonly ICarmineClient _carmine;

	public Users() {
		var configuration = new ConfigurationBuilder().AddUserSecrets<Trips>().Build();

		_carmine = new CarmineClient(configuration["CarmineKey"]!, new HttpClient());
	}

	[Benchmark]
	public async Task GetAsync() {
		var list = await _carmine.ListUsersAsync().ConfigureAwait(false);
		var user = list.Users.FirstOrDefault();

		if (user is null) {
			return;
		}

		_ = await _carmine.GetUserAsync(user.Id).ConfigureAwait(false);
	}

	[Benchmark]
	public async Task ListAsync() {
		var response = await _carmine.ListUsersAsync().ConfigureAwait(false);

		_ = response.ToString();
	}
}
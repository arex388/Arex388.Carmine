using Microsoft.Extensions.Configuration;

namespace Arex388.Carmine.Benchmarks;

internal sealed class Config {
	private static readonly IConfigurationRoot _configuration = new ConfigurationBuilder().AddUserSecrets<Config>().Build();

	public static string Key = _configuration["key"]!;
}
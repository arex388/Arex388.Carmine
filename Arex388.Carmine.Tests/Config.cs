using Microsoft.Extensions.Configuration;

namespace Arex388.Carmine.Tests;

internal sealed class Config {
	private static readonly IConfigurationRoot _configuration = new ConfigurationBuilder().AddUserSecrets<Config>().Build();

	public static string Key1 = _configuration["key-1"]!;
	public static string Key2 = _configuration["key-2"]!;
}
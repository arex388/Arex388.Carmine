using Microsoft.Extensions.Configuration;

namespace Arex388.Carmine.Tests;

internal sealed class Config {
	private static readonly IConfigurationRoot _configuration = new ConfigurationBuilder().AddUserSecrets<Config>().Build();

	public static string CarmineKey1 => _configuration["CarmineKey-1"]!;
	public static string CarmineKey2 => _configuration["CarmineKey-2"]!;
	public static string CarmineKey3 => _configuration["CarmineKey-3"]!;
}
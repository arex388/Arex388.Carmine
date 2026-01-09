<Query Kind="Program">
  <Reference Relative="Arex388.Carmine\bin\Debug\netstandard2.0\Arex388.Carmine.dll">E:\Software Development\Arex388.Carmine\Arex388.Carmine\bin\Debug\netstandard2.0\Arex388.Carmine.dll</Reference>
  <NuGetReference>Microsoft.Extensions.Http</NuGetReference>
  <Namespace>Arex388.Carmine</Namespace>
  <Namespace>Microsoft.Extensions.DependencyInjection</Namespace>
  <Namespace>System.Threading.Tasks</Namespace>
</Query>

private static readonly CarmineClientOptions _options = new CarmineClientOptions {
	Key = Util.GetPassword("carmine.key")
};

async Task Main() {
	GetClientMultiple();
	GetClientSingle();
}

public ICarmineClient GetClientMultiple() {
	var services = new ServiceCollection().AddCarmine().BuildServiceProvider();
	var carmineFactory = services.GetRequiredService<ICarmineClientFactory>();

	return carmineFactory.CreateClient(_options);
}

public ICarmineClient GetClientSingle() {
	var services = new ServiceCollection().AddCarmine(_options).BuildServiceProvider();

	return services.GetRequiredService<ICarmineClient>();
}

//	============================================================================
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//
//	EoF
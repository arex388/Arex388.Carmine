﻿using Arex388.Carmine;
using Microsoft.Extensions.Caching.Memory;

namespace Microsoft.Extensions.DependencyInjection;

/// <summary>
/// <c>IServiceCollection</c> extensions.
/// </summary>
public static class ServiceCollectionExtensions {
	/// <summary>
	/// Add Carmine.io service for interacting with multiple accounts.
	/// </summary>
	/// <param name="services">The service collection.</param>
	/// <returns>The service collection.</returns>
	public static IServiceCollection AddCarmine(
		this IServiceCollection services) => services.AddSingleton<ICarmineClientFactory>(
		sp => {
			var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
			var httpClient = httpClientFactory.CreateClient(nameof(CarmineClientFactory));
			var memoryCache = sp.GetRequiredService<IMemoryCache>();

			return new CarmineClientFactory(httpClient, memoryCache);
		});

	/// <summary>
	/// Add Carmine.io service for interacting with a single account.
	/// </summary>
	/// <param name="services">The service collection.</param>
	/// <param name="apiKey">The Carmine.io API key.</param>
	/// <returns>The service collection.</returns>
	public static IServiceCollection AddCarmine(
		this IServiceCollection services,
		string apiKey) => services.AddSingleton<ICarmineClient>(
		sp => {
			var httpClientFactory = sp.GetRequiredService<IHttpClientFactory>();
			var httpClient = httpClientFactory.CreateClient(nameof(CarmineClient));

			return new CarmineClient(apiKey, httpClient);
		});
}
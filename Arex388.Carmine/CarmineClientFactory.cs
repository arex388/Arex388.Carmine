﻿using Microsoft.Extensions.Caching.Memory;
using System;
using System.Net.Http;

#nullable enable

namespace Arex388.Carmine;

/// <summary>
/// Carmine.io API client factory for applications integrating with more than one Carmine.io account.
/// </summary>
public sealed class CarmineClientFactory :
	ICarmineClientFactory {
	private static readonly MemoryCacheEntryOptions _memoryCacheEntryOptions = new() {
		SlidingExpiration = TimeSpan.MaxValue
	};

	private readonly HttpClient _httpClient;
	private readonly IMemoryCache _memoryCache;

	/// <summary>
	/// Create an instance of the Carmine.io client factory.
	/// </summary>
	/// <param name="httpClient">An instance of HttpClient.</param>
	/// <param name="memoryCache">An instance of MemoryCache.</param>
	public CarmineClientFactory(
		HttpClient httpClient,
		IMemoryCache memoryCache) {
		_httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
		_memoryCache = memoryCache ?? throw new ArgumentNullException(nameof(memoryCache));
	}

	/// <summary>
	/// Create and cache an instance of the Carmine.io API client.
	/// </summary>
	/// <param name="apiKey">Your Carmine.io API key. The value will be used as the cache identifier.</param>
	/// <returns>A new or cached instance of CarmineClient.</returns>
	public ICarmineClient CreateClient(
		string apiKey) {
		var key = $"{nameof(Arex388)}.{nameof(Carmine)}.Key[{apiKey}]";

		if (_memoryCache.TryGetValue(key, out ICarmineClient? carmineClient)) {
			return carmineClient!;
		}

		carmineClient = new CarmineClient(apiKey, _httpClient);

		_memoryCache.Set(key, carmineClient, _memoryCacheEntryOptions);

		return carmineClient;
	}
}
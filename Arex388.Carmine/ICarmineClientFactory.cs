#nullable enable

namespace Arex388.Carmine;

/// <summary>
/// Carmine.io API client factory for applications integrating with more than one Carmine.io account.
/// </summary>
public interface ICarmineClientFactory {
	/// <summary>
	/// Create and cache an instance of the Carmine.io API client.
	/// </summary>
	/// <param name="apiKey">Your Carmine.io API key. The value will be used as the cache identifier.</param>
	/// <returns>A new or cached instance of CarmineClient.</returns>
	ICarmineClient CreateClient(
		string apiKey);
}
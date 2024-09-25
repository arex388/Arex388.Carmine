namespace Arex388.Carmine;

/// <summary>
/// Carmine.io API client factory for interacting with multiple accounts.
/// </summary>
public interface ICarmineClientFactory {
	/// <summary>
	/// Create and cache an instance of the Carmine.io API client.
	/// </summary>
	/// <param name="options">The client's configuration options.</param>
	/// <returns>An instance of the client.</returns>
	ICarmineClient CreateClient(
		CarmineClientOptions options);
}
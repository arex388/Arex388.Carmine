using System.Runtime.CompilerServices;

namespace Arex388.Carmine.Tests;

/// <summary>
/// A fact that runs against the live Carmine.io API and spends real quota.
/// Skipped unless the CARMINE_LIVE_TESTS environment variable is set to "1"
/// AND the key-1 user secret is present — a bare test run never hits the API.
/// </summary>
public sealed class LiveFactAttribute :
	FactAttribute {
	public LiveFactAttribute(
		[CallerFilePath] string? sourceFilePath = null,
		[CallerLineNumber] int sourceLineNumber = -1) :
		base(sourceFilePath, sourceLineNumber) {
		if (Environment.GetEnvironmentVariable("CARMINE_LIVE_TESTS") != "1") {
			Skip = "Live Carmine.io tests are opt-in. Set CARMINE_LIVE_TESTS=1 and configure the key-1/key-2 user secrets to run them.";
		} else if (string.IsNullOrEmpty(Config.Key1)) {
			Skip = "CARMINE_LIVE_TESTS is set but the key-1 user secret is missing.";
		}
	}
}

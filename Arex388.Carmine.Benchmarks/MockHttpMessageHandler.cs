using System.Net;
using System.Text;

namespace Arex388.Carmine.Benchmarks;

/// <summary>
/// Mock HTTP handler that returns cached JSON responses for benchmarking.
/// This allows benchmarking JSON deserialization without hitting the real API.
/// </summary>
internal sealed class MockHttpMessageHandler :
    HttpMessageHandler {
    private readonly string _responsesDir;
    private readonly Dictionary<string, string> _responseCache = new();

    public MockHttpMessageHandler() {
        _responsesDir = Path.Combine(AppContext.BaseDirectory, "Responses");

        if (!Directory.Exists(_responsesDir)) {
            throw new DirectoryNotFoundException($"Responses directory not found at: {_responsesDir}. Run ResponseCapture.CaptureAsync() first.");
        }

        LoadResponses();
    }

    private void LoadResponses() {
        foreach (var file in Directory.GetFiles(_responsesDir, "*.json")) {
            var fileName = Path.GetFileName(file);
            var content = File.ReadAllText(file);

            _responseCache[fileName] = content;
        }

        if (_responseCache.Count == 0) {
            throw new InvalidOperationException($"No JSON response files found in: {_responsesDir}. Run ResponseCapture.CaptureAsync() first.");
        }
    }

    protected override Task<HttpResponseMessage> SendAsync(
        HttpRequestMessage request,
        CancellationToken cancellationToken) {
        var path = request.RequestUri?.AbsolutePath ?? string.Empty;
        var fileName = GetResponseFileName(path);

        if (!_responseCache.TryGetValue(fileName, out var json)) {
            return Task.FromResult(new HttpResponseMessage(HttpStatusCode.NotFound) {
                Content = new StringContent($"No cached response for: {path}")
            });
        }

        var response = new HttpResponseMessage(HttpStatusCode.OK) {
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        };

        return Task.FromResult(response);
    }

    private static string GetResponseFileName(
        string path) {
        var cleanPath = path.Replace("/v2/", "").Split('?')[0];

        if (cleanPath.StartsWith("trips/")) {
            return "trip.json";
        }

        if (cleanPath == "trips") {
            return "trips.json";
        }

        if (cleanPath.StartsWith("users/")) {
            return "user.json";
        }

        if (cleanPath == "users") {
            return "users.json";
        }

        if (cleanPath.StartsWith("vehicles/")) {
            return "vehicle.json";
        }

        if (cleanPath == "vehicles") {
            return "vehicles.json";
        }

        return $"{cleanPath}.json";
    }
}
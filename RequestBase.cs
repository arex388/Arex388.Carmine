namespace Arex388.Carmine; 

/// <summary>
/// Request object base.
/// </summary>
public abstract class RequestBase {
    internal const string Ampersand = "&";
    internal const string Comma = ",";
    internal const string EndpointRoot = "https://api.carmine.io/v2";

    internal abstract string Endpoint { get; }
    internal virtual string Wrapper => "{0}";
}
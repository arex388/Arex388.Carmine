using System;

namespace Arex388.Carmine; 

/// <summary>
/// Get trip request object.
/// </summary>
public sealed class GetTripRequest :
    RequestBase {
    internal override string Endpoint => $"{EndpointRoot}/trips/{Id}?lang={Language.ToValue()}";

    /// <summary>
    /// The trip's id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The language of the results, English by default.
    /// </summary>
    public Language Language { get; set; } = Language.English;
}
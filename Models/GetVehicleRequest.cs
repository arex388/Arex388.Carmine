using System;

namespace Arex388.Carmine; 

/// <summary>
/// Get vehicle request object.
/// </summary>
public sealed class GetVehicleRequest :
    RequestBase {
    internal override string Endpoint => $"{EndpointRoot}/vehicles/{Id}?lang={Language.ToValue()}";

    /// <summary>
    /// The vehicle's id.
    /// </summary>
    public Guid Id { get; set; }

    /// <summary>
    /// The language of the results, English by default.
    /// </summary>
    public Language Language { get; set; } = Language.English;
}
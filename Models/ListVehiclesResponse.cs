using System.Collections.Generic;
using System.Linq;

namespace Arex388.Carmine; 

/// <summary>
/// List vehicles response object.
/// </summary>
public sealed class ListVehiclesResponse :
    IResponse {
    /// <summary>
    /// The response JSON if debugging is enabled.
    /// </summary>
    public string ResponseJson { get; set; }

    /// <summary>
    /// The response status.
    /// </summary>
    public ResponseStatus ResponseStatus { get; set; }

    /// <summary>
    /// List of vehicles.
    /// </summary>
    public IEnumerable<Vehicle> Vehicles { get; set; } = Enumerable.Empty<Vehicle>();
}
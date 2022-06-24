using System.Collections.Generic;
using System.Linq;

namespace Arex388.Carmine; 

/// <summary>
/// List trips response object.
/// </summary>
public sealed class ListTripsResponse :
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
    /// List of trips.
    /// </summary>
    public IEnumerable<Trip> Trips { get; set; } = Enumerable.Empty<Trip>();
}
using System.Collections.Generic;
using System.Linq;

namespace Arex388.Carmine; 

/// <summary>
/// List waypoints response object.
/// </summary>
public sealed class ListWaypointsResponse :
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
    /// List of waypoints.
    /// </summary>
    public IEnumerable<Waypoint> Waypoints { get; set; } = Enumerable.Empty<Waypoint>();
}
using System.Collections.Generic;
using System.Linq;

namespace Arex388.Carmine {
    /// <summary>
    /// Get trip response object.
    /// </summary>
    public sealed class GetTripResponse :
        TripBase,
        IResponse {
        /// <summary>
        /// The trip's events.
        /// </summary>
        public IEnumerable<Event> Events { get; set; } = Enumerable.Empty<Event>();

        /// <summary>
        /// The trip's driver.
        /// </summary>
        public GetUserResponse Driver { get; set; }

        /// <summary>
        /// The response JSON if debugging is enabled.
        /// </summary>
        public string ResponseJson { get; set; }

        /// <summary>
        /// The response status.
        /// </summary>
        public ResponseStatus ResponseStatus { get; set; }

        /// <summary>
        /// The trip's vehicle.
        /// </summary>
        public GetVehicleResponse Vehicle { get; set; }

        /// <summary>
        /// The trip's waypoints.
        /// </summary>
        public IEnumerable<Waypoint> Waypoints { get; set; } = Enumerable.Empty<Waypoint>();
    }
}
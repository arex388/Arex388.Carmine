using System;

namespace Arex388.Carmine {
    /// <summary>
    /// List waypoints request object.
    /// </summary>
    public sealed class ListWaypointsRequest :
        RequestBase {
        internal override string Endpoint => $"{EndpointRoot}/waypoints/?trip={TripId}&lang={Language.ToValue()}";
        internal override string Wrapper => "{{\"waypoints\":{0}}}";

        /// <summary>
        /// The trip's id.
        /// </summary>
        public Guid TripId { get; set; }

        /// <summary>
        /// The language of the results, English by default.
        /// </summary>
        public Language Language { get; set; } = Language.English;
    }
}
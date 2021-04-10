using Newtonsoft.Json;
using System;

namespace Arex388.Carmine {
    /// <summary>
    /// Trip object.
    /// </summary>
    public sealed class Trip :
        TripBase {
        /// <summary>
        /// The trip's driver id.
        /// </summary>
        [JsonProperty("driver")]
        public Guid? DriverId { get; set; }

        /// <summary>
        /// The trip's vehicle id.
        /// </summary>
        [JsonProperty("vehicle")]
        public Guid VehicleId { get; set; }
    }
}
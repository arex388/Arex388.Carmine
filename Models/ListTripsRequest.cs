using System;
using System.Collections.Generic;
using System.Linq;

namespace Arex388.Carmine {
    /// <summary>
    /// List trips request object.
    /// </summary>
    public sealed class ListTripsRequest :
        RequestBase {
        internal override string Endpoint => GetEndpoint();
        internal override string Wrapper => "{{\"trips\":{0}}}";

        /// <summary>
        /// Filter by drivers.
        /// </summary>
        public IEnumerable<Guid> DriverIds { get; set; } = Enumerable.Empty<Guid>();

        /// <summary>
        /// Filter by end timestamp in UTC.
        /// </summary>
        public DateTime? EndedAtUtc { get; set; }

        /// <summary>
        /// The results language, English by default.
        /// </summary>
        public Language Language { get; set; } = Language.English;

        /// <summary>
        /// Filter by start timestamp in UTC.
        /// </summary>
        public DateTime? StartedAtUtc { get; set; }

        /// <summary>
        /// Limit the results to this many.
        /// </summary>
        public int Take { get; set; } = 100;

        /// <summary>
        /// Filter by vehicles.
        /// </summary>
        public IEnumerable<Guid> VehicleIds { get; set; } = Enumerable.Empty<Guid>();

        //  ====================================================================
        //  Utilities
        //  ====================================================================

        private string GetEndpoint() {
            var parameters = new HashSet<string> {
                    $"lang={Language.ToValue()}",
                    $"per_page={Take}"
                };

            if (DriverIds.Any()) {
                var driverIds = DriverIds.Select(
                    _ => _.ToString()).StringJoin(Comma);

                parameters.Add($"driver={driverIds}");
            }

            if (EndedAtUtc.HasValue) {
                parameters.Add($"end_time={EndedAtUtc.Value:O}");
            }

            if (StartedAtUtc.HasValue) {
                parameters.Add($"start_time={StartedAtUtc.Value:O}");
            }

            if (VehicleIds.Any()) {
                var vehicleIds = VehicleIds.Select(
                    _ => _.ToString()).StringJoin(Comma);

                parameters.Add($"vehicle={vehicleIds}");
            }

            return $"{EndpointRoot}/trips?{parameters.StringJoin(Ampersand)}";
        }
    }
}
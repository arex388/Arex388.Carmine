using System;
using System.Collections.Generic;
using System.Net;

namespace Arex388.Carmine {
    /// <summary>
    /// List vehicles request object.
    /// </summary>
    public sealed class ListVehiclesRequest :
        RequestBase {
        internal override string Endpoint => GetEndpoint();
        internal override string Wrapper => "{{\"vehicles\":{0}}}";

        /// <summary>
        /// The language of the results, English by default.
        /// </summary>
        public Language Language { get; set; } = Language.English;

        /// <summary>
        /// Filter results.
        /// </summary>
        public string Search { get; set; }

        /// <summary>
        /// Filter results by status, All by default.
        /// </summary>
        public VehicleStatus Status { get; set; } = VehicleStatus.All;

        //  ====================================================================
        //  Utilities
        //  ====================================================================

        private string GetEndpoint() {
            var parameters = new HashSet<string> {
                $"lang={Language.ToValue()}"
            };

            if (Search.HasValue()) {
                var search = WebUtility.UrlEncode(Search);

                parameters.Add($"search={search}");
            }

            if (Status != VehicleStatus.All) {
                parameters.Add($"status={Status.ToValue()}");
            }

            return $"{EndpointRoot}/vehicles?{parameters.StringJoin(Ampersand)}";
        }
    }
}
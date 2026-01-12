using System.Text;

namespace Arex388.Carmine;

/// <summary>
/// ListTrips request and response containers.
/// </summary>
public static class ListTrips {
    /// <summary>
    /// ListTrips request container.
    /// </summary>
    public sealed class Request :
        RequestBase {
        internal static Request Default = new();

        /// <summary>
        /// The trip's driver id.
        /// </summary>
        public UserId? DriverId { get; init; }

        /// <summary>
        /// The trip's end timestamp in UTC.
        /// </summary>
        public DateTime? EndAtUtc { get; init; }

        internal override string Endpoint => GetEndpoint(this);

        /// <summary>
        /// The language the results should be returned in. <c>English</c> by default.
        /// </summary>
        public Language Language { get; init; } = Language.English;

        /// <summary>
        /// The trip's start timestamp in UTC.
        /// </summary>
        public DateTime? StartAtUtc { get; init; }

        /// <summary>
        /// The number of results to return. <c>100</c> by default.
        /// </summary>
        public int Take { get; init; } = 100;

        /// <summary>
        /// The trip's vehicle id.
        /// </summary>
        public VehicleId? VehicleId { get; init; }

        //	========================================================================
        //	Utilities
        //	========================================================================

        private static string GetEndpoint(
            Request request) {
            var sb = new StringBuilder("trips?");
            sb.Append("lang=").Append(request.Language.ToStringFast(true));
            sb.Append("&per_page=").Append(request.Take);

            if (request.DriverId.HasValue) {
                sb.Append("&driver[]=").Append(request.DriverId);
            }

            if (request.EndAtUtc.HasValue) {
                sb.Append("&end_time=").AppendFormat("{0:yyyy-MM-ddThh:mm:ss}", request.EndAtUtc);
            }

            if (request.StartAtUtc.HasValue) {
                sb.Append("&start_time=").AppendFormat("{0:yyyy-MM-ddThh:mm:ss}", request.StartAtUtc);
            }

            if (request.VehicleId.HasValue) {
                sb.Append("&vehicle[]=").Append(request.VehicleId);
            }

            return sb.ToString();
        }
    }

    /// <summary>
    /// ListTrips response container.
    /// </summary>
    public sealed class Response :
        ResponseBase<Response> {
        /// <summary>
        /// The matched trips.
        /// </summary>
        public IList<Trip> Trips { get; init; } = [];
    }
}
using System.Net;
using System.Text;

namespace Arex388.Carmine;

/// <summary>
/// ListVehicles request and response containers.
/// </summary>
public static class ListVehicles {
    /// <summary>
    /// ListVehicles request container.
    /// </summary>
    public sealed class Request :
        RequestBase {
        internal static readonly Request Default = new();

        internal override string Endpoint => GetEndpoint(this);

        /// <summary>
        /// The language the results should be returned in. <c>English</c> by default.
        /// </summary>
        public Language Language { get; init; } = Language.English;

        /// <summary>
        /// Filter by the vehicle's VIN, license plate number, year, make, or model.
        /// </summary>
        public string? Search { get; init; }

        /// <summary>
        /// Filter by the vehicle's status. <c>All</c> by default.
        /// </summary>
        public VehicleStatus? Status { get; init; }

        //	========================================================================
        //	Utilities
        //	========================================================================

        private static string GetEndpoint(
            Request request) {
            var sb = new StringBuilder("vehicles?");
            sb.Append("lang=").Append(request.Language.ToStringFast(true));

            if (request.Search.HasValue()) {
                var search = WebUtility.UrlEncode(request.Search);
                sb.Append("&search=").Append(search);
            }

            //  None is the unparsed-fallback member — treat it as "no filter",
            //  same as null (previously it leaked "&status=None" to the API).
            if (request.Status.HasValue
                && request.Status.Value != VehicleStatus.None) {
                sb.Append("&status=").Append(request.Status.Value.ToStringFast(true));
            }

            return sb.ToString();
        }
    }

    /// <summary>
    /// ListVehicles response container.
    /// </summary>
    public sealed class Response :
        ResponseBase<Response> {
        /// <summary>
        /// The matched vehicles.
        /// </summary>
        public IReadOnlyList<Vehicle> Vehicles { get; init; } = [];
    }
}
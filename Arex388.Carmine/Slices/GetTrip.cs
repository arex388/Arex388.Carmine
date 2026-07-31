using FluentValidation;
using static Arex388.Carmine.GetTrip;

namespace Arex388.Carmine;

/// <summary>
/// GetTrip request and response containers.
/// </summary>
public static class GetTrip {
    /// <summary>
    /// GetTrip request container.
    /// </summary>
    public sealed class Request :
        RequestBase {
        //  Explicit concatenation: on netstandard2.0 interpolation with a struct
        //  hole lowers to string.Format and boxes the id.
        internal override string Endpoint => "trips/" + Id.ToString() + "?lang=" + Language.ToStringFast(true);

        /// <summary>
        /// The trip's id.
        /// </summary>
        public TripId Id { get; init; }

        /// <summary>
        /// The language the results should be returned in. <c>English</c> by default.
        /// </summary>
        public Language Language { get; init; } = Language.English;
    }

    /// <summary>
    /// GetTrip response container.
    /// </summary>
    public sealed class Response :
        ResponseBase<Response> {
        /// <summary>
        /// The matched trip.
        /// </summary>
        public TripExpanded? Trip { get; init; }
    }
}

//	================================================================================
//	Validators
//	================================================================================

file sealed class RequestValidator :
    AbstractValidator<Request> {
    public RequestValidator() {
        RuleFor(r => r.Id).NotEmpty();
    }
}
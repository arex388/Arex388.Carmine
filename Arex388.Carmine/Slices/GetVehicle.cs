using FluentValidation;
using static Arex388.Carmine.GetVehicle;

namespace Arex388.Carmine;

/// <summary>
/// GetVehicle request and response containers.
/// </summary>
public static class GetVehicle {
    /// <summary>
    /// GetVehicle request container.
    /// </summary>
    public sealed class Request :
        RequestBase {
        internal override string Endpoint => $"vehicles/{Id}?lang={Language.ToStringFast(true)}";

        /// <summary>
        /// The vehicle's id.
        /// </summary>
        public VehicleId Id { get; init; }

        /// <summary>
        /// The language the results should be returned in. <c>English</c> by default.
        /// </summary>
        public Language Language { get; init; } = Language.English;
    }

    /// <summary>
    /// GetVehicle response container.
    /// </summary>
    public sealed class Response :
        ResponseBase<Response> {
        /// <summary>
        /// The matched vehicle.
        /// </summary>
        public Vehicle? Vehicle { get; init; }
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
using FluentValidation;
using static Arex388.Carmine.GetUser;

namespace Arex388.Carmine;

/// <summary>
/// GetUser request and response containers.
/// </summary>
public static class GetUser {
    /// <summary>
    /// GetUser request container.
    /// </summary>
    public sealed class Request :
        RequestBase {
        //  Explicit concatenation: on netstandard2.0 interpolation with a struct
        //  hole lowers to string.Format and boxes the id.
        internal override string Endpoint => "users/" + Id.ToString() + "?lang=" + Language.ToStringFast(true);

        /// <summary>
        /// The user's id.
        /// </summary>
        public UserId Id { get; init; }

        /// <summary>
        /// The language the results should be returned in. <c>English</c> by default.
        /// </summary>
        public Language Language { get; init; } = Language.English;
    }

    /// <summary>
    /// GetUser response container.
    /// </summary>
    public sealed class Response :
        ResponseBase<Response> {
        /// <summary>
        /// The matched user.
        /// </summary>
        public User? User { get; init; }
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
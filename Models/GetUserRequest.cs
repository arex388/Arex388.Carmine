using System;

namespace Arex388.Carmine {
    /// <summary>
    /// Get user request object.
    /// </summary>
    public sealed class GetUserRequest :
        RequestBase {
        internal override string Endpoint => $"{EndpointRoot}/users/{Id}?lang={Language.ToValue()}";

        /// <summary>
        /// The user's id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The language of the results, English by default.
        /// </summary>
        public Language Language { get; set; } = Language.English;
    }
}
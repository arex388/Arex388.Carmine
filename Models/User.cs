using Newtonsoft.Json;
using System;

namespace Arex388.Carmine {
    /// <summary>
    /// User object.
    /// </summary>
    public class User {
        /// <summary>
        /// Can the user share their vehicle's ETA.
        /// </summary>
        [JsonProperty("can_share_eta")]
        public bool CanShareEta { get; set; }

        /// <summary>
        /// The user's created timestamp in UTC.
        /// </summary>
        [JsonProperty("created")]
        public DateTime CreatedAtUtc { get; set; }

        /// <summary>
        /// The user's email address.
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// The user's id.
        /// </summary>
        public Guid Id { get; set; }

        [JsonProperty("active")]
        private bool IsActive { get; set; }

        /// <summary>
        /// Is the user's email validated.
        /// </summary>
        [JsonProperty("validated")]
        public bool IsEmailValidated { get; set; }

        /// <summary>
        /// The user's last activity timestamp in UTC.
        /// </summary>
        [JsonProperty("last_activity")]
        public DateTime? LastActivityAtUtc { get; set; }

        /// <summary>
        /// The user's last trip id.
        /// </summary>
        [JsonProperty("last_trip")]
        public Guid? LastTripId { get; set; }

        /// <summary>
        /// The user's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The user's phone number.
        /// </summary>
        [JsonProperty("sms")]
        public string Phone { get; set; }

        /// <summary>
        /// The user's photo URL.
        /// </summary>
        [JsonProperty("photo_url")]
        public string PhotoUrl { get; set; }

        [JsonProperty("role")]
        private string RoleInternal { get; set; }

        /// <summary>
        /// The user's role.
        /// </summary>
        public UserRole Role => GetRole();

        /// <summary>
        /// The user's current status.
        /// </summary>
        public UserStatus Status => GetStatus();

        /// <summary>
        /// The user's current vehicle id.
        /// </summary>
        [JsonProperty("current_vehicle")]
        public Guid? VehicleId { get; set; }

        //  ====================================================================
        //  Utilities
        //  ====================================================================

        internal const string RoleAdministrator = "company_admin";
        internal const string RoleDriver = "driver";

        private UserRole GetRole() => RoleInternal switch {
            RoleAdministrator => UserRole.Administrator,
            RoleDriver => UserRole.Driver,
            _ => throw new ArgumentException(nameof(RoleInternal))
        };

        private UserStatus GetStatus() =>
            IsActive
                ? UserStatus.Active
                : UserStatus.Inactive;
    }
}
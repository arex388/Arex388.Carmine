using System.ComponentModel.DataAnnotations;

namespace Arex388.Carmine {
    /// <summary>
    /// Vehicle status preference.
    /// </summary>
    public enum VehicleStatus :
        byte {
        /// <summary>
        /// Return results for only active vehicles.
        /// </summary>
        [Display(Name = "active")]
        Active,

        /// <summary>
        /// Return results for all vehicles.
        /// </summary>
        All,

        /// <summary>
        /// Returns results for only inactive vehicles.
        /// </summary>
        [Display(Name = "inactive")]
        Inactive
    }
}
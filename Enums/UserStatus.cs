using System.ComponentModel.DataAnnotations;

namespace Arex388.Carmine;

/// <summary>
/// User status preference.
/// </summary>
public enum UserStatus :
    byte {
    /// <summary>
    /// Returns results for only active users.
    /// </summary>
    [Display(Name = "true")]
    Active,

    /// <summary>
    /// Returns results for all users.
    /// </summary>
    All,

    /// <summary>
    /// Returns results for only inactive users.
    /// </summary>
    [Display(Name = "false")]
    Inactive
}
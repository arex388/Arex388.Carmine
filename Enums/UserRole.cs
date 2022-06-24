using System.ComponentModel.DataAnnotations;

namespace Arex388.Carmine;

/// <summary>
/// User role.
/// </summary>
public enum UserRole :
    byte {
    /// <summary>
    /// The user is an administrator.
    /// </summary>
    [Display(Name = User.RoleAdministrator)]
    Administrator,

    /// <summary>
    /// The user is a driver.
    /// </summary>
    [Display(Name = User.RoleDriver)]
    Driver
}
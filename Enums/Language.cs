using System.ComponentModel.DataAnnotations;

namespace Arex388.Carmine; 

/// <summary>
/// Results language preference.
/// </summary>
public enum Language :
    byte {
    /// <summary>
    /// Return results in Dutch.
    /// </summary>
    [Display(Name = "nl")]
    Dutch,

    /// <summary>
    /// Return results in English.
    /// </summary>
    [Display(Name = "en")]
    English,

    /// <summary>
    /// Return results in French.
    /// </summary>
    [Display(Name = "fr")]
    French,

    /// <summary>
    /// Return results in German.
    /// </summary>
    [Display(Name = "de")]
    German,

    /// <summary>
    /// Return results in Spanish.
    /// </summary>
    [Display(Name = "es")]
    Spanish
}
using NetEscapades.EnumGenerators;
using System.ComponentModel.DataAnnotations;

namespace Arex388.Carmine;

/// <summary>
/// The <c>Vehicle</c>'s status.
/// </summary>
[EnumExtensions]
public enum VehicleStatus :
	byte {
	/// <summary>
	/// The default status. If this is the value, then the response value wasn't parsed and this was used as a fallback.
	/// </summary>
	None,

	/// <summary>
	/// The vehicle is active.
	/// </summary>
	[Display(Name = "active")]
	Active,

	/// <summary>
	/// The vehicle is inactive.
	/// </summary>
	[Display(Name = "inactive")]
	Inactive
}
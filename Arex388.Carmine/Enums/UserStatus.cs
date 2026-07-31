using NetEscapades.EnumGenerators;

namespace Arex388.Carmine;

/// <summary>
/// The <c>User</c>'s status.
/// </summary>
[EnumExtensions]
public enum UserStatus :
	byte {
	/// <summary>
	/// The default status. If this is the value, then the response value wasn't parsed and this was used as a fallback.
	/// </summary>
	None,

	/// <summary>
	/// The user is active.
	/// </summary>
	Active,

	/// <summary>
	/// The user is inactive.
	/// </summary>
	Inactive
}
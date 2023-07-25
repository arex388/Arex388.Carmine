namespace Arex388.Carmine;

/// <summary>
/// The <c>User</c>'s role.
/// </summary>
public enum UserRole :
	byte {
	/// <summary>
	/// The default role. If this is the value, then the response value wasn't parsed and this was used as a fallback.
	/// </summary>
	None,

	/// <summary>
	/// The user is an administrator.
	/// </summary>
	Administrator,

	/// <summary>
	/// The user is a driver.
	/// </summary>
	Driver
}
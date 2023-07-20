using NetEscapades.EnumGenerators;

#nullable enable

namespace Arex388.Carmine;

/// <summary>
/// The <c>User</c>'s status.
/// </summary>
[EnumExtensions]
public enum UserStatus :
	byte {
	/// <summary>
	/// The user is active.
	/// </summary>
	Active,

	/// <summary>
	/// The user is inactive.
	/// </summary>
	Inactive
}
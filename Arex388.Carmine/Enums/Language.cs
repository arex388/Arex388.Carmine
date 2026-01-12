using NetEscapades.EnumGenerators;
using System.ComponentModel.DataAnnotations;

namespace Arex388.Carmine;

/// <summary>
/// The response language.
/// </summary>
[EnumExtensions(MetadataSource = MetadataSource.DisplayAttribute)]
public enum Language :
	byte {
	/// <summary>
	/// Dutch.
	/// </summary>
	[Display(Name = "nl")]
	Dutch,

	/// <summary>
	/// English (default).
	/// </summary>
	[Display(Name = "en")]
	English,

	/// <summary>
	/// French.
	/// </summary>
	[Display(Name = "fr")]
	French,

	/// <summary>
	/// German.
	/// </summary>
	[Display(Name = "de")]
	German,

	/// <summary>
	/// Spanish.
	/// </summary>
	[Display(Name = "es")]
	Spanish
}
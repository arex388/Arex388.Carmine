namespace Arex388.Carmine;

/// <summary>
/// The <c>Event</c>'s type.
/// </summary>
public enum EventType :
	byte {
	/// <summary>
	/// The default type. If this is the value, then the response value wasn't parsed and this was used as a fallback.
	/// </summary>
	None,

	/// <summary>
	/// The event was for after hours driving.
	/// </summary>
	AfterHoursDriving,

	/// <summary>
	/// The event was for excessive idling.
	/// </summary>
	ExcessiveIdling,

	/// <summary>
	/// The event was for extreme acceleration.
	/// </summary>
	ExtremeAcceleration,

	/// <summary>
	/// The event was for extreme breaking.
	/// </summary>
	ExtremeBreaking,

	/// <summary>
	/// The event was for harsh acceleration.
	/// </summary>
	HarshAcceleration,

	/// <summary>
	/// The event was for harsh braking.
	/// </summary>
	HarshBraking,

	/// <summary>
	/// The event was for harsh cornering.
	/// </summary>
	HarshCornering,

	/// <summary>
	/// The event was for high engine RPM.
	/// </summary>
	HighRpm,

	/// <summary>
	/// The event was for trip duration time limit reached.
	/// </summary>
	MaxTripDuration,

	/// <summary>
	/// The event was for speeding.
	/// </summary>
	Speeding
}
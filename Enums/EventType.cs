namespace Arex388.Carmine; 

/// <summary>
/// Event type.
/// </summary>
public enum EventType :
    byte {
    /// <summary>
    /// After hours driving.
    /// </summary>
    AfterHoursDriving,

    /// <summary>
    /// Excessive idling.
    /// </summary>
    ExcessiveIdling,

    /// <summary>
    /// Extreme acceleration.
    /// </summary>
    ExtremeAcceleration,

    /// <summary>
    /// Extreme braking.
    /// </summary>
    ExtremeBraking,

    /// <summary>
    /// Harsh acceleration.
    /// </summary>
    HarshAcceleration,

    /// <summary>
    /// Harsh braking.
    /// </summary>
    HarshBraking,

    /// <summary>
    /// Harsh cornering.
    /// </summary>
    HarshCornering,

    /// <summary>
    /// High RPM.
    /// </summary>
    HighRpm,

    /// <summary>
    /// Max trip duration.
    /// </summary>
    MaxTripDuration,

    /// <summary>
    /// Speeding.
    /// </summary>
    Speeding
}
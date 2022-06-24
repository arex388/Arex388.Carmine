namespace Arex388.Carmine; 

/// <summary>
/// Response object interface.
/// </summary>
public interface IResponse {
    /// <summary>
    /// The response JSON if debugging is enabled.
    /// </summary>
    string ResponseJson { get; set; }

    /// <summary>
    /// The response status.
    /// </summary>
    ResponseStatus ResponseStatus { get; set; }
}
namespace Arex388.Carmine; 

/// <summary>
/// Get user request object.
/// </summary>
public sealed class GetUserResponse :
    User,
    IResponse {
    /// <summary>
    /// The response JSON if debugging is enabled.
    /// </summary>
    public string ResponseJson { get; set; }

    /// <summary>
    /// The response status.
    /// </summary>
    public ResponseStatus ResponseStatus { get; set; }
}
using System.Collections.Generic;
using System.Linq;

namespace Arex388.Carmine; 

/// <summary>
/// List users response object.
/// </summary>
public sealed class ListUsersResponse :
    IResponse {
    /// <summary>
    /// The response JSON if debugging is enabled.
    /// </summary>
    public string ResponseJson { get; set; }

    /// <summary>
    /// The response status.
    /// </summary>
    public ResponseStatus ResponseStatus { get; set; }

    /// <summary>
    /// List of users.
    /// </summary>
    public IEnumerable<User> Users { get; set; } = Enumerable.Empty<User>();
}
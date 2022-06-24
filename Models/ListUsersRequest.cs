using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace Arex388.Carmine; 

/// <summary>
/// List users request object.
/// </summary>
public sealed class ListUsersRequest :
    RequestBase {
    internal override string Endpoint => GetEndpoint();
    internal override string Wrapper => "{{\"users\":{0}}}";

    /// <summary>
    /// The language of the results, English by default.
    /// </summary>
    public Language Language { get; set; } = Language.English;

    /// <summary>
    /// Filter results by roles.
    /// </summary>
    public IEnumerable<UserRole> Roles { get; set; } = Enumerable.Empty<UserRole>();

    /// <summary>
    /// Filter results.
    /// </summary>
    public string Search { get; set; }

    /// <summary>
    /// Filter results by status, All by default.
    /// </summary>
    public UserStatus Status { get; set; } = UserStatus.All;

    //  ====================================================================
    //  Utilities
    //  ====================================================================

    private string GetEndpoint() {
        var parameters = new HashSet<string> {
            $"lang={Language.ToValue()}"
        };

        if (Roles.Any()) {
            var roles = Roles.Select(
                _ => _.ToValue()).StringJoin(Comma);

            parameters.Add($"roles={roles}");
        }

        if (Search.HasValue()) {
            var search = WebUtility.UrlEncode(Search);

            parameters.Add($"search={search}");
        }

        if (Status != UserStatus.All) {
            parameters.Add($"status={Status.ToValue()}");
        }

        return $"{EndpointRoot}/users?{parameters.StringJoin(Ampersand)}";
    }
}
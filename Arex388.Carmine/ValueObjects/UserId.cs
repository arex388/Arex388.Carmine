using StronglyTypedIds;

#nullable enable

namespace Arex388.Carmine;

/// <summary>
/// User id struct.
/// </summary>
[StronglyTypedId(StronglyTypedIdBackingType.Guid, StronglyTypedIdConverter.SystemTextJson)]
public partial struct UserId {
}
using StronglyTypedIds;

#nullable enable

namespace Arex388.Carmine;

/// <summary>
/// Vehicle id struct.
/// </summary>
[StronglyTypedId(StronglyTypedIdBackingType.Guid, StronglyTypedIdConverter.SystemTextJson)]
public partial struct VehicleId {
}
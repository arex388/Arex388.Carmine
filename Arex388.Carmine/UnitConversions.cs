namespace Arex388.Carmine;

//	Single source of truth for the derived-property conversion factors used by
//	Trip, Vehicle, and Waypoint — full precision only, so a truncated factor
//	can't slip into one model while the others carry the exact value.
internal static class UnitConversions {
	public const decimal LitersPerGallon = 3.785M;
	public const decimal MetersPerKilometer = 1000M;
	public const decimal MetersPerLiterToKilometersPerLiter = .001M;
	public const decimal MetersPerLiterToMilesPerGallon = .0023521442146661M;
	public const decimal MetersPerMile = 1609.344M;
	public const decimal MetersPerSecondToKilometersPerHour = 3.6M;
	public const decimal MetersPerSecondToMilesPerHour = 2.2369362920544M;
}

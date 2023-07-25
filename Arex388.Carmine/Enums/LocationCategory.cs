namespace Arex388.Carmine;

/// <summary>
/// The <c>Location</c>'s category.
/// </summary>
public enum LocationCategory :
	byte {
	/// <summary>
	/// The default category. If this is the value, then the response value wasn't parsed and this was used as a fallback.
	/// </summary>
	None,

	/// <summary>
	/// The location is business related infrastructure.
	/// </summary>
	BusinessRelatedInfrastructure,

	/// <summary>
	/// The location is a customer.
	/// </summary>
	Customer,

	/// <summary>
	/// The location is a company asset.
	/// </summary>
	CompanyAsset,

	/// <summary>
	/// The location is a company office.
	/// </summary>
	CompanyOffice,

	/// <summary>
	/// The location is a fuel station.
	/// </summary>
	FuelStation,

	/// <summary>
	/// The location is unknown.
	/// </summary>
	Miscellaneous,

	/// <summary>
	/// The location is a parking lot.
	/// </summary>
	Parking,

	/// <summary>
	/// The location is a private address.
	/// </summary>
	PrivateAddress,

	/// <summary>
	/// The location is a restaurant.
	/// </summary>
	Restaurant,

	/// <summary>
	/// The location is a service area.
	/// </summary>
	ServiceArea,

	/// <summary>
	/// The location is a warehouse.
	/// </summary>
	Warehouse,

	/// <summary>
	/// The location is a wholesale or general supplier.
	/// </summary>
	WholesaleOrSupplies
}
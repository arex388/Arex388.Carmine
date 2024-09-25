using System.Text.Json;
using System.Text.Json.Serialization;

namespace Arex388.Carmine.Converters;

internal sealed class LocationCategoryJsonConverter :
	JsonConverter<LocationCategory> {
	public override LocationCategory Read(
		ref Utf8JsonReader reader,
		Type typeToConvert,
		JsonSerializerOptions options) => reader.GetString() switch {
			"business_related_infrastructure" => LocationCategory.BusinessRelatedInfrastructure,
			"customer" => LocationCategory.Customer,
			"company_asset" => LocationCategory.CompanyAsset,
			"company_office" => LocationCategory.CompanyOffice,
			"fuel_station" => LocationCategory.FuelStation,
			"miscellaneous" => LocationCategory.Miscellaneous,
			"parking" => LocationCategory.Parking,
			"private_address" => LocationCategory.PrivateAddress,
			"restaurant" => LocationCategory.Restaurant,
			"service_area" => LocationCategory.ServiceArea,
			"warehouse" => LocationCategory.Warehouse,
			"wholesale_or_supplies" => LocationCategory.WholesaleOrSupplies,
			_ => LocationCategory.None
		};

	public override void Write(
		Utf8JsonWriter writer,
		LocationCategory value,
		JsonSerializerOptions options) => throw new NotImplementedException();
}
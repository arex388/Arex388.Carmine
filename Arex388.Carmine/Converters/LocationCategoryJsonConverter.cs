using System.Text.Json;
using System.Text.Json.Serialization;

namespace Arex388.Carmine.Converters;

internal sealed class LocationCategoryJsonConverter :
    JsonConverter<LocationCategory> {
    private static ReadOnlySpan<byte> _businessRelatedInfrastructure => "business_related_infrastructure"u8;
    private static ReadOnlySpan<byte> _customer => "customer"u8;
    private static ReadOnlySpan<byte> _companyAsset => "company_asset"u8;
    private static ReadOnlySpan<byte> _companyOffice => "company_office"u8;
    private static ReadOnlySpan<byte> _fuelStation => "fuel_station"u8;
    private static ReadOnlySpan<byte> _miscellaneous => "miscellaneous"u8;
    private static ReadOnlySpan<byte> _parking => "parking"u8;
    private static ReadOnlySpan<byte> _privateAddress => "private_address"u8;
    private static ReadOnlySpan<byte> _restaurant => "restaurant"u8;
    private static ReadOnlySpan<byte> _serviceArea => "service_area"u8;
    private static ReadOnlySpan<byte> _warehouse => "warehouse"u8;
    private static ReadOnlySpan<byte> _wholesaleOrSupplies => "wholesale_or_supplies"u8;

    public override LocationCategory Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options) {
        if (reader.ValueTextEquals(_businessRelatedInfrastructure)) {
            return LocationCategory.BusinessRelatedInfrastructure;
        }

        if (reader.ValueTextEquals(_customer)) {
            return LocationCategory.Customer;
        }

        if (reader.ValueTextEquals(_companyAsset)) {
            return LocationCategory.CompanyAsset;
        }

        if (reader.ValueTextEquals(_companyOffice)) {
            return LocationCategory.CompanyOffice;
        }

        if (reader.ValueTextEquals(_fuelStation)) {
            return LocationCategory.FuelStation;
        }

        if (reader.ValueTextEquals(_miscellaneous)) {
            return LocationCategory.Miscellaneous;
        }

        if (reader.ValueTextEquals(_parking)) {
            return LocationCategory.Parking;
        }

        if (reader.ValueTextEquals(_privateAddress)) {
            return LocationCategory.PrivateAddress;
        }

        if (reader.ValueTextEquals(_restaurant)) {
            return LocationCategory.Restaurant;
        }

        if (reader.ValueTextEquals(_serviceArea)) {
            return LocationCategory.ServiceArea;
        }

        if (reader.ValueTextEquals(_warehouse)) {
            return LocationCategory.Warehouse;
        }

        if (reader.ValueTextEquals(_wholesaleOrSupplies)) {
            return LocationCategory.WholesaleOrSupplies;
        }

        return LocationCategory.None;
    }

    public override void Write(
        Utf8JsonWriter writer,
        LocationCategory value,
        JsonSerializerOptions options) => throw new NotImplementedException();
}
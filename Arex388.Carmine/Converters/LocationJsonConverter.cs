using System.Text.Json;
using System.Text.Json.Serialization;

namespace Arex388.Carmine.Converters;

internal sealed class LocationJsonConverter :
    JsonConverter<Location> {
    // Property names
    private static ReadOnlySpan<byte> _address => "address"u8;
    private static ReadOnlySpan<byte> _category => "category"u8;
    private static ReadOnlySpan<byte> _created => "created"u8;
    private static ReadOnlySpan<byte> _createdById => "created_by_id"u8;
    private static ReadOnlySpan<byte> _driverId => "driver_id"u8;
    private static ReadOnlySpan<byte> _geometry => "geometry"u8;
    private static ReadOnlySpan<byte> _lastActivityTime => "last_activity_time"u8;
    private static ReadOnlySpan<byte> _latitude => "latitude"u8;
    private static ReadOnlySpan<byte> _longitude => "longitude"u8;
    private static ReadOnlySpan<byte> _name => "name"u8;
    private static ReadOnlySpan<byte> _notes => "notes"u8;
    private static ReadOnlySpan<byte> _popularity => "popularity"u8;
    private static ReadOnlySpan<byte> _type => "type"u8;

    // Location category values
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

    // Location type values
    private static ReadOnlySpan<byte> _cache => "cache"u8;
    private static ReadOnlySpan<byte> _geofence => "geofence"u8;
    private static ReadOnlySpan<byte> _googleCache => "google_cache"u8;
    private static ReadOnlySpan<byte> _poi => "poi"u8;

    public override Location? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options) {
        if (reader.TokenType == JsonTokenType.Null) {
            return null;
        }

        if (reader.TokenType != JsonTokenType.StartObject) {
            throw new JsonException("Expected StartObject token");
        }

        string address = null!;
        var category = LocationCategory.None;
        DateTime createdAtUtc = default;
        UserId? createdById = null;
        UserId? driverId = null;
        string geometry = null!;
        DateTime? lastActivityAt = null;
        decimal latitude = 0;
        decimal longitude = 0;
        string? name = null;
        string? notes = null;
        var type = LocationType.None;
        var visitedCount = 0;

        while (reader.Read()) {
            if (reader.TokenType == JsonTokenType.EndObject) {
                break;
            }

            if (reader.TokenType != JsonTokenType.PropertyName) {
                continue;
            }

            if (reader.ValueTextEquals(_address)) {
                reader.Read();

                address = reader.GetString()!;
            } else if (reader.ValueTextEquals(_category)) {
                reader.Read();

                category = ParseCategory(ref reader);
            } else if (reader.ValueTextEquals(_created)) {
                reader.Read();

                createdAtUtc = reader.GetDateTime();
            } else if (reader.ValueTextEquals(_createdById)) {
                reader.Read();

                if (reader.TokenType != JsonTokenType.Null) {
                    createdById = new UserId(reader.GetGuid());
                }
            } else if (reader.ValueTextEquals(_driverId)) {
                reader.Read();

                if (reader.TokenType != JsonTokenType.Null) {
                    driverId = new UserId(reader.GetGuid());
                }
            } else if (reader.ValueTextEquals(_geometry)) {
                reader.Read();

                geometry = reader.GetString()!;
            } else if (reader.ValueTextEquals(_lastActivityTime)) {
                reader.Read();

                if (reader.TokenType != JsonTokenType.Null) {
                    lastActivityAt = reader.GetDateTime();
                }
            } else if (reader.ValueTextEquals(_latitude)) {
                reader.Read();

                latitude = reader.TokenType == JsonTokenType.String
                    ? decimal.Parse(reader.GetString()!)
                    : reader.GetDecimal();
            } else if (reader.ValueTextEquals(_longitude)) {
                reader.Read();

                longitude = reader.TokenType == JsonTokenType.String
                    ? decimal.Parse(reader.GetString()!)
                    : reader.GetDecimal();
            } else if (reader.ValueTextEquals(_name)) {
                reader.Read();

                name = reader.GetString();
            } else if (reader.ValueTextEquals(_notes)) {
                reader.Read();

                notes = reader.GetString();
            } else if (reader.ValueTextEquals(_popularity)) {
                reader.Read();

                visitedCount = reader.TokenType == JsonTokenType.String
                    ? int.Parse(reader.GetString()!)
                    : reader.GetInt32();
            } else if (reader.ValueTextEquals(_type)) {
                reader.Read();

                type = ParseType(ref reader);
            } else {
                JsonSerializer.Deserialize<JsonElement>(ref reader, options);
            }
        }

        return new Location {
            Address = address,
            Category = category,
            CreatedAtUtc = createdAtUtc,
            CreatedById = createdById,
            DriverId = driverId,
            Geometry = geometry,
            LastActivityAt = lastActivityAt,
            Latitude = latitude,
            Longitude = longitude,
            Name = name,
            Notes = notes,
            Type = type,
            VisitedCount = visitedCount
        };
    }

    public override void Write(
        Utf8JsonWriter writer,
        Location value,
        JsonSerializerOptions options) => throw new NotImplementedException();

    //  ============================================================================
    //  Utilities
    //  ============================================================================

    private static LocationCategory ParseCategory(
        ref Utf8JsonReader reader) {
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

    private static LocationType ParseType(
        ref Utf8JsonReader reader) {
        if (reader.ValueTextEquals(_cache)) {
            return LocationType.Cache;
        }

        if (reader.ValueTextEquals(_geofence)) {
            return LocationType.Geofence;
        }

        if (reader.ValueTextEquals(_googleCache)) {
            return LocationType.GoogleCache;
        }

        if (reader.ValueTextEquals(_poi)) {
            return LocationType.PointOfInterest;
        }

        return LocationType.None;
    }
}
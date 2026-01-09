using System.Text.Json;
using System.Text.Json.Serialization;

namespace Arex388.Carmine.Converters;

internal sealed class VehicleJsonConverter :
    JsonConverter<Vehicle> {
    // Property names (snake_case as returned by API)
    private static ReadOnlySpan<byte> _carbonFootprint => "carbon_footprint"u8;
    private static ReadOnlySpan<byte> _color => "color"u8;
    private static ReadOnlySpan<byte> _created => "created"u8;
    private static ReadOnlySpan<byte> _currentDriver => "current_driver"u8;
    private static ReadOnlySpan<byte> _currentLatitude => "current_latitude"u8;
    private static ReadOnlySpan<byte> _currentLongitude => "current_longitude"u8;
    private static ReadOnlySpan<byte> _faultCodes => "fault_codes"u8;
    private static ReadOnlySpan<byte> _fuelEconomy => "fuel_economy"u8;
    private static ReadOnlySpan<byte> _fuelLevel => "fuel_level"u8;
    private static ReadOnlySpan<byte> _id => "id"u8;
    private static ReadOnlySpan<byte> _lastActivity => "last_activity"u8;
    private static ReadOnlySpan<byte> _lastTrip => "last_trip"u8;
    private static ReadOnlySpan<byte> _licensePlate => "license_plate"u8;
    private static ReadOnlySpan<byte> _make => "make"u8;
    private static ReadOnlySpan<byte> _model => "model"u8;
    private static ReadOnlySpan<byte> _notes => "notes"u8;
    private static ReadOnlySpan<byte> _odometer => "odometer"u8;
    private static ReadOnlySpan<byte> _status => "status"u8;
    private static ReadOnlySpan<byte> _vin => "vin"u8;
    private static ReadOnlySpan<byte> _year => "year"u8;

    // Status values
    private static ReadOnlySpan<byte> _active => "active"u8;
    private static ReadOnlySpan<byte> _inactive => "inactive"u8;

    public override Vehicle? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options) {
        if (reader.TokenType == JsonTokenType.Null) {
            return null;
        }

        if (reader.TokenType != JsonTokenType.StartObject) {
            throw new JsonException("Expected StartObject token");
        }

        decimal? carbonEmissionsInTonsPerGallon = null;
        string? color = null;
        DateTime createdAtUtc = default;
        UserId? driverId = null;
        IDictionary<string, string>? faults = null;
        var fuelConsumptionInMetersPerLiter = 0;
        byte fuelRemaining = 0;
        VehicleId id = default;
        DateTime? lastActivityAt = null;
        TripId? lastTripId = null;
        decimal? latitude = null;
        string? licensePlateNumber = null;
        decimal? longitude = null;
        string? make = null;
        string? model = null;
        string? notes = null;
        int? odometerInMeters = null;
        var status = VehicleStatus.None;
        string? vin = null;
        short? year = null;

        while (reader.Read()) {
            if (reader.TokenType == JsonTokenType.EndObject) {
                break;
            }

            if (reader.TokenType != JsonTokenType.PropertyName) {
                continue;
            }

            if (reader.ValueTextEquals(_id)) {
                reader.Read();

                id = new VehicleId(reader.GetGuid());
            } else if (reader.ValueTextEquals(_carbonFootprint)) {
                reader.Read();

                if (reader.TokenType != JsonTokenType.Null) {
                    carbonEmissionsInTonsPerGallon = reader.GetDecimal();
                }
            } else if (reader.ValueTextEquals(_color)) {
                reader.Read();

                color = reader.GetString();
            } else if (reader.ValueTextEquals(_created)) {
                reader.Read();

                createdAtUtc = reader.GetDateTime();
            } else if (reader.ValueTextEquals(_currentDriver)) {
                reader.Read();

                if (reader.TokenType != JsonTokenType.Null) {
                    driverId = new UserId(reader.GetGuid());
                }
            } else if (reader.ValueTextEquals(_currentLatitude)) {
                reader.Read();

                if (reader.TokenType != JsonTokenType.Null) {
                    latitude = reader.GetDecimal();
                }
            } else if (reader.ValueTextEquals(_currentLongitude)) {
                reader.Read();

                if (reader.TokenType != JsonTokenType.Null) {
                    longitude = reader.GetDecimal();
                }
            } else if (reader.ValueTextEquals(_faultCodes)) {
                reader.Read();

                if (reader.TokenType != JsonTokenType.StartObject) {
                    continue;
                }

                faults = new Dictionary<string, string>();

                while (reader.Read() && reader.TokenType != JsonTokenType.EndObject) {
                    if (reader.TokenType != JsonTokenType.PropertyName) {
                        continue;
                    }

                    var key = reader.GetString()!;

                    reader.Read();

                    var value = reader.GetString() ?? string.Empty;

                    faults[key] = value;
                }
            } else if (reader.ValueTextEquals(_fuelEconomy)) {
                reader.Read();

                fuelConsumptionInMetersPerLiter = reader.TokenType == JsonTokenType.String
                    ? int.Parse(reader.GetString()!)
                    : reader.GetInt32();
            } else if (reader.ValueTextEquals(_fuelLevel)) {
                reader.Read();

                fuelRemaining = reader.TokenType == JsonTokenType.String
                    ? byte.Parse(reader.GetString()!)
                    : reader.GetByte();
            } else if (reader.ValueTextEquals(_lastActivity)) {
                reader.Read();

                if (reader.TokenType != JsonTokenType.Null) {
                    lastActivityAt = reader.GetDateTime();
                }
            } else if (reader.ValueTextEquals(_lastTrip)) {
                reader.Read();

                if (reader.TokenType != JsonTokenType.Null) {
                    lastTripId = new TripId(reader.GetGuid());
                }
            } else if (reader.ValueTextEquals(_licensePlate)) {
                reader.Read();

                licensePlateNumber = reader.GetString();
            } else if (reader.ValueTextEquals(_make)) {
                reader.Read();

                make = reader.GetString();
            } else if (reader.ValueTextEquals(_model)) {
                reader.Read();

                model = reader.GetString();
            } else if (reader.ValueTextEquals(_notes)) {
                reader.Read();

                notes = reader.GetString();
            } else if (reader.ValueTextEquals(_odometer)) {
                reader.Read();

                if (reader.TokenType != JsonTokenType.Null) {
                    odometerInMeters = reader.TokenType == JsonTokenType.String
                        ? int.Parse(reader.GetString()!)
                        : reader.GetInt32();
                }
            } else if (reader.ValueTextEquals(_status)) {
                reader.Read();

                if (reader.ValueTextEquals(_active)) {
                    status = VehicleStatus.Active;
                } else if (reader.ValueTextEquals(_inactive)) {
                    status = VehicleStatus.Inactive;
                }
            } else if (reader.ValueTextEquals(_vin)) {
                reader.Read();

                vin = reader.GetString();
            } else if (reader.ValueTextEquals(_year)) {
                reader.Read();

                if (reader.TokenType != JsonTokenType.Null) {
                    year = reader.TokenType == JsonTokenType.String
                        ? short.Parse(reader.GetString()!)
                        : reader.GetInt16();
                }
            } else {
                JsonSerializer.Deserialize<JsonElement>(ref reader, options);
            }
        }

        return new Vehicle {
            CarbonEmissionsInTonsPerGallon = carbonEmissionsInTonsPerGallon,
            Color = color,
            CreatedAtUtc = createdAtUtc,
            DriverId = driverId,
            Faults = faults,
            FuelConsumptionInMetersPerLiter = fuelConsumptionInMetersPerLiter,
            FuelRemaining = fuelRemaining,
            Id = id,
            LastActivityAt = lastActivityAt,
            LastTripId = lastTripId,
            Latitude = latitude,
            LicensePlateNumber = licensePlateNumber,
            Longitude = longitude,
            Make = make,
            Model = model,
            Notes = notes,
            OdometerInMeters = odometerInMeters,
            Status = status,
            Vin = vin,
            Year = year
        };
    }

    public override void Write(
        Utf8JsonWriter writer,
        Vehicle value,
        JsonSerializerOptions options) => throw new NotImplementedException();
}
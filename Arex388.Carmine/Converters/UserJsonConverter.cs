using System.Text.Json;
using System.Text.Json.Serialization;

namespace Arex388.Carmine.Converters;

internal sealed class UserJsonConverter :
    JsonConverter<User> {
    // Property names (snake_case as returned by API)
    private static ReadOnlySpan<byte> _active => "active"u8;
    private static ReadOnlySpan<byte> _canShareEta => "can_share_eta"u8;
    private static ReadOnlySpan<byte> _color => "color"u8;
    private static ReadOnlySpan<byte> _created => "created"u8;
    private static ReadOnlySpan<byte> _currentVehicle => "current_vehicle"u8;
    private static ReadOnlySpan<byte> _email => "email"u8;
    private static ReadOnlySpan<byte> _id => "id"u8;
    private static ReadOnlySpan<byte> _lastActivity => "last_activity"u8;
    private static ReadOnlySpan<byte> _lastTrip => "last_trip"u8;
    private static ReadOnlySpan<byte> _name => "name"u8;
    private static ReadOnlySpan<byte> _photoUrl => "photo_url"u8;
    private static ReadOnlySpan<byte> _role => "role"u8;
    private static ReadOnlySpan<byte> _sms => "sms"u8;
    private static ReadOnlySpan<byte> _validated => "validated"u8;

    // Role values
    private static ReadOnlySpan<byte> _administrator => "company_admin"u8;
    private static ReadOnlySpan<byte> _driver => "driver"u8;

    public override User? Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options) {
        if (reader.TokenType == JsonTokenType.Null) {
            return null;
        }

        if (reader.TokenType != JsonTokenType.StartObject) {
            throw new JsonException("Expected StartObject token");
        }

        var canShareVehiclesEta = false;
        string? color = null;
        DateTime createdAtUtc = default;
        string? email = null;
        UserId id = default;
        var isEmailValidated = false;
        DateTime? lastActivityAt = null;
        TripId? lastTripId = null;
        string name = null!;
        long? phone = null;
        string? photoUrl = null;
        var role = UserRole.None;
        var status = UserStatus.None;
        VehicleId? vehicleId = null;

        while (reader.Read()) {
            if (reader.TokenType == JsonTokenType.EndObject) {
                break;
            }

            if (reader.TokenType != JsonTokenType.PropertyName) {
                continue;
            }

            if (reader.ValueTextEquals(_id)) {
                reader.Read();

                id = new UserId(reader.GetGuid());
            } else if (reader.ValueTextEquals(_active)) {
                reader.Read();

                status = reader.GetBoolean()
                    ? UserStatus.Active
                    : UserStatus.Inactive;
            } else if (reader.ValueTextEquals(_canShareEta)) {
                reader.Read();

                canShareVehiclesEta = reader.GetBoolean();
            } else if (reader.ValueTextEquals(_color)) {
                reader.Read();

                color = reader.GetString();
            } else if (reader.ValueTextEquals(_created)) {
                reader.Read();

                createdAtUtc = reader.GetDateTime();
            } else if (reader.ValueTextEquals(_currentVehicle)) {
                reader.Read();

                if (reader.TokenType != JsonTokenType.Null) {
                    vehicleId = new VehicleId(reader.GetGuid());
                }
            } else if (reader.ValueTextEquals(_email)) {
                reader.Read();

                email = reader.GetString();
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
            } else if (reader.ValueTextEquals(_name)) {
                reader.Read();

                name = reader.GetString()!;
            } else if (reader.ValueTextEquals(_photoUrl)) {
                reader.Read();

                photoUrl = reader.GetString();
            } else if (reader.ValueTextEquals(_role)) {
                reader.Read();

                if (reader.TokenType == JsonTokenType.String) {
                    if (reader.ValueTextEquals(_administrator)) {
                        role = UserRole.Administrator;
                    } else if (reader.ValueTextEquals(_driver)) {
                        role = UserRole.Driver;
                    }
                } else if (reader.TokenType is JsonTokenType.StartObject or JsonTokenType.StartArray) {
                    reader.Skip();
                }
            } else if (reader.ValueTextEquals(_sms)) {
                reader.Read();

                if (reader.TokenType == JsonTokenType.String) {
                    phone = ParsePhone(ref reader);
                } else if (reader.TokenType == JsonTokenType.Number) {
                    phone = reader.GetInt64();
                }
            } else if (reader.ValueTextEquals(_validated)) {
                reader.Read();

                isEmailValidated = reader.GetBoolean();
            } else {
                reader.Skip();
            }
        }

        return new User {
            CanShareVehiclesEta = canShareVehiclesEta,
            Color = color,
            CreatedAtUtc = createdAtUtc,
            Email = email,
            Id = id,
            IsEmailValidated = isEmailValidated,
            LastActivityAt = lastActivityAt,
            LastTripId = lastTripId,
            Name = name,
            Phone = phone,
            PhotoUrl = photoUrl,
            Role = role,
            Status = status,
            VehicleId = vehicleId
        };
    }

    public override void Write(
        Utf8JsonWriter writer,
        User value,
        JsonSerializerOptions options) => throw new NotImplementedException();

    //  ============================================================================
    //  Utilities
    //  ============================================================================

    private static long? ParsePhone(
        ref Utf8JsonReader reader) {
        //  A phone string never legitimately needs JSON escapes, so the
        //  allocation-free UTF-8 scan covers virtually all real payloads.
        if (!reader.HasValueSequence
            && !reader.ValueIsEscaped) {
            return ParsePhone(reader.ValueSpan);
        }

        long phone = 0;
        var digits = 0;

        foreach (var c in reader.GetString()!) {
            if (c is >= '0' and <= '9') {
                if (++digits > 18) {
                    return null;
                }

                phone = phone * 10 + (c - '0');
            } else if (c is not ('+' or '-' or '(' or ')' or '.' or ' ')) {
                return null;
            }
        }

        return digits == 0
            ? null
            : phone;
    }

    private static long? ParsePhone(
        ReadOnlySpan<byte> value) {
        long phone = 0;
        var digits = 0;

        foreach (var b in value) {
            if (b is >= (byte)'0' and <= (byte)'9') {
                if (++digits > 18) {
                    return null;
                }

                phone = phone * 10 + (b - '0');
            } else if (b is not ((byte)'+' or (byte)'-' or (byte)'(' or (byte)')' or (byte)'.' or (byte)' ')) {
                return null;
            }
        }

        return digits == 0
            ? null
            : phone;
    }
}
using Arex388.Carmine.Converters;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Arex388.Carmine;

internal sealed class CarmineClient(
    IServiceProvider services,
    HttpClient? httpClient = null,
    CarmineClientOptions? options = null) :
    ICarmineClient {
    private static readonly JsonSerializerOptions _jsonSerializerOptions = new() {
        Converters = {
            new EventJsonConverter(),
            new LocationJsonConverter(),
            new TripJsonConverter(),
            new TripExpandedJsonConverter(),
            new UserJsonConverter(),
            new VehicleJsonConverter(),
            new WaypointJsonConverter()
        },
        NumberHandling = JsonNumberHandling.AllowReadingFromString,
        PropertyNamingPolicy = JsonNamingPolicy.SnakeCaseLower
    };

    private readonly IValidator<GetTrip.Request> _getTripRequestValidator = services.GetRequiredService<IValidator<GetTrip.Request>>();
    private readonly IValidator<GetUser.Request> _getUserRequestValidator = services.GetRequiredService<IValidator<GetUser.Request>>();
    private readonly IValidator<GetVehicle.Request> _getVehicleRequestValidator = services.GetRequiredService<IValidator<GetVehicle.Request>>();
    private readonly HttpClient _httpClient = httpClient ?? services.GetRequiredService<IHttpClientFactory>().CreateClient(nameof(ICarmineClient));
    private readonly CarmineClientOptions _options = options ?? services.GetRequiredService<CarmineClientOptions>();

    public Task<GetTrip.Response> GetTripAsync(
        TripId id,
        CancellationToken cancellationToken = default) => GetTripAsync(new GetTrip.Request {
            Id = id
        }, cancellationToken);

    public async Task<GetTrip.Response> GetTripAsync(
        GetTrip.Request request,
        CancellationToken cancellationToken = default) {
        if (cancellationToken.IsSupportedAndCancelled()) {
            return GetTrip.Response.Cancelled;
        }

        // ReSharper disable once MethodHasAsyncOverloadWithCancellation
        var validationResult = _getTripRequestValidator.Validate(request);

        if (!validationResult.IsValid) {
            return GetTrip.Response.Invalid(validationResult);
        }

        try {
            var trip = await _httpClient.GetFromJsonAsync<TripExpanded>(request.GetEndpoint(_options), _jsonSerializerOptions, cancellationToken).ConfigureAwait(false);

            if (trip is null) {
                return GetTrip.Response.Failed;
            }

            return new GetTrip.Response {
                Trip = trip
            };
        } catch {
            return GetTrip.Response.Failed;
        }
    }

    public Task<GetUser.Response> GetUserAsync(
        UserId id,
        CancellationToken cancellationToken = default) => GetUserAsync(new GetUser.Request {
            Id = id
        }, cancellationToken);

    public async Task<GetUser.Response> GetUserAsync(
        GetUser.Request request,
        CancellationToken cancellationToken = default) {
        if (cancellationToken.IsSupportedAndCancelled()) {
            return GetUser.Response.Cancelled;
        }

        // ReSharper disable once MethodHasAsyncOverloadWithCancellation
        var validationResult = _getUserRequestValidator.Validate(request);

        if (!validationResult.IsValid) {
            return GetUser.Response.Invalid(validationResult);
        }

        try {
            var user = await _httpClient.GetFromJsonAsync<User>(request.GetEndpoint(_options), _jsonSerializerOptions, cancellationToken).ConfigureAwait(false);

            if (user is null) {
                return GetUser.Response.Failed;
            }

            return new GetUser.Response {
                User = user
            };
        } catch {
            return GetUser.Response.Failed;
        }
    }

    public Task<GetVehicle.Response> GetVehicleAsync(
        VehicleId id,
        CancellationToken cancellationToken = default) => GetVehicleAsync(new GetVehicle.Request {
            Id = id
        }, cancellationToken);

    public async Task<GetVehicle.Response> GetVehicleAsync(
        GetVehicle.Request request,
        CancellationToken cancellationToken = default) {
        if (cancellationToken.IsSupportedAndCancelled()) {
            return GetVehicle.Response.Cancelled;
        }

        // ReSharper disable once MethodHasAsyncOverloadWithCancellation
        var validationResult = _getVehicleRequestValidator.Validate(request);

        if (!validationResult.IsValid) {
            return GetVehicle.Response.Invalid(validationResult);
        }

        try {
            var vehicle = await _httpClient.GetFromJsonAsync<Vehicle?>(request.GetEndpoint(_options), _jsonSerializerOptions, cancellationToken).ConfigureAwait(false);

            if (vehicle is null) {
                return GetVehicle.Response.Failed;
            }

            return new GetVehicle.Response {
                Vehicle = vehicle
            };
        } catch {
            return GetVehicle.Response.Failed;
        }
    }

    public Task<ListTrips.Response> ListTripsAsync(
        CancellationToken cancellationToken = default) => ListTripsAsync(ListTrips.Request.Default, cancellationToken);

    public async Task<ListTrips.Response> ListTripsAsync(
        ListTrips.Request request,
        CancellationToken cancellationToken = default) {
        if (cancellationToken.IsSupportedAndCancelled()) {
            return ListTrips.Response.Cancelled;
        }

        try {
            var trips = await _httpClient.GetFromJsonAsync<IList<Trip>>(request.GetEndpoint(_options), _jsonSerializerOptions, cancellationToken).ConfigureAwait(false);

            return new ListTrips.Response {
                Trips = trips ?? []
            };
        } catch {
            return ListTrips.Response.Failed;
        }
    }

    public Task<ListUsers.Response> ListUsersAsync(
        CancellationToken cancellationToken = default) => ListUsersAsync(ListUsers.Request.Default, cancellationToken);

    public async Task<ListUsers.Response> ListUsersAsync(
        ListUsers.Request request,
        CancellationToken cancellationToken = default) {
        if (cancellationToken.IsSupportedAndCancelled()) {
            return ListUsers.Response.Cancelled;
        }

        try {
            var users = await _httpClient.GetFromJsonAsync<IList<User>>(request.GetEndpoint(_options), _jsonSerializerOptions, cancellationToken).ConfigureAwait(false);

            return new ListUsers.Response {
                Users = users ?? []
            };
        } catch {
            return ListUsers.Response.Failed;
        }
    }

    public Task<ListVehicles.Response> ListVehiclesAsync(
        CancellationToken cancellationToken = default) => ListVehiclesAsync(ListVehicles.Request.Default, cancellationToken);

    public async Task<ListVehicles.Response> ListVehiclesAsync(
        ListVehicles.Request request,
        CancellationToken cancellationToken = default) {
        if (cancellationToken.IsSupportedAndCancelled()) {
            return ListVehicles.Response.Cancelled;
        }

        try {
            var vehicles = await _httpClient.GetFromJsonAsync<IList<Vehicle>>(request.GetEndpoint(_options), _jsonSerializerOptions, cancellationToken).ConfigureAwait(false);

            return new ListVehicles.Response {
                Vehicles = vehicles ?? []
            };
        } catch {
            return ListVehicles.Response.Failed;
        }
    }
}
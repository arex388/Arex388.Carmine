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
    private readonly IValidator<ListTrips.Request> _listTripsRequestValidator = services.GetRequiredService<IValidator<ListTrips.Request>>();
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
            using var httpResponse = await _httpClient.GetAsync(request.GetEndpoint(_options), HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);

            if (!httpResponse.IsSuccessStatusCode) {
                return GetTrip.Response.FailedWith(GetStatusDetail(httpResponse));
            }

            var trip = await httpResponse.Content.ReadFromJsonAsync<TripExpanded>(_jsonSerializerOptions, cancellationToken).ConfigureAwait(false);

            if (trip is null) {
                return GetTrip.Response.Failed;
            }

            return new GetTrip.Response {
                Trip = trip
            };
        } catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested) {
            return GetTrip.Response.Cancelled;
        } catch (Exception e) {
            return GetTrip.Response.FailedWith(GetExceptionDetail(e));
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
            using var httpResponse = await _httpClient.GetAsync(request.GetEndpoint(_options), HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);

            if (!httpResponse.IsSuccessStatusCode) {
                return GetUser.Response.FailedWith(GetStatusDetail(httpResponse));
            }

            var user = await httpResponse.Content.ReadFromJsonAsync<User>(_jsonSerializerOptions, cancellationToken).ConfigureAwait(false);

            if (user is null) {
                return GetUser.Response.Failed;
            }

            return new GetUser.Response {
                User = user
            };
        } catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested) {
            return GetUser.Response.Cancelled;
        } catch (Exception e) {
            return GetUser.Response.FailedWith(GetExceptionDetail(e));
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
            using var httpResponse = await _httpClient.GetAsync(request.GetEndpoint(_options), HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);

            if (!httpResponse.IsSuccessStatusCode) {
                return GetVehicle.Response.FailedWith(GetStatusDetail(httpResponse));
            }

            var vehicle = await httpResponse.Content.ReadFromJsonAsync<Vehicle>(_jsonSerializerOptions, cancellationToken).ConfigureAwait(false);

            if (vehicle is null) {
                return GetVehicle.Response.Failed;
            }

            return new GetVehicle.Response {
                Vehicle = vehicle
            };
        } catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested) {
            return GetVehicle.Response.Cancelled;
        } catch (Exception e) {
            return GetVehicle.Response.FailedWith(GetExceptionDetail(e));
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

        // ReSharper disable once MethodHasAsyncOverloadWithCancellation
        var validationResult = _listTripsRequestValidator.Validate(request);

        if (!validationResult.IsValid) {
            return ListTrips.Response.Invalid(validationResult);
        }

        try {
            using var httpResponse = await _httpClient.GetAsync(request.GetEndpoint(_options), HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);

            if (!httpResponse.IsSuccessStatusCode) {
                return ListTrips.Response.FailedWith(GetStatusDetail(httpResponse));
            }

            var trips = await httpResponse.Content.ReadFromJsonAsync<IList<Trip>>(_jsonSerializerOptions, cancellationToken).ConfigureAwait(false);

            return new ListTrips.Response {
                Trips = trips ?? []
            };
        } catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested) {
            return ListTrips.Response.Cancelled;
        } catch (Exception e) {
            return ListTrips.Response.FailedWith(GetExceptionDetail(e));
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
            using var httpResponse = await _httpClient.GetAsync(request.GetEndpoint(_options), HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);

            if (!httpResponse.IsSuccessStatusCode) {
                return ListUsers.Response.FailedWith(GetStatusDetail(httpResponse));
            }

            var users = await httpResponse.Content.ReadFromJsonAsync<IList<User>>(_jsonSerializerOptions, cancellationToken).ConfigureAwait(false);

            return new ListUsers.Response {
                Users = users ?? []
            };
        } catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested) {
            return ListUsers.Response.Cancelled;
        } catch (Exception e) {
            return ListUsers.Response.FailedWith(GetExceptionDetail(e));
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
            using var httpResponse = await _httpClient.GetAsync(request.GetEndpoint(_options), HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);

            if (!httpResponse.IsSuccessStatusCode) {
                return ListVehicles.Response.FailedWith(GetStatusDetail(httpResponse));
            }

            var vehicles = await httpResponse.Content.ReadFromJsonAsync<IList<Vehicle>>(_jsonSerializerOptions, cancellationToken).ConfigureAwait(false);

            return new ListVehicles.Response {
                Vehicles = vehicles ?? []
            };
        } catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested) {
            return ListVehicles.Response.Cancelled;
        } catch (Exception e) {
            return ListVehicles.Response.FailedWith(GetExceptionDetail(e));
        }
    }

    //  ============================================================================
    //  Utilities
    //  ============================================================================

    //  Detail strings are appended to Errors after "The request has failed." —
    //  never echo the endpoint URL, which carries the api_key.
    private static string GetExceptionDetail(
        Exception exception) => $"{exception.GetType().Name}: {exception.Message}";

    private static string GetStatusDetail(
        HttpResponseMessage response) => string.IsNullOrEmpty(response.ReasonPhrase)
            ? $"HTTP {(int)response.StatusCode} {response.StatusCode}"
            : $"HTTP {(int)response.StatusCode} {response.ReasonPhrase}";
}
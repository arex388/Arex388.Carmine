using Arex388.Carmine.Converters;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Globalization;
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

    public Task<GetTrip.Response> GetTripAsync(
        GetTrip.Request request,
        CancellationToken cancellationToken = default) => SendAsync(request, _getTripRequestValidator, static (TripExpanded? trip) => trip is null
            ? null
            : new GetTrip.Response {
                Trip = trip
            }, cancellationToken);

    public Task<GetUser.Response> GetUserAsync(
        UserId id,
        CancellationToken cancellationToken = default) => GetUserAsync(new GetUser.Request {
            Id = id
        }, cancellationToken);

    public Task<GetUser.Response> GetUserAsync(
        GetUser.Request request,
        CancellationToken cancellationToken = default) => SendAsync(request, _getUserRequestValidator, static (User? user) => user is null
            ? null
            : new GetUser.Response {
                User = user
            }, cancellationToken);

    public Task<GetVehicle.Response> GetVehicleAsync(
        VehicleId id,
        CancellationToken cancellationToken = default) => GetVehicleAsync(new GetVehicle.Request {
            Id = id
        }, cancellationToken);

    public Task<GetVehicle.Response> GetVehicleAsync(
        GetVehicle.Request request,
        CancellationToken cancellationToken = default) => SendAsync(request, _getVehicleRequestValidator, static (Vehicle? vehicle) => vehicle is null
            ? null
            : new GetVehicle.Response {
                Vehicle = vehicle
            }, cancellationToken);

    public Task<ListTrips.Response> ListTripsAsync(
        CancellationToken cancellationToken = default) => ListTripsAsync(ListTrips.Request.Default, cancellationToken);

    public Task<ListTrips.Response> ListTripsAsync(
        ListTrips.Request request,
        CancellationToken cancellationToken = default) => SendAsync(request, _listTripsRequestValidator, static (List<Trip>? trips) => new ListTrips.Response {
            Trips = trips ?? []
        }, cancellationToken);

    public Task<ListUsers.Response> ListUsersAsync(
        CancellationToken cancellationToken = default) => ListUsersAsync(ListUsers.Request.Default, cancellationToken);

    public Task<ListUsers.Response> ListUsersAsync(
        ListUsers.Request request,
        CancellationToken cancellationToken = default) => SendAsync(request, null, static (List<User>? users) => new ListUsers.Response {
            Users = users ?? []
        }, cancellationToken);

    public Task<ListVehicles.Response> ListVehiclesAsync(
        CancellationToken cancellationToken = default) => ListVehiclesAsync(ListVehicles.Request.Default, cancellationToken);

    public Task<ListVehicles.Response> ListVehiclesAsync(
        ListVehicles.Request request,
        CancellationToken cancellationToken = default) => SendAsync(request, null, static (List<Vehicle>? vehicles) => new ListVehicles.Response {
            Vehicles = vehicles ?? []
        }, cancellationToken);

    //  ============================================================================
    //  Pipeline
    //  ============================================================================

    //  The single request pipeline behind all six operations: cancellation
    //  pre-check → optional validation → GET with headers-read → deserialize →
    //  map. A null map result means "the API returned nothing usable" (Failed);
    //  list mappers never return null, coalescing a null payload to empty.
    private async Task<TResponse> SendAsync<TRequest, TModel, TResponse>(
        TRequest request,
        IValidator<TRequest>? validator,
        Func<TModel?, TResponse?> map,
        CancellationToken cancellationToken)
        where TRequest : RequestBase
        where TModel : class
        where TResponse : ResponseBase<TResponse>, new() {
        if (cancellationToken.IsSupportedAndCancelled()) {
            return ResponseBase<TResponse>.Cancelled;
        }

        if (validator is not null) {
            // ReSharper disable once MethodHasAsyncOverloadWithCancellation
            var validationResult = validator.Validate(request);

            if (!validationResult.IsValid) {
                return ResponseBase<TResponse>.Invalid(validationResult);
            }
        }

        try {
            using var httpResponse = await _httpClient.GetAsync(request.GetEndpoint(_options), HttpCompletionOption.ResponseHeadersRead, cancellationToken).ConfigureAwait(false);

            if (!httpResponse.IsSuccessStatusCode) {
                return ResponseBase<TResponse>.FailedWith(GetStatusDetail(httpResponse));
            }

            var model = await httpResponse.Content.ReadFromJsonAsync<TModel>(_jsonSerializerOptions, cancellationToken).ConfigureAwait(false);

            return map(model) ?? ResponseBase<TResponse>.Failed;
        } catch (OperationCanceledException) when (cancellationToken.IsCancellationRequested) {
            return ResponseBase<TResponse>.Cancelled;
        } catch (Exception e) {
            return ResponseBase<TResponse>.FailedWith(GetExceptionDetail(e));
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
        HttpResponseMessage response) {
        //  Explicit concatenation: interpolation lowers to string.Format on
        //  netstandard2.0 and boxes the status code; Enum.ToString is also
        //  reflection-based, so the name is resolved only when needed.
        var code = ((int)response.StatusCode).ToString(CultureInfo.InvariantCulture);
        var name = response.ReasonPhrase;

        if (string.IsNullOrEmpty(name)) {
            name = response.StatusCode.ToString();

            //  An unnamed status code stringifies to its numeric value — don't
            //  render it twice ("HTTP 599 599").
            if (name == code) {
                return "HTTP " + code;
            }
        }

        return "HTTP " + code + " " + name;
    }
}
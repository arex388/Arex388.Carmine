using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Arex388.Carmine;

/// <summary>
/// Carmine.io API client.
/// </summary>
public sealed class CarmineClient {
    private static JsonSerializerSettings JsonSerializerSettings => new() {
        DateFormatHandling = DateFormatHandling.IsoDateFormat,
        DateTimeZoneHandling = DateTimeZoneHandling.Utc
    };

    private readonly bool _debug;
    private readonly HttpClient _httpClient;
    private readonly string _key;

    /// <summary>
    /// Create an instance of the Carmine.io API client.
    /// </summary>
    /// <param name="httpClient">An instance of HttpClient</param>
    /// <param name="key">Your Carmine.io API key.</param>
    /// <param name="debug">Toggle response debugging. Disabled by default.</param>
    public CarmineClient(
        HttpClient httpClient,
        string key,
        bool debug = false) {
        _debug = debug;
        _httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
        _key = key ?? throw new ArgumentNullException(nameof(key));
    }

    //  ========================================================================
    //  Actions
    //  ========================================================================

    /// <summary>
    /// Returns a trip.
    /// </summary>
    /// <param name="id">Teh trip's id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>GetTripResponse</returns>
    public async Task<GetTripResponse> GetTripAsync(
        Guid id,
        CancellationToken cancellationToken = default) => await GetTripAsync(new GetTripRequest {
        Id = id
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>
    /// Returns a trip.
    /// </summary>
    /// <param name="request">The request parameters.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>GetUserResponse</returns>
    public async Task<GetTripResponse> GetTripAsync(
        GetTripRequest request,
        CancellationToken cancellationToken = default) {
        if (cancellationToken.IsCancellationRequested) {
            return CancelledResponse<GetTripResponse>();
        }

        return await RequestAsync<GetTripResponse>(request, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Returns a user.
    /// </summary>
    /// <param name="id">The user's id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>GetUserResponse</returns>
    public async Task<GetUserResponse> GetUserAsync(
        Guid id,
        CancellationToken cancellationToken = default) => await GetUserAsync(new GetUserRequest {
        Id = id
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>
    /// Returns a user.
    /// </summary>
    /// <param name="request">The request parameters.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>GetUserResponse</returns>
    public async Task<GetUserResponse> GetUserAsync(
        GetUserRequest request,
        CancellationToken cancellationToken = default) {
        if (cancellationToken.IsCancellationRequested) {
            return CancelledResponse<GetUserResponse>();
        }

        return await RequestAsync<GetUserResponse>(request, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Returns a vehicle by its id.
    /// </summary>
    /// <param name="id">The vehicle's id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>GetVehicleResponse</returns>
    public async Task<GetVehicleResponse> GetVehicleAsync(
        Guid id,
        CancellationToken cancellationToken = default) => await GetVehicleAsync(new GetVehicleRequest {
        Id = id
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>
    /// Returns a vehicle.
    /// </summary>
    /// <param name="request">The request parameters.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>GetVehicleResponse</returns>
    public async Task<GetVehicleResponse> GetVehicleAsync(
        GetVehicleRequest request,
        CancellationToken cancellationToken = default) {
        if (cancellationToken.IsCancellationRequested) {
            return CancelledResponse<GetVehicleResponse>();
        }

        return await RequestAsync<GetVehicleResponse>(request, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Returns a list of trip.s
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>ListTripsResponse</returns>
    public async Task<ListTripsResponse> ListTripsAsync(
        CancellationToken cancellationToken = default) {
        if (cancellationToken.IsCancellationRequested) {
            return CancelledResponse<ListTripsResponse>();
        }

        return await ListTripsAsync(new ListTripsRequest(), cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Returns a list of trips.
    /// </summary>
    /// <param name="request">The request parametes.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>ListTripsResponse2</returns>
    public async Task<ListTripsResponse> ListTripsAsync(
        ListTripsRequest request,
        CancellationToken cancellationToken = default) {
        if (cancellationToken.IsCancellationRequested) {
            return CancelledResponse<ListTripsResponse>();
        }

        return await RequestAsync<ListTripsResponse>(request, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Returns a list of users.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>ListUsersResponse</returns>
    public async Task<ListUsersResponse> ListUsersAsync(
        CancellationToken cancellationToken = default) {
        if (cancellationToken.IsCancellationRequested) {
            return CancelledResponse<ListUsersResponse>();
        }

        return await ListUsersAsync(new ListUsersRequest(), cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Returns a list of users.
    /// </summary>
    /// <param name="request">The request parameters.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>ListUsersResponse</returns>
    public async Task<ListUsersResponse> ListUsersAsync(
        ListUsersRequest request,
        CancellationToken cancellationToken = default) {
        if (cancellationToken.IsCancellationRequested) {
            return CancelledResponse<ListUsersResponse>();
        }

        return await RequestAsync<ListUsersResponse>(request, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Returns a list of vehicles.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>ListVehiclesResponse</returns>
    public async Task<ListVehiclesResponse> ListVehiclesAsync(
        CancellationToken cancellationToken = default) {
        if (cancellationToken.IsCancellationRequested) {
            return CancelledResponse<ListVehiclesResponse>();
        }

        return await ListVehiclesAsync(new ListVehiclesRequest(), cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Returns a list of vehicles.
    /// </summary>
    /// <param name="request">The request parameters.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>ListVehiclesResponse</returns>
    public async Task<ListVehiclesResponse> ListVehiclesAsync(
        ListVehiclesRequest request,
        CancellationToken cancellationToken = default) {
        if (cancellationToken.IsCancellationRequested) {
            return CancelledResponse<ListVehiclesResponse>();
        }

        return await RequestAsync<ListVehiclesResponse>(request, cancellationToken).ConfigureAwait(false);
    }

    /// <summary>
    /// Returns a list of waypoints for a trip.
    /// </summary>
    /// <param name="tripId">The trip id.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>ListWaypointsResponse</returns>
    public async Task<ListWaypointsResponse> ListWaypointsAsync(
        Guid tripId,
        CancellationToken cancellationToken = default) => await ListWaypointsAsync(new ListWaypointsRequest {
        TripId = tripId
    }, cancellationToken).ConfigureAwait(false);

    /// <summary>
    /// Returns a list of waypoints for a trip.
    /// </summary>
    /// <param name="request">The request parameters.</param>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>ListWaypointsResponse</returns>
    public async Task<ListWaypointsResponse> ListWaypointsAsync(
        ListWaypointsRequest request,
        CancellationToken cancellationToken = default) {
        if (cancellationToken.IsCancellationRequested) {
            return CancelledResponse<ListWaypointsResponse>();
        }

        return await RequestAsync<ListWaypointsResponse>(request, cancellationToken).ConfigureAwait(false);
    }

    //  ========================================================================
    //  Utilities
    //  ========================================================================

    private async Task<T> RequestAsync<T>(
        RequestBase request,
        CancellationToken cancellationToken = default)
        where T : IResponse, new() {
        if (cancellationToken.IsCancellationRequested) {
            return CancelledResponse<T>();
        }

        try {
            using var response = await _httpClient.GetAsync($"{request.Endpoint}&api_key={_key}", cancellationToken).ConfigureAwait(false);
            var json = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var success = response.StatusCode == HttpStatusCode.OK;

            return Deserialize<T>(new ResponseResult(string.Format(request.Wrapper, json), success));
        } catch (HttpRequestException) {
            return FailureResponse<T>();
        } catch (TaskCanceledException) {
            return TimeOutResponse<T>();
        }
    }

    private T Deserialize<T>(
        ResponseResult response)
        where T : IResponse, new() {
        var json = response.Json;

        if (!json.HasValue()) {
            return new T();
        }

        var t = JsonConvert.DeserializeObject<T>(json, JsonSerializerSettings);

        if (t is null) {
            return new T();
        }

        t.ResponseJson = _debug
            ? json
            : null;
        t.ResponseStatus = response.Success
            ? ResponseStatus.Success
            : ResponseStatus.Failure;

        return t;
    }

    //  ========================================================================
    //  Responses
    //  ========================================================================

    private static T CancelledResponse<T>()
        where T : IResponse, new() => new() {
        ResponseStatus = ResponseStatus.Cancelled
    };

    private static T FailureResponse<T>()
        where T : IResponse, new() => new() {
        ResponseStatus = ResponseStatus.Failure
    };

    private static T TimeOutResponse<T>()
        where T : IResponse, new() => new() {
        ResponseStatus = ResponseStatus.TimeOut
    };
}
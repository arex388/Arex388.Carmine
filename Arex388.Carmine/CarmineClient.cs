using Arex388.Carmine.Extensions;
using Arex388.Carmine.Validators;
using System.Net.Http.Json;

// ReSharper disable MethodHasAsyncOverloadWithCancellation

namespace Arex388.Carmine;

/// <summary>
/// Carmine.io API client.
/// </summary>
public sealed class CarmineClient :
	ICarmineClient {
	private const string _endpointHost = "https://api.carmine.io/v2";

	private readonly string _apiKey;
	private readonly HttpClient _httpClient;

	private GetTripRequestValidator? _getTripRequestValidator;
	private GetUserRequestValidator? _getUserRequestValidator;
	private GetVehicleRequestValidator? _getVehicleRequestValidator;

	/// <summary>
	/// Create an instance of the Carmine.io API client.
	/// </summary>
	/// <param name="apiKey">Your Carmine.io API key.</param>
	/// <param name="httpClient">An instance of <c>HttpClient</c>.</param>
	public CarmineClient(
		string apiKey,
		HttpClient httpClient) {
		_apiKey = $"api_key={apiKey}" ?? throw new ArgumentNullException(nameof(apiKey));
		_httpClient = httpClient ?? throw new ArgumentNullException(nameof(httpClient));
	}

	//	============================================================================
	//	Actions
	//	============================================================================

	/// <summary>
	/// Returns a trip.
	/// </summary>
	/// <param name="id">The trip's id.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>An instance of <c>GetTrip.Response</c>.</returns>
	public Task<GetTrip.Response> GetTripAsync(
		TripId id,
		CancellationToken cancellationToken = default) => GetTripAsync(new GetTrip.Request {
			Id = id
		}, cancellationToken);

	/// <summary>
	/// Returns a trip.
	/// </summary>
	/// <param name="request">An instance of <c>GetTrip.Request</c> containing the request's parameters.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>An instance of <c>GetTrip.Response</c>.</returns>
	public async Task<GetTrip.Response> GetTripAsync(
		GetTrip.Request request,
		CancellationToken cancellationToken = default) {
		if (cancellationToken.IsCancellationRequested) {
			return GetTrip.Cancelled;
		}

		var validator = _getTripRequestValidator ??= new GetTripRequestValidator();
		var validation = validator.Validate(request);

		if (!validation.IsValid) {
			return GetTrip.Invalid(validation);
		}

		try {
			var trip = await _httpClient.GetFromJsonAsync<TripExpanded?>($"{_endpointHost}/{request.GetEndpoint()}&{_apiKey}", cancellationToken).ConfigureAwait(false);

			return new GetTrip.Response {
				Status = ResponseStatus.Succeeded,
				Trip = trip
			};
		} catch (TaskCanceledException) {
			return GetTrip.TimedOut;
		} catch (Exception e) {
			return GetTrip.Failed(e);
		}
	}

	/// <summary>
	/// Returns a user.
	/// </summary>
	/// <param name="id">The user's id.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>An instance of <c>GetUser.Response</c>.</returns>
	public Task<GetUser.Response> GetUserAsync(
		UserId id,
		CancellationToken cancellationToken = default) => GetUserAsync(new GetUser.Request {
			Id = id
		}, cancellationToken);

	/// <summary>
	/// Returns a user.
	/// </summary>
	/// <param name="request">An instance of <c>GetUser.Request</c> containing the request's parameters.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>An instance of <c>GetUser.Response</c>.</returns>
	public async Task<GetUser.Response> GetUserAsync(
		GetUser.Request request,
		CancellationToken cancellationToken = default) {
		if (cancellationToken.IsCancellationRequested) {
			return GetUser.Cancelled;
		}

		var validator = _getUserRequestValidator ??= new GetUserRequestValidator();
		var validation = validator.Validate(request);

		if (!validation.IsValid) {
			return GetUser.Invalid(validation);
		}

		try {
			var user = await _httpClient.GetFromJsonAsync<User?>($"{_endpointHost}/{request.GetEndpoint()}&{_apiKey}", cancellationToken).ConfigureAwait(false);

			return new GetUser.Response {
				Status = ResponseStatus.Succeeded,
				User = user
			};
		} catch (TaskCanceledException) {
			return GetUser.TimedOut;
		} catch (Exception e) {
			return GetUser.Failed(e);
		}
	}

	/// <summary>
	/// Returns a vehicle.
	/// </summary>
	/// <param name="id">The vehicle's id.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>An instance of <c>GetVehicle.Response</c>.</returns>
	public Task<GetVehicle.Response> GetVehicleAsync(
		VehicleId id,
		CancellationToken cancellationToken = default) => GetVehicleAsync(new GetVehicle.Request {
			Id = id
		}, cancellationToken);

	/// <summary>
	///	Returns a vehicle.
	/// </summary>
	/// <param name="request">An instance of <c>GetVehicle.Request</c> containing the request's parameters.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>An instance of <c>GetVehicle.Response</c>.</returns>
	public async Task<GetVehicle.Response> GetVehicleAsync(
		GetVehicle.Request request,
		CancellationToken cancellationToken = default) {
		if (cancellationToken.IsCancellationRequested) {
			return GetVehicle.Cancelled;
		}

		var validator = _getVehicleRequestValidator ??= new GetVehicleRequestValidator();
		var validation = validator.Validate(request);

		if (!validation.IsValid) {
			return GetVehicle.Invalid(validation);
		}

		try {
			var vehicle = await _httpClient.GetFromJsonAsync<Vehicle?>($"{_endpointHost}/{request.GetEndpoint()}&{_apiKey}", cancellationToken).ConfigureAwait(false);

			return new GetVehicle.Response {
				Status = ResponseStatus.Succeeded,
				Vehicle = vehicle
			};
		} catch (TaskCanceledException) {
			return GetVehicle.TimedOut;
		} catch (Exception e) {
			return GetVehicle.Failed(e);
		}
	}

	/// <summary>
	/// Returns a list of trips.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>An instance of <c>ListTrips.Response</c>.</returns>
	public Task<ListTrips.Response> ListTripsAsync(
		CancellationToken cancellationToken = default) => ListTripsAsync(new ListTrips.Request(), cancellationToken);

	/// <summary>
	/// Returns a list of trips.
	/// </summary>
	/// <param name="request">An instance of <c>ListTrips.Request</c> containing the request's parameters.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>An instance of <c>ListTrips.Response</c>.</returns>
	public async Task<ListTrips.Response> ListTripsAsync(
		ListTrips.Request request,
		CancellationToken cancellationToken = default) {
		if (cancellationToken.IsCancellationRequested) {
			return ListTrips.Cancelled;
		}

		try {
			var trips = await _httpClient.GetFromJsonAsync<IList<Trip>>($"{_endpointHost}/{request.GetEndpoint()}&{_apiKey}", cancellationToken).ConfigureAwait(false)
						?? new List<Trip>(0);

			return new ListTrips.Response {
				Status = ResponseStatus.Succeeded,
				Trips = trips
			};
		} catch (TaskCanceledException) {
			return ListTrips.TimedOut;
		} catch {
			return ListTrips.Failed;
		}
	}

	/// <summary>
	/// Returns a list of users.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>An instance of <c>ListUsers.Response</c>.</returns>
	public Task<ListUsers.Response> ListUsersAsync(
		CancellationToken cancellationToken = default) => ListUsersAsync(new ListUsers.Request(), cancellationToken);

	/// <summary>
	/// Returns a list of users.
	/// </summary>
	/// <param name="request">An instance of <c>ListUsers.Request</c> containing the request's parameters.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>An instance of <c>ListUsers.Response</c>.</returns>
	public async Task<ListUsers.Response> ListUsersAsync(
		ListUsers.Request request,
		CancellationToken cancellationToken = default) {
		if (cancellationToken.IsCancellationRequested) {
			return ListUsers.Cancelled;
		}

		try {
			var users = await _httpClient.GetFromJsonAsync<IList<User>>($"{_endpointHost}/{request.GetEndpoint()}&{_apiKey}", cancellationToken).ConfigureAwait(false)
						?? new List<User>(0);

			return new ListUsers.Response {
				Status = ResponseStatus.Succeeded,
				Users = users
			};
		} catch (TaskCanceledException) {
			return ListUsers.TimedOut;
		} catch {
			return ListUsers.Failed;
		}
	}

	/// <summary>
	/// Returns a list of vehicles.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>An instance of <c>ListVehicles.Response</c>.</returns>
	public Task<ListVehicles.Response> ListVehiclesAsync(
		CancellationToken cancellationToken = default) => ListVehiclesAsync(new ListVehicles.Request(), cancellationToken);

	/// <summary>
	/// Returns a list of vehicles.
	/// </summary>
	/// <param name="request">An instance of <c>ListVehicles.Request</c> containing the request's parameters.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>An instance of <c>ListVehicles.Response</c>.</returns>
	public async Task<ListVehicles.Response> ListVehiclesAsync(
		ListVehicles.Request request,
		CancellationToken cancellationToken = default) {
		if (cancellationToken.IsCancellationRequested) {
			return ListVehicles.Cancelled;
		}

		try {
			var vehicles = await _httpClient.GetFromJsonAsync<IList<Vehicle>>($"{_endpointHost}/{request.GetEndpoint()}&{_apiKey}", cancellationToken).ConfigureAwait(false)
						   ?? new List<Vehicle>(0);

			return new ListVehicles.Response {
				Status = ResponseStatus.Succeeded,
				Vehicles = vehicles
			};
		} catch (TaskCanceledException) {
			return ListVehicles.TimedOut;
		} catch {
			return ListVehicles.Failed;
		}
	}
}
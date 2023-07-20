using System;
using System.Threading;
using System.Threading.Tasks;

#nullable enable

namespace Arex388.Carmine;

/// <summary>
/// Carmine.io API client.
/// </summary>
public interface ICarmineClient {
	/// <summary>
	/// Returns a trip.
	/// </summary>
	/// <param name="id">The trip's id.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>An instance of <c>GetTrip.Response</c>.</returns>
	Task<GetTrip.Response> GetTripAsync(
		TripId id,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns a trip.
	/// </summary>
	/// <param name="request">The trip's id.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>An instance of <c>GetTrip.Response</c>.</returns>
	Task<GetTrip.Response> GetTripAsync(
		GetTrip.Request request,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns a user.
	/// </summary>
	/// <param name="id">The user's id.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>An instance of <c>GetUser.Response</c>.</returns>
	Task<GetUser.Response> GetUserAsync(
		UserId id,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns a user.
	/// </summary>
	/// <param name="request">An instance of <c>GetUser.Request</c> containing the request's parameters.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>An instance of <c>GetUser.Response</c>.</returns>
	Task<GetUser.Response> GetUserAsync(
		GetUser.Request request,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns a vehicle.
	/// </summary>
	/// <param name="id">The vehicle's id.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>An instance of <c>GetVehicle.Response</c>.</returns>
	Task<GetVehicle.Response> GetVehicleAsync(
		VehicleId id,
		CancellationToken cancellationToken = default);

	/// <summary>
	///	Returns a vehicle.
	/// </summary>
	/// <param name="request">An instance of <c>GetVehicle.Request</c> containing the request's parameters.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>An instance of <c>GetVehicle.Response</c>.</returns>
	Task<GetVehicle.Response> GetVehicleAsync(
		GetVehicle.Request request,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns a list of trips.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>An instance of <c>ListTrips.Response</c>.</returns>
	Task<ListTrips.Response> ListTripsAsync(
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns a list of trips.
	/// </summary>
	/// <param name="request">An instance of <c>ListTrips.Request</c> containing the request's parameters.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>An instance of <c>ListTrips.Response</c>.</returns>
	Task<ListTrips.Response> ListTripsAsync(
		ListTrips.Request request,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns a list of users.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>An instance of <c>ListUsers.Response</c>.</returns>
	Task<ListUsers.Response> ListUsersAsync(
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns a list of users.
	/// </summary>
	/// <param name="request">An instance of <c>ListUsers.Request</c> containing the request's parameters.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>An instance of <c>ListUsers.Response</c>.</returns>
	Task<ListUsers.Response> ListUsersAsync(
		ListUsers.Request request,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns a list of vehicles.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>An instance of <c>ListVehicles.Response</c>.</returns>
	Task<ListVehicles.Response> ListVehiclesAsync(
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns a list of vehicles.
	/// </summary>
	/// <param name="request">An instance of <c>ListVehicles.Request</c> containing the request's parameters.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>An instance of <c>ListVehicles.Response</c>.</returns>
	Task<ListVehicles.Response> ListVehiclesAsync(
		ListVehicles.Request request,
		CancellationToken cancellationToken = default);
}
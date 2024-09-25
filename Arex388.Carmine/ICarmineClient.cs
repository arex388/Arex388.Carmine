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
	/// <returns>The <see cref="GetTrip.Response"/> response.</returns>
	Task<GetTrip.Response> GetTripAsync(
		TripId id,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns a trip.
	/// </summary>
	/// <param name="request">The <see cref="GetTrip.Request"/>> request.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The <see cref="GetTrip.Response"/> response.</returns>
	Task<GetTrip.Response> GetTripAsync(
		GetTrip.Request request,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns a user.
	/// </summary>
	/// <param name="id">The user's id.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The <see cref="GetUser.Response"/> response.</returns>
	Task<GetUser.Response> GetUserAsync(
		UserId id,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns a user.
	/// </summary>
	/// <param name="request">The <see cref="GetUser.Request"/> request.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The <see cref="GetUser.Response"/> response.</returns>
	Task<GetUser.Response> GetUserAsync(
		GetUser.Request request,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns a vehicle.
	/// </summary>
	/// <param name="id">The vehicle's id.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The <see cref="GetVehicle.Response"/> response.</returns>
	Task<GetVehicle.Response> GetVehicleAsync(
		VehicleId id,
		CancellationToken cancellationToken = default);

	/// <summary>
	///	Returns a vehicle.
	/// </summary>
	/// <param name="request">The <see cref="GetVehicle.Request"/> request.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The <see cref="GetVehicle.Response"/> response.</returns>
	Task<GetVehicle.Response> GetVehicleAsync(
		GetVehicle.Request request,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns a list of trips.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The <see cref="ListTrips.Response"/> response.</returns>
	Task<ListTrips.Response> ListTripsAsync(
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns a list of trips.
	/// </summary>
	/// <param name="request">The <see cref="ListTrips.Request"/> request.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The <see cref="ListTrips.Response"/> response.</returns>
	Task<ListTrips.Response> ListTripsAsync(
		ListTrips.Request request,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns a list of users.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The <see cref="ListUsers.Response"/> response.</returns>
	Task<ListUsers.Response> ListUsersAsync(
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns a list of users.
	/// </summary>
	/// <param name="request">The <see cref="ListUsers.Request"/> request.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The <see cref="ListUsers.Response"/> response.</returns>
	Task<ListUsers.Response> ListUsersAsync(
		ListUsers.Request request,
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns a list of vehicles.
	/// </summary>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The <see cref="ListVehicles.Response"/> response.</returns>
	Task<ListVehicles.Response> ListVehiclesAsync(
		CancellationToken cancellationToken = default);

	/// <summary>
	/// Returns a list of vehicles.
	/// </summary>
	/// <param name="request">The <see cref="ListVehicles.Request"/> request.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>The <see cref="ListVehicles.Response"/> response.</returns>
	Task<ListVehicles.Response> ListVehiclesAsync(
		ListVehicles.Request request,
		CancellationToken cancellationToken = default);
}
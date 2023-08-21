namespace Arex388.Carmine;

/// <summary>
/// <c>ICarmineClient</c> extensions.
/// </summary>
public static class CarmineClientExtensions {
	/// <summary>
	/// Returns an active vehicle with the specified VIN.
	/// </summary>
	/// <param name="carmine">An instance of <c>CarmineClient</c>.</param>
	/// <param name="vin">The vehicle's VIN.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>An instance of <c>Vehicle</c>.</returns>
	public static async Task<Vehicle?> GetActiveVehicleAsync(
		this ICarmineClient carmine,
		string vin,
		CancellationToken cancellationToken) {
		var response = await carmine.ListVehiclesAsync(new ListVehicles.Request {
			Search = vin,
			Status = VehicleStatus.Active
		}, cancellationToken).ConfigureAwait(false);

		return response.Vehicles.FirstOrDefault();
	}

	/// <summary>
	/// Returns a list of all active vehicles.
	/// </summary>
	/// <param name="carmine">An instance of <c>CarmineClient</c>.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A list of <c>Vehicle</c>.</returns>
	public static async Task<IList<Vehicle>> ListActiveVehiclesAsync(
		this ICarmineClient carmine,
		CancellationToken cancellationToken) {
		var response = await carmine.ListVehiclesAsync(new ListVehicles.Request {
			Status = VehicleStatus.Active
		}, cancellationToken).ConfigureAwait(false);

		return response.Vehicles;
	}

	/// <summary>
	/// Returns a list of recently active vehicles.
	/// </summary>
	/// <param name="carmine">An instance of <c>CarmineClient</c>.</param>
	/// <param name="minutes">A negative number indicating how far back to check the <c>LastActivityAtUtc</c>.</param>
	/// <param name="cancellationToken">The cancellation token.</param>
	/// <returns>A list of <c>Vehicle</c>.</returns>
	public static async Task<IList<Vehicle>> ListRecentlyActiveVehiclesAsync(
		this ICarmineClient carmine,
		int minutes,
		CancellationToken cancellationToken = default) {
		var at = DateTime.Now.AddMinutes(minutes);
		var response = await carmine.ListVehiclesAsync(cancellationToken).ConfigureAwait(false);

		return response.Vehicles.Where(
			v => v.LastActivityAt >= at).ToList();
	}
}
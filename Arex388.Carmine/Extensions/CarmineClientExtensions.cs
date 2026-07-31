namespace Arex388.Carmine;

/// <summary>
/// <c>ICarmineClient</c> extensions.
/// </summary>
public static class CarmineClientExtensions {
    /// <param name="carmine">An instance of <c>CarmineClient</c>.</param>
    extension(
        ICarmineClient carmine) {
            /// <summary>
            /// Returns an active vehicle with the specified VIN.
            /// </summary>
            /// <param name="vin">The vehicle's VIN.</param>
            /// <param name="cancellationToken">The cancellation token.</param>
            /// <returns>An instance of <c>Vehicle</c>.</returns>
            public async Task<Vehicle?> GetActiveVehicleAsync(
                string vin,
                CancellationToken cancellationToken = default) {
            var response = await carmine.ListVehiclesAsync(new ListVehicles.Request {
                Search = vin,
                Status = VehicleStatus.Active
            }, cancellationToken).ConfigureAwait(false);

            return response.Vehicles.FirstOrDefault();
        }

        /// <summary>
        /// Returns a list of all active vehicles.
        /// </summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A list of <c>Vehicle</c>.</returns>
        public async Task<IList<Vehicle>> ListActiveVehiclesAsync(CancellationToken cancellationToken = default) {
            var response = await carmine.ListVehiclesAsync(new ListVehicles.Request {
                Status = VehicleStatus.Active
            }, cancellationToken).ConfigureAwait(false);

            return response.Vehicles;
        }

        /// <summary>
        /// Returns a list of recently active vehicles.
        /// </summary>
        /// <param name="minutes">How many minutes back to check the <c>LastActivityAt</c>. Either sign is accepted.</param>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>A list of <c>Vehicle</c>.</returns>
        public async Task<IList<Vehicle>> ListRecentlyActiveVehiclesAsync(
            int minutes,
            CancellationToken cancellationToken = default) {
            var at = DateTime.UtcNow.AddMinutes(-Math.Abs(minutes));
            var response = await carmine.ListVehiclesAsync(cancellationToken).ConfigureAwait(false);

            return response.Vehicles.Where(
                v => v.LastActivityAt >= at).ToList();
        }
    }
}
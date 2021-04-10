namespace Arex388.Carmine {
    /// <summary>
    /// Location type.
    /// </summary>
    public enum LocationType :
        byte {
        /// <summary>
        /// Cache.
        /// </summary>
        Cache,

        /// <summary>
        /// Geofence.
        /// </summary>
        Geofence,

        /// <summary>
        /// Google cache.
        /// </summary>
        GoogleCache,

        /// <summary>
        /// Point of interest.
        /// </summary>
        PointOfInterest
    }
}
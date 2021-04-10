using Newtonsoft.Json;
using System;

namespace Arex388.Carmine {
    /// <summary>
    /// Location object.
    /// </summary>
    public sealed class Location {
        /// <summary>
        /// The location's address.
        /// </summary>
        public string Address { get; set; }

        [JsonProperty("category")]
        private string CategoryInternal { get; set; }

        /// <summary>
        /// The location's category.
        /// </summary>
        public LocationCategory Category => GetCategory();

        /// <summary>
        /// The location's created timestamp in UTC.
        /// </summary>
        [JsonProperty("created")]
        public DateTime CreatedAtUtc { get; set; }

        /// <summary>
        /// The location's creator id.
        /// </summary>
        [JsonProperty("creator")]
        public Guid? CreatedById { get; set; }

        /// <summary>
        /// The location's geometry as well known text.
        /// </summary>
        [JsonProperty("geometry")]
        public string GeometryWkt { get; set; }

        /// <summary>
        /// The location's id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The location's last activity timestamp in UTC.
        /// </summary>
        [JsonProperty("last_activity_time")]
        public DateTime? LastActivityAtUtc { get; set; }

        /// <summary>
        /// The location's last activity driver id.
        /// </summary>
        [JsonProperty("last_activity_driver")]
        public Guid? LastDriverId { get; set; }

        /// <summary>
        /// The location's center latitude.
        /// </summary>
        public decimal Latitude { get; set; }

        /// <summary>
        /// The location's center longitude.
        /// </summary>
        public decimal Longitude { get; set; }

        /// <summary>
        /// The location's name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The location's notes.
        /// </summary>
        public string Notes { get; set; }

        [JsonProperty("type")]
        private string TypeInternal { get; set; }

        /// <summary>
        /// The location's type.
        /// </summary>
        public LocationType Type => GetType();

        /// <summary>
        /// The location's visit count indicating its popularity.
        /// </summary>
        [JsonProperty("popularity")]
        public int VisitedCount { get; set; }

        //  ====================================================================
        //  Utilities
        //  ====================================================================

        private LocationCategory GetCategory() => CategoryInternal switch {
            "business_related_infrastructure" => LocationCategory.BusinessRelatedInfrastructure,
            "company_asset" => LocationCategory.CompanyAsset,
            "company_office" => LocationCategory.CompanyOffice,
            "customer" => LocationCategory.Customer,
            "fuel_station" => LocationCategory.FuelStation,
            "miscellaneous" => LocationCategory.Miscellaneous,
            "parking" => LocationCategory.Parking,
            "private_address" => LocationCategory.PrivateAddress,
            "restaurant" => LocationCategory.Restaurant,
            "service_area" => LocationCategory.ServiceArea,
            "wholesale_or_supplies" => LocationCategory.WholesaleOrSupplies,
            "warehouse" => LocationCategory.Warehouse,
            _ => throw new ArgumentException(nameof(CategoryInternal))
        };

        private new LocationType GetType() => TypeInternal switch {
            "cache" => LocationType.Cache,
            "geofence" => LocationType.Geofence,
            "google_cache" => LocationType.GoogleCache,
            "poi" => LocationType.PointOfInterest,
            _ => throw new ArgumentException(nameof(TypeInternal))
        };
    }
}
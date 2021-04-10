using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Arex388.Carmine {
    /// <summary>
    /// Vehicle object.
    /// </summary>
    public class Vehicle {
        /// <summary>
        /// The vehicle's CO2 emissions per gallon of fuel.
        /// </summary>
        [JsonProperty("carbon_footprint")]
        public decimal? CarbonEmissionsPerGallon { get; set; }

        /// <summary>
        /// The vehicle's created timestamp in UTC.
        /// </summary>
        [JsonProperty("created")]
        public DateTime CreatedAtUtc { get; set; }

        /// <summary>
        /// The vehicle's current driver id.
        /// </summary>
        [JsonProperty("current_driver")]
        public Guid? DriverId { get; set; }

        /// <summary>
        /// Any diagnostic fault codes.
        /// </summary>
        [JsonProperty("fault_codes")]
        public IDictionary<string, string> Faults { get; set; }

        /// <summary>
        /// The vehicle's fuel consumption in kilometers per liter rounded to two decimal places.
        /// </summary>
        public decimal FuelConsumptionInKilometersPerLiter => FuelConsumptionInMetersPerLiter / 1000.0M;

        /// <summary>
        /// The vehicle's fuel consumption in meters per liter.
        /// </summary>
        [JsonProperty("fuel_economy")]
        public int FuelConsumptionInMetersPerLiter { get; set; }

        /// <summary>
        /// The vehicle's fuel consumption in miles per gallon rounded to two decimal places.
        /// </summary>
        public decimal FuelConsumptionInMilesPerGallon => Math.Round(FuelConsumptionInKilometersPerLiter * 2.35M, 2);

        [JsonProperty("fuel_level")]
        private byte FuelLevel { get; set; }

        /// <summary>
        /// The vehicle's current remaining fuel percentage rounded to two decimal places.
        /// </summary>
        public decimal FuelRemaining => Math.Round(FuelLevel / 100.0M, 2);

        /// <summary>
        /// The vehicle's id.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The vehicle's last activity timestamp in UTC.
        /// </summary>
        [JsonProperty("last_activity")]
        public DateTime? LastActivityAtUtc { get; set; }

        /// <summary>
        /// The vehicle's last trip id.
        /// </summary>
        [JsonProperty("last_trip")]
        public Guid? LastTripId { get; set; }

        /// <summary>
        /// The vehicle's current latitude position.
        /// </summary>
        [JsonProperty("current_latitude")]
        public decimal? Latitude { get; set; }

        /// <summary>
        /// The vehicle's current longitude position.
        /// </summary>
        [JsonProperty("current_longitude")]
        public decimal? Longitude { get; set; }

        /// <summary>
        /// The vehicle's manufacturer name.
        /// </summary>
        public string Make { get; set; }

        /// <summary>
        /// The vehicle's model name.
        /// </summary>
        public string Model { get; set; }

        /// <summary>
        /// Notes for the vehicle.
        /// </summary>
        public string Notes { get; set; }

        /// <summary>
        /// The vehicle's odometer reading in kilometers rounded to two decimal places.
        /// </summary>
        public decimal OdometerInKilometers => Math.Round(OdometerInMeters / 1000.0M, 2);

        /// <summary>
        /// The vehicle's odometer reading in meters.
        /// </summary>
        [JsonProperty("odometer")]
        public int OdometerInMeters { get; set; }

        /// <summary>
        /// The vehicle's odometer reading in miles rounded to two decimal places.
        /// </summary>
        public decimal OdometerInMiles => Math.Round(OdometerInMeters / 1609.0M, 2);

        /// <summary>
        /// The vehicle's registration number.
        /// </summary>
        [JsonProperty("license_plate")]
        public string Registration { get; set; }

        /// <summary>
        /// The vehicle's current status.
        /// </summary>
        public VehicleStatus Status { get; set; }

        /// <summary>
        /// The vehicle's VIN number.
        /// </summary>
        public string Vin { get; set; }

        /// <summary>
        /// The vehicle's manufacturing year.
        /// </summary>
        public short Year { get; set; }
    }
}
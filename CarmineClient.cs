using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Arex388.Carmine {
	public sealed class CarmineClient {
		private static readonly JsonSerializerSettings JsonSerializerSettings = new JsonSerializerSettings {
			DateFormatHandling = DateFormatHandling.IsoDateFormat,
			DateTimeZoneHandling = DateTimeZoneHandling.Utc
		};

		private HttpClient Client { get; }
		private string Key { get; }

		public CarmineClient(
			HttpClient client,
			string key) {
			Client = client ?? throw new ArgumentNullException(nameof(client));
			Key = key ?? throw new ArgumentNullException(nameof(key));
		}

		public async Task<TripResponse> GetTripAsync(
			string id,
			string language = Languages.English) {
			return await GetTripAsync(new TripRequest {
				Id = id,
				Language = language
			});
		}

		public async Task<TripResponse> GetTripAsync(
			TripRequest request) {
			if (request == null) {
				return null;
			}

			var response = await GetResponseAsync(request);

			return JsonConvert.DeserializeObject<TripResponse>(response, JsonSerializerSettings);
		}

		public async Task<IEnumerable<TripResponse>> GetTripsAsync(
			DateTime? startedAtUtc = null,
			DateTime? endedAtUtc = null,
			IEnumerable<string> vehicleIds = null,
			IEnumerable<string> driverIds = null,
			int limit = 100,
			string language = Languages.English) {
			var request = new TripsRequest {
				EndedAtUtc = endedAtUtc,
				Language = language,
				Limit = limit,
				StartedAtUtc = startedAtUtc
			};

			if (driverIds != null) {
				request.DriverIds = driverIds;
			}

			if (vehicleIds != null) {
				request.VehicleIds = vehicleIds;
			}

			return await GetTripsAsync(request);
		}

		public async Task<IEnumerable<TripResponse>> GetTripsAsync(
			TripsRequest request) {
			if (request == null) {
				return null;
			}

			var response = await GetResponseAsync(request);

			return JsonConvert.DeserializeObject<IEnumerable<TripResponse>>(response, JsonSerializerSettings);
		}

		public async Task<UserResponse> GetUserAsync(
			string id,
			string language = Languages.English) {
			return await GetUserAsync(new UserRequest {
				Id = id,
				Language = language
			});
		}

		public async Task<UserResponse> GetUserAsync(
			UserRequest request) {
			if (request == null) {
				return null;
			}

			var response = await GetResponseAsync(request);

			return JsonConvert.DeserializeObject<UserResponse>(response, JsonSerializerSettings);
		}

		public async Task<IEnumerable<UserResponse>> GetUsersAsync(
			string name = null,
			string search = null,
			bool? active = null,
			IEnumerable<string> roles = null,
			string language = Languages.English) {
			var request = new UsersRequest {
				Active = active,
				Language = language,
				Name = name,
				Search = search
			};

			if (roles != null) {
				request.Roles = roles;
			}

			return await GetUsersAsync(request);
		}

		public async Task<IEnumerable<UserResponse>> GetUsersAsync(
			UsersRequest request) {
			if (request == null) {
				return null;
			}

			var response = await GetResponseAsync(request);

			return JsonConvert.DeserializeObject<IEnumerable<UserResponse>>(response, JsonSerializerSettings);
		}

		public async Task<VehicleResponse> GetVehicleAsync(
			string id,
			string language = Languages.English) {
			return await GetVehicleAsync(new VehicleRequest {
				Id = id,
				Language = language
			});
		}

		public async Task<VehicleResponse> GetVehicleAsync(
			VehicleRequest request) {
			if (request == null) {
				return null;
			}

			var response = await GetResponseAsync(request);

			return JsonConvert.DeserializeObject<VehicleResponse>(response, JsonSerializerSettings);
		}

		public async Task<IEnumerable<VehicleResponse>> GetVehiclesAsync(
			string search = null,
			string status = null,
			string language = Languages.English) {
			return await GetVehiclesAsync(new VehiclesRequest {
				Search = search,
				Status = status,
				Language = language
			});
		}

		public async Task<IEnumerable<VehicleResponse>> GetVehiclesAsync(
			VehiclesRequest request) {
			if (request == null) {
				return null;
			}

			var response = await GetResponseAsync(request);

			return JsonConvert.DeserializeObject<IEnumerable<VehicleResponse>>(response, JsonSerializerSettings);
		}

		public async Task<IEnumerable<WaypointResponse>> GetWaypointsAsync(
			string tripId,
			string language = Languages.English) {
			return await GetWaypointsAsync(new WaypointsRequest {
				Language = language,
				TripId = tripId
			});
		}

		public async Task<IEnumerable<WaypointResponse>> GetWaypointsAsync(
			WaypointsRequest request) {
			if (request == null) {
				return null;
			}

			var response = await GetResponseAsync(request);

			return JsonConvert.DeserializeObject<IEnumerable<WaypointResponse>>(response, JsonSerializerSettings);
		}

		private async Task<string> GetResponseAsync(
			RequestBase request) {
			var endpoint = $"{request.Endpoint}&api_key={Key}";

			try {
				var response = await Client.GetAsync(endpoint);

				return await response.Content.ReadAsStringAsync();
			} catch (HttpRequestException) {
				return null;
			}
		}
	}
}
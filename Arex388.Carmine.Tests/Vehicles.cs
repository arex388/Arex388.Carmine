﻿using Xunit;

namespace Arex388.Carmine.Tests;

public sealed class Vehicles {
	private readonly ICarmineClient _carmine = new CarmineClient(Config.CarmineKey1, new HttpClient());

	[Fact]
	public async Task GetAsync() {
		var list = await _carmine.ListVehiclesAsync();
		var vehicle = list.Vehicles.First();

		var response = await _carmine.GetVehicleAsync(vehicle.Id);

		Assert.Equal(ResponseStatus.Succeeded, response.Status);
		Assert.NotNull(response.Vehicle);
	}

	[Fact]
	public async Task ListAsync() {
		var response = await _carmine.ListVehiclesAsync();

		Assert.Equal(ResponseStatus.Succeeded, response.Status);
		Assert.NotEmpty(response.Vehicles);
	}
}
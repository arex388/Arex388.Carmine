﻿using Xunit;

namespace Arex388.Carmine.Tests;

public sealed class Users {
	private readonly ICarmineClient _carmine = new CarmineClient(Config.CarmineKey1, new HttpClient());

	[Fact]
	public async Task GetAsync() {
		var list = await _carmine.ListUsersAsync();
		var user = list.Users.First();

		var response = await _carmine.GetUserAsync(user.Id);

		Assert.Equal(ResponseStatus.Succeeded, response.Status);
		Assert.NotNull(response.User);
	}

	[Fact]
	public async Task ListAsync() {
		var response = await _carmine.ListUsersAsync();

		Assert.Equal(ResponseStatus.Succeeded, response.Status);
		Assert.NotEmpty(response.Users);
	}
}
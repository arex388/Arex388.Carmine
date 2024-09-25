using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;

namespace Arex388.Carmine.Tests;

public sealed class UsersTests {
	private readonly ITestOutputHelper _console;
	private readonly ICarmineClient _carmine;

	public UsersTests(
		ITestOutputHelper console) {
		var services = new ServiceCollection().AddCarmine(new CarmineClientOptions {
			Key = Config.Key1
		}).BuildServiceProvider();

		_carmine = services.GetRequiredService<ICarmineClient>();
		_console = console;
	}

	[Fact]
	public async Task Get_Succeeds() {
		//	========================================================================
		//	Arrange
		//	========================================================================

		var list = await _carmine.ListUsersAsync();
		var user = list.Users.First();

		//	========================================================================
		//	Act
		//	========================================================================

		var getUser = await _carmine.GetUserAsync(user.Id);

		_console.WriteLineWithHeader(nameof(getUser), getUser);

		//	========================================================================
		//	Assert
		//	========================================================================

		getUser.Errors.Should().BeEmpty();
		getUser.Success.Should().BeTrue();
		getUser.User.Should().NotBeNull();
	}

	[Fact]
	public async Task List_Succeeds() {
		//	========================================================================
		//	Arrange
		//	========================================================================

		//	========================================================================
		//	Act
		//	========================================================================

		var listUsers = await _carmine.ListUsersAsync();

		_console.WriteLineWithHeader(nameof(listUsers), listUsers);

		//	========================================================================
		//	Assert
		//	========================================================================

		listUsers.Errors.Should().BeEmpty();
		listUsers.Success.Should().BeTrue();
		listUsers.Users.Should().NotBeEmpty();
	}
}
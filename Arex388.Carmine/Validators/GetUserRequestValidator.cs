using FluentValidation;

#nullable enable

namespace Arex388.Carmine.Validators;

internal sealed class GetUserRequestValidator :
	AbstractValidator<GetUser.Request> {
	public GetUserRequestValidator() {
		RuleFor(_ => _.Id).NotEmpty();
	}
}
using FluentValidation;

namespace Arex388.Carmine.Validators;

internal sealed class GetUserRequestValidator :
	AbstractValidator<GetUser.Request> {
	public GetUserRequestValidator() {
		RuleFor(r => r.Id).NotEmpty();
	}
}
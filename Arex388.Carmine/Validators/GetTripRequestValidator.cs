using FluentValidation;

namespace Arex388.Carmine.Validators;

internal sealed class GetTripRequestValidator :
	AbstractValidator<GetTrip.Request> {
	public GetTripRequestValidator() {
		RuleFor(r => r.Id).NotEmpty();
	}
}
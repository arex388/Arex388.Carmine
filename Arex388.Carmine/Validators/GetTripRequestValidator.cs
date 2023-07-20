using FluentValidation;

#nullable enable

namespace Arex388.Carmine.Validators;

internal sealed class GetTripRequestValidator :
	AbstractValidator<GetTrip.Request> {
	public GetTripRequestValidator() {
		RuleFor(_ => _.Id).NotEmpty();
	}
}
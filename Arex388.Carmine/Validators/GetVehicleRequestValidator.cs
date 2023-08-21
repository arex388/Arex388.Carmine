using FluentValidation;

namespace Arex388.Carmine.Validators;

internal sealed class GetVehicleRequestValidator :
	AbstractValidator<GetVehicle.Request> {
	public GetVehicleRequestValidator() {
		RuleFor(r => r.Id).NotEmpty();
	}
}
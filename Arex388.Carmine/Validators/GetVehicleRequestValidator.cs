using FluentValidation;

#nullable enable

namespace Arex388.Carmine.Validators;

internal sealed class GetVehicleRequestValidator :
	AbstractValidator<GetVehicle.Request> {
	public GetVehicleRequestValidator() {
		RuleFor(_ => _.Id).NotEmpty();
	}
}
using Application.Services.Shipments.Requests;
using FluentValidation;

namespace LogisticsShipmentTrackingSystemWebApi.Validators;

public class ShipmentRequestValidator : AbstractValidator<ShipmentRequest>
{
    public ShipmentRequestValidator()
    {
       
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Name cannot be empty.");

      
        RuleFor(x => x.DeliveryState)
            .IsInEnum().WithMessage("DeliveryState must be a valid enum value.");

        RuleFor(x => x.DeliveryDate)
            .GreaterThan(DateTime.Now).WithMessage("DeliveryDate must be later than the current date and time.");
    }
}

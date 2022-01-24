using FluentValidation;

namespace ApiAsp.services.Validations
{
    public class OrderValidator : AbstractValidator<ApiAsp.Models.Entitys.OrderModel>
    {
        public OrderValidator()
        {
            RuleFor(x => x.ShipName)
                .NotEmpty()
                .NotNull();
            RuleFor(x => x.total).GreaterThan(0);
        }
    }
}

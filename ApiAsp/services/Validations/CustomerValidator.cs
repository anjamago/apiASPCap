using FluentValidation;

namespace ApiAsp.services.Validations
{
    public class CustomerValidator:AbstractValidator<ApiAsp.Models.Entitys.Customer>
    {
        public CustomerValidator()
        {
            RuleFor(r => r.CustomerId)
                .NotNull()
                .WithMessage("El codigo del cliente no puede ser vacio o nulo");
            RuleFor(r => r.Address).NotNull().When(c => c.PostalCode != null);

            RuleForEach(r => r.Order).
                Where(w=> w.CustomerId != null)
                .SetValidator(new OrderValidator());
                
        }
    }
}

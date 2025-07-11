using FluentValidation;
using MPago.Application.DTOs;

namespace MPago.Application.Validations
{
    public class AgregarMPagoDtoValidator : AbstractValidator<AgregarMPagoDto>
    {
        public AgregarMPagoDtoValidator()
        {
            RuleFor(x => x.IdPostor)
                .NotEmpty().WithMessage("El Id del Postor es obligatorio.")
                .Length(1, 50).WithMessage("El Id del Postor debe tener entre 1 y 50 caracteres.");

            RuleFor(x => x.IdMPagoStripe)
                .NotEmpty().WithMessage("El Id de MPago en Stripe es obligatorio.")
                .Length(1, 50).WithMessage("El Id de MPago en Stripe debe tener entre 1 y 50 caracteres.")
                .Must(id => id.StartsWith("pm_")).WithMessage("El Id del MPago Stripe debe comenzar con 'pm_'.");

            RuleFor(x => x.Marca)
                .NotEmpty().WithMessage("La Marca de la tarjeta es obligatoria.")
                .Length(1, 25).WithMessage("La Marca de la tarjeta debe tener entre 1 y 25 caracteres.");


            RuleFor(x => x.MesExpiracion)
                .InclusiveBetween(1, 12).WithMessage("El Mes de expiración debe estar entre 1 y 12.");

            RuleFor(x => x.AnioExpiracion)
                .GreaterThanOrEqualTo(DateTime.Now.Year).WithMessage("El Año de expiración no puede ser anterior al año actual.");

            RuleFor(x => x.FechaRegistro)
                .LessThanOrEqualTo(DateTime.Now).WithMessage("La Fecha de registro no puede ser futura.");

            /*RuleFor(x => x.Predeterminado)
                .NotNull().WithMessage("El campo Predeterminado es obligatorio.");*/
        }
    }
}

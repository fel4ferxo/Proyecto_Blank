
namespace Tareo.Transversal
{
    using FluentValidation;
    using Tareo.Aplicacion.Dto;

    public class ItemDtoValidator : AbstractValidator<ItemDto>
    {
        public ItemDtoValidator()
        {
            RuleFor(x => x.Id)
                .NotNull().WithMessage("El campo Id no puede ser nulo.")
                .NotEmpty().WithMessage("El campo Id no puede estar vacío.");

            RuleFor(x => x.RowVersion)
                .NotNull().WithMessage("El campo RowVersion no puede ser nulo.")
                .NotEmpty().WithMessage("El campo RowVersion no puede estar vacío.");
        }
    }
}

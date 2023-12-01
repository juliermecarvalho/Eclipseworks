using Eclipseworks.Api.Models;
using FluentValidation;

namespace Eclipseworks.Api.Validation;

public class UsuarioModelValidator : AbstractValidator<UsuarioModel>
{
    public UsuarioModelValidator()
    {

        RuleFor(x => x.Nome)
            .NotNull()
            .WithMessage("O campo {PropertyName} é obrigatório")
            .NotEmpty()
            .WithMessage("O campo {PropertyName} é obrigatório");



    }
}
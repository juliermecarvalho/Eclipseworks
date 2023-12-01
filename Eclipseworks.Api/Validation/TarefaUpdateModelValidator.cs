using Eclipseworks.Api.Models;
using FluentValidation;

namespace Eclipseworks.Api.Validation;

public class TarefaUpdateModelValidator : AbstractValidator<TarefaUpdateModel>
{
    public TarefaUpdateModelValidator()
    {
        RuleFor(x => x.Status)
            .InclusiveBetween(0, 2)
            .WithMessage("O valor do {PropertyName} deve estar entre 0 e 2.");
        
    }
}
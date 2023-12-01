using Eclipseworks.Api.Models;
using FluentValidation;

namespace Eclipseworks.Api.Validation;

public class TarefaModelValidator : AbstractValidator<TarefaModel>
{
    public TarefaModelValidator()
    {

        RuleFor(x => x.Titulo)
            .NotNull()
            .WithMessage("O campo {PropertyName} é obrigatório")
            .NotEmpty()
            .WithMessage("O campo {PropertyName} é obrigatório");

        RuleFor(x => x.Descricao)
            .Length(0, 250)
            .WithMessage("O campo {PropertyName} é tem tamanho máximo de 250")
            .NotNull()
            .WithMessage("O campo {PropertyName} é obrigatório")
            .NotEmpty()
            .WithMessage("O campo {PropertyName} é obrigatório");

        RuleFor(x => x.Detalhes)
            .Length(0, 500)
            .WithMessage("O campo {PropertyName} é tem tamanho máximo de 500")
            .NotNull()
            .WithMessage("O campo {PropertyName} é obrigatório")
            .NotEmpty()
            .WithMessage("O campo {PropertyName} é obrigatório");

        RuleFor(x => x.DataVencimento)
            .NotNull()
            .WithMessage("O campo {PropertyName} é obrigatório");

        RuleFor(x => x.Status )
            .InclusiveBetween(0, 2)
            .WithMessage("O valor do {PropertyName} deve estar entre 0 e 2.");

        RuleFor(x => x.Prioridade)
            .InclusiveBetween(0, 2)
            .WithMessage("O valor do {PropertyName} deve estar entre 0 e 2.");

        RuleFor(x => x.ProjetoId)
            .GreaterThan(0)
            .WithMessage("{PropertyName} deve ser maior que zero");

    }
}
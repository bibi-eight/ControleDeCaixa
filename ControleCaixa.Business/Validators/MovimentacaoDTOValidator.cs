using ControleCaixa.Business.Dtos;
using FluentValidation;

namespace ControleCaixa.Business.Validators;

public class MovimentacaoDTOValidator : AbstractValidator<MovimentacaoDTO>
{
    public MovimentacaoDTOValidator()
    {
        RuleFor(x => x.CaixaId)
            .GreaterThan(0)
            .WithMessage("O caixa é obrigatório.");

        RuleFor(x => x.Descricao)
            .NotEmpty()
            .WithMessage("A descrição é obrigatória.")
            .MaximumLength(200)
            .WithMessage("A descrição deve ter no máximo 200 caracteres.");

        RuleFor(x => x.Tipo)
            .IsInEnum()
            .WithMessage("O tipo da movimentação é inválido.");

        RuleFor(x => x.Categoria)
            .IsInEnum()
            .WithMessage("A categoria é inválida.");

        RuleFor(x => x.Valor)
            .GreaterThan(0)
            .WithMessage("O valor deve ser maior que zero.");
    }
}
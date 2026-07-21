using ControleCaixa.Business.Dtos;
using FluentValidation;

namespace ControleCaixa.Business.Validators;

public class CaixaDTOValidator : AbstractValidator<CaixaDTO>
{
    public CaixaDTOValidator()
    {
        RuleFor(x => x.Nome)
            .NotEmpty()
            .WithMessage("O nome do caixa é obrigatório.")
            .MaximumLength(100)
            .WithMessage("O nome deve ter no máximo 100 caracteres.");

        RuleFor(x => x.SaldoMinimo)
            .GreaterThanOrEqualTo(0)
            .WithMessage("O saldo mínimo não pode ser negativo.");
    }
}
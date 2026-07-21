using ControleCaixa.Business;
using ControleCaixa.Business.Dtos;
using ControleCaixa.Business.Interfaces;
using ControleCaixa.Business.Services;
using ControleCaixa.Business.Validators;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

public static class BusinessDependencyInjection
{
    public static IServiceCollection AddBusiness(
        this IServiceCollection services)
    {
        services.AddScoped<ICaixaService, CaixaService>();
        services.AddScoped<IMovimentacaoService, MovimentacaoService>();

        services.AddScoped<IValidator<MovimentacaoDTO>, MovimentacaoDTOValidator>();
        services.AddScoped<IValidator<CaixaDTO>, CaixaDTOValidator>();

        return services;
    }
}
using ControleCaixa.Data.Context;
using ControleCaixa.Data.Interfaces;
using ControleCaixa.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ControleCaixa.Data.DependencyInjection;

public static class DataDependencyInjection
{
    public static IServiceCollection AddData(
        this IServiceCollection services,
        string connectionString)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<IMovimentacaoRepository, MovimentacaoRepository>();
        services.AddScoped<ICaixaRepository, CaixaRepository>();

        services.AddScoped<IUnitOfWork>(provider =>
            provider.GetRequiredService<AppDbContext>());

        return services;
    }
}
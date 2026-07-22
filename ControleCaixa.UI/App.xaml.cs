using System.Windows;
using ControleCaixa.Business.Dtos;
using ControleCaixa.Business.Interfaces;
using ControleCaixa.Business.Services;
using ControleCaixa.Business.Validators;
using ControleCaixa.Data;
using ControleCaixa.Data.Context;
using ControleCaixa.Data.Interfaces;
using ControleCaixa.Data.Repositories;
using ControleCaixa.UI.ViewModels;
using ControleCaixa.UI.Views;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ControleCaixa.UI;

public partial class App : Application
{
    private ServiceProvider? _serviceProvider;

    protected override void OnStartup(StartupEventArgs e)
    {
        base.OnStartup(e);

        var configuration = new ConfigurationBuilder()
            .SetBasePath(AppContext.BaseDirectory)
            .AddJsonFile(
                "appsettings.json",
                optional: false,
                reloadOnChange: true)
            .Build();

        var connectionString =
            configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException(
                "A connection string 'DefaultConnection' não foi encontrada.");

        var services = new ServiceCollection();

        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));

        services.AddScoped<ICaixaRepository, CaixaRepository>();
        services.AddScoped<IMovimentacaoRepository, MovimentacaoRepository>();
        
        services.AddScoped<ICaixaService, CaixaService>();
        services.AddScoped<IMovimentacaoService, MovimentacaoService>();
        
        services.AddScoped<IUnitOfWork>(provider =>
            provider.GetRequiredService<AppDbContext>());

        services.AddTransient<IValidator<CaixaDTO>, CaixaDTOValidator>();
        services.AddTransient<IValidator<MovimentacaoDTO>, MovimentacaoDTOValidator>();
        

        services.AddScoped<IUnitOfWork>(provider =>
            provider.GetRequiredService<AppDbContext>());

        services.AddTransient<CaixaView>();
        services.AddTransient<CaixaViewModel>();
        
        _serviceProvider = services.BuildServiceProvider();

        var caixaView =
            _serviceProvider.GetRequiredService<CaixaView>();

        caixaView.Show();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _serviceProvider?.Dispose();

        base.OnExit(e);
    }
}
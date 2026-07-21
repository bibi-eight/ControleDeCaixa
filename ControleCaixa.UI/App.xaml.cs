using System.Windows;
using ControleCaixa.Data;
using ControleCaixa.Data.Context;
using ControleCaixa.Data.Interfaces;
using ControleCaixa.Data.Repositories;
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

        services.AddScoped<IUnitOfWork>(provider =>
            provider.GetRequiredService<AppDbContext>());

        services.AddTransient<MainWindow>();

        _serviceProvider = services.BuildServiceProvider();

        var mainWindow =
            _serviceProvider.GetRequiredService<MainWindow>();

        mainWindow.Show();
    }

    protected override void OnExit(ExitEventArgs e)
    {
        _serviceProvider?.Dispose();

        base.OnExit(e);
    }
}
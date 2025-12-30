using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System.Windows;
using Serilog;

namespace KNX;

public partial class App
{
    private static readonly CancellationTokenSource s_cancellationTokenSource = new();

    protected override void OnStartup(StartupEventArgs e)
    {
        //////////////////////////////////
        //
        // Debugger.Launch();
        //
        //////////////////////////////////

        base.OnStartup(e);

        Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .Enrich.FromLogContext()
            .WriteTo.File( "Knx.log", rollingInterval: RollingInterval.Day)
            .WriteTo.Debug()
            .CreateLogger();


        var args = Environment.GetCommandLineArgs();

        var builder = Host.CreateApplicationBuilder(args);
        _ = builder.Logging.ClearProviders();
        _ = builder.Logging.AddSerilog();

        _ = builder.Services.AddSingleton(s_cancellationTokenSource);

        _ = builder.Services.AddSingleton<MainWindow>();
        _ = builder.Services.AddSingleton<ViewModel.ViewModel>();
        _ = builder.Services.AddSingleton<Model.Model>();
        _ = builder.Services.AddSingleton<Model.DateienUndOrdner>();

        var host = builder.Build();
        var window = host.Services.GetRequiredService<MainWindow>();

        window.Show();
    }
}

using AutoServis.WorkerApp.Views;
using AutoServis.WorkerApp.ViewModels;
using AutoServis.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.IO;
using System.Windows;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace AutoServis.WorkerApp;

public partial class App : Application
{
    public static string? SelectedTheme { get; set; }

    private IServiceProvider _serviceProvider;

    protected override void OnStartup(StartupEventArgs e)
    {
        try
        {
            var services = new ServiceCollection();

            // Učitavanje konfiguracije
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            services.AddSingleton<IConfiguration>(config);

            // ✅ Učitaj temu iz konfiguracije (ako postoji), inače koristi podrazumevanu
            SelectedTheme = config["SelectedTheme"] ?? "DefaultBlue";

            // Registracija DbContext-a kao TRANSIENT
            services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(
                    config.GetConnectionString("DefaultConnection"),
                    new MySqlServerVersion(new Version(8, 0, 36))
                ),
                contextLifetime: ServiceLifetime.Transient,
                optionsLifetime: ServiceLifetime.Transient
            );

            // ViewModels
            services.AddTransient<RegisterViewModel>();
            services.AddTransient<LoginViewModel>();
            services.AddTransient<DashboardViewModel>();
            services.AddTransient<TipoviUslugeViewModel>();
            services.AddTransient<DijeloviLagerViewModel>();
            services.AddTransient<RezervacijeViewModel>();
            services.AddTransient<UpravljanjeKorisnicimaViewModel>();
            services.AddTransient<KlijentiViewModel>();
            services.AddTransient<KreirajRacunViewModel>();

            // Views
            services.AddTransient<RegisterView>();
            services.AddTransient<LoginView>();
            services.AddTransient<DashboardView>();
            services.AddTransient<TipoviUslugeView>();
            services.AddTransient<DijeloviLagerView>();
            services.AddTransient<RezervacijeView>();
            services.AddTransient<UpravljanjeKorisnicimaView>();
            services.AddTransient<KlijentiView>();
            services.AddTransient<KreirajRacunView>();
            services.AddTransient<MainWindow>();

            _serviceProvider = services.BuildServiceProvider();

            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Greška pri pokretanju aplikacije:\n{ex.Message}\n\nDetalji:\n{ex.InnerException?.Message}",
                            "Kritična greška",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
            Environment.Exit(1);
        }
    }

    public static IServiceProvider Services => ((App)Current)._serviceProvider;
}
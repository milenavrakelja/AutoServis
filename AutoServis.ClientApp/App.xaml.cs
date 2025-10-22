using AutoServis.ClientApp.Views;
using AutoServis.ClientApp.ViewModels;
using AutoServis.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using System;
using System.IO;
using System.Windows;

namespace AutoServis.ClientApp;

public partial class App : Application

{
    public static int? LoggedInKlijentId { get; set; } // ID trenutno prijavljenog klijenta

    private IServiceProvider _serviceProvider;

    public static string? SelectedTheme { get; set; } 


    protected override void OnStartup(StartupEventArgs e)
    {
        try
        {
            var services = new ServiceCollection();

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            services.AddSingleton<IConfiguration>(config);
            services.AddDbContext<AppDbContext>(options =>
                options.UseMySql(
                    config.GetConnectionString("DefaultConnection"),
                    new MySqlServerVersion(new Version(8, 0, 36))
                ), ServiceLifetime.Transient); 

            // ViewModels
            services.AddTransient<LoginViewModel>();
            services.AddTransient<RegisterViewModel>();
            services.AddTransient<TipUslugeViewModel>();
            services.AddTransient<RezervacijeViewModel>();
            services.AddTransient<RacuniViewModel>();
            services.AddTransient<MainMenuViewModel>();


            // Views
            services.AddTransient<MainWindow>();
            services.AddTransient<LoginView>();
            services.AddTransient<RegisterView>();
            services.AddTransient<MainMenuView>();
            services.AddTransient<TipUslugeView>();
            services.AddTransient<RezervacijeView>();
            services.AddTransient<RacuniView>();

            _serviceProvider = services.BuildServiceProvider();

            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Greška pri pokretanju: {ex.Message}");
            Environment.Exit(1);
        }
    }

    public static IServiceProvider Services => ((App)Current)._serviceProvider;
}
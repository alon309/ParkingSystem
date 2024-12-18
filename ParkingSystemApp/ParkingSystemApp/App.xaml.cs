using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using ParkingSystemApp.Authentication;
using ParkingSystemApp.AuthenticationService;
using ParkingSystemApp.AuthenticationService.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace ParkingSystemApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider _serviceProvider;
        protected override void OnStartup(StartupEventArgs e)
        {
            var services = new ServiceCollection();

            // Add logging to the service collection
            services.AddLogging(configure => configure.AddConsole());

            // Register services
            services.AddSingleton<IUserRepository, UserRepository>();
            services.AddSingleton<IAuthService, AuthService>();
            
            // Register the ViewModel and View
            services.AddSingleton<LoginViewModel>();
            services.AddTransient<LoginView>();

            _serviceProvider = services.BuildServiceProvider();

            // Show the login window
            var loginWindow = _serviceProvider.GetRequiredService<LoginView>();
            loginWindow.Show();
            
            base.OnStartup(e);
        }
    }
}

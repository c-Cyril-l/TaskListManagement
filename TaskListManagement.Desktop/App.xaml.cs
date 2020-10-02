using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;
using TaskListManagement.Desktop.IoC;
using TaskListManagement.Desktop.Views;

namespace TaskListManagement.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {

        public static IServiceProvider Provider { get; set; }

        #region Overrides of Application

        protected override void OnStartup(StartupEventArgs e)
        {
            var services = new ServiceCollection();
            services.RegisterServices();
            services.RegisterViewModels();
            services.RegisterViews();

            Provider = services.BuildServiceProvider();

            var service = Provider.GetRequiredService<MainWindow>();
            service.Show();

        }

        public static TService GetRequiredService<TService>()
        {
            return Provider.GetRequiredService<TService>();
        }


        #endregion


    }
}

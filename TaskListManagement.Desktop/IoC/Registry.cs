using Microsoft.Extensions.DependencyInjection;
using TaskListManagement.Desktop.Assets.Icons;
using TaskListManagement.Desktop.Services.Abstractions;
using TaskListManagement.Desktop.Services.Concrete;
using TaskListManagement.Desktop.ViewModels;
using TaskListManagement.Desktop.Views;

namespace TaskListManagement.Desktop.IoC
{
    public static class Registry
    {

        public static void RegisterViewModels(this IServiceCollection services)
        {
            services.AddSingleton<MainViewModel>();
            services.AddSingleton<InfoTaskViewModel>();
            services.AddSingleton<CriticalTaskViewModel>();
            services.AddSingleton<WarningTaskViewModel>();
            services.AddSingleton<SucceedTaskViewModel>();
            services.AddSingleton<SettingViewModel>();
            services.AddSingleton<AboutViewModel>();
        }
        public static void RegisterServices(this IServiceCollection services)
        {
            services.AddSingleton<IFileHelper, FileHelper>();
            services.AddSingleton<ITaskService, TaskService>();
            services.AddSingleton<IJsonSerialize, JsonSerialize>();
            services.AddSingleton<GeometryData>();
            services.AddSingleton<MenuItemService>();
        }

        public static void RegisterViews(this IServiceCollection services)
        {
            services.AddSingleton<MainWindow>();
            services.AddSingleton<InfoTaskView>();
            services.AddSingleton<CriticalTaskView>();
            services.AddSingleton<WarningTaskView>();
            services.AddSingleton<SucceedTaskView>();
            services.AddSingleton<SettingView>();
            services.AddSingleton<AboutView>();
        }

    }
}
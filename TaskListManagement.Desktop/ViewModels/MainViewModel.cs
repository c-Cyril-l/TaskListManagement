using Microsoft.Extensions.DependencyInjection;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Windows;
using System.Windows.Media;
using TaskListManagement.Desktop.Assets.Icons;
using TaskListManagement.Desktop.Commands;
using TaskListManagement.Desktop.Enums;
using TaskListManagement.Desktop.Extensions;
using TaskListManagement.Desktop.Models;
using TaskListManagement.Desktop.Services.Abstractions;

namespace TaskListManagement.Desktop.ViewModels
{
    public class MainViewModel : BaseViewModel
    {

        #region Declarations

        //private MenuItemService ItemService => App.GetRequiredService<MenuItemService>();

        private ITaskService TaskService => App.GetRequiredService<ITaskService>();

        private GeometryData GeometryData => App.GetRequiredService<GeometryData>();


        #endregion

        #region Constructor

        public MainViewModel()
        {
            TodoTasks = new ObservableCollection<TodoTask>();
            MenuItems = new ObservableCollection<MenuItem>();
            MenuItem = new MenuItem();

            InitializeViewModelInstances();



            Navigate(AboutViewModel);
        }

        #endregion

        #region Commands

        public RelayCommand LoadMainCommand
        {
            get
            {
                return new RelayCommand(async (i) =>
                {
                    var tasks = await TaskService.ReadTaskAsync(TaskPath, TodoTasks).ConfigureAwait(true);
                    foreach (var todoTask in tasks)
                    {
                        TodoTasks.Add(todoTask);
                    }

                    AddMenuItems(tasks);

                });
            }
        }

        public RelayCommand ShowAboutCommand
        {
            get
            {
                return new RelayCommand((x) => Navigate(AboutViewModel));
            }
        }

        public RelayCommand ShowSettingsCommand
        {
            get
            {
                return new RelayCommand((x) => Navigate(SettingsViewModel));
            }
        }

        public RelayCommand SelectionChangedCommand
        {
            get
            {
                return new RelayCommand((x) => MenuItemSelectionChanged(MenuItem));
            }
        }

        public RelayCommand CloseApplicationCommand
        {
            get
            {
                return new RelayCommand((x) => Application.Current.Shutdown());
            }
        }

        public RelayCommand SaveTasksCommand
        {
            get
            {
                return new RelayCommand((i) => SaveSettings());
            }
        }

        #endregion

        #region Properties

        public string Title { get; set; } = "TaskList Management";

        public object Content { get; set; }

        public MenuItem MenuItem { get; set; }

        public ObservableCollection<MenuItem> MenuItems { get; set; }

        public ObservableCollection<TodoTask> TodoTasks { get; set; }

        #endregion

        #region ViewModel Instances

        public AboutViewModel AboutViewModel { get; set; }

        public SettingViewModel SettingsViewModel { get; set; }

        public InfoTaskViewModel InfoTaskViewModel { get; set; }

        public WarningTaskViewModel WarningTaskViewModel { get; set; }

        public CriticalTaskViewModel CriticalTaskViewModel { get; set; }

        public SucceedTaskViewModel SucceedTaskViewModel { get; set; }

        #endregion

        #region Methods

        #region Initialize ViewModel Instances

        private void InitializeViewModelInstances()
        {
            InfoTaskViewModel = App.Provider.GetRequiredService<InfoTaskViewModel>();
            CriticalTaskViewModel = App.Provider.GetRequiredService<CriticalTaskViewModel>();
            WarningTaskViewModel = App.Provider.GetRequiredService<WarningTaskViewModel>();
            SucceedTaskViewModel = App.Provider.GetRequiredService<SucceedTaskViewModel>();
            AboutViewModel = App.Provider.GetRequiredService<AboutViewModel>();
            SettingsViewModel = App.Provider.GetRequiredService<SettingViewModel>();
        }

        #endregion

        /// <summary>
        /// Menu navigation item changed so that we reflect to the change.
        /// </summary>
        /// <param name="menuItem">Menu Item to navigate to.</param>
        private void MenuItemSelectionChanged(MenuItem menuItem)
        {
            if (menuItem == null) return;
            switch (menuItem.Type)
            {
                case MenuType.Info:
                    Navigate(InfoTaskViewModel);
                    break;
                case MenuType.Critical:
                    Navigate(CriticalTaskViewModel);
                    break;
                case MenuType.Warning:
                    Navigate(WarningTaskViewModel);
                    break;
                default:
                    Navigate(SucceedTaskViewModel);
                    break;
            }
        }

        /// <summary>
        /// Navigate to specific view model.
        /// </summary>
        /// <param name="taskBaseViewModel"></param>
        private void Navigate(BaseViewModel taskBaseViewModel)
        {
            Content = taskBaseViewModel;
        }

        private void AddMenuItems(ICollection<TodoTask> tasks)
        {
            var infoTasks = tasks.FilterByTaskType(TaskType.Info);
            var criticalTasks = tasks.FilterByTaskType(TaskType.Critical);
            var warningTasks = tasks.FilterByTaskType(TaskType.Warning);
            var succeedTasks = tasks.FilterByTaskType(TaskType.Success);

            AddMenuItem(MenuType.Info, infoTasks.Count());
            AddMenuItem(MenuType.Critical, criticalTasks.Count());
            AddMenuItem(MenuType.Warning, warningTasks.Count());
            AddMenuItem(MenuType.Success, succeedTasks.Count());

        }

        private void AddMenuItem(MenuType type, int taskCount)
        {
            var menuItem = new MenuItem
            {
                Type = type,
                Name = GetMenuItemNameByType(type),
                ForeColor = GetMenuItemColorByType(type),
                IconData = GetMenuItemIconByType(type),
                ItemCount = taskCount
            };
            MenuItems.Add(menuItem);
        }

        private static string GetMenuItemNameByType(MenuType menuType)
        {
            return menuType switch
            {
                MenuType.Info => "Info Tasks",
                MenuType.Critical => "Critical Tasks",
                MenuType.Warning => "Warning Tasks",
                _ => "Succeed Tasks"
            };
        }

        private string GetMenuItemIconByType(MenuType menuType)
        {
            return menuType switch
            {
                MenuType.Info => GeometryData.Info,
                MenuType.Critical => GeometryData.Critical,
                MenuType.Warning => GeometryData.Warning,
                _ => GeometryData.Success
            };
        }

        private static Brush GetMenuItemColorByType(MenuType menuType)
        {
            return menuType switch
            {
                MenuType.Info => Brushes.DodgerBlue,
                MenuType.Critical => Brushes.Crimson,
                MenuType.Warning => Brushes.Orange,
                _ => Brushes.MediumSeaGreen
            };
        }

        private ICollection<TodoTask> CombineTasks()
        {
            var infoTasks = GetInfoTasks();
            var criticalTasks = GetCriticalTasks();
            var warningTasks = GetWarningTasks();
            var completedTasks = GetCompletedTasks();

            var tasks = new ObservableCollection<TodoTask>()
                .AddRange(infoTasks)
                .AddRange(criticalTasks)
                .AddRange(warningTasks)
                .AddRange(completedTasks);

            foreach (var todoTask in tasks)
            {
                if (!todoTask.IsFinished)
                    continue;
                todoTask.CanBeFinished = false;
            }

            return tasks;
        }

        private List<TodoTask> GetInfoTasks()
        {
            var tasks = InfoTaskViewModel.IsRegistered ? InfoTaskViewModel.TodoTasks.ToList() : TodoTasks.FilterByTaskType(TaskType.Info).ToList();

            return tasks;
        }

        private List<TodoTask> GetCriticalTasks()
        {
            var tasks = CriticalTaskViewModel.IsRegistered ? CriticalTaskViewModel.TodoTasks.ToList() : TodoTasks.FilterByTaskType(TaskType.Critical).ToList();

            return tasks;
        }

        private List<TodoTask> GetWarningTasks()
        {
            var tasks = WarningTaskViewModel.IsRegistered ? WarningTaskViewModel.TodoTasks.ToList() : TodoTasks.FilterByTaskType(TaskType.Warning).ToList();

            return tasks;
        }

        private IEnumerable<TodoTask> GetCompletedTasks()
        {
            var tasks = SucceedTaskViewModel.IsRegistered ?
                SucceedTaskViewModel.TodoTasks.ToList() :
                TodoTasks.FilterByTaskType(TaskType.Success).ToList();

            return tasks;
        }

        private void SaveSettings()
        {
            TaskService.WriteTasksAsync(CombineTasks(), TaskPath);
            if (Properties.Settings.Default.IsRestartAfterSave)
                RestartApplication();
        }

        private static void RestartApplication()
        {
            var processModule = Process.GetCurrentProcess().MainModule;
            if (processModule != null)
                Process.Start(processModule.FileName);
            Application.Current.Shutdown();
        }

        #endregion

    }
}
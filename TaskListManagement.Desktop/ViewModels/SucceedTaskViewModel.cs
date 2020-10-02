using Microsoft.Extensions.DependencyInjection;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TaskListManagement.Desktop.Assets.Icons;
using TaskListManagement.Desktop.Commands;
using TaskListManagement.Desktop.Enums;
using TaskListManagement.Desktop.Models;
using TaskListManagement.Desktop.Services.Abstractions;

namespace TaskListManagement.Desktop.ViewModels
{
    public sealed class SucceedTaskViewModel : BaseViewModel
    {

        #region Declarations

        private readonly ITaskService _taskService;
        private readonly GeometryData _geometryData;

        public bool IsRegistered;

        #endregion

        #region Constructor

        public SucceedTaskViewModel(ITaskService taskService)
        {
            _taskService = taskService;
            _geometryData = App.Provider.GetRequiredService<GeometryData>();
            TodoTasks = new ObservableCollection<TodoTask>();
        }

        #endregion

        #region Properties

        public TodoTask SelectedToDoTask { get; set; }
        public ObservableCollection<TodoTask> TodoTasks { get; set; }

        #endregion

        #region Commands

        public RelayCommand LoadMainCommand
        {
            get
            {
                return new RelayCommand(async (i) =>
                {
                    if (IsRegistered) return;
                    await ReadTasksAsync(TaskPath).ConfigureAwait(false);
                    IsRegistered = true;
                });
            }
        }

        public RelayCommand DeleteTaskCommand
        {
            get
            {
                return new RelayCommand((i) => DeleteTask(SelectedToDoTask), (i) => SelectedToDoTask != null);
            }
        }

        public RelayCommand ClearAllCommand
        {
            get
            {
                return new RelayCommand((i) => TodoTasks.Clear(), (i) => TodoTasks.Count > 0);
            }
        }

        #endregion

        #region Methods


        public async Task ReadTasksAsync(string taskPath)
        {
            var tasks = await _taskService.ReadTaskAsync(taskPath, TodoTasks).ConfigureAwait(true);
            TodoTasks.Clear();
            foreach (var todoTask in tasks)
            {
                if (todoTask.Type != TaskType.Success) continue;
                todoTask.IconData = _geometryData.Success;
                TodoTasks.Add(todoTask);
            }
        }

        public void DeleteTask(TodoTask task)
        {
            TodoTasks.Remove(task);
        }


        #endregion

    }
}
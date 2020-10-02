using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TaskListManagement.Desktop.Abstractions;
using TaskListManagement.Desktop.Assets.Icons;
using TaskListManagement.Desktop.Commands;
using TaskListManagement.Desktop.Enums;
using TaskListManagement.Desktop.Models;
using TaskListManagement.Desktop.Services.Abstractions;

namespace TaskListManagement.Desktop.ViewModels
{
    public class InfoTaskViewModel : BaseViewModel, ITodoTask
    {

        #region Declarations

        private readonly ITaskService _taskService;
        private readonly GeometryData _geometryData;

        public bool IsRegistered;

        #endregion

        #region Constructor

        public InfoTaskViewModel(ITaskService taskService)
        {
            _taskService = taskService;
            _geometryData = App.GetRequiredService<GeometryData>();
            TodoTasks = new ObservableCollection<TodoTask>();
            ToDoTask = new TodoTask { CanBeFinished = true };
        }

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

        public RelayCommand AddTaskCommand
        {
            get
            {
                return new RelayCommand((i) => AddTask(ToDoTask), (i) => !string.IsNullOrWhiteSpace(ToDoTask.Content));
            }
        }

        public RelayCommand ClearAllCommand
        {
            get
            {
                return new RelayCommand((i) => TodoTasks.Clear(), (i) => TodoTasks.Count > 0);
            }
        }

        public RelayCommand DeleteTaskCommand
        {
            get
            {
                return new RelayCommand((i) => DeleteTask(SelectedToDoTask), (i) => SelectedToDoTask != null);
            }
        }

        public RelayCommand RemoveSelectionCommand
        {
            get
            {
                return new RelayCommand((i) =>
                {
                    SelectedToDoTask = null;
                    ToDoTask.Title = string.Empty;
                    ToDoTask.Content = string.Empty;
                });
            }
        }

        public RelayCommand FinishCommand
        {
            get
            {
                return new RelayCommand((i) => FinishTask(SelectedToDoTask), (i) => SelectedToDoTask != null);
            }
        }

        #endregion

        #region Properties

        public TodoTask ToDoTask { get; set; }

        public TodoTask SelectedToDoTask { get; set; }

        public ObservableCollection<TodoTask> TodoTasks { get; set; }

        public string Title { get; set; }

        #endregion

        #region Implementation of ITodoTask

        public void FinishTask(TodoTask task)
        {
            task.Type = task.IsFinished ?
                TaskType.Success : TaskType.Info;

            task.IconData = task.IsFinished ?
                _geometryData.Success : _geometryData.Info;

            SelectedToDoTask = null;
        }

        public async Task ReadTasksAsync(string taskPath)
        {
            var tasks = await _taskService.ReadTaskAsync(taskPath, TodoTasks).ConfigureAwait(true);
            TodoTasks.Clear();
            foreach (var todoTask in tasks)
            {
                if (todoTask.Type != TaskType.Info) continue;
                todoTask.IconData = _geometryData.Info;
                TodoTasks.Add(todoTask);
            }
        }

        public void DeleteTask(TodoTask task)
        {
            TodoTasks.Remove(task);
        }

        public void AddTask(TodoTask task)
        {
            task.Id = Guid.NewGuid();
            task.IconData = _geometryData.Info;
            task.Type = TaskType.Info;
            task.Title = Title;
            TodoTasks.Add(task);
            ToDoTask = new TodoTask { CanBeFinished = true };
        }

        public bool IsTaskSelected => SelectedToDoTask != null;

        #endregion
    }
}
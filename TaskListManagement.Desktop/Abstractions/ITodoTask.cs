using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TaskListManagement.Desktop.Models;

namespace TaskListManagement.Desktop.Abstractions
{
    public interface ITodoTask
    {
        TodoTask ToDoTask { get; set; }

        TodoTask SelectedToDoTask { get; set; }

        ObservableCollection<TodoTask> TodoTasks { get; set; }

        void FinishTask(TodoTask task);

        Task ReadTasksAsync(string taskPath);

        void DeleteTask(TodoTask task);

        void AddTask(TodoTask task);

        bool IsTaskSelected { get; }

    }
}
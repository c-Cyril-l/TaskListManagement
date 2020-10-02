using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using TaskListManagement.Desktop.Enums;
using TaskListManagement.Desktop.Models;

namespace TaskListManagement.Desktop.Services.Abstractions
{
    public interface ITaskService
    {
        Task<ICollection<TodoTask>> ReadTaskAsync(string filePath, ObservableCollection<TodoTask> target);
        bool AddTask(TodoTask todoTask, TaskType taskType, ObservableCollection<TodoTask> target);
        bool WriteTasksAsync(ICollection<TodoTask> tasks, string filePath);
    }
}
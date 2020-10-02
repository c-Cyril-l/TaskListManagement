using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TaskListManagement.Desktop.Enums;
using TaskListManagement.Desktop.Models;

namespace TaskListManagement.Desktop.Extensions
{
    public static class TaskTypeCollectionExtensions
    {

        public static IEnumerable<TodoTask> FilterByTaskType(this IEnumerable<TodoTask> tasks, TaskType taskType)
        {
            return tasks.Where(t => t.Type == taskType);
        }

        public static ObservableCollection<TodoTask> AddRange(this ObservableCollection<TodoTask> collection,
            IEnumerable<TodoTask> tasks)
        {
            foreach (var todoTask in tasks)
            {
                collection.Add(todoTask);
            }

            return collection;
        }

    }
}
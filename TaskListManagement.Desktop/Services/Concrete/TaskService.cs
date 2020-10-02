using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using TaskListManagement.Desktop.Assets.Icons;
using TaskListManagement.Desktop.Enums;
using TaskListManagement.Desktop.Models;
using TaskListManagement.Desktop.Services.Abstractions;

namespace TaskListManagement.Desktop.Services.Concrete
{
    public class TaskService : ITaskService
    {

        private readonly IFileHelper _fileHelper;
        private readonly IJsonSerialize _jsonSerialize;
        private readonly GeometryData _geometryData;

        public TaskService(IFileHelper fileHelper, IJsonSerialize jsonSerialize)
        {
            _fileHelper = fileHelper;
            _jsonSerialize = jsonSerialize;

            _geometryData = App.Provider.GetRequiredService<GeometryData>();
        }

        public async Task<ICollection<TodoTask>> ReadTaskAsync(string filePath, ObservableCollection<TodoTask> target)
        {
            var content = await _fileHelper.ReadFileAsync(filePath).ConfigureAwait(true);
            return _jsonSerialize.DeserializeTasks(content);

            //AddToTasks(tasks, target);

        }

        public bool AddTask(TodoTask todoTask, TaskType taskType, ObservableCollection<TodoTask> target)
        {
            todoTask.Id = Guid.NewGuid();
            todoTask.Type = taskType;
            todoTask.IconData = GetTaskIcon(taskType);
            var isExist = target.FirstOrDefault(t => t.Id == todoTask.Id) != null;
            if (isExist)
                return false;

            target.Add(todoTask);
            return true;
        }


        public bool WriteTasksAsync(ICollection<TodoTask> tasks, string filePath)
        {
            try
            {
                var serializeTasks = _jsonSerialize.SerializeTasks(tasks);
                _fileHelper.WriteFileAsync(filePath, serializeTasks);
                return true;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
                return false;
            }
        }

        private string GetTaskIcon(TaskType taskType)
        {
            return taskType switch
            {
                TaskType.Info => _geometryData.Info,
                TaskType.Critical => _geometryData.Critical,
                TaskType.Warning => _geometryData.Warning,
                _ => _geometryData.Success
            };
        }

    }
}
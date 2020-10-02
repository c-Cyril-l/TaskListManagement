using System.Collections.Generic;
using System.Collections.ObjectModel;
using Newtonsoft.Json;
using TaskListManagement.Desktop.Models;
using TaskListManagement.Desktop.Services.Abstractions;

namespace TaskListManagement.Desktop.Services.Concrete
{
    public class JsonSerialize : IJsonSerialize
    {
        public ICollection<TodoTask> DeserializeTasks(string content)
        {
            var isContent = !string.IsNullOrEmpty(content);
            return isContent ? JsonConvert.DeserializeObject<ObservableCollection<TodoTask>>(content) :
                new ObservableCollection<TodoTask>();
        }

        public string SerializeTasks(ICollection<TodoTask> tasks)
        {
            return JsonConvert.SerializeObject(tasks);
        }

    }
}
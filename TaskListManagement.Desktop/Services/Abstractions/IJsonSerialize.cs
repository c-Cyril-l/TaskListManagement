using System.Collections.Generic;
using TaskListManagement.Desktop.Models;

namespace TaskListManagement.Desktop.Services.Abstractions
{
    public interface IJsonSerialize
    {
        ICollection<TodoTask> DeserializeTasks(string content);
        string SerializeTasks(ICollection<TodoTask> tasks);
    }
}
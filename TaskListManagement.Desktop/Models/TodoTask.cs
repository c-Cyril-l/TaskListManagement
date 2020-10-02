using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.ComponentModel.DataAnnotations;
using TaskListManagement.Desktop.Enums;
using TaskListManagement.Desktop.Miscellaneous;

namespace TaskListManagement.Desktop.Models
{
    public class TodoTask : ObservableObject
    {


        /// <summary>
        /// Identity.
        /// </summary>
        [JsonProperty]
        public Guid Id { get; set; }

        /// <summary>
        /// Title of the task.
        /// </summary>
        [JsonProperty]
        public string Title { get; set; }

        /// <summary>
        /// Title of the task.
        /// </summary>
        [JsonProperty]
        public string Content { get; set; }

        /// <summary>
        /// Task type (level of task).
        /// </summary>
        [JsonProperty]
        [JsonConverter(typeof(StringEnumConverter))]
        [EnumDataType(typeof(TaskType))]
        public TaskType Type { get; set; }

        /// <summary>
        /// Geometry data which will be used as icon for current task.
        /// </summary>
        [JsonIgnore]
        public string IconData { get; set; }

        /// <summary>
        /// Whether the task is completed.
        /// </summary>
        [JsonIgnore]
        public bool IsFinished { get; set; }

        /// <summary>
        /// Whether the task can be finished.
        /// </summary>
        [JsonProperty]
        public bool CanBeFinished { get; set; }

    }
}
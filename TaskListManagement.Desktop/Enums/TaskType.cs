namespace TaskListManagement.Desktop.Enums
{
    public enum TaskType
    {

        /// <summary>
        /// Whether the task critical and needs to be finished urgently.
        /// </summary>
        Critical = 0,

        /// <summary>
        /// Whether the task needs to be finished but not critical.
        /// </summary>
        Warning = 1,

        /// <summary>
        /// The Task better to be finished.
        /// </summary>
        Info = 2,

        /// <summary>
        /// Succeed.
        /// </summary>
        Success = 3

    }
}
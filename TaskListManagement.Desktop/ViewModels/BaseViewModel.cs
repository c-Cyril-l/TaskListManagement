using TaskListManagement.Desktop.Miscellaneous;

namespace TaskListManagement.Desktop.ViewModels
{
    public class BaseViewModel : ObservableObject
    {
        protected static string TaskPath => @"Tasks.json";
    }
}
using PropertyChanged;
using System.Windows.Media;
using TaskListManagement.Desktop.Enums;
using TaskListManagement.Desktop.Miscellaneous;

namespace TaskListManagement.Desktop.Models
{
    public class MenuItem : ObservableObject
    {

        [DependsOn(nameof(ItemCount))]
        public string Title => $"{Name} ( {ItemCount} )";

        public Brush ForeColor { get; set; }

        public string IconData { get; set; }

        public MenuType Type { get; set; }

        public string Name { get; set; }

        public int ItemCount { get; set; }

    }
}
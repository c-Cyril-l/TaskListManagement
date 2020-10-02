using System.Windows.Input;
using TaskListManagement.Desktop.Assets.Tray;

namespace TaskListManagement.Desktop.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();
            // Enable "minimize to tray" behavior for this Window
            MinimizeToTray.Enable(this);
        }

        private void MainWindow_OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}

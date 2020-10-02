using System.Windows;

namespace TaskListManagement.Desktop.Views
{
	/// <summary>
	/// Interaction logic for SettingView.xaml
	/// </summary>
	public partial class SettingView
	{
		public SettingView()
		{
			InitializeComponent();
		}

		private void Button_Click(object sender, RoutedEventArgs e)
		{
			App.GetRequiredService<MainWindow>().WindowState = WindowState.Minimized;
		}
	}
}

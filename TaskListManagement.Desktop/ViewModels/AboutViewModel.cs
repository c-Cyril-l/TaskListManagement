using System.Diagnostics;
using TaskListManagement.Desktop.Commands;

namespace TaskListManagement.Desktop.ViewModels
{
    public class AboutViewModel : BaseViewModel
    {
        private const string FacebookProfile = "https://www.facebook.com/CryilDouglas";
        private const string GithubProfile = "https://github.com/c-Cyril-l";
        private const string Email = "mailto:cyrildouglas07@gmail.com";

        public RelayCommand OpenFacebookProfileCommand
        {
            get
            {
                return new RelayCommand((i) => Process.Start(FacebookProfile));
            }
        }

        public RelayCommand OpenGithubProfileCommand
        {
            get
            {
                return new RelayCommand((i) => Process.Start(GithubProfile));
            }
        }

        public RelayCommand EmailMeCommand
        {
            get
            {
                return new RelayCommand((i) => Process.Start(Email));
            }
        }



    }
}
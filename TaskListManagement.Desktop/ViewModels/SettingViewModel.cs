using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.IO;
using TaskListManagement.Desktop.Commands;

namespace TaskListManagement.Desktop.ViewModels
{
    public class SettingViewModel : BaseViewModel
    {

        #region Declcarations

        private const string KeyName = @"TaskManagement";

        private const string StartupRegistrationKey =
            @"SOFTWARE\Microsoft\Windows\CurrentVersion\Run";

        #endregion

        #region Constructor

        #endregion

        #region Properties

        public bool IsAddToStartUp { get; set; }
        public bool IsRestartAfterSave { get; set; }

        #endregion

        #region Commands

        public RelayCommand LoadedCommand
        {
            get
            {
                return new RelayCommand((i) =>
                {
                    ReadApplicationSettings();
                });
            }
        }

        public RelayCommand UnloadedCommand
        {
            get
            {
                return new RelayCommand((i) =>
                {
                    UpdateSettings();
                });
            }
        }

        public RelayCommand AddToStartupCommand
        {
            get
            {
                return new RelayCommand((i) =>
                {
                    if (IsAddToStartUp)
                        AddToStartup(KeyName);
                    else
                        RemoveFromStartup(KeyName);

                    UpdateSettings();
                });
            }
        }

        public RelayCommand RestartAfterSaveCommand
        {
            get
            {
                return new RelayCommand((i) => UpdateSettings());
            }
        }

        #endregion

        #region Methods

        /// <summary>
        /// Remove application from startup.
        /// </summary>
        /// <param name="keyName">Application key name in startup.</param>
        private static void RemoveFromStartup(string keyName)
        {
            var key = Registry.CurrentUser.OpenSubKey(StartupRegistrationKey, true);

            if (key?.GetValue(keyName) != null)
                key.DeleteValue(keyName);
        }

        /// <summary>
        /// Add application to startup.
        /// </summary>
        /// <param name="keyName">Application key name in startup.</param>
        private static void AddToStartup(string keyName)
        {
            var processModule = Process.GetCurrentProcess().MainModule;
            if (processModule == null) return;
            var appName = Path.GetFileName(processModule.FileName);
            var appExePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory ?? string.Empty, appName);

            var registryKey = Registry.CurrentUser.OpenSubKey(StartupRegistrationKey, true);

            if (registryKey?.GetValue(keyName) != null)
                return;

            registryKey?.SetValue(keyName, appExePath);
        }

        private void ReadApplicationSettings()
        {
            IsAddToStartUp = Properties.Settings.Default.IsAddToStartup;
            IsRestartAfterSave = Properties.Settings.Default.IsRestartAfterSave;
        }

        private void UpdateSettings()
        {
            Properties.Settings.Default.IsAddToStartup = IsAddToStartUp;
            Properties.Settings.Default.IsRestartAfterSave = IsRestartAfterSave;
            Properties.Settings.Default.Save();
        }



        #endregion

    }
}
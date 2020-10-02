using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Forms;

namespace TaskListManagement.Desktop.Assets.Tray
{
    /// <summary>
    /// Class implementing support for "minimize to tray" functionality.
    /// Credits : https://dlaa.me/blog/post/9889700#:~:text=%22Minimize%20to%20tray%22%20is%20a,taskbar%20button%20would%20have%20done.
    /// </summary>
    public static class MinimizeToTray
    {
        /// <summary>
        /// Enables "minimize to tray" behavior for the specified Window.
        /// </summary>
        /// <param name="window">Window to enable the behavior for.</param>
        public static void Enable(Window window)
        {
            // No need to track this instance; its event handlers will keep it alive
            // ReSharper disable once ObjectCreationAsStatement
            new MinimizeToTrayInstance(window);
        }

        /// <summary>
        /// Class implementing "minimize to tray" functionality for a Window instance.
        /// </summary>
        private class MinimizeToTrayInstance
        {
            private readonly Window _window;
            private NotifyIcon _notifyIcon;
            private bool _balloonShown;

            /// <summary>
            /// Initializes a new instance of the MinimizeToTrayInstance class.
            /// </summary>
            /// <param name="window">Window instance to attach to.</param>
            public MinimizeToTrayInstance(Window window)
            {
                Debug.Assert(window != null, "window parameter is null.");
                _window = window;
                _window.StateChanged += HandleStateChanged;
            }

            /// <summary>
            /// Handles the Window's StateChanged event.
            /// </summary>
            /// <param name="sender">Event source.</param>
            /// <param name="e">Event arguments.</param>
            private void HandleStateChanged(object sender, EventArgs e)
            {
                if (_notifyIcon == null)
                {
                    // Initialize NotifyIcon instance "on demand"

                    _notifyIcon = new NotifyIcon
                    {
                        Icon = new System.Drawing.Icon(Path.Combine(AppContext.BaseDirectory, "TaskManagement.ico"))
                    };
                    _notifyIcon.MouseClick += HandleNotifyIconOrBalloonClicked;
                    _notifyIcon.BalloonTipClicked += HandleNotifyIconOrBalloonClicked;
                }
                // Update copy of Window Title in case it has changed
                _notifyIcon.Text = _window.Title;

                // Show/hide Window and NotifyIcon
                var minimized = (_window.WindowState == WindowState.Minimized);
                _window.ShowInTaskbar = !minimized;
                _notifyIcon.Visible = minimized;
                if (minimized && !_balloonShown)
                {
                    // If this is the first time minimizing to the tray, show the user what happened
                    _notifyIcon.ShowBalloonTip(1000, null, _window.Title, ToolTipIcon.None);
                    _balloonShown = true;
                }
            }

            /// <summary>
            /// Handles a click on the notify icon or its balloon.
            /// </summary>
            /// <param name="sender">Event source.</param>
            /// <param name="e">Event arguments.</param>
            private void HandleNotifyIconOrBalloonClicked(object sender, EventArgs e)
            {
                // Restore the Window
                _window.WindowState = WindowState.Normal;
            }
        }
    }

}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryGPT
{
    public static class FormUtilities
    {
        // Center a form on the screen.
        public static void CenterFormOnScreen(Form form)
        {
            form.StartPosition = FormStartPosition.CenterScreen;
        }

        // Set form to full screen.
        public static void SetFullScreen(Form form)
        {
            form.WindowState = FormWindowState.Maximized;
            form.FormBorderStyle = FormBorderStyle.None;
            form.TopMost = true;
        }

        // Exit full screen mode.
        public static void ExitFullScreen(Form form)
        {
            form.WindowState = FormWindowState.Normal;
            form.FormBorderStyle = FormBorderStyle.Sizable;
            form.TopMost = false;
        }

        // Toggle full screen mode.
        public static void ToggleFullScreen(Form form)
        {
            if (form.WindowState == FormWindowState.Maximized)
            {
                ExitFullScreen(form);
            }
            else
            {
                SetFullScreen(form);
            }
        }

        // Set form opacity.
        public static void SetOpacity(Form form, double opacity)
        {
            form.Opacity = opacity;
        }

        // Set form background color.
        public static void SetBackgroundColor(Form form, Color color)
        {
            form.BackColor = color;
        }

        // Show a message box with a custom message.
        public static void ShowMessageBox(string message, string title = "Message", MessageBoxButtons buttons = MessageBoxButtons.OK, MessageBoxIcon icon = MessageBoxIcon.Information)
        {
            MessageBox.Show(message, title, buttons, icon);
        }

        // Prompt the user with a Yes/No dialog and return the result.
        public static bool PromptUser(string message, string title = "Confirm")
        {
            return MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        // Close the form safely.
        public static void SafeClose(Form form)
        {
            if (form.InvokeRequired)
            {
                form.Invoke(new Action(form.Close));
            }
            else
            {
                form.Close();
            }
        }

        // Minimize the form.
        public static void MinimizeForm(Form form)
        {
            form.WindowState = FormWindowState.Minimized;
        }

        // Restore the form to its normal state.
        public static void RestoreForm(Form form)
        {
            form.WindowState = FormWindowState.Normal;
        }

        // Update the status label on the status strip.
        public static void UpdateStatusLabel(StatusStrip statusStrip, string statusMessage)
        {
            if (statusStrip.Items.Count > 0)
            {
                if (statusStrip.Items[0] is ToolStripStatusLabel statusLabel)
                {
                    statusLabel.Text = statusMessage;
                }
            }
        }

        // Display version information on the status strip.
        public static void DisplayVersionInfo(StatusStrip statusStrip, string version)
        {
            if (statusStrip.Items.Count > 1)
            {
                if (statusStrip.Items[1] is ToolStripStatusLabel versionLabel)
                {
                    versionLabel.Text = $"Version: {version}";
                }
            }
        }

        // Update the status icon on the status strip based on the status type.
        public static void UpdateStatusIcon(StatusStrip statusStrip, string statusType)
        {
            if (statusStrip.Items.Count > 2)
            {
                if (statusStrip.Items[2] is ToolStripStatusLabel iconLabel)
                {
                    switch (statusType.ToLower())
                    {
                        case "success":
                            iconLabel.Image = LibraryGPT.Properties.Resources.tick; // Assuming you have these images in your resources.
                            break;
                        case "warning":
                            iconLabel.Image = Properties.Resources.warning;
                            break;
                        case "error":
                            iconLabel.Image = Properties.Resources.error;
                            break;
                        default:
                            iconLabel.Image = null;
                            break;
                    }
                }
            }
        }

        /// <summary>
        /// Brings the control to front by setting maximum z index
        /// </summary>
        /// <param name="control">Current Element</param>
        public static void BringToFrontEx(this Control control)
        {
            if (control != null)
            {
                control.BringToFront();
            }
        }


        /// <summary>
        /// Sets up the system tray behavior for the specified form. When the form is minimized, 
        /// it hides the form and shows a notify icon in the system tray. Double-clicking the 
        /// notify icon restores the form to its normal state.
        /// </summary>
        /// <param name="form">The form for which the system tray behavior should be set up.</param>
        public static void SetupSystemTray<T>(T form) where T : Form
        {
            NotifyIcon notifyIcon = new NotifyIcon
            {
                Icon = SystemIcons.Application
            };

            notifyIcon.DoubleClick += (sender, e) =>
            {
                form.Show();
                form.WindowState = FormWindowState.Normal;
            };

            form.Resize += (sender, e) =>
            {
                if (form.WindowState == FormWindowState.Minimized)
                {
                    form.Hide();
                    notifyIcon.Visible = true;
                }
            };
        }
    }
}

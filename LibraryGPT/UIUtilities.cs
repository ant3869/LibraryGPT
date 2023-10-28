using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace LibraryGPT
{
    public static class UIUtilities
    {
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int wMsg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        // Centers a form on the screen.
        public static void CenterFormOnScreen(Form form)
        {
            form.StartPosition = FormStartPosition.CenterScreen;
        }

        // Sets the form to be draggable from any point.
        public static void MakeFormDraggable(Form form)
        {
            form.MouseDown += (s, e) =>
            {
                if (e.Button == MouseButtons.Left)
                {
                    ReleaseCapture();
                    SendMessage(form.Handle, 0xA1, 0x2, 0);
                }
            };
        }


        // Sets a tooltip for a control.
        public static void SetTooltip(Control control, string tooltipText)
        {
            ToolTip toolTip = new ToolTip();
            toolTip.SetToolTip(control, tooltipText);
        }

        // Changes the cursor to a waiting cursor.
        public static void ShowWaitingCursor()
        {
            Cursor.Current = Cursors.WaitCursor;
        }

        // Restores the cursor to the default cursor.
        public static void RestoreDefaultCursor()
        {
            Cursor.Current = Cursors.Default;
        }

        // Sets the visibility of a group of controls.
        public static void SetControlsVisibility(bool isVisible, params Control[] controls)
        {
            foreach (Control control in controls)
            {
                control.Visible = isVisible;
            }
        }

        // Sets a background image for a form.
        public static void SetFormBackgroundImage(Form form, Image backgroundImage)
        {
            form.BackgroundImage = backgroundImage;
            form.BackgroundImageLayout = ImageLayout.Stretch;
        }

        // Enables or disables a group of controls.
        public static void SetControlsEnabled(bool isEnabled, params Control[] controls)
        {
            foreach (Control control in controls)
            {
                control.Enabled = isEnabled;
            }
        }

        // Toggles the visibility of a control.
        public static void ToggleControlVisibility(Control control)
        {
            control.Visible = !control.Visible;
        }
    }
}

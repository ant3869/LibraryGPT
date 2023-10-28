using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryGPT
{
    public static class DialogUtilities
    {
        /// <summary>
        /// Displays an information message to the user.
        /// </summary>
        /// <param name="message">The message to display.</param>
        /// <param name="title">The title of the dialog.</param>
        public static void ShowInfo(string message, string title = "Information")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Displays an error message to the user.
        /// </summary>
        /// <param name="message">The error message to display.</param>
        /// <param name="title">The title of the dialog.</param>
        public static void ShowError(string message, string title = "Error")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        /// <summary>
        /// Displays a warning message to the user.
        /// </summary>
        /// <param name="message">The warning message to display.</param>
        /// <param name="title">The title of the dialog.</param>
        public static void ShowWarning(string message, string title = "Warning")
        {
            MessageBox.Show(message, title, MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// Asks the user a yes/no question.
        /// </summary>
        /// <param name="message">The question to ask.</param>
        /// <param name="title">The title of the dialog.</param>
        /// <returns>True if the user clicked "Yes", false otherwise.</returns>
        public static bool AskYesNo(string message, string title = "Question")
        {
            return MessageBox.Show(message, title, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }

        ///// <summary>
        ///// Asks the user for input.
        ///// </summary>
        ///// <param name="message">The message to display.</param>
        ///// <param name="title">The title of the dialog.</param>
        ///// <returns>The user's input, or null if they cancelled.</returns>
        //public static string AskForInput(string message, string title = "Input")
        //{
        //    using (var inputBox = new InputBox(message, title))
        //    {
        //        if (inputBox.ShowDialog() == DialogResult.OK)
        //        {
        //            return inputBox.InputText;
        //        }
        //        return null;
        //    }
        //}

        public class UserInputResult
        {
            public bool IsConfirmed { get; set; }
            public string InputValue { get; set; }
        }

        /// <summary>
        /// Asks the user for input using a provided TextBox control.
        /// </summary>
        /// <param name="message">The message to display above the TextBox.</param>
        /// <param name="inputBox">The TextBox control to get input from.</param>
        /// <param name="title">The title of the dialog.</param>
        /// <returns>An object containing the user's input and a confirmation flag.</returns>
        public static UserInputResult AskForInput(string message, TextBox inputBox, string title = "Input")
        {
            DialogResult result = MessageBox.Show(inputBox, message, title, MessageBoxButtons.OKCancel);
            return new UserInputResult
            {
                IsConfirmed = result == DialogResult.OK,
                InputValue = inputBox.Text
            };
        }
    }
}

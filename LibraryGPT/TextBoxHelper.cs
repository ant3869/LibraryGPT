using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LibraryGPT.Enums;

namespace LibraryGPT
{
    public class TextBoxHelper
    {
        /// <summary>
        /// Automatically scrolls a multiline TextBox to the bottom.
        /// </summary>
        /// <param name="textBox">The TextBox to scroll.</param>
        public static void AutoScrollToEnd(TextBox textBox)
        {
            textBox.SelectionStart = textBox.Text.Length;
            textBox.ScrollToCaret();
        }

        /// <summary>
        /// Inserts a timestamp at the current cursor position in the TextBox.
        /// </summary>
        /// <param name="textBox">The TextBox where the timestamp will be inserted.</param>
        /// <param name="format">The format of the timestamp (optional).</param>
        public static void InsertTimestamp(TextBox textBox, string format = "yyyy-MM-dd HH:mm:ss")
        {
            textBox.Text = textBox.Text.Insert(textBox.SelectionStart, DateTime.Now.ToString(format));
        }

        /// <summary>
        /// Highlights all instances of a specified text within the TextBox.
        /// </summary>
        /// <param name="textBox">The TextBox to perform the highlight operation on.</param>
        /// <param name="textToHighlight">The text to highlight.</param>
        /// <param name="highlightColor">The color to use for highlighting.</param>
        public static void HighlightText(RichTextBox textBox, string textToHighlight, Color highlightColor)
        {
            int sIndex = 0;
            while (sIndex < textBox.Text.LastIndexOf(textToHighlight))
            {
                textBox.Find(textToHighlight, sIndex, textBox.TextLength, RichTextBoxFinds.None);
                textBox.SelectionBackColor = highlightColor;
                sIndex = textBox.Text.IndexOf(textToHighlight, sIndex) + 1;
            }
        }

        /// <summary>
        /// Counts the number of words in a TextBox.
        /// </summary>
        /// <param name="textBox">The TextBox to count words in.</param>
        /// <returns>The number of words in the TextBox.</returns>
        public static int CountWords(TextBox textBox)
        {
            return !string.IsNullOrWhiteSpace(textBox.Text) ? textBox.Text.Split(new char[] { ' ', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries).Length : 0;
        }

        /// <summary>
        /// Converts the text in the TextBox to specified case format.
        /// </summary>
        /// <param name="textBox">The TextBox whose text is to be converted.</param>
        /// <param name="textCase">The case format to convert to (Upper, Lower, Title).</param>
        public static void ConvertCase(TextBox textBox, TextCase textCase)
        {
            switch (textCase)
            {
                case TextCase.Upper:
                    textBox.Text = textBox.Text.ToUpper();
                    break;
                case TextCase.Lower:
                    textBox.Text = textBox.Text.ToLower();
                    break;
                case TextCase.Title:
                    CultureInfo cultureInfo = Thread.CurrentThread.CurrentCulture;
                    TextInfo textInfo = cultureInfo.TextInfo;
                    textBox.Text = textInfo.ToTitleCase(textBox.Text);
                    break;
            }
        }

        /// <summary>
        /// Enables dragging and dropping text into a TextBox.
        /// </summary>
        /// <param name="textBox">The TextBox to enable drag and drop on.</param>
        public static void EnableDragDropText(TextBox textBox)
        {
            textBox.AllowDrop = true;

            textBox.DragEnter += (sender, e) =>
            {
                if (e.Data.GetDataPresent(DataFormats.Text))
                    e.Effect = DragDropEffects.Copy;
                else
                    e.Effect = DragDropEffects.None;
            };

            textBox.DragDrop += (sender, e) =>
            {
                textBox.Text += (string)e.Data.GetData(DataFormats.Text);
            };
        }

        /// <summary>
        /// Finds and replaces specified text in a TextBox.
        /// </summary>
        /// <param name="textBox">The TextBox in which to find and replace text.</param>
        /// <param name="findText">The text to find.</param>
        /// <param name="replaceText">The text to replace with.</param>
        public static void FindAndReplace(TextBox textBox, string findText, string replaceText)
        {
            textBox.Text = textBox.Text.Replace(findText, replaceText);
        }

        /// <summary>
        /// Undoes the last action in a TextBox.
        /// </summary>
        /// <param name="textBox">The TextBox where the action will be undone.</param>
        public static void UndoLastAction(TextBox textBox)
        {
            if (textBox.CanUndo)
            {
                textBox.Undo();
            }
        }

        /// <summary>
        /// Redoes the last undone action in a TextBox.
        /// </summary>
        /// <param name="textBox">The TextBox where the action will be redone.</param>
        public static void RedoLastAction(TextBox textBox)
        {
            if (textBox.CanUndo)
            {
                // textBox.Undo();
            }
        }

        /// <summary>
        /// Toggles the read-only state of a TextBox.
        /// </summary>
        /// <param name="textBox">The TextBox to toggle read-only state.</param>
        public static void ToggleReadOnly(TextBox textBox)
        {
            textBox.ReadOnly = !textBox.ReadOnly;
        }

        /// <summary>
        /// Clears all formatting in a RichTextBox.
        /// </summary>
        /// <param name="richTextBox">The RichTextBox to clear formatting.</param>
        public static void ClearFormatting(RichTextBox richTextBox)
        {
            string plainText = richTextBox.Text;
            richTextBox.Clear();
            richTextBox.AppendText(plainText);
        }

        /// <summary>
        /// Loads text from a specified file into a TextBox.
        /// </summary>
        /// <param name="textBox">The TextBox where the text will be loaded.</param>
        /// <param name="filePath">The file path from which to load the text.</param>
        public static void LoadTextFromFile(TextBox textBox, string filePath)
        {
            textBox.Text = File.ReadAllText(filePath);
        }

        /// <summary>
        /// Saves the content of a TextBox to a specified file.
        /// </summary>
        /// <param name="textBox">The TextBox whose content will be saved.</param>
        /// <param name="filePath">The file path to save the content.</param>
        public static void SaveTextToFile(TextBox textBox, string filePath)
        {
            File.WriteAllText(filePath, textBox.Text);
        }

        /// <summary>
        /// Appends text with a new line to the TextBox.
        /// </summary>
        /// <param name="textBox">The TextBox to append text.</param>
        /// <param name="text">The text to append.</param>
        public static void AppendTextWithNewLine(TextBox textBox, string text)
        {
            textBox.AppendText(text + Environment.NewLine);
        }

        /// <summary>
        /// Limits the number of characters that can be entered in a TextBox.
        /// </summary>
        /// <param name="textBox">The TextBox to limit characters.</param>
        /// <param name="maxLength">The maximum number of characters allowed.</param>
        public static void LimitTextLength(TextBox textBox, int maxLength)
        {
            textBox.MaxLength = maxLength;
        }

        /// <summary>
        /// Converts each line of text in a RichTextBox to a bullet list item.
        /// </summary>
        /// <param name="richTextBox">The RichTextBox to convert.</param>
        public static void ConvertToBulletList(RichTextBox richTextBox)
        {
            richTextBox.SelectionBullet = true;
            richTextBox.BulletIndent = 10;
        }

        /// <summary>
        /// Applies highlight color to selected text in a RichTextBox.
        /// </summary>
        /// <param name="richTextBox">The RichTextBox to apply highlight.</param>
        /// <param name="highlightColor">The color to use for highlighting.</param>
        public static void ApplyTextHighlight(RichTextBox richTextBox, Color highlightColor)
        {
            richTextBox.SelectionBackColor = highlightColor;
        }

        /// <summary>
        /// Sets the font style of selected text in a RichTextBox.
        /// </summary>
        /// <param name="richTextBox">The RichTextBox to set font style.</param>
        /// <param name="fontStyle">The font style to apply.</param>
        public static void SetFontStyle(RichTextBox richTextBox, FontStyle fontStyle)
        {
            richTextBox.SelectionFont = new Font(richTextBox.SelectionFont, fontStyle);
        }






        /// <summary>
        /// Asynchronously generates a summary of the content in a TextBox.
        /// </summary>
        /// <param name="textBoxText">The text content of the TextBox.</param>
        /// <returns>The task representing the asynchronous operation, returning a summary of the text.</returns>
        public static async Task<string> GenerateTextSummaryAsync(string textBoxText)
        {
            // Simulate asynchronous operation
            return await Task.Run(() => GenerateSummary(textBoxText));
        }

        /// <summary>
        /// Generates a summary of the provided text.
        /// </summary>
        /// <param name="text">The text to summarize.</param>
        /// <returns>The summarized text.</returns>
        private static string GenerateSummary(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
                return string.Empty;

            // Split text into sentences
            var sentences = text.Split(new[] { '.', '?', '!' }, StringSplitOptions.RemoveEmptyEntries);

            // Simple algorithm to pick every nth sentence to form a summary
            var nth = 5; // Adjust this value to change summarization granularity
            var summarySentences = sentences.Where((sentence, index) => index % nth == 0);

            return string.Join(". ", summarySentences) + ".";
        }
    }
}

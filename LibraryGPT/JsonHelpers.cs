using NLog;
using System.Collections.ObjectModel;
using System.Media;
using System.Text;
using System.Text.Json;

//namespace LibraryGPT
//{
    //    /// <summary>
    //    /// Class to help with JSON files
    //    /// </summary>
    //    internal static class JsonHelpers
    //    {
    //        #region NLog Instance
    //        private static readonly Logger log = LogManager.GetCurrentClassLogger();
    //        #endregion NLog Instance

    //        #region Read the JSON file for the main list
    //        /// <summary>
    //        /// Read the JSON file and deserialize it into the ObservableCollection
    //        /// </summary>
    //        public static void ReadJson()
    //        {
    //            string jsonfile = GetMainListFile();

    //            if (!File.Exists(jsonfile))
    //            {
    //                CreateNewMainJson(jsonfile);
    //            }

    //            log.Debug($"My Launcher List file is: {jsonfile}");
    //            try
    //            {
    //                string json = File.ReadAllText(jsonfile);
    //                MyListItem.Children = JsonSerializer.Deserialize<ObservableCollection<MyListItem>>(json);
    //            }
    //            catch (Exception ex) when (ex is DirectoryNotFoundException || ex is FileNotFoundException)
    //            {
    //                log.Error(ex, "File or Directory not found {0}", jsonfile);
    //                SystemSounds.Exclamation.Play();
    //                MDCustMsgBox mbox = new($"File or Directory not found:\n\n{ex.Message}\n\nUnable to continue.",
    //                                    "My Launcher Error",
    //                                    ButtonType.Ok,
    //                                    true,
    //                                    true,
    //                                    null,
    //                                    true);
    //                _ = mbox.ShowDialog();
    //                Environment.Exit(1);
    //            }
    //            catch (Exception ex)
    //            {
    //                log.Error(ex, "Error reading file: {0}", jsonfile);
    //                SystemSounds.Exclamation.Play();
    //                MDCustMsgBox mbox = new($"Error reading file:\n\n{ex.Message}",
    //                                    "My Launcher Error",
    //                                    ButtonType.Ok,
    //                                    true,
    //                                    true,
    //                                    null,
    //                                    true);
    //                _ = mbox.ShowDialog();
    //            }

    //            if (MyListItem.Children == null)
    //            {
    //                log.Error("File {0} is empty or is invalid", jsonfile);
    //                SystemSounds.Exclamation.Play();
    //                MDCustMsgBox mbox = new($"File {jsonfile} is empty or is invalid\n\nUnable to continue.",
    //                                    "My Launcher Error",
    //                                    ButtonType.Ok,
    //                                    true,
    //                                    true,
    //                                    null,
    //                                    true);
    //                _ = mbox.ShowDialog();
    //                Environment.Exit(2);
    //            }

    //            if (MyListItem.Children.Count == 1)
    //            {
    //                log.Info($"Read {MyListItem.Children.Count} entry from {jsonfile}");
    //            }
    //            else
    //            {
    //                log.Info($"Read {MyListItem.Children.Count} entries from {jsonfile}");
    //            }
    //        }
    //        #endregion Read the JSON file for the main list

    //        #region Read the JSON file for the menu list
    //        /// <summary>
    //        /// Read the menu JSON file and deserialize it into the ObservableCollection
    //        /// </summary>
    //        public static void ReadMenuJson()
    //        {
    //            string jsonfile = GetMenuListFile();

    //            if (!File.Exists(jsonfile))
    //            {
    //                CreateNewMenuJson(jsonfile);
    //            }

    //            log.Debug($"My Launcher Menu file is: {jsonfile}");
    //            try
    //            {
    //                string json = File.ReadAllText(jsonfile);
    //                MyMenuItem.MLMenuItems = JsonSerializer.Deserialize<ObservableCollection<MyMenuItem>>(json);
    //            }
    //            catch (Exception ex)
    //            {
    //                log.Error(ex, "Error reading file: {0}", jsonfile);
    //                SystemSounds.Exclamation.Play();
    //                MDCustMsgBox mbox = new($"Error reading file:\n\n{ex.Message}",
    //                                    "My Launcher Error",
    //                                    ButtonType.Ok,
    //                                    true,
    //                                    true,
    //                                    null,
    //                                    true);
    //                _ = mbox.ShowDialog();
    //            }
    //            if (MyMenuItem.MLMenuItems.Count == 1)
    //            {
    //                log.Info($"Read {MyMenuItem.MLMenuItems.Count} entry from {jsonfile}");
    //            }
    //            else
    //            {
    //                log.Info($"Read {MyMenuItem.MLMenuItems.Count} entries from {jsonfile}");
    //            }
    //        }
    //        #endregion Read the JSON file for the menu list

    //        #region Save the main list JSON file
    //        /// <summary>
    //        ///  Serialize the ObservableCollection and write it to a JSON file
    //        /// </summary>
    //        public static void SaveMainJson()
    //        {
    //            List<MyListItem> tempCollection = new();

    //            foreach (MyListItem item in MyListItem.Children)
    //            {
    //                if (!string.IsNullOrEmpty(item.Title))
    //                {
    //                    MyListItem ch = new()
    //                    {
    //                        Title = item.Title.Trim(),
    //                        FilePathOrURI = item.FilePathOrURI?.Trim(),
    //                        FileIcon = item.FileIcon,
    //                        Arguments = item.Arguments,
    //                        WorkingDir = item.WorkingDir?.Trim(),
    //                        IconSource = item.IconSource?.Trim(),
    //                        EntryType = item.EntryType,
    //                        MyListItems = item.MyListItems,
    //                        ItemID = item.ItemID,
    //                        PopupHeight = item.PopupHeight,
    //                        PopupLeft = item.PopupLeft,
    //                        PopupTop = item.PopupTop,
    //                        PopupWidth = item.PopupWidth,
    //                        RunElevated = item.RunElevated,
    //                    };
    //                    tempCollection.Add(ch);
    //                }
    //            }
    //            JsonSerializerOptions opts = new() { WriteIndented = true };
    //            try
    //            {
    //                log.Info($"Saving JSON file: {GetMainListFile()}");
    //                string json = JsonSerializer.Serialize(tempCollection, opts);
    //                File.WriteAllText(GetMainListFile(), json);
    //                SnackbarMsg.QueueMessage("File Saved.", 2000);
    //                tempCollection.Clear();
    //            }
    //            catch (Exception ex)
    //            {
    //                log.Error(ex, "Error saving file.");
    //                SystemSounds.Exclamation.Play();
    //                MDCustMsgBox mbox = new($"Error saving file.\n{ex.Message}.",
    //                                    "ERROR",
    //                                    ButtonType.Ok,
    //                                    true,
    //                                    true,
    //                                    null,
    //                                    true);
    //                _ = mbox.ShowDialog();
    //            }
    //        }
    //        #endregion Save the main list JSON file

    //        #region Save the menu items JSON file
    //        /// <summary>
    //        ///  Serialize the ObservableCollection and write it to a JSON file
    //        /// </summary>
    //        public static void SaveMenuJson()
    //        {
    //            List<MyMenuItem> tempCollection = new();

    //            foreach (MyMenuItem item in MyMenuItem.MLMenuItems)
    //            {
    //                if (!string.IsNullOrEmpty(item.Title))
    //                {
    //                    MyMenuItem mmi = new()
    //                    {
    //                        Title = item.Title.Trim(),
    //                        FilePathOrURI = item.FilePathOrURI?.Trim(),
    //                        Arguments = item.Arguments,
    //                        ItemType = item.ItemType,
    //                        SubMenuItems = item.SubMenuItems,
    //                        WorkingDir = item.WorkingDir?.Trim(),
    //                        ItemID = item.ItemID,
    //                        PopupID = item.PopupID,
    //                        RunElevated = item.RunElevated,
    //                    };
    //                    tempCollection.Add(mmi);
    //                }
    //            }
    //            JsonSerializerOptions opts = new() { WriteIndented = true };
    //            try
    //            {
    //                log.Info($"Saving JSON file: {GetMenuListFile()}");
    //                string json = JsonSerializer.Serialize(tempCollection, opts);
    //                File.WriteAllText(GetMenuListFile(), json);
    //                SnackbarMsg.QueueMessage("File Saved.", 2000);
    //                tempCollection.Clear();
    //            }
    //            catch (Exception ex)
    //            {
    //                log.Error(ex, "Error saving file.");
    //                SystemSounds.Exclamation.Play();
    //                MDCustMsgBox mbox = new($"Error saving file.\n{ex.Message}.",
    //                                    "ERROR",
    //                                    ButtonType.Ok,
    //                                    true,
    //                                    true,
    //                                    null,
    //                                    true);
    //                _ = mbox.ShowDialog();
    //            }
    //        }
    //        #endregion Save the menu items JSON file

    //        #region Create a backup for the main list file
    //        /// <summary>
    //        /// Creates a backup of the list file by copying the current file
    //        /// to a location of the user's choosing.
    //        /// </summary>
    //        public static void CreateBackupFile()
    //        {
    //            string tStamp = string.Format("{0:yyyyMMdd_HHmm}", DateTime.Now);
    //            SaveFileDialog dlgSave = new()
    //            {
    //                Title = "Choose Export location",
    //                CheckPathExists = true,
    //                CheckFileExists = false,
    //                OverwritePrompt = true,
    //                AddExtension = true,
    //                FileName = $"MyLauncher_{tStamp}_List_backup.json",
    //                Filter = "JSON (*.json)|*.json|All files (*.*)|*.*"
    //            };
    //            if (dlgSave.ShowDialog() == true)
    //            {
    //                try
    //                {
    //                    File.Copy(GetMainListFile(), dlgSave.FileName, true);
    //                    log.Info($"List backed up to {dlgSave.FileName}");
    //                }
    //                catch (Exception ex)
    //                {
    //                    log.Error(ex, "Backup failed.");
    //                    SystemSounds.Exclamation.Play();
    //                    MDCustMsgBox mbox = new($"Backup failed.\n{ex.Message}.",
    //                                        "ERROR",
    //                                        ButtonType.Ok,
    //                                        true,
    //                                        true,
    //                                        null,
    //                                        true);
    //                    _ = mbox.ShowDialog();
    //                }
    //            }
    //        }
    //        #endregion Create a backup for the list file

    //        #region Create a backup for the menu file
    //        /// <summary>
    //        /// Creates a backup of the menu file by copying the current file
    //        /// to a location of the user's choosing.
    //        /// </summary>
    //        public static void CreateMenuBackup()
    //        {
    //            string tStamp = string.Format("{0:yyyyMMdd_HHmm}", DateTime.Now);
    //            SaveFileDialog dlgSave = new()
    //            {
    //                Title = "Choose Export location",
    //                CheckPathExists = true,
    //                CheckFileExists = false,
    //                OverwritePrompt = true,
    //                AddExtension = true,
    //                FileName = $"MyLauncher_{tStamp}_Menu_backup.json",
    //                Filter = "JSON (*.json)|*.json|All files (*.*)|*.*"
    //            };
    //            if (dlgSave.ShowDialog() == true)
    //            {
    //                try
    //                {
    //                    File.Copy(GetMenuListFile(), dlgSave.FileName, true);
    //                    log.Info($"List backed up to {dlgSave.FileName}");
    //                }
    //                catch (Exception ex)
    //                {
    //                    log.Error(ex, "Backup failed.");
    //                    SystemSounds.Exclamation.Play();
    //                    MDCustMsgBox mbox = new($"Backup failed.\n{ex.Message}.",
    //                                        "ERROR",
    //                                        ButtonType.Ok,
    //                                        true,
    //                                        true,
    //                                        null,
    //                                        true);
    //                    _ = mbox.ShowDialog();
    //                }
    //            }
    //        }
    //        #endregion Create a backup for the menu file

    //        #region Create starter main JSON file
    //        /// <summary>
    //        /// Creates a new JSON file containing entries for the Windows calculator
    //        /// </summary>
    //        /// <param name="file">Name of the main list file</param>
    //        private static void CreateNewMainJson(string file)
    //        {
    //            Guid guid = Guid.NewGuid();
    //            string thisguid = guid.ToString();
    //            StringBuilder sb = new();
    //            _ = sb.AppendLine("[")
    //                  .AppendLine("  {")
    //                  .AppendLine("    \"Title\": \"Calculator\",")
    //                  .Append("    \"ItemID\": \"")
    //                  .Append(thisguid)
    //                  .AppendLine("\",")
    //                  .AppendLine("    \"EntryType\": 0,")
    //                  .AppendLine("    \"FilePathOrURI\": \"calc.exe\",")
    //                  .AppendLine("    \"Arguments\": \"\",")
    //                  .AppendLine("    \"IconSource\": \"\",")
    //                  .AppendLine("    \"Children\": null")
    //                  .AppendLine("  }")
    //                  .AppendLine("]");
    //            File.WriteAllText(file, sb.ToString());
    //            log.Debug($"Creating new JSON file for main list - {file}");
    //        }
    //        #endregion Create starter main JSON file

    //        #region Create starter menu JSON file
    //        /// <summary>
    //        /// Creates a new JSON file containing menu item for the Windows calculator
    //        /// </summary>
    //        /// <param name="file">Name of the main list file</param>
    //        private static void CreateNewMenuJson(string file)
    //        {
    //            Guid guid = Guid.NewGuid();
    //            string thisguid = guid.ToString();
    //            StringBuilder sb = new();
    //            _ = sb.AppendLine("[")
    //                  .AppendLine("  {")
    //                  .AppendLine("    \"Title\": \"Calculator\",")
    //                  .AppendLine("    \"ItemType\": 1,")
    //                  .AppendLine("    \"FilePathOrURI\": \"calc.exe\",")
    //                  .AppendLine("    \"Arguments\": \"\",")
    //                  .Append("    \"ItemID\": \"")
    //                  .Append(thisguid)
    //                  .AppendLine("\",")
    //                  .AppendLine("    \"MenuItems\": null")
    //                  .AppendLine("  }")
    //                  .AppendLine("]");
    //            File.WriteAllText(file, sb.ToString());
    //            log.Debug($"Creating new JSON file for menu items - {file}");
    //        }
    //        #endregion Create starter menu JSON file

    //        #region Get the name of the main list JSON file
    //        /// <summary>
    //        /// Gets the filename of the JSON file containing the main list
    //        /// </summary>
    //        /// <returns>A string containing the filename</returns>
    //        public static string GetMainListFile()
    //        {
    //            return Path.Combine(AppInfo.AppDirectory, "MyLauncherItems.json");
    //        }
    //        #endregion Get the name of the main list JSON file

    //        #region Import a List file
    //        /// <summary>
    //        /// Imports a json file to be used as the List file
    //        /// </summary>
    //        /// <returns>True if successful, false otherwise</returns>
    //        internal static bool ImportListFile()
    //        {
    //            OpenFileDialog dlgOpen = new()
    //            {
    //                DefaultExt = "json",
    //                Title = "Choose a File to Import",
    //                Multiselect = false,
    //                CheckFileExists = false,
    //                CheckPathExists = true,
    //                Filter = "JSON (*.json)|*.json"
    //            };
    //            bool? result = dlgOpen.ShowDialog();
    //            if (result == true)
    //            {
    //                try
    //                {
    //                    log.Info($"Importing {dlgOpen.FileName}");
    //                    string json = File.ReadAllText(dlgOpen.FileName);
    //                    const string findit = "\"Children\":";
    //                    if (!json.Contains(findit, StringComparison.CurrentCulture))
    //                    {
    //                        log.Error($"Import failed. {dlgOpen.FileName}  is not a List Items file.");
    //                        SystemSounds.Exclamation.Play();
    //                        MDCustMsgBox mbox = new("This file is not a List Items file.",
    //                                            "ERROR",
    //                                            ButtonType.Ok,
    //                                            true,
    //                                            true,
    //                                            null,
    //                                            true);
    //                        _ = mbox.ShowDialog();
    //                        return false;
    //                    }
    //                    MyListItem.Children.Clear();
    //                    MyListItem.Children = JsonSerializer.Deserialize<ObservableCollection<MyListItem>>(json);
    //                    return true;
    //                }
    //                catch (Exception ex)
    //                {
    //                    log.Error(ex, "Import failed.");
    //                    SystemSounds.Exclamation.Play();
    //                    MDCustMsgBox mbox = new($"Import failed.\n{ex.Message}.",
    //                                        "ERROR",
    //                                        ButtonType.Ok,
    //                                        true,
    //                                        true,
    //                                        null,
    //                                        true);
    //                    _ = mbox.ShowDialog();
    //                    return false;
    //                }
    //            }
    //            return false;
    //        }
    //        #endregion Import a List file

    //        #region Import a Menu file
    //        /// <summary>
    //        /// Imports a json file to be used as the Menu file
    //        /// </summary>
    //        /// <returns>True if successful, false otherwise</returns>
    //        internal static bool ImportMenuFile()
    //        {
    //            OpenFileDialog dlgOpen = new()
    //            {
    //                DefaultExt = "json",
    //                Title = "Choose a File to Import",
    //                Multiselect = false,
    //                CheckFileExists = false,
    //                CheckPathExists = true,
    //                Filter = "JSON (*.json)|*.json"
    //            };
    //            bool? result = dlgOpen.ShowDialog();
    //            if (result == true)
    //            {
    //                try
    //                {
    //                    log.Info($"Importing {dlgOpen.FileName}");
    //                    string json = File.ReadAllText(dlgOpen.FileName);
    //                    const string findit = "\"MenuItems\":";
    //                    if (!json.Contains(findit, StringComparison.CurrentCulture))
    //                    {
    //                        log.Error($"Import failed. {dlgOpen.FileName}  is not a Menu Items file.");
    //                        SystemSounds.Exclamation.Play();
    //                        MDCustMsgBox mbox = new("This file is not a Menu Items file.",
    //                                            "ERROR",
    //                                            ButtonType.Ok,
    //                                            true,
    //                                            true,
    //                                            null,
    //                                            true);
    //                        _ = mbox.ShowDialog();
    //                        return false;
    //                    }
    //                    MyMenuItem.MLMenuItems.Clear();
    //                    MyMenuItem.MLMenuItems = JsonSerializer.Deserialize<ObservableCollection<MyMenuItem>>(json);
    //                    return true;
    //                }
    //                catch (Exception ex)
    //                {
    //                    log.Error(ex, "Import failed.");
    //                    SystemSounds.Exclamation.Play();
    //                    MDCustMsgBox mbox = new($"Import failed.\n{ex.Message}.",
    //                                        "ERROR",
    //                                        ButtonType.Ok,
    //                                        true,
    //                                        true,
    //                                        null,
    //                                        true);
    //                    _ = mbox.ShowDialog();
    //                    return false;
    //                }
    //            }
    //            return false;
    //        }
    //        #endregion Import a Menu file

    //        #region Get the name of the menu items JSON file
    //        /// <summary>
    //        /// Gets the filename of the JSON file containing the menu items
    //        /// </summary>
    //        /// <returns>A string containing the filename</returns>
    //        public static string GetMenuListFile()
    //        {
    //            return Path.Combine(AppInfo.AppDirectory, "MyMenuItems.json");
    //        }
    //        #endregion Get the name of the menu items JSON file
    //    }
    //}


//using LibraryGPT;
//using Microsoft.VisualBasic.ApplicationServices;
//using Microsoft.VisualBasic.Devices;
//using Microsoft.VisualBasic;
//using static System.ComponentModel.Design.ObjectSelectorEditor;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
//using static System.Windows.Forms.VisualStyles.VisualStyleElement;
//using System.ComponentModel;
//using System.Configuration;
//using System.Drawing;
//using System.Globalization;
//using System.IO.Packaging;
//using System.Net.NetworkInformation;
//using System.Reflection.Metadata;
//using System.Reflection;
//using System.Runtime.CompilerServices;
//using System.Text.RegularExpressions;
//using System.Windows.Forms;

/*

Install - Package Selenium.WebDriver
Install-Package Selenium.WebDriver.ChromeDriver


// This code initializes a Chrome browser, navigates to the specified URL, finds all input boxes of type 'text', and then prints their absolute XPath.

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

class Program
{
    static void Main()
    {
        // Initialize the Chrome driver
        using (IWebDriver driver = new ChromeDriver())
        {
            // Navigate to the desired web page
            driver.Navigate().GoToUrl("https://www.example.com");

            // Find all input boxes on the page
            var inputBoxes = driver.FindElements(By.TagName("input"));

            foreach (var inputBox in inputBoxes)
            {
                // Check if the input box is of type 'text'
                if (inputBox.GetAttribute("type") == "text")
                {
                    // Print the xpath of the input box
                    Console.WriteLine(GetAbsoluteXPath(inputBox, driver));
                }
            }
        }
    }

    // Function to get the absolute XPath of an element
    public static string GetAbsoluteXPath(IWebElement element, IWebDriver driver)
    {
        return (string)((IJavaScriptExecutor)driver).ExecuteScript(
            "function absoluteXPath(element) {" +
            "var comp, comps = [];" +
            "var parent = null;" +
            "var xpath = '';" +
            "var getPos = function(element) {" +
            "var position = 1, curNode;" +
            "if (element.nodeType == Node.ATTRIBUTE_NODE) {" +
            "return null;" +
            "}" +
            "for (curNode = element.previousSibling; curNode; curNode = curNode.previousSibling) {" +
            "if (curNode.nodeName == element.nodeName) {" +
            "++position;" +
            "}" +
            "}" +
            "return position;" +
            "};" +
            "if (element instanceof Document) {" +
            "return '/';" +
            "}" +
            "for (; element && !(element instanceof Document); element = element.nodeType == Node.ATTRIBUTE_NODE ? element.ownerElement : element.parentNode) {" +
            "comp = comps[comps.length] = {};" +
            "switch (element.nodeType) {" +
            "case Node.TEXT_NODE:" +
            "comp.name = 'text()';" +
            "break;" +
            "case Node.ATTRIBUTE_NODE:" +
            "comp.name = '@' + element.nodeName;" +
            "break;" +
            "case Node.PROCESSING_INSTRUCTION_NODE:" +
            "comp.name = 'processing-instruction()';" +
            "break;" +
            "case Node.COMMENT_NODE:" +
            "comp.name = 'comment()';" +
            "break;" +
            "case Node.ELEMENT_NODE:" +
            "comp.name = element.nodeName;" +
            "break;" +
            "}" +
            "comp.position = getPos(element);" +
            "}" +
            "for (var i = comps.length - 1; i >= 0; i--) {" +
            "comp = comps[i];" +
            "xpath += '/' + comp.name.toLowerCase();" +
            "if (comp.position !== null) {" +
            "xpath += '[' + comp.position + ']';" +
            "}" +
            "}" +
            "return xpath;" +
            "} return absoluteXPath(arguments[0]);", element);
    }
}


// Dictionary or a mapping that associates each dropdown option with its corresponding set of text values for the input boxes.

Dictionary<string, List<string>> inputDataMapping = new Dictionary<string, List<string>>
{
    { "Option1", new List<string> { "Text1A", "Text1B", "Text1C" } },
    { "Option2", new List<string> { "Text2A", "Text2B" } },
    // ... add more options and their corresponding text values
};

// Selects an option from the dropdown menu.

void SelectDropdownOption(IWebDriver driver, string option)
{
    var dropdown = driver.FindElement(By.Id("dropdownElementId")); // Replace with your dropdown's locator
    var selectElement = new SelectElement(dropdown);
    selectElement.SelectByText(option);
}

// Fills the input boxes with the corresponding option text values.

void FillInputBoxes(IWebDriver driver, string selectedOption)
{
    if (inputDataMapping.ContainsKey(selectedOption))
    {
        var inputTexts = inputDataMapping[selectedOption];
        var inputBoxes = driver.FindElements(By.TagName("input"));

        for (int i = 0; i < inputTexts.Count && i < inputBoxes.Count; i++)
        {
            inputBoxes[i].SendKeys(inputTexts[i]);
        }
    }
}


Integrate the Functions:
Integrate the functions in your main code to automate the process.

using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System.Collections.Generic;

class Program
{
    static Dictionary<string, List<string>> inputDataMapping = new Dictionary<string, List<string>>
    {
        { "Option1", new List<string> { "Text1A", "Text1B", "Text1C" } },
        { "Option2", new List<string> { "Text2A", "Text2B" } },
        // ... add more options and their corresponding text values
    };

    static void Main()
    {
        using (IWebDriver driver = new ChromeDriver())
        {
            driver.Navigate().GoToUrl("https://www.example.com");

            // Select an option from the dropdown
            SelectDropdownOption(driver, "Option1");

            // Fill the input boxes based on the selected option
            FillInputBoxes(driver, "Option1");
        }
    }

    static void SelectDropdownOption(IWebDriver driver, string option)
    {
        var dropdown = driver.FindElement(By.Id("dropdownElementId")); // Replace with your dropdown's locator
        var selectElement = new SelectElement(dropdown);
        selectElement.SelectByText(option);
    }

    static void FillInputBoxes(IWebDriver driver, string selectedOption)
    {
        if (inputDataMapping.ContainsKey(selectedOption))
        {
            var inputTexts = inputDataMapping[selectedOption];
            var inputBoxes = driver.FindElements(By.TagName("input"));

            for (int i = 0; i < inputTexts.Count && i < inputBoxes.Count; i++)
            {
                inputBoxes[i].SendKeys(inputTexts[i]);
            }
        }
    }




}






namespace HelperSharp
{
    /// <summary>
    /// Exception helper.
    /// </summary>
    public static class ExceptionHelper
    {
        /// <summary>
        /// Throws an ArgumentNullException if argument is null.
        /// </summary>
        /// <param name="argumentName">The argument name.</param>
        /// <param name="argument">The argument.</param>
        public static void ThrowIfNull(string argumentName, object argument)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        /// <summary>
        /// Throws an ArgumentNullException if argument is null or an ArgumentException if string is empty.
        /// </summary>
        /// <param name="argumentName">The argument name.</param>
        /// <param name="argument">The argument.</param>
        public static void ThrowIfNullOrEmpty(string argumentName, string argument)
        {
            ThrowIfNull(argumentName, argument);

            if (string.IsNullOrEmpty(argument))
            {
                throw new ArgumentException("Argument '{0}' can't be empty.".With(argumentName), argumentName);
            }
        }
    }



//   This code allows the user to open a file dialog and select a file. It's useful when you want to let the user select a file to be processed.

    OpenFileDialog openFileDialog = new OpenFileDialog();
    if (openFileDialog.ShowDialog() == DialogResult.OK)
    {
        string selectedFilePath = openFileDialog.FileName;
        // Process the selected file
    }
   ```


   // This code allows the user to open a save file dialog and specify where they want to save a file.

   SaveFileDialog saveFileDialog = new SaveFileDialog();
if (saveFileDialog.ShowDialog() == DialogResult.OK)
{
    string savePath = saveFileDialog.FileName;
    // Save your data to the specified path
}
   ```


 //  This code displays a message box to the user. It's useful for showing notifications or asking for confirmations.

   MessageBox.Show("This is a message!", "Message Title", MessageBoxButtons.OK, MessageBoxIcon.Information);



 //  This code starts a background task, which is useful for performing long-running operations without freezing the UI.

   using System.ComponentModel;

    BackgroundWorker backgroundWorker = new BackgroundWorker();
    backgroundWorker.DoWork += (sender, e) =>
    {
        // Long-running task here
    };
    backgroundWorker.RunWorkerCompleted += (sender, e) =>
    {
        // Task completed. Update UI here
    };
    backgroundWorker.RunWorkerAsync();



 //  This code adds items to a ComboBox control.
        csharp
     comboBox1.Items.Add("Item 1");
    comboBox1.Items.Add("Item 2");
    comboBox1.Items.Add("Item 3");



 //  This code handles the event when the form is about to close. It's useful for asking the user for confirmation or saving data before closing.

   private void Form1_FormClosing(object sender, FormClosingEventArgs e)
{
    if (MessageBox.Show("Are you sure you want to exit?", "Confirm Exit", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
    {
        e.Cancel = true;
    }
}




 //  This code demonstrates how to use a timer to execute code at regular intervals.

       Timer timer = new Timer();
    timer.Interval = 1000; // 1 second
    timer.Tick += (sender, e) =>
    {
        // Code to be executed every second
    };
    timer.Start();



 //  This code dynamically creates a button and adds it to the form.

   Button dynamicButton = new Button();
    dynamicButton.Text = "Click Me!";
    dynamicButton.Location = new Point(50, 50);
    dynamicButton.Click += (sender, e) =>
    {
        MessageBox.Show("Button was clicked!");
    };
    this.Controls.Add(dynamicButton);



  // This code binds a list of objects to a DataGridView.

   public class Person
    {
        public string Name { get; set; }
        public int Age { get; set; }
    }

    List<Person> people = new List<Person>
       {
           new Person { Name = "John", Age = 30 },
           new Person { Name = "Jane", Age = 25 }
    };

    dataGridView1.DataSource = people;



  //  This code changes the opacity of the form, making it semi-transparent.

    this.Opacity = 0.75; // 75% opacity



 //   This code allows users to drag and drop files onto the form.

    this.AllowDrop = true;
    this.DragEnter += (sender, e) =>
    {
        if (e.Data.GetDataPresent(DataFormats.FileDrop))
            e.Effect = DragDropEffects.Copy;
    };
    this.DragDrop += (sender, e) =>
    {
        string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
        foreach (string file in files)
        {
            // Process each file
        }
    };



 //   This code resizes a control (e.g., a TextBox) proportionally as the form is resized.

    private void Form1_Resize(object sender, EventArgs e)
    {
        textBox1.Width = this.Width - 40; // Adjust as needed
    }


 //   This code retrieves the selected radio button from a group of radio buttons on a panel.

    RadioButton selectedRadioButton = panel1.Controls.OfType<RadioButton>().FirstOrDefault(r => r.Checked);
    if (selectedRadioButton != null)
    {
        string selectedValue = selectedRadioButton.Text;
    }



    // This code hides the form to the system tray when minimized.

    NotifyIcon notifyIcon = new NotifyIcon();
    notifyIcon.Icon = SystemIcons.Application;
    notifyIcon.DoubleClick += (sender, e) =>
    {
        this.Show();
        this.WindowState = FormWindowState.Normal;
    };

    private void Form1_Resize(object sender, EventArgs e)
    {
        if (this.WindowState == FormWindowState.Minimized)
        {
            this.Hide();
            notifyIcon.Visible = true;
        }
    }
  

 //  Implementing a logging system can be crucial for debugging and tracking user actions.
 
   private void Log(string message)
    {
        // Assuming you have a TextBox or RichTextBox for logging
        txtLog.AppendText($"{DateTime.Now}: {message}\n");
    }
  


 //  Displaying a user-friendly error message when something goes wrong.

   private void HandleError(Exception ex)
    {
        Log($"Error: {ex.Message}");
        MessageBox.Show($"An error occurred: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
    }



  // Displaying a loading animation or message when a long-running task is being performed.

    private void ShowLoading(bool isLoading)
    {
        if (isLoading)
        {
            // Assuming you have a PictureBox with a loading GIF
            picLoading.Visible = true;
            Log("Loading...");
        }
        else
        {
            picLoading.Visible = false;
        }
    }



 //  Using delegates to fetch data can make your code more modular and allow for easier testing.

    public delegate void DataFetchedHandler(object sender, DataFetchedEventArgs e);
    public event DataFetchedHandler DataFetched;

    public class DataFetchedEventArgs : EventArgs
    {
        public string Data { get; set; }
    }


// Triggering the DataFetched Event

    private static void OnDataFetched(string data)
    {
        DataFetched?.Invoke(this, new DataFetchedEventArgs { Data = data });
    }
  


  
 //  This pattern can be useful if you want certain parts of your application to react to changes in another part without them being directly linked.

   public interface IObserver
{
    void Update(string message);
}

    public interface ISubject
    {
        void RegisterObserver(IObserver observer);
        void RemoveObserver(IObserver observer);
        void NotifyObservers(string message);
    }



 //  Storing configurations like API endpoints, keys, or other settings in an external file can make your application more flexible.

   private void LoadConfigurations()
    {
        string apiEndpoint = ConfigurationManager.AppSettings["ApiEndpoint"];
        // Use the configurations as needed
    }
   


 //  Applying a consistent theme or style across your application can enhance the user experience.

   private void ApplyTheme()
   {
       this.BackColor = Color.LightGray;
       // Set colors, fonts, and other styles for controls
   }



  // Creating custom events can help in communicating between different parts of your application.

   public delegate void CustomEventHandler(object sender, CustomEventArgs e);
   public event CustomEventHandler CustomEventRaised;

   public class CustomEventArgs : EventArgs
   {
       public string CustomData { get; set; }
   }

   private void RaiseCustomEvent(string data)
   {
       CustomEventRaised?.Invoke(this, new CustomEventArgs { CustomData = data });
   }




  // Dynamically creating controls based on data or user input.

   private void CreateDynamicButtons(List<string> buttonNames)
   {
       int startY = 10;
       foreach (var name in buttonNames)
       {
           Button btn = new Button
           {
               Text = name,
               Location = new Point(10, startY)
           };
           btn.Click += DynamicButton_Click;
           this.Controls.Add(btn);
           startY += btn.Height + 10;
       }
   }

   private void DynamicButton_Click(object sender, EventArgs e)
   {
       Button clickedButton = sender as Button;
       MessageBox.Show($"You clicked: {clickedButton.Text}");
   }


//   Interacting with the system clipboard to copy or paste data.

   private void CopyToClipboard(string data)
   {
       Clipboard.SetText(data);
   }

   private string PasteFromClipboard()
   {
       return Clipboard.GetText();
   }



//   Displaying modal dialogs to get user input or show messages.

   private void ShowModalDialog()
   {
       using (Form modalForm = new Form())
       {
           modalForm.Text = "Modal Dialog";
           modalForm.ShowDialog(this);
       }
   }



 //  Performing actions after a certain delay or at regular intervals.

   Timer actionTimer = new Timer();

   private void StartTimedAction()
   {
       actionTimer.Interval = 5000; // 5 seconds
       actionTimer.Tick += ActionTimer_Tick;
       actionTimer.Start();
   }

   private void ActionTimer_Tick(object sender, EventArgs e)
   {
       // Perform the timed action
       Log("Timed action performed!");
   }
  


 //  Implementing keyboard shortcuts for faster user operations.

   protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
   {
       if (keyData == (Keys.Control | Keys.S))
       {
           SaveData(); // Or any other action
           return true;
       }
       return base.ProcessCmdKey(ref msg, keyData);
   }



 //  Allowing users to drag and drop files onto the application.

   this.AllowDrop = true;
   this.DragEnter += Form_DragEnter;
   this.DragDrop += Form_DragDrop;

   private void Form_DragEnter(object sender, DragEventArgs e)
   {
       if (e.Data.GetDataPresent(DataFormats.FileDrop))
           e.Effect = DragDropEffects.Copy;
   }

   private void Form_DragDrop(object sender, DragEventArgs e)
   {
       string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
       foreach (string file in files)
       {
           // Process each file
           Log($"File dropped: {file}");
       }
   }



 //  Supporting multiple languages in your application.

   private void SetLanguage(string cultureName)
   {
       CultureInfo culture = new CultureInfo(cultureName);
       Thread.CurrentThread.CurrentUICulture = culture;
       // Update UI elements with localized strings
   }



 //  Validating user input before processing.

   private bool ValidateInput(string input)
   {
       if (string.IsNullOrWhiteSpace(input))
       {
           MessageBox.Show("Input cannot be empty!", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
           return false;
       }
       // Add more validation rules as needed
       return true;
   }



 //  Displaying application status or messages in a status bar.

   private void UpdateStatusBar(string message)
   {
       statusBarLabel.Text = message;
   }



//   Creating reusable custom controls for consistent UI elements.

   public class CustomTextBox : TextBox
   {
       // Add custom properties, methods, or events
   }
 



   private async void btnStartOperation_Click(object sender, EventArgs e)
   {
       await LongRunningOperationAsync();
       MessageBox.Show("Operation completed!");
   }



 //  Creating custom events to notify other parts of your application.

   public event EventHandler<CustomEventArgs> CustomEvent;

   public class CustomEventArgs : EventArgs
   {
       public string CustomMessage { get; set; }
   }

   private void RaiseCustomEvent(string message)
   {
       CustomEvent?.Invoke(this, new CustomEventArgs { CustomMessage = message });
   }



//  Filtering or transforming lists of data.

   List<string> items = new List<string> { "apple", "banana", "cherry" };
   var filteredItems = items.Where(item => item.StartsWith("a")).ToList();



 //  Organizing your UI using tabs.

   TabControl tabControl = new TabControl();
   TabPage tabPage1 = new TabPage("Tab 1");
   TabPage tabPage2 = new TabPage("Tab 2");
   tabControl.TabPages.Add(tabPage1);
   tabControl.TabPages.Add(tabPage2);
   this.Controls.Add(tabControl);


 //  Storing user preferences for a personalized experience.

   private void SaveUserPreferences()
   {
       Properties.Settings.Default.UserName = txtUserName.Text;
       Properties.Settings.Default.Save();
   }

   private void LoadUserPreferences()
   {
       txtUserName.Text = Properties.Settings.Default.UserName;
   }



 //  Adding right-click options to controls.

   ContextMenuStrip contextMenu = new ContextMenuStrip();
   contextMenu.Items.Add("Option 1");
   contextMenu.Items.Add("Option 2");
   txtInput.ContextMenuStrip = contextMenu;



 //  Enhancing the appearance of controls.

   private void panel1_Paint(object sender, PaintEventArgs e)
   {
       e.Graphics.DrawEllipse(Pens.Red, 10, 10, 50, 50);
   }



  // Providing additional information on hover.

   ToolTip toolTip = new ToolTip();


//   For background operations with the ability to report progress and handle completion.

   BackgroundWorker worker = new BackgroundWorker();
   worker.DoWork += Worker_DoWork;
   worker.ProgressChanged += Worker_ProgressChanged;
   worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
   worker.WorkerReportsProgress = true;
   worker.RunWorkerAsync();

   private void Worker_DoWork(object sender, DoWorkEventArgs e)
   {
       // Long-running task
       (sender as BackgroundWorker).ReportProgress(50);
   }

   private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
   {
       progressBar.Value = e.ProgressPercentage;
   }

   private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
   {
       MessageBox.Show("Task completed!");
   }


 //  Reading and writing to files.

   private void SaveToFile(string content, string path)
   {
       File.WriteAllText(path, content);
   }

   private string ReadFromFile(string path)
   {
       return File.ReadAllText(path);
   }



 //  Simple database operations using ADO.NET (assuming you have a connection string).

   using (SqlConnection connection = new SqlConnection(connectionString))
   {
       connection.Open();
       using (SqlCommand command = new SqlCommand("SELECT * FROM Users", connection))
       {
           SqlDataReader reader = command.ExecuteReader();
           while (reader.Read())
           {
               string username = reader["Username"].ToString();
               // Process data
           }
       }
   }



//   Running multiple operations in parallel.

   Parallel.Invoke(() => Method1(), () => Method2());




 //  This is the modern approach to run asynchronous operations without blocking the main thread. It's simpler and more readable than traditional threading methods.


   private async void btnStart_Click(object sender, EventArgs e)
   {
       await UpdateTextBoxAsync();
   }

   private async Task UpdateTextBoxAsync()
   {
       for (int i = 0; i < 10; i++)
       {
           txtOutput.AppendText($"Updating line {i + 1}\n");
           await Task.Delay(1000); // Simulate a long-running task
       }
   }

//   `BackgroundWorker` is a component that allows operations to run on a separate, dedicated thread. It provides events to safely update the UI when the operation is done.

   private BackgroundWorker _worker = new BackgroundWorker();

   public MainForm()
   {
       InitializeComponent();
       _worker.DoWork += Worker_DoWork;
       _worker.ProgressChanged += Worker_ProgressChanged;
       _worker.WorkerReportsProgress = true;
   }

   private void btnStart_Click(object sender, EventArgs e)
   {
       _worker.RunWorkerAsync();
   }

   private void Worker_DoWork(object sender, DoWorkEventArgs e)
   {
       for (int i = 0; i < 10; i++)
       {
           _worker.ReportProgress(i + 1);
           Thread.Sleep(1000); // Simulate a long-running task
       }
   }

   private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
   {
       txtOutput.AppendText($"Updating line {e.ProgressPercentage}\n");
   }
   ```

//   If you're using traditional threads, you'll need to use the `Invoke` method to safely update the UI from another thread.

   private Thread _updateThread;

   private void btnStart_Click(object sender, EventArgs e)
   {
       _updateThread = new Thread(new ThreadStart(UpdateTextBox));
       _updateThread.Start();
   }

   private void UpdateTextBox()
   {
       for (int i = 0; i < 10; i++)
       {
           AppendText($"Updating line {i + 1}\n");
           Thread.Sleep(1000); // Simulate a long-running task
       }
   }

   private void AppendText(string text)
   {
       if (txtOutput.InvokeRequired)
       {
           txtOutput.Invoke(new Action<string>(AppendText), text);
       }
       else
       {
           txtOutput.AppendText(text);
       }
   }


//   If you have operations that don't need to update the UI and can run independently, you can use `Task.Run`.

   private void btnStart_Click(object sender, EventArgs e)
   {
       Task.Run(() => 
       {
           // Long-running task
           Thread.Sleep(5000);
           AppendText("Task completed!\n");
       });
   }

*/
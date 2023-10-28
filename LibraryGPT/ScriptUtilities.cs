using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryGPT
{
    public static class ScriptUtilities
    {
        public static async Task RunScriptAsync(string scriptPath, Control outputControl)
        {
            using Process process = new();
            try
            {
                process.StartInfo.FileName = DetermineInterpreter(scriptPath);
                process.StartInfo.Arguments = $"\"{scriptPath}\"";
                process.StartInfo.RedirectStandardOutput = true;
                process.StartInfo.RedirectStandardError = true;
                process.StartInfo.UseShellExecute = false;
                process.StartInfo.CreateNoWindow = true;

                process.OutputDataReceived += (sender, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        UpdateOutputControl(outputControl, e.Data);
                    }
                };

                process.ErrorDataReceived += (sender, e) =>
                {
                    if (!string.IsNullOrEmpty(e.Data))
                    {
                        UpdateOutputControl(outputControl, e.Data);
                    }
                };

                process.Start();
                process.BeginOutputReadLine();
                process.BeginErrorReadLine();

                await Task.Run(() => process.WaitForExit());
            }
            catch (Exception ex)
            {
                UpdateOutputControl(outputControl, $"Error: {ex.Message}");
            }
        }

        private static string DetermineInterpreter(string scriptPath)
        {
            string extension = Path.GetExtension(scriptPath).ToLower();

            return extension switch
            {
                ".ps1" => "powershell",
                ".bat" => "cmd",
                ".py" => "python",
                _ => throw new Exception("Unsupported script type"),
            };
        }

        private static void UpdateOutputControl(Control control, string message)
        {
            control.ExecuteOnUIThread(() =>
            {
                // Check if the control has an 'AppendText' method (like TextBox)
                var appendTextMethod = control.GetType().GetMethod("AppendText");
                if (appendTextMethod != null)
                {
                    appendTextMethod.Invoke(control, new object[] { message + Environment.NewLine });
                    return;
                }

                // Check if the control has a 'Text' property (like Label, KryptonLabel, etc.)
                var textProperty = control.GetType().GetProperty("Text");
                if (textProperty != null)
                {
                    textProperty.SetValue(control, message);
                    return;
                }

                // You can add more checks here if there are other custom methods or properties you want to support.
            });
        }
    }
}

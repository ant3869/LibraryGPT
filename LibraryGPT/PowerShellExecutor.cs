using System;
using System.Management.Automation;
using System.Management.Automation.Runspaces;

namespace LibraryGPT
{
    public static class PowerShellExecutor
    {
        /// <summary>
        /// Executes a PowerShell script and outputs data in real-time. This function creates a PowerShell runspace,
        /// executes the provided script, and handles output streams (such as error, warning, and information) asynchronously.
        /// This allows for real-time output of script execution progress and results.
        /// </summary>
        /// <param name="script">The PowerShell script to execute. This should be a string containing valid PowerShell commands.</param>
        /// <example>
        /// Example of executing a simple PowerShell script:
        /// <code>
        /// string script = "Get-Process | Where-Object { $_.CPU -gt 100 }";
        /// PowerShellExecutor.ExecuteScript(script);
        /// </code>
        /// This will output information about processes with CPU usage greater than 100.
        ///
        /// Example of executing a more complex script involving variables and multiple commands:
        /// <code>
        /// string complexScript = @"
        ///     $diskInfo = Get-Disk | Select-Object -First 1
        ///     Write-Host ('Disk Name: ' + $diskInfo.FriendlyName)
        ///     Write-Host ('Disk Size: ' + $diskInfo.Size)
        /// ";
        /// PowerShellExecutor.ExecuteScript(complexScript);
        /// </code>
        /// This will output the name and size of the first disk found on the system.
        /// </example>
        public static void ExecuteScript(string script)
        {
            using Runspace runspace = RunspaceFactory.CreateRunspace();
            runspace.Open();

            using (PowerShell ps = PowerShell.Create())
            {
                ps.Runspace = runspace;
                ps.AddScript(script);

                ps.Streams.Error.DataAdded += (sender, e) =>
                {
                    if (sender is PSDataCollection<ErrorRecord> errorStream && e.Index >= 0 && e.Index < errorStream.Count)
                    {
                        var error = errorStream[e.Index];

                        if (error != null)
                            Console.WriteLine("Error: " + error.ToString());
                    }
                };

                ps.Streams.Warning.DataAdded += (sender, e) =>
                {
                    if (sender is PSDataCollection<WarningRecord> warningStream && e.Index >= 0 && e.Index < warningStream.Count)
                    {
                        var warning = warningStream[e.Index];

                        if (warning != null)
                            Console.WriteLine("Warning: " + warning.Message);
                    }
                };

                ps.Streams.Information.DataAdded += (sender, e) =>
                {
                    if (sender is PSDataCollection<InformationRecord> informationStream && e.Index >= 0 && e.Index < informationStream.Count)
                    {
                        var info = informationStream[e.Index];

                        if (info != null)
                            Console.WriteLine("Info: " + info.MessageData);
                    }
                };

                // Begin invoke executes the script asynchronously
                IAsyncResult asyncResult = ps.BeginInvoke();

                // Do something else if needed while PowerShell runs
                // ...

                // Wait for the script to complete
                ps.EndInvoke(asyncResult);
            }

            runspace.Close();
        }
    }
}
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Management.Automation.Runspaces;
using System.Management.Automation;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using System.DirectoryServices;

namespace LibraryGPT
{
    public static class WorkstationHelper
    {
        public static async Task InstallPrinterDriverAsync(string machineName, string driverPath, string installerName)
        {
            // PowerShell script to remotely invoke the printer driver installer
            string script = $@"
            $installerPath = Join-Path -Path '{driverPath}' -ChildPath '{installerName}.exe'
            Start-Process -FilePath $installerPath -ArgumentList '/S' -Wait
            ";

            await PowerShellExecutor.ExecuteRemoteScriptAsync(machineName, script);
        }

        private static async Task InstallPrinterDriver8020Async(string machineName)
        {
            string driverPath = @"\\phont85023usa\";
            string installerPcl = "PrinterReleasePCL";
            string installerPs = "PrinterReleasePS";

            // Wait for the PCL driver to install before starting PS driver installation
            await InstallPrinterDriverAsync(machineName, driverPath, installerPcl);
            await InstallPrinterDriverAsync(machineName, driverPath, installerPs);
        }

        private static async Task InstallPrinterDriver8025Async(string machineName)
        {
            string driverPath = @"\\phont85025usa\";
            string installerPcl = "PrinterReleasePCL";
            string installerPs = "PrinterReleasePS";

            // Wait for the PCL driver to install before starting PS driver installation
            await InstallPrinterDriverAsync(machineName, driverPath, installerPcl);
            await InstallPrinterDriverAsync(machineName, driverPath, installerPs);
        }

        public static bool PingHost(string hostname)
        {
            using var ping = new Ping();
            try
            {
                var reply = ping.Send(hostname);
                return reply.Status == IPStatus.Success;
            }
            catch
            {
                return false;
            }
        }

        public static void ContinuousPing(string hostname, CancellationToken token)
        {
            using var ping = new Ping();

            while (!token.IsCancellationRequested)
            {
                var reply = ping.Send(hostname);

                if (reply.Status == IPStatus.Success)
                {
                    // Log success or do something
                }
                else
                {
                    // Log failure or do something
                }

                Thread.Sleep(1000); // Sleep for a second or as needed
            }
        }


        public static string? GetFqdn(string hostname)
        {
            try
            {
                var hostEntry = Dns.GetHostEntry(hostname);
                return hostEntry.HostName;
            }
            catch
            {
                return null;
            }
        }

        public static void OpenRemoteDesktop(string hostname)
        {
            Process.Start("mstsc", $"/v:{hostname}");
        }

        public static void ExecuteRemoteScript(string hostname, string scriptText)
        {
            using var runspace = RunspaceFactory.CreateRunspace();
            runspace.Open();
            PowerShell ps = PowerShell.Create();
            ps.Runspace = runspace;

            try
            {
                // Create the remote session
                ps.AddCommand("New-PSSession").AddParameter("ComputerName", hostname);
                var remoteSession = ps.Invoke()[0];

                // Check for errors in creating the session
                if (ps.Streams.Error.Count > 0 || remoteSession == null)
                {
                    // Handle errors or throw
                    throw new Exception("Failed to create a remote session.");
                }

                // Dispose of the initial PowerShell instance
                ps.Dispose();

                // Use a new PowerShell instance to invoke the script within the remote session
                using var psInvoke = PowerShell.Create();
                psInvoke.Runspace = runspace;
                psInvoke.AddScript(scriptText);
                psInvoke.AddParameter("Session", remoteSession);
                psInvoke.Invoke(); // Executes the script

                // Check for invocation errors
                if (psInvoke.Streams.Error.Count > 0)
                {
                    // Handle errors or throw
                    throw new Exception("Errors occurred while running the script.");
                }
            }
            finally
            {
                // Dispose of the PowerShell instance if it hasn't been already
                ps?.Dispose();
            }
        }

        public static void UpdateGroupPolicy(string hostname)
        {
            var script = $"Invoke-GPUpdate -Computer {hostname} -Force";
            ExecuteRemoteScript(hostname, script);
        }

        public static void RestartRemoteService(string hostname, string serviceName)
        {
            var script = $@"Get-Service -ComputerName {hostname} -Name {serviceName} | Restart-Service -Force";
            ExecuteRemoteScript(hostname, script);
        }

        //public static List<EventLogEntry> GetRemoteEventLogs(string hostname, string logName)
        //{
        //    string? script = $@"Get-EventLog -LogName {logName} -ComputerName {hostname}";
        //    // Execute and process the script results
        //    // Return a list of EventLogEntry objects
        //    ExecuteRemoteScript(hostname, script);
        //}

        public static void ClearChromeCacheRemotely(string hostname, string username)
        {
            var script = $@"
            $folders = @('Archived History', 'Cache\*', 'Cookies', 'History', 'Login Data', 'Top Sites', 'Visited Links', 'Web Data')
            foreach ($folder in $folders) {{
                $path = ""C:\Users\{username}\AppData\Local\Google\Chrome\User Data\Default\$folder""
                Remove-Item -Path $path -Recurse -Force -ErrorAction SilentlyContinue
            }}";

            ExecuteRemoteScript(hostname, script);
        }

        public static List<DirectoryEntry> SearchActiveDirectory(string searchFilter)
        {
            var directoryEntries = new List<DirectoryEntry>();

            using (var searcher = new DirectorySearcher(new DirectoryEntry("LDAP://RootDSE")))
            {
                searcher.Filter = searchFilter;

                foreach (SearchResult result in searcher.FindAll())
                {
                    directoryEntries.Add(result.GetDirectoryEntry());
                }
            }

            return directoryEntries;
        }

        public static void OpenRemoteMachineFileExplorer(string machineName) // Example: OpenRemoteMachineFileExplorer("DESKTOP-12345");
        {
            string remotePath = $@"\\{machineName}\c$";

            try
            {
                Process.Start("explorer.exe", remotePath);
            }
            catch (Exception ex)
            {
                // Log the exception or display a message to the user
                // Could not open the remote path, possibly due to insufficient privileges or network issues.
            }
        }

        public static async Task<List<string>> GetPackageLogDirectoriesAsync(string machineName)
        {
            // Example: var directoryNames = await GetPackageLogDirectoriesAsync("REMOTE_PC_NAME");


            var script = $@"
                $directories = Get-ChildItem -Path \\{machineName}\c$\windows\packagelogs -Directory
                $directories.Name
            ";

            using var runspace = RunspaceFactory.CreateRunspace();
            runspace.Open();

            using var powershell = PowerShell.Create();
            powershell.Runspace = runspace;
            powershell.AddScript(script);

            try
            {
                var results = await Task.Factory.FromAsync(powershell.BeginInvoke(), powershell.EndInvoke);
                var directories = results.Select(psObject => psObject.ToString()).ToList();
                return directories;
            }
            catch (Exception ex)
            {
                // Log or handle exceptions
                Console.WriteLine($"An error occurred: {ex.Message}");
                return new List<string>(); // Return an empty list on error
            }
        }


    }
}

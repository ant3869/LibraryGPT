using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Management.Automation;
using System.Management.Automation.Runspaces;
using System.Text;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace LibraryGPT
{
    public static class Maintenance
    {
        /// <summary>
        /// Deletes the Windows.old folder which contains previous installations of Windows.
        /// </summary>
        public static void DeleteWindowsOld()
        {
            var windowsOldPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Windows.old");

            if (Directory.Exists(windowsOldPath))
                Directory.Delete(windowsOldPath, true);
        }

        /// <summary>
        /// Deletes temporary files from the system's temp directory.
        /// </summary>
        public static void DeleteTempFiles()
        {
            var tempPath = Path.GetTempPath();

            foreach (var file in Directory.GetFiles(tempPath))
            {
                try
                {
                    File.Delete(file);
                }
                catch
                {
                    // Handle exceptions, e.g. file might be in use
                }
            }
        }

        /// <summary>
        /// Deletes temporary internet files.
        /// </summary>
        public static void DeleteTemporaryInternetFiles()
        {
            var tempInternetFilesPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Microsoft", "Windows", "INetCache");
            
            if (Directory.Exists(tempInternetFilesPath))
            {
                foreach (var file in Directory.GetFiles(tempInternetFilesPath))
                {
                    try
                    {
                        File.Delete(file);
                    }
                    catch
                    {
                        // Handle exceptions
                    }
                }
            }
        }

        /// <summary>
        /// Asynchronously deletes prefetch files.
        /// </summary>
        public static async Task DeletePrefetchFilesAsync()
        {
            string prefetchPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Windows), "Prefetch");

            if (Directory.Exists(prefetchPath))
            {
                var files = Directory.GetFiles(prefetchPath, "*.pf");

                foreach (var file in files)
                {
                    await Task.Run(() => File.Delete(file));
                }
            }
        }

        /// <summary>
        /// Asynchronously deletes the contents of the recycle bin.
        /// </summary>
        public static async Task DeleteRecycleBinContentsAsync()
        {
            // This uses a shell command to empty the recycle bin.
            await Task.Run(() =>
            {
                Process.Start("cmd.exe", "/c " + "rd /s /q C:\\$Recycle.Bin");
            });
        }

        /// <summary>
        /// Asynchronously optimizes startup programs. 
        /// Note: This is a placeholder. Actual implementation will depend on the specifics of what you want to achieve.
        /// </summary>
        public static async Task OptimizeStartupProgramsAsync()
        {
            await Task.Run(() =>
            {
                // Placeholder for logic to optimize startup programs.
            });
        }

        /// <summary>
        /// Asynchronously cleans the registry.
        /// Note: Be very careful with this one. Incorrect modifications can harm the system.
        /// </summary>
        public static async Task CleanRegistryAsync()
        {
            await Task.Run(() =>
            {
                // Placeholder for logic to clean the registry.
                // Consider using a trusted library or tool to handle registry cleaning.
            });
        }

        /// <summary>
        /// Asynchronously defragments the hard drive.
        /// Note: This should be used for HDDs, not SSDs.
        /// </summary>
        public static async Task DefragmentHardDriveAsync(string driveLetter = "C:")
        {
            await Task.Run(() =>
            {
                Process.Start("defrag", $"{driveLetter} /O");
            });
        }

        /// <summary>
        /// Clears the Chrome cache on a remote computer for a given user.
        /// </summary>
        /// <param name="username">The username of the account to remove Chrome data from</param>
        /// <param name="computerName">The name of the remote computer</param>
        public static async Task ClearRemoteChromeCacheAsync(string username, string computerName)
        {
            StringBuilder sb = new();

            sb.AppendLine("$folders = 'Archived History', 'Cache\\*', 'Network\\Cookies', 'History', 'Login Data', 'Top Sites', 'Visited Links', 'Web Data'");
            sb.AppendLine("$scriptBlock = {");
            sb.AppendLine("    Stop-Process -Name chrome -Force");
            sb.AppendLine("    Start-Sleep -Seconds 5");
            sb.AppendLine("    foreach ($folder in $folders) {");
            sb.AppendLine($"       $path = \"\\users\\{username}\\appdata\\local\\google\\chrome\\User Data\\Default\\$folder\"");
            sb.AppendLine("        if (Test-Path $path) {");
            sb.AppendLine("            Remove-Item $path -Recurse -Force");
            sb.AppendLine("        }");
            sb.AppendLine("    }");
            sb.AppendLine("}");
            sb.AppendLine($"$session = New-PSSession -ComputerName {computerName} -ErrorAction SilentlyContinue");
            sb.AppendLine("if (!$Error) {");
            sb.AppendLine("    Invoke-Command -Session $session -ScriptBlock $scriptBlock");
            sb.AppendLine("    Remove-PSSession -Session $session");
            sb.AppendLine("} else {");
            sb.AppendLine("    Write-Host $Error");
            sb.AppendLine("}");

            await Task.Run(() =>
            {
                PowerShellExecutor.ExecuteScript(sb.ToString());
            });
        }
    }
}

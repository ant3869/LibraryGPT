using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            {
                Directory.Delete(windowsOldPath, true);
            }
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
    }
}

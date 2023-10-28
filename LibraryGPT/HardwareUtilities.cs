using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace LibraryGPT
{
    public static class HardwareUtilities
    {
        // Gets the CPU name.
        public static string GetCPUName()
        {
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_Processor"))
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    return obj["Name"].ToString();
                }
            }
            return "Unknown";
        }

        // Gets the total RAM in GB.
        public static double GetTotalRAMInGB()
        {
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_ComputerSystem"))
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    double totalRam = double.Parse(obj["TotalPhysicalMemory"].ToString()) / (1024 * 1024 * 1024);
                    return Math.Round(totalRam, 2);
                }
            }
            return 0;
        }

        // Gets the computer's name.
        public static string GetComputerName()
        {
            return Environment.MachineName;
        }

        // Gets the operating system name.
        public static string GetOperatingSystemName()
        {
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_OperatingSystem"))
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    return obj["Caption"].ToString();
                }
            }
            return "Unknown";
        }

        // Checks if a specific device is connected (e.g., a USB device).
        public static bool IsDeviceConnected(string deviceId)
        {
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher($"SELECT * FROM Win32_PnPEntity WHERE DeviceID = '{deviceId}'"))
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    return true;
                }
            }
            return false;
        }

        // Gets the list of all connected USB devices.
        public static string[] GetConnectedUSBDevices()
        {
            var devices = new System.Collections.Generic.List<string>();
            using (ManagementObjectSearcher searcher = new ManagementObjectSearcher("SELECT * FROM Win32_USBHub"))
            {
                foreach (ManagementObject obj in searcher.Get())
                {
                    devices.Add(obj["DeviceID"].ToString());
                }
            }
            return devices.ToArray();
        }
    }
}

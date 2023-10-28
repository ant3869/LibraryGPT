using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryGPT
{
    public static class ConfigUtilities
    {
        // Fetch a configuration setting by its key.
        public static string? GetConfigValue(string key)
        {
            return ConfigurationManager.AppSettings[key];
        }

        // Update or add a configuration setting.
        public static void SetConfigValue(string key, string value)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            config.AppSettings.Settings[key].Value = value;
            config.Save(ConfigurationSaveMode.Modified);
            ConfigurationManager.RefreshSection("appSettings");
        }

        // Fetch an environment variable.
        public static string? GetEnvironmentVariable(string variableName)
        {
            return Environment.GetEnvironmentVariable(variableName);
        }

        // Check if a configuration file exists.
        public static bool ConfigFileExists(string filePath)
        {
            return File.Exists(filePath);
        }

        // Create a new configuration file with default settings.
        public static void CreateDefaultConfigFile(string filePath)
        {
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                writer.WriteLine("[appSettings]");
                // Add default settings here.
                writer.WriteLine("DefaultSetting1=Value1");
                writer.WriteLine("DefaultSetting2=Value2");
                // ...
            }
        }

        // Other configuration-related utility methods can be added here...
    }
}

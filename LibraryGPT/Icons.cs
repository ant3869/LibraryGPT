using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using Microsoft.Win32;

namespace LibraryGPT
{
    public static class Icons
    {
        #region Custom Exceptions

        /// <summary>
        /// Exception thrown when the specified icon is not found.
        /// </summary>
        public class IconNotFoundException : Exception
        {
            public IconNotFoundException(string fileName, int index)
                : base($"Icon with Id = {index} wasn't found in file {fileName}")
            {
            }
        }

        /// <summary>
        /// Exception thrown when unable to extract the specified icons.
        /// </summary>
        public class UnableToExtractIconsException : Exception
        {
            public UnableToExtractIconsException(string fileName, int firstIconIndex, int iconCount)
                : base($"Tried to extract {iconCount} icons starting from the one with id {firstIconIndex} from the \"{fileName}\" file but failed")
            {
            }
        }

        #endregion

        #region DllImports and Structs

        // Struct containing information about a file object.
        private struct Shfileinfo
        {
            public IntPtr hIcon;  // Handle to the icon representing the file.
            public IntPtr iIcon;  // Index of the icon image within the system image list.
            public uint dwAttributes;  // Attributes of the file object.
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 260)]
            public string szDisplayName;  // Name of the file as it appears in Windows Shell.
            [MarshalAs(UnmanagedType.ByValTStr, SizeConst = 80)]
            public string szTypeName;  // Type of file.
        };

        // Flags indicating the attributes of the file object.
        [Flags]
        private enum FileInfoFlags
        {
            ShgfiIcon = 0x000000100,
            ShgfiUsefileattributes = 0x000000010
        }

        // External method to extract icons from the specified file.
        [DllImport("Shell32", CharSet = CharSet.Auto)]
        private static extern int ExtractIconEx(
            [MarshalAs(UnmanagedType.LPTStr)] string lpszFile,
            int nIconIndex,
            IntPtr[] phIconLarge,
            IntPtr[] phIconSmall,
            int nIcons);

        // External method to retrieve information about an object in the file system.
        [DllImport("Shell32", CharSet = CharSet.Auto)]
        private static extern IntPtr SHGetFileInfo(
            string pszPath,
            int dwFileAttributes,
            out Shfileinfo psfi,
            int cbFileInfo,
            FileInfoFlags uFlags);

        #endregion

        // Enum representing the size of the system icon.
        public enum SystemIconSize
        {
            Large,
            Small
        }

        // Extracts icons from the specified file.
        public static void ExtractEx(string fileName, List<Icon> largeIcons, List<Icon> smallIcons, int firstIconIndex, int iconCount)
        {
            IntPtr[]? smallIconsPtrs = smallIcons != null ? new IntPtr[iconCount] : null;
            IntPtr[]? largeIconsPtrs = largeIcons != null ? new IntPtr[iconCount] : null;

            int apiResult = ExtractIconEx(fileName, firstIconIndex, largeIconsPtrs, smallIconsPtrs, iconCount);

            if (apiResult != iconCount)
            {
                throw new UnableToExtractIconsException(fileName, firstIconIndex, iconCount);
            }

            if (smallIcons != null)
            {
                smallIcons.Clear();

                foreach (IntPtr actualIconPtr in smallIconsPtrs)
                {
                    smallIcons.Add(Icon.FromHandle(actualIconPtr));
                }
            }

            if (largeIcons != null)
            {
                largeIcons.Clear();

                foreach (IntPtr actualIconPtr in largeIconsPtrs)
                {
                    largeIcons.Add(Icon.FromHandle(actualIconPtr));
                }
            }
        }

        // Extracts icons from the specified file based on the specified size.
        public static List<Icon> ExtractEx(string fileName, SystemIconSize size, int firstIconIndex, int iconCount)
        {
            List<Icon> iconList = new();

            switch (size)
            {
                case SystemIconSize.Large:
                    ExtractEx(fileName, iconList, null, firstIconIndex, iconCount);
                    break;

                case SystemIconSize.Small:
                    ExtractEx(fileName, null, iconList, firstIconIndex, iconCount);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(size));
            }

            return iconList;
        }

        // Extracts all icons from the specified file.
        public static void Extract(string fileName, List<Icon> largeIcons, List<Icon> smallIcons)
        {
            int iconCount = GetIconsCountInFile(fileName);
            ExtractEx(fileName, largeIcons, smallIcons, 0, iconCount);
        }

        // Extracts all icons from the specified file based on the specified size.
        public static List<Icon> Extract(string fileName, SystemIconSize size)
        {
            int iconCount = GetIconsCountInFile(fileName);
            return ExtractEx(fileName, size, 0, iconCount);
        }

        // Extracts a single icon from the specified file based on the specified index and size.
        public static Icon ExtractOne(string fileName, int index, SystemIconSize size)
        {
            List<Icon> iconList = ExtractEx(fileName, size, index, 1);
            return iconList[0];
        }

        // Extracts a single icon from the specified file based on the specified index.
        public static void ExtractOne(string fileName, int index, out Icon largeIcon, out Icon smallIcon)
        {
            List<Icon> smallIconList = new List<Icon>();
            List<Icon> largeIconList = new List<Icon>();
            ExtractEx(fileName, largeIconList, smallIconList, index, 1);
            largeIcon = largeIconList[0];
            smallIcon = smallIconList[0];
        }

        // Retrieves the icon associated with the specified file extension.
        public static Icon IconFromExtension(string extension, SystemIconSize size)
        {
            if (extension[0] != '.') extension = '.' + extension;

            RegistryKey root = Registry.ClassesRoot;
            RegistryKey extensionKey = root.OpenSubKey(extension);
            if (extensionKey != null)
            {
                RegistryKey applicationKey = root.OpenSubKey(extensionKey.GetValue("").ToString());
                if (applicationKey != null)
                {
                    string iconLocation = applicationKey.OpenSubKey("DefaultIcon").GetValue("").ToString();
                    List<string> iconPath = iconLocation.Split(',').ToList();
                    if (iconPath.Count == 1)
                    {
                        iconPath.Add("0");
                    }
                    IntPtr[] large = new IntPtr[1], small = new IntPtr[1];
                    ExtractIconEx(iconPath[0], Convert.ToInt16(iconPath[1]), large, small, 1);
                    return size == SystemIconSize.Large ? Icon.FromHandle(large[0]) : Icon.FromHandle(small[0]);
                }
            }
            return null;
        }

        // Retrieves the icon associated with the specified file extension using the shell.
        public static Icon IconFromExtensionShell(string extension, SystemIconSize size)
        {
            if (extension[0] != '.') extension = '.' + extension;

            Shfileinfo fileInfo = new Shfileinfo();
            SHGetFileInfo(extension, 0, out fileInfo, Marshal.SizeOf(fileInfo), FileInfoFlags.ShgfiIcon | FileInfoFlags.ShgfiUsefileattributes | (FileInfoFlags)size);
            return Icon.FromHandle(fileInfo.hIcon);
        }

        // Retrieves the icon from the specified resource.
        public static Icon IconFromResource(string resourceName)
        {
            Assembly assembly = Assembly.GetCallingAssembly();
            return new Icon(assembly.GetManifestResourceStream(resourceName));
        }

        // Parses the registry string to extract the file name and index of the icon.
        public static void ExtractInformationsFromRegistryString(string regString, out string fileName, out int index)
        {
            if (regString == null)
            {
                throw new ArgumentNullException(nameof(regString));
            }
            if (regString.Length == 0)
            {
                throw new ArgumentException("The string should not be empty.", nameof(regString));
            }

            index = 0;
            string[] strArr = regString.Replace("\"", "").Split(',');
            fileName = strArr[0].Trim();
            if (strArr.Length > 1)
            {
                int.TryParse(strArr[1].Trim(), out index);
            }
        }

        // Extracts the icon from the registry string based on the specified size.
        public static Icon ExtractFromRegistryString(string regString, SystemIconSize size)
        {
            ExtractInformationsFromRegistryString(regString, out string fileName, out int index);
            return ExtractOne(fileName, index, size);
        }

        // Retrieves the number of icons in the specified file.
        private static int GetIconsCountInFile(string fileName)
        {
            return ExtractIconEx(fileName, -1, null, null, 0);
        }
    }
}

using System;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryGPT
{
    public static class IOUtilities
    {
        /// <summary>
        /// Asynchronously reads all text from a file.
        /// </summary>
        public static async Task<string> ReadAllTextAsync(string path)
        {
            using (var reader = File.OpenText(path))
            {
                return await reader.ReadToEndAsync();
            }
        }

        /// <summary>
        /// Asynchronously writes text to a file.
        /// </summary>
        public static async Task WriteAllTextAsync(string path, string content)
        {
            using (var writer = new StreamWriter(path, false))
            {
                await writer.WriteAsync(content);
            }
        }

        /// <summary>
        /// Checks if a file is in use.
        /// </summary>
        public static bool IsFileInUse(string path)
        {
            try
            {
                using (var stream = new FileStream(path, FileMode.Open))
                {
                    return false;
                }
            }
            catch (IOException)
            {
                return true;
            }
        }

        /// <summary>
        /// Safely deletes a file, catching any exceptions.
        /// </summary>
        public static void SafeDeleteFile(string path)
        {
            try
            {
                File.Delete(path);
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
            }
        }

        /// <summary>
        /// Asynchronously copies a file.
        /// </summary>
        public static async Task CopyFileAsync(string sourcePath, string destinationPath, bool overwrite = false)
        {
            using (var sourceStream = new FileStream(sourcePath, FileMode.Open))
            using (var destinationStream = new FileStream(destinationPath, overwrite ? FileMode.Create : FileMode.CreateNew))
            {
                await sourceStream.CopyToAsync(destinationStream);
            }
        }

        /// <summary>
        /// Creates a directory if it doesn't exist.
        /// </summary>
        public static void EnsureDirectoryExists(string path)
        {
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
        }

        /// <summary>
        /// Safely deletes a directory and its contents.
        /// </summary>
        public static void SafeDeleteDirectory(string path)
        {
            try
            {
                Directory.Delete(path, true);
            }
            catch (Exception ex)
            {
                // Log or handle the exception as needed
            }
        }

        /// <summary>
        /// Gets the size of a directory in bytes.
        /// </summary>
        public static long GetDirectorySize(string path)
        {
            var dirInfo = new DirectoryInfo(path);
            return dirInfo.EnumerateFiles("*", SearchOption.AllDirectories).Sum(file => file.Length);
        }

        /// <summary>
        /// Checks if a directory is empty.
        /// </summary>
        public static bool IsDirectoryEmpty(string path)
        {
            return !Directory.EnumerateFileSystemEntries(path).Any();
        }

        /// <summary>
        /// Asynchronously moves a file from the source path to the destination path.
        /// </summary>
        public static async Task MoveFileAsync(string sourcePath, string destinationPath)
        {
            await Task.Run(() => File.Move(sourcePath, destinationPath));
        }

        /// <summary>
        /// Moves a directory from the source path to the destination path.
        /// </summary>
        public static void MoveDirectory(string sourcePath, string destinationPath)
        {
            Directory.Move(sourcePath, destinationPath);
        }

        /// <summary>
        /// Gets the attributes of a file.
        /// </summary>
        public static FileAttributes GetFileAttributes(string path)
        {
            return File.GetAttributes(path);
        }

        /// <summary>
        /// Sets the attributes of a file.
        /// </summary>
        public static void SetFileAttributes(string path, FileAttributes fileAttributes)
        {
            File.SetAttributes(path, fileAttributes);
        }

        /// <summary>
        /// Gets the creation time of a file.
        /// </summary>
        public static DateTime GetFileCreationTime(string path)
        {
            return File.GetCreationTime(path);
        }

        /// <summary>
        /// Sets the creation time of a file.
        /// </summary>
        public static void SetFileCreationTime(string path, DateTime creationTime)
        {
            File.SetCreationTime(path, creationTime);
        }

        /// <summary>
        /// Gets the last access time of a file.
        /// </summary>
        public static DateTime GetFileLastAccessTime(string path)
        {
            return File.GetLastAccessTime(path);
        }

        /// <summary>
        /// Sets the last access time of a file.
        /// </summary>
        public static void SetFileLastAccessTime(string path, DateTime lastAccessTime)
        {
            File.SetLastAccessTime(path, lastAccessTime);
        }

        /// <summary>
        /// Gets the last write time of a file.
        /// </summary>
        public static DateTime GetFileLastWriteTime(string path)
        {
            return File.GetLastWriteTime(path);
        }

        /// <summary>
        /// Sets the last write time of a file.
        /// </summary>
        public static void SetFileLastWriteTime(string path, DateTime lastWriteTime)
        {
            File.SetLastWriteTime(path, lastWriteTime);
        }

        /// <summary>
        /// Checks if a file exists at the specified path.
        /// </summary>
        public static bool FileExists(string path)
        {
            return File.Exists(path);
        }

        /// <summary>
        /// Checks if a directory exists at the specified path.
        /// </summary>
        public static bool DirectoryExists(string path)
        {
            return Directory.Exists(path);
        }

        /// <summary>
        /// Creates a new file and returns a StreamWriter to write to it.
        /// </summary>
        public static StreamWriter CreateFile(string path)
        {
            return File.CreateText(path);
        }

        /// <summary>
        /// Deletes a file at the specified path.
        /// </summary>
        public static void DeleteFile(string path)
        {
            File.Delete(path);
        }

        /// <summary>
        /// Deletes a directory at the specified path.
        /// </summary>
        public static void DeleteDirectory(string path)
        {
            Directory.Delete(path, true);
        }

        // ... You can continue adding more methods as needed.
    }
}

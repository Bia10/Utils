using System;
using System.IO;

namespace Utils.FileSystem
{
    public static class FileSystemInfoExtensions
    {
        public static bool Exists(this FileSystemInfo fileSystemInfo)
        {
            return fileSystemInfo switch
            {
                DirectoryInfo { Exists: true } or FileInfo { Exists: true } => true,
                _ => false,
            };
        }

        public static void DeleteReadOnly(this FileSystemInfo fileSystemInfo)
        {
            if (!fileSystemInfo.Exists) return;

            if (fileSystemInfo is DirectoryInfo directoryInfo)
                foreach (var childInfo in directoryInfo.GetFileSystemInfos())
                    childInfo.DeleteReadOnly();

            fileSystemInfo.Attributes = FileAttributes.Normal;
            fileSystemInfo.Delete();
        }

        public static bool RenameFolder(this FileSystemInfo fileSystemInfo, string newName)
        {
            switch (fileSystemInfo)
            {
                case DirectoryInfo directoryInfo:
                    try
                    {
                        if (!directoryInfo.Exists) return false;
                        if (string.Equals(directoryInfo.Name, newName, StringComparison.OrdinalIgnoreCase))
                            return false; //new folder name is the same with the old one.

                        var newDirectory = Path.Combine(directoryInfo.Parent == null 
                            ? fileSystemInfo.FullName 
                            : directoryInfo.Parent.FullName, newName);

                        if (new DirectoryInfo(newDirectory).Exists()) 
                            return false; //target directory already exists

                        directoryInfo.MoveTo(newDirectory);
                        return true;
                    }
                    catch
                    {
                        return false;
                    }
                case FileInfo fileInfo:
                    try
                    {
                        if (!fileInfo.Exists) return false;
                        if (fileInfo.Directory == null || !fileInfo.Directory.Exists) return false;

                        fileInfo.MoveTo(Path.Combine(fileInfo.Directory.FullName, newName));
                        return true;
                    }
                    catch 
                    {
                        return false;
                    }
            }

            return false;
        }
    }
}
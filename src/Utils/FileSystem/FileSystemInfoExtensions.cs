using System;
using System.IO;
using Utils.String;

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

        public static void ExistsDelete(this FileSystemInfo fileSystemInfo)
        {
            switch (fileSystemInfo.Exists)
            {
                case false:
                    return;
                case true:
                    fileSystemInfo.Delete();
                    break;
            }
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

        public static bool Rename(this FileSystemInfo fileSystemInfo, string newName)
        {
            if (!fileSystemInfo.Exists || !newName.Valid()) return false;

            switch (fileSystemInfo)
            {
                case DirectoryInfo directoryInfo:
                    try
                    {
                        if (!directoryInfo.Exists) return false;
                        if (string.Equals(directoryInfo.Name, newName, StringComparison.OrdinalIgnoreCase))
                            return false; //new folder name is the same with the old one.

                        var newDirPath = Path.Combine(directoryInfo.Parent == null 
                            ? fileSystemInfo.FullName 
                            : directoryInfo.Parent.FullName, newName);
                        if (new DirectoryInfo(newDirPath).Exists()) 
                            return false; //target directory already exists

                        directoryInfo.MoveTo(newDirPath);
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

                        var newFilePath = Path.Combine(fileInfo.Directory.FullName, newName);
                        if (new FileInfo(newFilePath).Exists())
                            return false; //target file already exists

                        fileInfo.MoveTo(newFilePath);
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
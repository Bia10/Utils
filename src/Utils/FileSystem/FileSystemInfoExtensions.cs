using System;
using System.IO;
using Utils.Types.String;

namespace Utils.FileSystem
{
    public static class FileSystemInfoExtensions
    {
        public static void ExistsDelete(this FileSystemInfo fileSystemInfo)
        {
            switch (fileSystemInfo.Exists)
            {
                case false: return;
                case true:
                    fileSystemInfo.Delete();
                    break;
            }
        }

        public static void NotExistsCreate(this FileSystemInfo fileSystemInfo)
        {
            switch (fileSystemInfo)
            {
                case FileInfo:
                    if (!fileSystemInfo.Exists)
                        File.Create(fileSystemInfo.FullName);
                    break;
                case DirectoryInfo:
                    if (!fileSystemInfo.Exists)
                        Directory.CreateDirectory(fileSystemInfo.FullName);
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

                        if (Directory.Exists(newDirPath)) 
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
                        if (File.Exists(newFilePath))
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

        public static void CopyTo(this FileSystemInfo fileSystemInfo, string targetPath, bool removeSrc = true)
        {
            if (!fileSystemInfo.Exists || !targetPath.Valid()) return;

            switch (fileSystemInfo)
            {
                case FileInfo:
                    File.Copy(fileSystemInfo.FullName, Path.Combine(targetPath, Path.GetFileName(fileSystemInfo.FullName)));
                    break;
                case DirectoryInfo:
                {
                    new DirectoryInfo(targetPath).NotExistsCreate();

                    foreach (var file in Directory.GetFiles(fileSystemInfo.FullName))
                        File.Copy(file, Path.Combine(targetPath, Path.GetFileName(file)));
                    foreach (var directory in Directory.GetDirectories(fileSystemInfo.FullName))
                        CopyTo(new DirectoryInfo(directory), Path.Combine(targetPath, Path.GetFileName(directory)));
                    break;
                }
            }

            if (!removeSrc) return;
            fileSystemInfo.ExistsDelete();
        }
    }
}
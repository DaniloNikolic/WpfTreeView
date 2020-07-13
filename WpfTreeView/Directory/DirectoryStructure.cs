using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WpfTreeView
{
    public static class DirectoryStructure
    {
        public static List<DirectoryItem> GetLogicalDrive()
        {
            return Directory.GetLogicalDrives().Select(drive => new DirectoryItem { FullPath = drive, Type = DirectoryItemType.Drive }).ToList();
        }


        public static List<DirectoryItem> GetDirectoryContent(string fullPath)
        {
            var Items = new List<DirectoryItem>();
            try
            {
                var dirs = Directory.GetDirectories(fullPath);

                if (dirs.Length > 0)
                    Items.AddRange(dirs.Select(dir => new DirectoryItem { FullPath = dir, Type = DirectoryItemType.Folder }));
            }
            catch { }

            try
            {
                var fs = Directory.GetFiles(fullPath);

                if (fs.Length > 0)
                    Items.AddRange(fs.Select(file => new DirectoryItem { FullPath = file, Type = DirectoryItemType.File }));
            }
            catch { }

            return Items;

        }

        public static string GetFileFolderName(string path)
        {
            if (string.IsNullOrEmpty(path))
                return string.Empty;

            var normalizedPath = path.Replace('/', '\\');

            var lastIndex = normalizedPath.LastIndexOf('\\');

            if (lastIndex <= 0)
                return path;

            return path.Substring(lastIndex + 1);
        }

    }
}

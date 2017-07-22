using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHero2.Core.Iterators
{
    public class FolderScanner
    {
        public SysFileItem ScanFolder(string folderPath)
        {
            DirectoryInfo dirInfo = new DirectoryInfo(folderPath);
            SysFileItem rootItem = null;

            if (!dirInfo.Exists)
                return null;

            rootItem = new SysFileItem(dirInfo.Name, dirInfo.FullName, FileType.Directory);

            foreach (DirectoryInfo dir in dirInfo.EnumerateDirectories())
            {
                SysFileItem sysDirItem = new SysFileItem(dir.Name, dir.FullName, FileType.Directory);
                rootItem.AssociatedItems.Add(sysDirItem);
            }

            foreach (FileInfo file in dirInfo.EnumerateFiles())
            {
                SysFileItem sysFileItem = new SysFileItem(file.Name, Path.Combine(file.FullName, file.Name), FileType.File);
                rootItem.AssociatedItems.Add(sysFileItem);
            }

            return rootItem;
        }
    }
}
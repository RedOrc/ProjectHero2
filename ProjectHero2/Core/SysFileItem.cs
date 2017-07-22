using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectHero2.Core
{
    [Serializable]
    public class SysFileItem
    {
        [ReadOnly(true)]
        [Description("The name of the item in the project.")]
        public string Name
        {
            get;
            set;
        }

        [Browsable(false)]
        public string FullPath
        {
            get;
            set;
        }

        [ReadOnly(true)]
        [Description("The type of solution item.")]
        public FileType FileType
        {
            get;
            set;
        }

        [Browsable(false)]
        public List<SysFileItem> AssociatedItems
        {
            get;
            set;
        }

        private SysFileItem()
        {
            this.AssociatedItems = new List<SysFileItem>();
        }

        public SysFileItem(string name, string fullPath, FileType fileType)
            : this()
        {
            this.Name = name;
            this.FullPath = fullPath;
            this.FileType = fileType;
        }

        public override string ToString()
        {
            return Name;
        }
    }
}

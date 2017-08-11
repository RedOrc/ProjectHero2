/**
 *   _____           _                _____                
 *  |  _  |___ ___  |_|___ ___| |_   |  |  |___ ___ ___ 
 *  |   __|  _| . | | | -_|  _|  _|  |     | -_|  _| . |
 *  |__|  |_| |___|_| |___|___|_|    |__|__|___|_| |___|
 *                |___|      
 *    
 * Copyright © 2017 Alphonso Turner
 * All Rights Reserved
 * 
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace ProjectHero2.Core
{
    #region View State Column Information

    [Serializable]
    public class ColumnInformation
    {
        [XmlElement("Name")]
        public string Name
        {
            get;
            set;
        }

        [XmlElement("Width")]
        public int Width
        {
            get;
            set;
        }
    }

    #endregion View State Column Information

    #region Quick Sync Binding Helpers

    [Serializable]
    public class SourceProjectBinding
    {
        [ReadOnly(true)]
        [Description("The name of the current project.")]
        [XmlElement]
        public string ProjectName
        {
            get;
            set;
        }

        [Description("Provide a meaningful nickname for this project.")]
        [XmlElement]
        public string ProjectNickname
        {
            get;
            set;
        }

        [Description("The current file path to the selected project.")]
        [ReadOnly(true)]
        [XmlIgnore]
        public string ProjectFilePath
        {
            get;
            set;
        }

        [Browsable(false)]
        [XmlElement]
        public string MD5Hash
        {
            get;
            set;
        }

        [Browsable(false)]
        [XmlArray]
        public IList<SysFileItem> SyncAssociationCollection
        {
            get;
            private set;
        }

        [Description("Configure the destination folders that the selected project should be synchronized to on successful build.")]
        [XmlArray]
        public IList<DestinationFolderBinding> DestinationFolderCollection
        {
            get;
            private set;
        }

        public SourceProjectBinding()
        {
            this.SyncAssociationCollection = new List<SysFileItem>();
            this.DestinationFolderCollection = new List<DestinationFolderBinding>();
        }
    }

    [Serializable]
    public class DestinationFolderBinding
    {
        [XmlElement]
        public string FolderName
        {
            get;
            set;
        }

        [ReadOnly(true)]
        [XmlElement]
        public string FolderPath
        {
            get;
            set;
        }

        public DestinationFolderBinding()
        {
        }

        public DestinationFolderBinding(string folderName, string folderPath)
        {
            this.FolderName = folderName;
            this.FolderPath = folderPath;
        }

        public override string ToString()
        {
            return FolderName;
        }
    }

    #endregion Quick Sync Binding Helpers

    [Serializable]
    public class ProjectHeroSettings
    {
        [XmlElement("DisplayOnStartup")]
        public bool DisplayOnStartup { get; set; }

        [XmlElement("DisplayOnBuildStart")]
        public bool DisplayOnBuildStart { get; set; }

        [XmlElement("DisplayOnSolutionChange")]
        public bool DisplayOnSolutionChange { get; set; }

        [XmlElement("OverrideVSOutputWindow")]
        public bool OverrideVSOutputWindow { get; set; }

        [XmlElement("EnableQuickSync")]
        public bool EnableQuickSync { get; set; }

        [XmlArray("ViewState")]
        public List<ColumnInformation> VisualSettings { get; set; }

        [XmlArray("QuickSyncAssociations")]
        public List<SourceProjectBinding> QuickSyncAssociations { get; set; }

        public ProjectHeroSettings()
        {
            VisualSettings = new List<ColumnInformation>();
            QuickSyncAssociations = new List<SourceProjectBinding>();
        }
    }
}

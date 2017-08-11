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

using Microsoft.VisualStudio.Settings;
using Microsoft.VisualStudio.Shell.Settings;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;
using System.Xml.Schema;

namespace ProjectHero2.Core
{
    public sealed class ProjectHeroSettingManager
    {
        private static ProjectHeroSettingManager _managerInstance = null;
        public static ProjectHeroSettingManager Manager
        {
            get
            {
                if (_managerInstance == null)
                    _managerInstance = new ProjectHeroSettingManager();

                return _managerInstance;
            }
        }

        public ProjectHeroSettings PluginSettings
        {
            get;
            private set;
        }

        public bool IsSettingsLoaded
        {
            get;
            private set;
        }

        public IServiceProvider ServiceProvider
        {
            get;
            private set;
        }

        private const string COLLECTION_GROUP = "ProjectHero2";

        private const string DISPLAY_ON_BUILD_START = "DisplayOnBuildStart";
        private const string DISPLAY_ON_SOLUTION_CHANGE = "DisplayOnSolutionChange";
        private const string DISPLAY_ON_STARTUP = "DisplayOnStartup";
        private const string OVERRIDE_VS_OUTPUT_WINDOW = "OverrideVsOutputWindow";
        private const string ENABLE_QUICK_SYNC = "EnableQuickSync";
        private const string VISUAL_SETTINGS = "VisualSettings";

        private const string NULL = "{NULL}";

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public void PreLoadSettings(IServiceProvider serviceProvider)
        {
            this.ServiceProvider = serviceProvider;

            SettingsManager settingsManager = new ShellSettingsManager(serviceProvider);
            WritableSettingsStore settingsStore = settingsManager.GetWritableSettingsStore(SettingsScope.UserSettings);

            this.PluginSettings = new ProjectHeroSettings();

            if (!settingsStore.CollectionExists(COLLECTION_GROUP))
            {
                settingsStore.CreateCollection(COLLECTION_GROUP);
            }
            
            if (!settingsStore.PropertyExists(COLLECTION_GROUP, DISPLAY_ON_BUILD_START))
            {
                settingsStore.SetBoolean(COLLECTION_GROUP, DISPLAY_ON_BUILD_START, true);
            }
            this.PluginSettings.DisplayOnBuildStart = settingsStore.GetBoolean(COLLECTION_GROUP, DISPLAY_ON_BUILD_START, true);

            if (!settingsStore.PropertyExists(COLLECTION_GROUP, DISPLAY_ON_SOLUTION_CHANGE))
            {
                settingsStore.SetBoolean(COLLECTION_GROUP, DISPLAY_ON_SOLUTION_CHANGE, true);
            }
            this.PluginSettings.DisplayOnSolutionChange = settingsStore.GetBoolean(COLLECTION_GROUP, DISPLAY_ON_SOLUTION_CHANGE, true);

            if (!settingsStore.PropertyExists(COLLECTION_GROUP, DISPLAY_ON_STARTUP))
            {
                settingsStore.SetBoolean(COLLECTION_GROUP, DISPLAY_ON_STARTUP, true);
            }
            this.PluginSettings.DisplayOnStartup = settingsStore.GetBoolean(COLLECTION_GROUP, DISPLAY_ON_STARTUP, true);

            if (!settingsStore.PropertyExists(COLLECTION_GROUP, OVERRIDE_VS_OUTPUT_WINDOW))
            {
                settingsStore.SetBoolean(COLLECTION_GROUP, OVERRIDE_VS_OUTPUT_WINDOW, true);
            }
            this.PluginSettings.OverrideVSOutputWindow = settingsStore.GetBoolean(COLLECTION_GROUP, OVERRIDE_VS_OUTPUT_WINDOW, true);

            if (!settingsStore.PropertyExists(COLLECTION_GROUP, ENABLE_QUICK_SYNC))
            {
                settingsStore.SetBoolean(COLLECTION_GROUP, ENABLE_QUICK_SYNC, false);
            }
            this.PluginSettings.EnableQuickSync = settingsStore.GetBoolean(COLLECTION_GROUP, ENABLE_QUICK_SYNC, false);
            
            if (!settingsStore.PropertyExists(COLLECTION_GROUP, VISUAL_SETTINGS))
            {
                settingsStore.SetString(COLLECTION_GROUP, VISUAL_SETTINGS, NULL);
            }
            else
            {
                string visualSettings = settingsStore.GetString(COLLECTION_GROUP, VISUAL_SETTINGS);
                CustomColumnInfoCompressor decompressor = new CustomColumnInfoCompressor();
                List<ColumnInformation> columns = decompressor.Decompress(visualSettings);
                if (columns != null)
                {
                    this.PluginSettings.VisualSettings = new List<ColumnInformation>(columns);
                }
            }

            this.IsSettingsLoaded = true;
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public void SaveSettings()
        {
            try
            {
                SettingsManager settingsManager = new ShellSettingsManager(this.ServiceProvider);
                WritableSettingsStore settingsStore = settingsManager.GetWritableSettingsStore(SettingsScope.UserSettings);

                settingsStore.SetBoolean(COLLECTION_GROUP, DISPLAY_ON_BUILD_START, this.PluginSettings.DisplayOnBuildStart);
                settingsStore.SetBoolean(COLLECTION_GROUP, DISPLAY_ON_SOLUTION_CHANGE, this.PluginSettings.DisplayOnSolutionChange);
                settingsStore.SetBoolean(COLLECTION_GROUP, DISPLAY_ON_STARTUP, this.PluginSettings.DisplayOnStartup);
                settingsStore.SetBoolean(COLLECTION_GROUP, OVERRIDE_VS_OUTPUT_WINDOW, this.PluginSettings.OverrideVSOutputWindow);
                settingsStore.SetBoolean(COLLECTION_GROUP, ENABLE_QUICK_SYNC, this.PluginSettings.EnableQuickSync);

                CustomColumnInfoCompressor compressor = new CustomColumnInfoCompressor();
                string visualContents = compressor.Compress(this.PluginSettings.VisualSettings);
                settingsStore.SetString(COLLECTION_GROUP, VISUAL_SETTINGS, visualContents);
            }
            catch (Exception e)
            {
                // Do nothing
            }
        }
    }

    internal class CustomColumnInfoCompressor
    {
        public string Compress(List<ColumnInformation> columnList)
        {
            if (columnList.Count == 0)
                return "{NULL}";

            StringBuilder builder = new StringBuilder();
            int ndx = 0;

            builder.Append("[");
            foreach (ColumnInformation column in columnList)
            {
                if (ndx > 0) builder.Append(",");
                builder.AppendFormat("({0}:{1})", column.Name, column.Width);
                ndx++;
            }
            builder.Append("]");

            return builder.ToString();
        }

        public List<ColumnInformation> Decompress(string contents)
        {
            if (string.IsNullOrEmpty(contents) || contents.Equals("{NULL}", StringComparison.CurrentCultureIgnoreCase))
                return null;
            
            string[] resultItems = contents.Replace("[", string.Empty)
                                           .Replace("]", string.Empty)
                                           .Split(',');

            List<ColumnInformation> columns = new List<ColumnInformation>();
            foreach (string result in resultItems)
            {
                string[] item = result.Replace("(", string.Empty)
                                    .Replace(")", string.Empty)
                                    .Split(':');
                ColumnInformation column = new ColumnInformation();
                column.Name = item[0];
                column.Width = Int32.Parse(item[1]);
                columns.Add(column);
            }
            return columns;
        }
    }
}

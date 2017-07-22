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
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

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

        private static string preferredSettingsLocation = null;
        private const string APP_CONFIG_FILE = "ProjectHeroConfig.xml";

        private string GetSuggestedConfigLocation()
        {
            string settingsPath = null;

            try
            {
                settingsPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
            }
            catch
            {
                try
                {
                    settingsPath = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
                }
                catch
                {
                    try
                    {
                        settingsPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                    }
                    catch { /* Out of options */}
                }
            }

            if (settingsPath != null)
            {
                settingsPath = Path.Combine(settingsPath, APP_CONFIG_FILE);
            }

            return settingsPath;
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public ProjectHeroSettings PreLoadSettings()
        {
            ProjectHeroSettings settings = null;

            try
            {
                // ================================================================================
                // Determine the path for the current executing assembly.
                // ================================================================================
                preferredSettingsLocation = GetSuggestedConfigLocation();

                Console.WriteLine("Preferred Settings location set at {0}", preferredSettingsLocation);

                // ================================================================================
                // If this file doesn't exist then we want to set our default settings.
                // ================================================================================
                if (!File.Exists(preferredSettingsLocation))
                {
                    settings = new ProjectHeroSettings();
                    settings.DisplayOnBuildStart = true;
                    settings.DisplayOnSolutionChange = true;
                    settings.DisplayOnStartup = true;
                    settings.OverrideVSOutputWindow = true;
                    settings.EnableQuickSync = false;
                    return settings;
                }

                XmlSerializer serializer = new XmlSerializer(typeof(ProjectHeroSettings));
                using (XmlReader reader = XmlReader.Create(preferredSettingsLocation))
                {
                    if (serializer.CanDeserialize(reader))
                    {
                        settings = serializer.Deserialize(reader) as ProjectHeroSettings;
                    }
                }

                return settings;
            }
            catch (Exception ex)
            {
                // Do nothing.
            }

            return null;
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public void LoadSettings()
        {
            try
            {
                // ================================================================================
                // NOTE:
                // I don't ever recommend duplication of code in two methods but since
                // this is so isolated I decided to take the poor programmers route
                // and not just make one method to return back an instance of the 
                // Project Settings.
                // ================================================================================

                // ================================================================================
                // Determine the path for the current executing assembly.
                // ================================================================================
                preferredSettingsLocation = GetSuggestedConfigLocation();

                // ================================================================================
                // If this file doesn't exist then we want to set our default settings.
                // ================================================================================
                if (!File.Exists(preferredSettingsLocation))
                {
                    PluginSettings = new ProjectHeroSettings();
                    PluginSettings.DisplayOnBuildStart = true;
                    PluginSettings.DisplayOnSolutionChange = true;
                    PluginSettings.DisplayOnStartup = true;
                    PluginSettings.OverrideVSOutputWindow = true;
                    PluginSettings.EnableQuickSync = false;
                    this.IsSettingsLoaded = true;
                    SaveSettings();
                    return;
                }

                XmlSerializer serializer = new XmlSerializer(typeof(ProjectHeroSettings));
                using (XmlReader reader = XmlReader.Create(preferredSettingsLocation))
                {
                    if (serializer.CanDeserialize(reader))
                    {
                        PluginSettings = serializer.Deserialize(reader) as ProjectHeroSettings;
                        this.IsSettingsLoaded = true;
                    }
                }
            }
            catch
            {
                // Do nothing.
            }
        }

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public void SaveSettings()
        {
            try
            {
                // ================================================================================
                // Determine the path for the current executing assembly.
                // ================================================================================
                preferredSettingsLocation = GetSuggestedConfigLocation();

                // ================================================================================
                // Let's serialize the settings object and then write it to file.
                // ================================================================================
                XmlSerializer serializer = new XmlSerializer(typeof(ProjectHeroSettings));
                using (XmlWriter writer = XmlWriter.Create(preferredSettingsLocation))
                {
                    serializer.Serialize(writer, PluginSettings);
                    writer.Flush();
                    writer.Close();
                }
            }
            catch (Exception ex)
            {
                // Do nothing.
            }
        }
    }
}

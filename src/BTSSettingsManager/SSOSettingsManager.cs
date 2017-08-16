using System;
using System.Collections.Generic;
using Microsoft.EnterpriseSingleSignOn.Interop;

namespace BTSSettingsManager
{
    /// <summary>
    /// Configuration manager of BizTalk application settings stored in the SSO database. 
    /// </summary>
    public sealed class SSOSettingsManager
    {
        #region Constants

        /// <summary>
        /// Unique Identifier where the application settings were stored.
        /// </summary>
        private const string UID = "{56D74464-67EA-464d-A9D4-3EBBA4090010}";

        /// <summary>
        /// Property Name of the application settings stored in the property bag.
        /// </summary>
        private const string PropName = "AppConfig";

        #endregion

        #region Properties

        /// <summary>
        /// Dictionary containing the application settings.
        /// </summary>
        private IDictionary<string, string> _Settings;

        /// <summary>
        /// Dictionary containing the application settings.
        /// </summary>
        public IDictionary<string, string> Settings
        {
            get { return this._Settings; }
        }

        /// <summary>
        /// PropertyBag with the application settings in the SSO format.
        /// </summary>
        private SSOPropertyBag PropertyBag;

        #endregion

        #region Singleton Implementation

        /// <summary>
        /// Instance of SSOConfigManager class.
        /// </summary>
        private static volatile SSOSettingsManager _Instance;

        /// <summary>
        /// Syncronization lock for implementing multithreaded singleton.
        /// </summary>
        private static object SyncRoot = new Object();

        /// <summary>
        /// Instance of SSOConfigManager class.
        /// </summary>
        public static SSOSettingsManager Instance
        {
            get 
            {
                if (SSOSettingsManager._Instance == null)
                {
                    lock (SSOSettingsManager.SyncRoot)
                    {
                        if (SSOSettingsManager._Instance == null)
                            SSOSettingsManager._Instance = new SSOSettingsManager();
                    }
                }

                return _Instance;
            }
        }
        
        /// <summary>
        /// Initializes an instance of the SSOConfigManager class.
        /// </summary>
        private SSOSettingsManager() 
        {
            this._Settings = new SortedDictionary<string, string>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Updates the value of a specific setting.
        /// </summary>
        /// <param name="settingName">The name of the setting to update.</param>
        /// <param name="newValue">The value to update the specified setting.</param>
        /// <returns>True, if the setting was updated sucessfully. False, otherwise.</returns>
        public bool UpdateSetting(string settingName, string newValue)
        {
            if (this.Settings.ContainsKey(settingName))
            {
                this.Settings[settingName] = newValue;
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Adds a new setting.
        /// </summary>
        /// <param name="settingName">The name of the setting to add.</param>
        /// <param name="value">The value to the specified setting.</param>
        /// <returns>True, if the setting was added sucessfully. False, otherwise.</returns>
        public bool AddSetting(string settingName, string value)
        {
            this.Settings.Add(settingName, value);
            return this.Settings.ContainsKey(settingName);
        }

        /// <summary>
        /// Removes a new setting.
        /// </summary>
        /// <param name="settingName">The name of the setting to remove.</param>
        /// <returns>True, if the setting was removed sucessfully. False, otherwise.</returns>
        public bool RemoveSetting(string settingName)
        {
            if (this.Settings.ContainsKey(settingName))
            {
                this.Settings.Remove(settingName);
                return !this.Settings.ContainsKey(settingName);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Replace an existing setting by a new one keeping the same value.
        /// </summary>
        /// <param name="oldSettingName">The name of the setting to be replaced.</param>
        /// <param name="newSettingName">The name of the setting to replace.</param>
        /// <returns>True, if the setting was replaced sucessfully. False, otherwise.</returns>
        public bool ReplaceSetting(string oldSettingName, string newSettingName)
        {
            if (this.Settings.ContainsKey(oldSettingName))
            {
                string value = this._Settings[oldSettingName];

                this.Settings.Remove(oldSettingName);
                this.Settings.Add(newSettingName, value);
                return this.Settings.ContainsKey(newSettingName);
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Loads the settings for the informed BizTalk application.
        /// </summary>
        /// <param name="applicationName">BizTalk application name.</param>
        public void Load(string applicationName)
        {
            // Clear any properties already loaded.
            this.Clear();

            ISSOConfigStore configStore = (ISSOConfigStore)new SSOConfigStore();

            configStore.GetConfigInfo(applicationName, SSOSettingsManager.UID, SSOFlag.SSO_FLAG_RUNTIME, this.PropertyBag);

            object ssoPropertyValue;
            this.PropertyBag.Read(SSOSettingsManager.PropName, out ssoPropertyValue, 0);

            settings ssoSettings = SerializationHelper.Instance.Deserialize<settings>(ssoPropertyValue.ToString());

            // Load the properties dictionary with the settings object.
            foreach (var property in ssoSettings.property)
            {
                this.Settings.Add(property.name, property.Value);
            }
        }

        /// <summary>
        /// Load the settings with user-loaded settings.
        /// </summary>
        /// <param name="ssoSettings">The user-loaded settings.</param>
        public void Load(settings ssoSettings)
        {
            // Clear any properties already loaded.
            this.Clear();

            // Load the properties dictionary with the settings object.
            foreach (var property in ssoSettings.property)
            {
                this.Settings.Add(property.name, property.Value);
            }
        }

        /// <summary>
        /// Saves the application settings for the informed BizTalk application.
        /// </summary>
        /// <param name="applicationName">BizTalk application name.</param>
        public void Save(string applicationName)
        {
            // Creates an settings object from the properties.
            settings ssoSettings = new settings();
            List<settingsProperty> ssoSettingsPropertyList = new List<settingsProperty>();

            foreach (var setting in this.Settings)
            {
                ssoSettingsPropertyList.Add(
                    new settingsProperty()
                    {
                        name = setting.Key,
                        Value = setting.Value
                    }
                );
            }

            ssoSettings.property = ssoSettingsPropertyList.ToArray();

            object ssoPropertyValue = SerializationHelper.Instance.Serialize<settings>(ssoSettings);
            this.PropertyBag.Write(SSOSettingsManager.PropName, ref ssoPropertyValue);

            ISSOConfigStore configStore = (ISSOConfigStore)new SSOConfigStore();

            configStore.SetConfigInfo(applicationName, SSOSettingsManager.UID, this.PropertyBag);
        }

        /// <summary>
        /// Clears the application settings.
        /// </summary>
        public void Clear()
        {
            this.Settings.Clear();
            this.PropertyBag = new SSOPropertyBag();
        }

        #endregion
    }
}

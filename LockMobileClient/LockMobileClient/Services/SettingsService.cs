using Plugin.Settings;
using Plugin.Settings.Abstractions;
using System;
using System.Collections.Generic;
using System.Text;

namespace LockMobileClient.Services
{
    public static class SettingsService
    {
        static ISettings AppSettings
        {
            get
            {
                return CrossSettings.Current;
            }
        }

        public static string DeviceId
        {
            get
            {
                return AppSettings.GetValueOrDefault("DeviceId", "");
            }
            set
            {
                AppSettings.AddOrUpdateValue("DeviceId", value);
            }
        }
    }
}

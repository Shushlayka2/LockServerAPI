using PCLAppConfig;
using Plugin.Settings;
using Plugin.Settings.Abstractions;

namespace LockMobileClient.Services
{
    public static class SettingsService
    {
        private static ISettings CrossAppSettings = CrossSettings.Current;

        internal static string DeviceId
        {
            get
            {
                return CrossAppSettings.GetValueOrDefault("DeviceId", "");
            }
            set
            {
                CrossAppSettings.AddOrUpdateValue("DeviceId", value);
            }
        }

        internal static string BaseAddress { get; } = ConfigurationManager.AppSettings["BaseAddress"];
    }
}

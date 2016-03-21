using System.Diagnostics;
using System.Configuration;

namespace QwikiWiki.Common.Config
{
    public static class AppSettingsReader
    {
        private static string GetAppSetting(string name)
        {
            return GetAppSetting(name, string.Empty);
        }

        private static string GetAppSetting(string name, string defaultValue)
        {
            string result = ConfigurationManager.AppSettings[name];

            if (string.IsNullOrEmpty(result))
            {
                result = defaultValue;
            }
            return result;
        }

        [DebuggerStepThrough]
        public static ConnectionStringSettings GetConnectionStringSetting(string name)
        {
            return ConfigurationManager.ConnectionStrings[name];
        }


    }
}
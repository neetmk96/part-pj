using System.ComponentModel;
using System.Globalization;
using System.Reflection;

namespace PartsInfo.Common.Configuration
{
    public class AppConfigs
    {
        private static IConfiguration _configuration;

        public static void LoadAll(IConfiguration configuration)
        {
            _configuration = configuration;
            ApiUrlBase = GetConfigValue("Appconfig:ApiUrlBase", "");
            MySqlConnection = GetConfigValue("Appconfig:MySqlConnection", "");
        }


        public static string ApiUrlBase { get; set; }
        public static string MySqlConnection { get; set; }

        /// <summary>
        /// Lấy ra giá trị config trong file .config
        /// </summary>
        private static T GetConfigValue<T>(string configKey, T defaultValue)
        {
            var value = defaultValue;
            var converter = TypeDescriptor.GetConverter(typeof(T));
            try
            {
                if (converter != null)
                {
                    var setting = _configuration.GetSection(configKey).Value;
                    if (!string.IsNullOrEmpty(setting))
                    {
                        value = (T)converter.ConvertFromString(setting);
                    }
                }
            }
            catch
            {
                value = defaultValue;
            }
            return value;
        }
    }
}

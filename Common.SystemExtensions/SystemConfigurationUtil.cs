using System;
using System.Configuration;
using System.Globalization;

namespace Common.SystemExtensions
{
    public class SystemConfigurationUtil
    {
        //
        // directory methods
        //

        public static string GetExistingDirectorySetting(string settingName)
        {
            string settingValue = ConfigurationManager.AppSettings[settingName];
            if (string.IsNullOrEmpty(settingValue) || !System.IO.Directory.Exists(settingValue))
            {
                throw new ConfigurationErrorsException("Illegal " + settingName + " setting. Must be an existing directory.");
            }
            return settingValue;
        }

        //
        // file methods
        //

        public static string GetExistingFileSetting(string settingName)
        {
            string settingValue = ConfigurationManager.AppSettings[settingName];
            if (string.IsNullOrEmpty(settingValue) || !System.IO.File.Exists(settingValue))
            {
                throw new ConfigurationErrorsException("Illegal " + settingName + " setting. Must be an existing file.");
            }
            return settingValue;
        }

        //
        // bool methods
        //

        public static bool GetBoolSetting(string settingName)
        {
            bool settingValue;
            if (!bool.TryParse(ConfigurationManager.AppSettings[settingName], out settingValue))
            {
                throw new ConfigurationErrorsException("Illegal " + settingName + " setting. Must be \"True\" or \"False\".");
            }
            return settingValue;
        }

        //
        // Datetime methods
        //

        public static DateTime GetDateTimeSetting(
            string settingName,
            string format)
        {
            DateTime settingValue;
            if (!DateTime.TryParseExact(ConfigurationManager.AppSettings[settingName], format, null, DateTimeStyles.None, out settingValue))
            {
                throw new ConfigurationErrorsException("Illegal " + settingName + " setting. Must be a valid date.");
            }

            return settingValue;
        }

        //
        // decimal methods
        //

        public static decimal GetDecimalSetting(string settingName)
        {
            decimal settingValue;
            if (!decimal.TryParse(ConfigurationManager.AppSettings[settingName], out settingValue))
            {
                throw new ConfigurationErrorsException("Illegal " + settingName + " setting. Must be a decimal.");
            }
            return settingValue;
        }

        public static decimal GetMinDecimalSetting(
        string settingName,
        decimal minValue
        )
        {
            decimal settingValue = GetDecimalSetting(settingName);
            if (settingValue < minValue)
            {
                throw new ConfigurationErrorsException("Illegal " + settingName + " setting. Must be a decimal >= " + minValue);
            }
            return settingValue;
        }

        //
        // int methods
        //

        public static int GetIntSetting(string settingName)
        {
            int settingValue;
            if (!int.TryParse(ConfigurationManager.AppSettings[settingName], out settingValue))
            {
                throw new ConfigurationErrorsException("Illegal " + settingName + " setting. Must be an integer.");
            }
            return settingValue;
        }

        public static int GetMinIntSetting(
            string settingName,
            int minValue
        )
        {
            int settingValue = GetIntSetting(settingName);
            if (settingValue < minValue)
            {
                throw new ConfigurationErrorsException("Illegal " + settingName + " setting. Must be an integer >= " + minValue);
            }
            return settingValue;
        }

        public static int GetRangeIntSetting(
            string settingName,
            int minValue,
            int maxValue
        )
        {
            int settingValue = GetIntSetting(settingName);
            if (settingValue < minValue || settingValue > maxValue)
            {
                throw new ConfigurationErrorsException("Illegal " + settingName + " setting. Must be an integer between " + minValue + " and " + maxValue);
            }
            return settingValue;
        }

        //
        // string methods
        //

        public static string GetConnectionString(string connectionStringName)
        {
            ConnectionStringSettings settingValue = ConfigurationManager.ConnectionStrings[connectionStringName];
            if (settingValue == null || string.IsNullOrEmpty(settingValue.ConnectionString))
            {
                throw new ConfigurationErrorsException("Illegal " + connectionStringName + " connectionString. Must be non-blank.");
            }
            return settingValue.ConnectionString;
        }

        public static string GetStringSetting(string settingName)
        {
            string settingValue = ConfigurationManager.AppSettings[settingName];
            if (settingValue == null)
            {
                throw new ConfigurationErrorsException("Illegal " + settingName + " setting. Must exist in config file.");
            }
            return settingValue;
        }

        public static string GetNonBlankStringSetting(string settingName)
        {
            string settingValue = ConfigurationManager.AppSettings[settingName];
            if (string.IsNullOrEmpty(settingValue))
            {
                throw new ConfigurationErrorsException("Illegal " + settingName + " setting. Must be non-blank.");
            }
            return settingValue;
        }

        //
        // TimeSpan methods
        //

        public static TimeSpan GetTimeSpanSetting(string settingName)
        {
            TimeSpan settingValue;
            if (!TimeSpan.TryParse(ConfigurationManager.AppSettings[settingName], out settingValue))
            {
                throw new ConfigurationErrorsException("Illegal " + settingName + " setting. Must be \"True\" or \"False\".");
            }
            return settingValue;
        }
    }
}

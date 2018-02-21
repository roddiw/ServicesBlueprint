using Common.SystemExtensions;

namespace MathServices
{
    class ConfigFileSettings : ISettings
    {
        public int IntSetting { get; private set; }

        public ConfigFileSettings()
        {
            IntSetting = SystemConfigurationUtil.GetIntSetting("IntSetting");
        }
    }
}

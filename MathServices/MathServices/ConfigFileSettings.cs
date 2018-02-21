using Common.SystemExtensions;

namespace MathServices
{
    class ConfigFileSettings : ISettings
    {
        public int AddIntsMaxResult { get; private set; }

        public ConfigFileSettings()
        {
            AddIntsMaxResult = SystemConfigurationUtil.GetIntSetting("AddIntsMaxResult");
        }
    }
}

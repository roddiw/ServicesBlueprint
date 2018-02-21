using Common.SystemExtensions;

namespace MathServices
{
    class ConfigFileSettings : ISettings
    {
        public int AddIntsMinNumber { get; private set; }
        public int AddIntsMaxNumber { get; private set; }
        public int AddIntsMaxResult { get; private set; }

        public ConfigFileSettings()
        {
            AddIntsMinNumber = SystemConfigurationUtil.GetIntSetting("AddIntsMinNumber");
            AddIntsMaxNumber = SystemConfigurationUtil.GetIntSetting("AddIntsMaxNumber");
            AddIntsMaxResult = SystemConfigurationUtil.GetIntSetting("AddIntsMaxResult");
        }
    }
}

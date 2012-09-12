using System.Configuration;

namespace TfsCommitMonitor.Config
{
    public class MonitoredProjectItemConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("itemId", IsRequired = true, IsKey = true)]
        public string ItemId
        {
            get
            {
                return this["itemId"].ToString();
            }
            set
            {
                this["itemId"] = value;
            }
        }

        [ConfigurationProperty("monitoredFolder", IsRequired = true)]
        public string MonitoredFolder
        {
            get
            {
                return this["monitoredFolder"].ToString();
            }
            set
            {
                this["monitoredFolder"] = value;
            }
        }
    }
}

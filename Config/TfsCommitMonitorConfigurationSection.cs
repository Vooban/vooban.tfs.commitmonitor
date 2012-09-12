using System.Configuration;

namespace TfsCommitMonitor.Config
{
    public class TfsCommitMonitorConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("servers", IsDefaultCollection = true)]
        public ServerConfigurationCollection Servers
        {
            get
            {
                return (ServerConfigurationCollection)this["servers"];
            }
            set
            {
                this["servers"] = value;
            }
        }

        [ConfigurationProperty("checkIntervalInSeconds", DefaultValue = 60)]
        public int CheckIntervalInSeconds
        {
            get
            {
                return (int)this["checkIntervalInSeconds"];
            }
            set
            {
                this["checkIntervalInSeconds"] = value;
            }
        }

        [ConfigurationProperty("notifierOpeningMilliseconds", DefaultValue = 1000)]
        public int NotifierOpeningMilliseconds
        {
            get
            {
                return (int)this["notifierOpeningMilliseconds"];
            }
            set
            {
                this["notifierOpeningMilliseconds"] = value;
            }
        }

        [ConfigurationProperty("notifierHidingMilliseconds", DefaultValue = 1000)]
        public int NotifierHidingMilliseconds
        {
            get
            {
                return (int)this["notifierHidingMilliseconds"];
            }
            set
            {
                this["notifierHidingMilliseconds"] = value;
            }
        }

        [ConfigurationProperty("notifierStayOpenMilliseconds", DefaultValue = 1000)]
        public int NotifierStayOpenMilliseconds
        {
            get
            {
                return (int)this["notifierStayOpenMilliseconds"];
            }
            set
            {
                this["notifierStayOpenMilliseconds"] = value;
            }
        }

        [ConfigurationProperty("ignoredUsers", IsDefaultCollection = false)]
        public IgnoredUsersConfigurationCollection IgnoredUsers
        {
            get
            {
                return (IgnoredUsersConfigurationCollection)this["ignoredUsers"];
            }
            set
            {
                this["ignoredUsers"] = value;
            }
        }
    }
}

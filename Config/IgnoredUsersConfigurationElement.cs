using System.Configuration;

namespace TfsCommitMonitor.Config
{
    public class IgnoredUsersConfigurationElement : ConfigurationElement
    {
        [ConfigurationProperty("id", IsRequired = true,IsKey=true)]
        public string Id 
        { 
            get 
            {
                return this["id"].ToString();
            } 
            set
            {
                this["id"] = value;
            } 
        }
    }
}

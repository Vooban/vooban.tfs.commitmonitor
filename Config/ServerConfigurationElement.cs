using System.Configuration;

namespace TfsCommitMonitor.Config
{
    public class ServerConfigurationElement : ConfigurationElement
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

        [ConfigurationProperty("changesetRetrieved", DefaultValue = 2500)]
        public int ChangesetRetrieved
        {
            get
            {
                return (int)this["changesetRetrieved"];
            }
            set
            {
                this["changesetRetrieved"] = value;
            }
        }

        [ConfigurationProperty("changesetDisplayed", DefaultValue = 250)]
        public int ChangesetDisplayed
        {
            get
            {
                return (int)this["changesetDisplayed"];
            }
            set
            {
                this["changesetDisplayed"] = value;
            }
        }

        [ConfigurationProperty("tfsTeamProjectCollection", IsRequired = true)]
        public string TfsTeamProjectCollection
        {
            get
            {
                return this["tfsTeamProjectCollection"].ToString();
            }
            set
            {
                this["tfsTeamProjectCollection"] = value;
            }
        }


        [ConfigurationProperty("", IsRequired = true, IsDefaultCollection = true)]
        public MonitoredProjectItemConfigurationCollection Folders
        {
            get
            {
                return (MonitoredProjectItemConfigurationCollection)this[""];
            }
            set
            {
                this[""] = value;
            }
        }
    }
}

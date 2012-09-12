using System.Configuration;

namespace TfsCommitMonitor.Config
{
    public class ServerConfigurationCollection : ConfigurationElementCollection
    {
        public override ConfigurationElementCollectionType CollectionType
        {
            get
            {
                return ConfigurationElementCollectionType.BasicMap;
            }
        }

        protected override string ElementName
        {
            get
            {
                return "server";
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ServerConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ServerConfigurationElement)element).Id;
        }

        public new ServerConfigurationElement this[string projectId]
        {
            get
            {
                return (ServerConfigurationElement)BaseGet(projectId);
            }
        }
    }
}

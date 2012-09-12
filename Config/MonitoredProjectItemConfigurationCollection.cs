using System.Collections;
using System.Collections.Generic;
using System.Configuration;

namespace TfsCommitMonitor.Config
{
    public class MonitoredProjectItemConfigurationCollection : ConfigurationElementCollection
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
                return "folder";
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new MonitoredProjectItemConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((MonitoredProjectItemConfigurationElement)element).ItemId;
        }
    }
}

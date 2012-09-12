using System.Collections.Generic;
using System.Configuration;
using System.Linq;

namespace TfsCommitMonitor.Config
{
    public class IgnoredUsersConfigurationCollection : ConfigurationElementCollection
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
                return "user";
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new IgnoredUsersConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((IgnoredUsersConfigurationElement)element).Id;
        }

        public new IgnoredUsersConfigurationElement this[string userId]
        {
            get
            {
                return (IgnoredUsersConfigurationElement)BaseGet(userId);
            }
        }

        public List<IgnoredUsersConfigurationElement> ToList()
        {
            var result = new List<IgnoredUsersConfigurationElement>();
            
            BaseGetAllKeys().ToList().ForEach(k => result.Add(this[(string)k]));

            return result;
        }
    }
}

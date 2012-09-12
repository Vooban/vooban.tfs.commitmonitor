using System.Configuration;

namespace TfsCommitMonitor.Config
{
    public class ConfigurationProvider : IConfigurationProvider
    {
        public TfsCommitMonitorConfigurationSection GetConfiguration()
        {
            return (TfsCommitMonitorConfigurationSection)ConfigurationManager.GetSection("TfsCommitMonitor");
        }
    }
}

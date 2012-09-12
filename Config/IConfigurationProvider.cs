using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TfsCommitMonitor.Config
{
    public interface IConfigurationProvider
    {
        TfsCommitMonitorConfigurationSection GetConfiguration();
    }
}

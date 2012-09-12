using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.Unity;
using TfsCommitMonitor.Config;

namespace TfsCommitMonitor.Injection
{
    class TfsCommitMonitorUnityContainerExtension : UnityContainerExtension
    {
        protected override void Initialize()
        {
            Container.RegisterType<IConfigurationProvider, ConfigurationProvider>();
        }
    }
}

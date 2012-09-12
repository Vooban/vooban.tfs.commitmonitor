using Microsoft.Practices.Unity;

namespace TfsCommitMonitor.Injection
{
    class IOC
    {
        private static UnityContainer _container;

        public static UnityContainer Container
        {
            get
            {
                if (_container == null)
                {
                    _container = new UnityContainer();
                    _container.AddNewExtension<TfsCommitMonitorUnityContainerExtension>();
                }

                return _container;
            }
        }
    }
}

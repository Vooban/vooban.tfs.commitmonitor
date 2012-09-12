using Microsoft.TeamFoundation.VersionControl.Client;

namespace TfsCommitMonitor
{
    static class TfsExtensions
    {
        public static void Dispose(this VersionControlServer server)
        {
            server.TeamProjectCollection.Dispose();
        }
    }
}

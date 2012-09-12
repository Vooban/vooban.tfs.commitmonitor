using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TfsCommitMonitor.SourceControl
{
    class TfsCheckinEqualityComparer : IEqualityComparer<TfsCheckin>
    {
        public bool Equals(TfsCheckin x, TfsCheckin y)
        {
            return x.ChangesetId == y.ChangesetId;
        }

        public int GetHashCode(TfsCheckin obj)
        {
            return obj.ChangesetId.GetHashCode();
        }
    }
}

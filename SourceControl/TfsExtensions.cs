using Microsoft.TeamFoundation.VersionControl.Client;

namespace TfsCommitMonitor.SourceControl
{
    public static class TfsExtensions
    {
        public static bool IsSet(this ChangeType value, ChangeType flag)
        {
            return (value & flag) == flag;
        }

        public static bool IsOneFlagSet(this ChangeType value, params ChangeType[] flag)
        {
            foreach (var flagValue in flag)
            {
                if ((value & flagValue) == flagValue)
                    return true;
            }

            return false;
        }

        public static bool AreAllSet(this ChangeType value, params ChangeType[] flag)
        {
            var allSet = true;

            foreach (var flagValue in flag)
            {
                allSet = allSet && (value & flagValue) == flagValue;
            }

            return allSet;
        }
    }
}

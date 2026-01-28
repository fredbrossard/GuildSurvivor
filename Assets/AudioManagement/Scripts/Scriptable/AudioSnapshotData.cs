using System;
using AudioManagement.Snapshot;

namespace AudioManagement.Scriptable
{
    [Serializable]
    public class AudioSnapshotData
    {
        public SnapshotBind[] binds;
        public SnapshotTransitionBind[] transitions;
    }
}
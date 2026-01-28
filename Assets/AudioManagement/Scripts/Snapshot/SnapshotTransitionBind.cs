using System;

namespace AudioManagement.Snapshot
{
    [Serializable]
    public class SnapshotTransitionBind
    {
        public string key;
        public string oldSnapshotKey;
        public float oldTimeToReach;
        public string newSnapshotKey;
        public float newTimeToReach;
        public AudioMasterType audioMasterType;
        public AudioBusType audioBusType;
    }
}

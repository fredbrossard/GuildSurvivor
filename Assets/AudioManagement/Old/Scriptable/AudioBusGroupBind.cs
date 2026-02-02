using System;

namespace AudioManagement.Scriptable
{
    [Serializable]
    public struct AudioBusGroupBind
    {
        public AudioBusType busType;
        public string mixerGroupName;
        public AudioLibrary[] libraries;
    }
}
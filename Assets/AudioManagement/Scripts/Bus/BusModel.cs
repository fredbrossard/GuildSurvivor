using System;
using UnityEngine.Audio;

namespace Audio.Bus
{
    [Serializable]
    public class BusModel
    {
        public AudioMixerGroup mixerGroup;
        public float CurrentVolume { get; set; }
        public string mixerGroupName => mixerGroup.name;
        public string playerPrefName;
        public string exposedParamName;
    }
}

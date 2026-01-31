using System;
using UnityEngine.Audio;

namespace Audio.Bus
{
    [Serializable]
    public class BusSetting
    {
        public AudioMixerGroup mixerGroup;
        public string mixerGroupName => mixerGroup.name;
        public string playerPrefName;
        public string exposedParamName;
    }
}

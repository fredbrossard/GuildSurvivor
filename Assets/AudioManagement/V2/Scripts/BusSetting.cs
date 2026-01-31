using System;
using UnityEngine.Audio;

namespace Audio
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

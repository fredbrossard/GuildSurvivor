using System;
using UnityEngine.Audio;

namespace AudioManagement.Scriptable
{
    [Serializable]
    public class AudioMixerData
    { 
        public AudioMixer mixer;
        public string groupMasterName;
    }
}

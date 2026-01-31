using UnityEngine;
using UnityEngine.Audio;

namespace Audio
{
    [CreateAssetMenu(fileName = "AudioSetting", menuName = "Data/Audio/AudioSetting", order = 1)]
    public class AudioSetting : ScriptableObject
    {
        [Header("Master")]
        public AudioMixer audioMixer;
        public MasterBusSetting[] masterBusSettings;
        public SimpleBusSetting[] simpleBusSettings; 
    }
}
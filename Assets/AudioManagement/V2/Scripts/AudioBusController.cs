using AudioManagement.Scriptable;
using System.Collections.Generic;
using UnityEngine;

namespace Audio
{
    //TODO no monobehaviour cs
    public class AudioBusController : MonoBehaviour
    {
        private Dictionary<Audio.AudioType, List<BusSetting>> simpleBusSettings;
        private Dictionary<Audio.AudioType, List<AudioElement>> audioElements;
    
        public void Bind(AudioSetting setting)
        {
            simpleBusSettings = new Dictionary<Audio.AudioType, List<BusSetting>>();
            audioElements = new Dictionary<AudioType, List<AudioElement>>();
            foreach(var busSetting in setting.simpleBusSettings)
            {
                if(!simpleBusSettings.ContainsKey(busSetting.audioType))
                    simpleBusSettings.Add(busSetting.audioType, new List<BusSetting>());

                simpleBusSettings[busSetting.audioType].Add(busSetting);
                
                InstantiateAudioSource(busSetting);
                if(busSetting.audioLibraries != null)
                {
                    foreach (var library in busSetting.audioLibraries)
                    {
                        foreach (var element in library.elements)
                        {
                            if(!audioElements.ContainsKey(busSetting.audioType))
                            {
                                audioElements.Add(busSetting.audioType, new List<AudioElement>());
                            }

                            audioElements[busSetting.audioType].Add(element);
                        }
                    }
                }
            }
        }

        private void InstantiateAudioSource(SimpleBusSetting setting)
        {
            AudioSource audioSource = new GameObject().AddComponent<AudioSource>();
            audioSource.transform.parent = transform;
            audioSource.name = setting.mixerGroupName;
            audioSource.outputAudioMixerGroup = setting.mixerGroup;
            setting.AudioSource = audioSource;
        }
    }
}
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;

namespace AudioManagement.Scriptable
{
    //[CreateAssetMenu(fileName = "AudioLibrary", menuName = "Data/Audio/AudioLibrary", order = 2)]
    public class AudioLibrary : ScriptableObject
    {
        [Tooltip("use it for file appear in git commit")] public int version;
        public string key;
        public bool preloadAudioData;
        public List<AudioElement> elements;
        private bool audioClipsAreLoaded = false;

        public async Task PreloadAudioData()
        {
            if (preloadAudioData && !audioClipsAreLoaded)
            {
                foreach (var element in elements)
                {
                    element?.clip?.LoadAudioData();
                    await Task.Yield();
                }

                audioClipsAreLoaded = true;
            }
        }

        public async Task UnloadAudioData()
        {
            if (preloadAudioData && audioClipsAreLoaded)
            {
                foreach (var element in elements)
                {
                    element?.clip?.UnloadAudioData();
                    await Task.Yield();
                }
                audioClipsAreLoaded = false;
            }
        }
    }
}




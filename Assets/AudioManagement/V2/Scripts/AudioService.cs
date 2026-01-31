using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Utils;
using Zenject;

namespace Audio
{
    public class AudioService : IInitializable
    {
        [Inject] private AudioSetting m_setting;
        [Inject] private AudioObjManager m_audioSources;

        private Dictionary<string, BusSetting> m_busSettings;
        private Dictionary<string, AudioElement> m_audioElements;

        public void Initialize()
        {
            TaskUtils.OnSameThread(() => Bind());
        }

        internal async Task Bind()
        {
            m_busSettings = new Dictionary<string, BusSetting>();
            m_audioElements = new Dictionary<string, AudioElement>();

            foreach(var masterSetting in m_setting.masterBusSettings)
            {
                m_busSettings.Add(masterSetting.mixerGroupName, masterSetting);
            }

            foreach (var busSetting in m_setting.simpleBusSettings)
            {
                busSetting.AudioSource = m_audioSources.InstantiateAudioSource(busSetting);
                if (busSetting.audioLibraries != null)
                {
                    foreach (var library in busSetting.audioLibraries)
                    {
                        await library.PreloadAudioData();

                        if (library.elements != null)
                        {
                            foreach (var element in library.elements)
                            {
                                if (!m_audioElements.ContainsKey(element.name))
                                {
                                    element.AudioSource = busSetting.AudioSource;
                                    m_audioElements.Add(element.clip.name, element);
                                }
                                else
                                {
                                    Debug.LogError("[AudioBusController]: audio element with " + element.name + " already exist");
                                }
                            }
                        }
                    }

                    m_busSettings.Add(busSetting.mixerGroupName, busSetting);
                }
                
            }
        }

        public async Task Unload()
        {
            if (m_busSettings != null)
            {
                foreach (var busSetting in m_setting.simpleBusSettings)
                {
                    if (busSetting.audioLibraries != null)
                    {
                        foreach (var library in busSetting.audioLibraries)
                            await library.UnloadAudioData();
                    }
                }
            }
        }

        public void Play(string clipName)
        {
            if (m_audioElements.ContainsKey(clipName))
            {
                PlayAudioElement(m_audioElements[clipName].AudioSource, m_audioElements[clipName]);
            }
        }

        private void PlayAudioElement(AudioSource _audioSource, AudioElement _audioElement)
        {
            _audioSource.clip = _audioElement.clip;
            _audioSource.volume = _audioElement.generalVolume;
            _audioSource.pitch = GetPitch(_audioElement.randomPitch);
            _audioSource.loop = _audioElement.isLooping;
           
            //_currentAudioClipIsPlaying = false;

            //if (!_isPaused)
            //{
            if (_audioElement.playOneShot)
            {
                _audioSource.PlayOneShot(_audioSource.clip);
            }
            else
            {
                _audioSource?.Play(/*audioElement.delay*/);
            }

                //_currentAudioClipIsPlaying = true;
            //}
        }

        private float GetPitch((float min, float max) pitch)
        {
            float generalPitch = (pitch.min == pitch.max)
                ? pitch.min
                : UnityEngine.Random.Range(pitch.min, pitch.max);

            return Mathf.Clamp(generalPitch, -3f, 3f);
        }
    }
}

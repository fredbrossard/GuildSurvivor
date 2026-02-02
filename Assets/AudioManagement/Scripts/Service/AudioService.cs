using Audio.Bus;
using Audio.Mono;
using Audio.Scriptable;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using Utils;
using Zenject;

namespace Audio
{
    public class AudioService : IInitializable
    {
        public bool IsInitialize { get; private set; }

        [Inject] private AudioSetup m_setting;
        [Inject] private AudioSourceController m_audioSources;

        private Dictionary<string, BusModel> m_busSettings;
        private Dictionary<string, AudioElementModel> m_audioElements;


        #region INIT
        public void Initialize()
        {
            TaskUtils.OnSameThread(() => Bind());
        }

        public async Task Bind()
        {
            IsInitialize = false;
            m_busSettings = new Dictionary<string, BusModel>();
            m_audioElements = new Dictionary<string, AudioElementModel>();

            foreach(var masterSetting in m_setting.masterBusSettings)
            {
                m_busSettings.Add(masterSetting.mixerGroupName, masterSetting);
                SetVolume(masterSetting.mixerGroupName, GetVolumeByPlayerPref(masterSetting.mixerGroupName));
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
                    SetVolume(busSetting.mixerGroupName, GetVolumeByPlayerPref(busSetting.mixerGroupName));
                }
            }

            IsInitialize = true;
        }
        #endregion

        #region DESTROY
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
        #endregion

        #region PLAY

        public void Play(string clipName)
        {
            if (m_audioElements.ContainsKey(clipName))
            {
                PlayAudioElement(m_audioElements[clipName].AudioSource, m_audioElements[clipName]);
            }
        }

        private void PlayAudioElement(AudioSource _audioSource, AudioElementModel _audioElement)
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
        #endregion

        #region VOLUME
        public void SetVolume(string _busName, float _volume)
        {
            if (m_busSettings.ContainsKey(_busName))
            {
                m_busSettings[_busName].CurrentVolume = Mathf.Clamp(_volume, 0.0001f, 1f);
                m_setting.audioMixer.SetFloat(m_busSettings[_busName].exposedParamName, Mathf.Log10(m_busSettings[_busName].CurrentVolume) * 20);
            }
            else
            {
                Debug.LogError("[AudioService]: set volume failed for bus with name - " + _busName);
            }
        }

        public float GetVolume(string _busName)
        {
            if (m_busSettings.ContainsKey(_busName))
            {
                return m_busSettings[_busName].CurrentVolume;
            }
            else
            {
                Debug.LogError("[AudioService]: get volume failed for bus with name - " + _busName);
                return 1f;
            }
        }
        #endregion

        #region PLAYER_PREF
        public void SaveAllVolumeByPlayerPref()
        {
            foreach(var busSetting in m_busSettings.Values)
            {
                if(!string.IsNullOrEmpty(busSetting.playerPrefName))
                    SaveVolumeByPlayerPref(busSetting.playerPrefName);
            }
        }

        public void SaveVolumeByPlayerPref(string _busName)
        {
            PlayerPrefs.SetFloat(m_busSettings[_busName].playerPrefName, m_busSettings[_busName].CurrentVolume);
        }

        public float GetVolumeByPlayerPref(string _busName)
        {
            if(m_busSettings.ContainsKey(_busName)
                && PlayerPrefs.HasKey(m_busSettings[_busName].playerPrefName))
            {
                return PlayerPrefs.GetFloat(m_busSettings[_busName].playerPrefName);
            }

            return 1f;
        }
        #endregion
    }
}

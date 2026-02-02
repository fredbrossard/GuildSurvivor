using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AudioManagement.Bind;
using AudioManagement.Service;
using AudioManagement.Scriptable;
using UnityEngine;
using UnityEngine.Audio;

namespace AudioManagement.Group
{
    [Serializable]
    public class AudioMasterGroup
    {
        public AudioMasterType MasterType { get; private set; }
        public float CurrentVolume { get; private set; } = 1f;
        private float _initialVolume;
        private string _exposedVolumeParam;
        private Dictionary<AudioBusType, AudioBusGroup> _busLibrary = null;
        private string _playerPrefSavingVolumeName;

        public AudioMasterGroup(AudioMasterGroupBind bind, AudioService controller, AudioSourceBusBind audioSourceBusBind)
        {
            if (!string.IsNullOrEmpty(bind.mixerGroupMasterName))
            {
                MasterType = bind.masterType;
                _exposedVolumeParam = bind.exposedVolumeParam;
                _playerPrefSavingVolumeName = bind.playerPrefSavingVolumeName;
                _initialVolume = bind.initialVolume;
                _busLibrary = new Dictionary<AudioBusType, AudioBusGroup>();

                foreach (var busBind in bind.busGroups)
                {
                    if (!_busLibrary.ContainsKey(busBind.busType))
                    {
                        _busLibrary.Add(busBind.busType, new AudioBusGroup(busBind, controller.GetOutputMixerGroup(busBind.mixerGroupName),
                            audioSourceBusBind.GetAudioSource(busBind.busType), controller.OnPaused));
                    }
                }
            }
        }

        public void Play(string key, AudioBusType audioBusType)
        {
            if (_busLibrary != null && _busLibrary.ContainsKey(audioBusType))
            {
                AudioElement audioElement = _busLibrary[audioBusType].GetAudioElement(key);

                if (audioElement != null)
                {
                    _busLibrary[audioBusType].Play(audioElement);
                }
            }
        }

        public void Play(string key, AudioBusType audioBusType, AudioSource specificAudioSource)
        {
            AudioElement audioElement = _busLibrary[audioBusType].GetAudioElement(key);

            if (audioElement != null)
            {
                specificAudioSource.outputAudioMixerGroup = GetOutputMixerGroup(audioBusType);
                if (_busLibrary != null && _busLibrary.ContainsKey(audioBusType))
                {
                    _busLibrary[audioBusType].Play(audioElement, specificAudioSource);
                }
            }
        }

        public void Pause(bool isPaused, AudioBusType audioBusType)
        {
            if (_busLibrary != null && _busLibrary.ContainsKey(audioBusType))
            {
                _busLibrary[audioBusType].Pause(isPaused);
            }
        }

        public void Stop(AudioBusType audioBusType)
        {
            if (_busLibrary != null && _busLibrary.ContainsKey(audioBusType))
            {
                _busLibrary[audioBusType].AudioSource.Stop();
            }
        }

        public void SetVolume(AudioMixer audioMixer, float value)
        {
            CurrentVolume = Mathf.Clamp(value, 0.0001f, 1f);
            audioMixer.SetFloat(_exposedVolumeParam, Mathf.Log10(CurrentVolume) * 20);
        }

        public AudioMixerGroup GetOutputMixerGroup(AudioBusType audioBusType)
        {
            if (_busLibrary != null && !_busLibrary.ContainsKey(audioBusType))
            {
                return _busLibrary[audioBusType].MixerGroup;
            }

            return null;
        }

        public void SaveVolumeByPlayerPref(float value)
        {
            if (!string.IsNullOrEmpty(_playerPrefSavingVolumeName))
            {
                PlayerPrefs.SetFloat(_playerPrefSavingVolumeName, value);
                Debug.Log("Volume at: " + value + " saved for " + MasterType.ToString() + " in player pref");
            }
            else
            {
                Debug.LogError("[Audio]: player pref saving volume name is empty");
            }
        }

        public float GetVolumeByPlayerPref()
        {
            if (PlayerPrefs.HasKey(_playerPrefSavingVolumeName))
            {
                return PlayerPrefs.GetFloat(_playerPrefSavingVolumeName);
            }
            else
            {
                return _initialVolume;
            }
        }

        public async Task PreloadAudioData(string key, AudioBusType audioBusType)
        {
            if (_busLibrary != null && _busLibrary.ContainsKey(audioBusType))
            {
                await _busLibrary[audioBusType].PreloadAudioData(key);
            }
        }

        public async Task UnloadAudioData(string key, AudioBusType audioBusType)
        {
            if (_busLibrary != null && _busLibrary.ContainsKey(audioBusType))
            {
                await _busLibrary[audioBusType].UnloadAudioData(key);
            }
        }
    }
}
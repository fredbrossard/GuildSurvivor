using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AudioManagement.Bind;
using AudioManagement.Group;
using AudioManagement.Scriptable;
using Utils;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

namespace AudioManagement.Service
{
    public class AudioService : IInitializable
    {
        //[SerializeField] private bool withAdressable = false;
        //[SerializeField, ShowIf("withAdressable", false)] 
        public bool IsInit { get; private set; } = false;
        public Action<bool, AudioBusType> OnPaused;

        [Inject] private AudioMixerData _audioMixerData;
        [Inject] private AudioMasterGroupBind[] _audioMasterGroups;
        [Inject] private AudioSourceBusBind _audioSourceBusBind;

        private Dictionary<AudioMasterType, AudioMasterGroup> _audioMasterGroupLibrary;
        private Dictionary<AudioMasterType, List<AudioBusType>> _audioBusTypeByMasterType;
        private Dictionary<string, AudioMixerGroup> _outputMixerGroups;

        void IInitializable.Initialize()
        {
            TaskUtils.OnSameThread(() => AsyncInit());
        }

        private async Task AsyncInit()
        {
            await LoadAudioMixerAndData();
            SetOutputAudioGroup();
            InitMasterGroups();
            InitializeAllVolumes();

            IsInit = true;
        }

        private async Task LoadAudioMixerAndData()
        {
//#if UNITY_EDITOR
//            //TODO Load with adressable
//            object dataObj = await LoadAsyncFromAssetBundle("AudioMixerInstaller.asset") as AudioMixerScriptable;

//            if(dataObj != null)
//            {
//                _data = dataObj as AudioMixerScriptable;
//            }
//            else
//            {
//                Debug.LogError("Audio mixer data scriptable is null");
//            }
//#endif

            await new WaitForEndOfFrame();
        }


        private void InitMasterGroups()
        {
            if (_audioMasterGroups != null)
            {
                _audioMasterGroupLibrary = new Dictionary<AudioMasterType, AudioMasterGroup>();
                _audioBusTypeByMasterType = new Dictionary<AudioMasterType, List<AudioBusType>>();

                foreach (AudioMasterGroupBind bind in _audioMasterGroups)
                {
                    if (!_audioMasterGroupLibrary.ContainsKey(bind.masterType))
                    {
                        _audioMasterGroupLibrary.Add(bind.masterType, new AudioMasterGroup(bind, this, _audioSourceBusBind));
                    }

                    if (!_audioBusTypeByMasterType.ContainsKey(bind.masterType))
                    {
                        _audioBusTypeByMasterType.Add(bind.masterType, new List<AudioBusType>());
                    }

                    foreach (var busGroup in bind.busGroups)
                    {
                        _audioBusTypeByMasterType[bind.masterType].Add(busGroup.busType);
                    }
                }
            }
        }

        private void InitializeAllVolumes()
        {
            if (_audioMasterGroupLibrary != null)
            {
                foreach (var masterGroup in _audioMasterGroupLibrary.Values)
                {
                    SetVolume(masterGroup.MasterType, GetVolumeByPlayerPref(masterGroup.MasterType));
                }
            }
        }

        #region STATE_METHODS
        public void Play(string key, AudioBusType busType)
        {
            AudioMasterType masterType = GetMasterType(busType);
            if(masterType != AudioMasterType.generalMaster && _audioMasterGroupLibrary.ContainsKey(masterType))
            {
                _audioMasterGroupLibrary[masterType].Play(key, busType);
            }
        }

        public void Play(string key, AudioBusType busType, AudioSource audioSource)
        {
            AudioMasterType masterType = GetMasterType(busType);
            if (masterType != AudioMasterType.generalMaster && _audioMasterGroupLibrary.ContainsKey(masterType))
            {
                _audioMasterGroupLibrary[masterType].Play(key, busType, audioSource);
            }
        }

        public void Pause(bool isPaused, AudioBusType busType)
        {
            AudioMasterType masterType = GetMasterType(busType);
            if (masterType != AudioMasterType.generalMaster && _audioMasterGroupLibrary.ContainsKey(masterType))
            {
                _audioMasterGroupLibrary[masterType].Pause(isPaused, busType);
            }
        }

        public void Stop(AudioBusType busType)
        {
            AudioMasterType masterType = GetMasterType(busType);
            if (masterType != AudioMasterType.generalMaster && _audioMasterGroupLibrary.ContainsKey(masterType))
            {
                _audioMasterGroupLibrary[masterType].Stop(busType);
            }
        }

        public float GetVolume(AudioMasterType audioMasterType)
        {
            if (_audioMasterGroupLibrary.ContainsKey(audioMasterType))
            {
                return _audioMasterGroupLibrary[audioMasterType].CurrentVolume;
            }

            return 1f;
        }

        public void SetVolume(AudioMasterType audioMasterType, float volume)
        {
            if (_audioMasterGroupLibrary.ContainsKey(audioMasterType))
            {
                _audioMasterGroupLibrary[audioMasterType].SetVolume(_audioMixerData.mixer, volume);
            }
        }
        #endregion

        #region GETTER

        private AudioMasterType GetMasterType(AudioBusType busType)
        {
            if(_audioBusTypeByMasterType != null)
            {
                foreach(var key in _audioBusTypeByMasterType.Keys)
                {
                    if (_audioBusTypeByMasterType[key].Contains(busType))
                        return key;
                }
            }

            return AudioMasterType.generalMaster;
        }
        #endregion

        #region MIXER_GROUP
        public AudioMixerGroup GetOutputMixerGroup(string key)
        {
            if (_outputMixerGroups != null && _outputMixerGroups.ContainsKey(key))
            {
                return _outputMixerGroups[key];
            }

            return null;
        }

        private void SetOutputAudioGroup()
        {
            _outputMixerGroups = new Dictionary<string, AudioMixerGroup>();
            AudioMixerGroup[] currentGroups = _audioMixerData.mixer.FindMatchingGroups(_audioMixerData.groupMasterName);
            if (currentGroups != null)
            {
                foreach (var group in currentGroups)
                {
                    if (!_outputMixerGroups.ContainsKey(group.name))
                    {
                        _outputMixerGroups.Add(group.name, group);
                    }
                }
            }
        }
        #endregion

        #region PLAYER_PREF
        public void SaveAllVolumeByPlayerPref()
        {
            if (_audioMasterGroupLibrary != null)
            {
                foreach(var audioMasterType in  _audioMasterGroupLibrary.Keys)
                {
                    SaveVolumeByPlayerPref(audioMasterType, GetVolume(audioMasterType));
                }
            }
        }
        public void SaveVolumeByPlayerPref(AudioMasterType audioMasterType, float value)
        {
            if (_audioMasterGroupLibrary != null && _audioMasterGroupLibrary.ContainsKey(audioMasterType))
            {
                _audioMasterGroupLibrary[audioMasterType].SaveVolumeByPlayerPref(value);
            }
        }

        public float GetVolumeByPlayerPref(AudioMasterType audioMasterType)
        {
            if (_audioMasterGroupLibrary != null && _audioMasterGroupLibrary.ContainsKey(audioMasterType))
            {
                return _audioMasterGroupLibrary[audioMasterType].GetVolumeByPlayerPref();
            }

            return 1f;
        }
        #endregion
    }
}

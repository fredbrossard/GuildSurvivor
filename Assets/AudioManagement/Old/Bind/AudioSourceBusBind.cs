using System;
using System.Collections.Generic;
using UnityEngine;

namespace AudioManagement.Bind
{
    public class AudioSourceBusBind : MonoBehaviour
    {
        [Serializable]
        public struct Bind 
        {
            public string name;
            public AudioBusType audioBusType;
            public AudioSource audioSource;
        }

        [SerializeField] private Bind[] _binds;

        private Dictionary<AudioBusType, AudioSource> _library;

        public void Init()
        {
            _library = new Dictionary<AudioBusType, AudioSource>();

            if(_binds != null)
            {
                foreach(var bind in _binds)
                {
                    if(!_library.ContainsKey(bind.audioBusType))
                    {
                        _library.Add(bind.audioBusType, bind.audioSource);
                    }
                }
            }
        }

        public AudioSource GetAudioSource(AudioBusType audioBusType)
        {
            if(_library.ContainsKey(audioBusType))
            {
                return _library[audioBusType];
            }
            return null;
        }
    }
}

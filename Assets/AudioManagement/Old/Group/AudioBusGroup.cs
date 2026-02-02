using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AudioManagement.Scriptable;
using UnityEngine;
using UnityEngine.Audio;

namespace AudioManagement.Group
{
    [Serializable]
    public class AudioBusGroup
    {
        public AudioSource AudioSource { get; private set; }
        public AudioBusType BusType { get; private set; }
        public AudioMixerGroup MixerGroup { get; private set; }

        private Dictionary<string, AudioElement> _audioElements;
        private Dictionary<string, AudioLibrary> _audioLibraries;
        private bool _isPaused = false;
        private bool _currentAudioClipIsPlaying = false;
        private Action<bool, AudioBusType> OnPaused;

        public AudioBusGroup(AudioBusGroupBind bind, AudioMixerGroup mixerGroup, AudioSource audioSource, Action<bool, AudioBusType> onPaused)
        {
            BusType = bind.busType;
            MixerGroup = mixerGroup;
            _audioElements = new Dictionary<string, AudioElement>();
            _audioLibraries = new Dictionary<string, AudioLibrary>();

            if (bind.libraries != null)
            {
                foreach (var library in bind.libraries)
                {
                    if (!string.IsNullOrEmpty(library.key) && !_audioLibraries.ContainsKey(library.key))
                    {
                        _audioLibraries.Add(library.key, library);
                    }

                    if (library.elements != null)
                    {
                        foreach (var element in library.elements)
                        {
                            if (!_audioElements.ContainsKey(element.name))
                            {
                                _audioElements.Add(element.name, element);
                            }
                        }
                    }
                }
            }

            AudioSource = audioSource;
            OnPaused = onPaused;
        }

        internal void Play(AudioElement audioElement, AudioSource specificAudioSource = null)
        {
            AudioSource currentAudioSource = specificAudioSource == null ? AudioSource : specificAudioSource;

            currentAudioSource.clip = audioElement.clip;
            currentAudioSource.outputAudioMixerGroup = MixerGroup;
            currentAudioSource.volume = audioElement.generalVolume;
            currentAudioSource.pitch = GetPitch(audioElement.randomPitch);
            currentAudioSource.loop = audioElement.isLooping;
            _currentAudioClipIsPlaying = false;

            if (!_isPaused)
            {
                if (audioElement.playOneShot)
                {
                    currentAudioSource.PlayOneShot(AudioSource.clip);
                }
                else
                {
                    currentAudioSource?.Play(/*audioElement.delay*/);
                }

                _currentAudioClipIsPlaying = true;
            }
        }

        internal void Pause(bool isPaused)
        {
            _isPaused = isPaused;
            if (isPaused)
            {
                AudioSource.Pause();
                OnPaused?.Invoke(true, BusType);
            }
            else if (!isPaused && PlayAfterPause())
            {
                AudioSource.Play();
                OnPaused?.Invoke(false, BusType);
            }
        }

        public bool PlayAfterPause()
        {
            if (AudioSource.loop)
            {
                return true;
            }

            if (!_currentAudioClipIsPlaying)
            {
                _currentAudioClipIsPlaying = true;
                return true;
            }

            return _currentAudioClipIsPlaying && AudioSource.time != 0f && AudioSource.time < AudioSource.clip.length;
        }

        private float GetPitch((float min, float max) pitch)
        {
            float generalPitch = (pitch.min == pitch.max)
                ? pitch.min
                : UnityEngine.Random.Range(pitch.min, pitch.max);

            return Mathf.Clamp(generalPitch, -3f, 3f);
        }

        public AudioElement GetAudioElement(string key)
        {
            if (_audioElements.ContainsKey(key))
            {
                return _audioElements[key];
            }
            else
            {
                Debug.LogError("[AudioManager] error on GetAudioElement: with key: " + key + ", in this bus group: " + BusType);
                return null;
            }
        }

        public async Task PreloadAudioData(string key)
        {
            if (_audioLibraries.ContainsKey(key))
            {
                await _audioLibraries[key].PreloadAudioData();
            }
        }

        public async Task UnloadAudioData(string key)
        {
            if (_audioLibraries.ContainsKey(key))
            {
                await _audioLibraries[key].UnloadAudioData();
            }
        }
    }
}
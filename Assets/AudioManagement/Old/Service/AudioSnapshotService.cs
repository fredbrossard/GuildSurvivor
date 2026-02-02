using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AudioManagement.Scriptable;
using AudioManagement.Snapshot;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

namespace AudioManagement.Service
{
    public class AudioSnapshotService : IInitializable
    {
        [Inject] private AudioService _audioService;
        [Inject] private AudioMixerData _audioMixerData;
        [Inject] private AudioSnapshotData _audioSnapshotData;

        private Dictionary<string, AudioMixerSnapshot> _snapshotLibrary;
        private Dictionary<string, SnapshotTransitionBind> _snapshotTransitionLibrary;

        public void Initialize()
        {
            _snapshotLibrary = new Dictionary<string, AudioMixerSnapshot>();
            if (_audioSnapshotData.binds != null)
            {
                foreach (var bind in _audioSnapshotData.binds)
                {
                    if (!_snapshotLibrary.ContainsKey(bind.key))
                    {
                        _snapshotLibrary.Add(bind.key, _audioMixerData.mixer.FindSnapshot(bind.key));
                    }
                }
            }

            _snapshotTransitionLibrary = new Dictionary<string, SnapshotTransitionBind>();
            {
                if (_audioSnapshotData.transitions != null)
                {
                    foreach (var bind in _audioSnapshotData.transitions)
                    {
                        if (bind != null && !_snapshotTransitionLibrary.ContainsKey(bind.key))
                        {
                            _snapshotTransitionLibrary.Add(bind.key, bind);
                        }
                    }
                }
            }
        }

        #region GETTER
        public AudioMixerSnapshot GetSnapshot(string key)
        {
            if (_snapshotLibrary != null && _snapshotLibrary.ContainsKey(key))
            {
                return _snapshotLibrary[key];
            }
            else
            {
                return null;
            }
        }

        public SnapshotTransitionBind GetTransitionSnapshot(string key)
        {
            if (_snapshotTransitionLibrary != null && _snapshotTransitionLibrary.ContainsKey(key))
            {
                return _snapshotTransitionLibrary[key];
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region TRANSITION
        public void ToTransition(string snapshotKey, float timeToReach)
        {
            AudioMixerSnapshot snapshot = GetSnapshot(snapshotKey);
            Debug.Log(snapshot == null ? "null" : snapshot.name);

            if (snapshot != null)
            {
                _snapshotLibrary[snapshotKey].TransitionTo(timeToReach);
            }
            else
            {
                Debug.Log("snapshot is null: " + snapshotKey);
            }
        }

        public async Task ToAsyncTransition(string snapshotKey, float timeToReach, Action OnTransitionEnded = null)
        {
            AudioMixerSnapshot snapshot = GetSnapshot(snapshotKey);
            Debug.Log("Snapshot launch: " + snapshot == null ? "null" : snapshot.name);

            if (snapshot != null)
            {
                _snapshotLibrary[snapshotKey].TransitionTo(timeToReach);
                await new WaitForSeconds(timeToReach);
                OnTransitionEnded?.Invoke();
            }
            else
            {
                Debug.Log("snapshot is null: " + snapshotKey);
            }
        }

        public async Task OldToNewTransition(string transitionKey, string newAudioClipKey)
        {
            SnapshotTransitionBind snapshotTransitionBind = GetTransitionSnapshot(transitionKey);
            if (snapshotTransitionBind != null)
            {
                await ToAsyncTransition(snapshotTransitionBind.oldSnapshotKey, snapshotTransitionBind.oldTimeToReach,
                   () => _audioService.Stop(snapshotTransitionBind.audioBusType));

                _audioService.Play(newAudioClipKey, snapshotTransitionBind.audioBusType);
                await ToAsyncTransition(snapshotTransitionBind.newSnapshotKey, snapshotTransitionBind.newTimeToReach);
            }
        }
        #endregion
    }
}

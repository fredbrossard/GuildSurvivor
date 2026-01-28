using AudioManagement.Service;
using UnityEngine;
using Zenject;

namespace AudioManagement.ProximyAudio
{
    [RequireComponent(typeof (AudioSource))]
    public class ProximityAudioSE : MonoBehaviour
    {
        [SerializeField] private string soundKey;
        [SerializeField] private bool playOnAwake = true;
        [SerializeField] private AudioMasterType audioMasterType;
        [SerializeField] private AudioBusType audioBusType;

        [Inject] private AudioService _audioService;

        private AudioSource _source;

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
            _audioService.OnPaused += OnPaused;
        }

        // Start is called before the first frame update
        void Start()
        {
            if (playOnAwake)
            {
                DoSound();
            }
        }

        private void OnDestroy()
        {
            StopAllCoroutines();
            _audioService.OnPaused -= OnPaused;
        }

        private void DoSound()
        {
            _audioService.Play(soundKey, audioBusType, _source);
        }

        private void OnPaused(bool paused, AudioBusType audioBusType)
        {
            if (audioBusType == this.audioBusType)
            {
                if (paused)
                {
                    _source.Pause();
                }
                else
                {
                    _source.Play();
                }
            }
        }
    } 
}

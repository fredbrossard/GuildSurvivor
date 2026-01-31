using Audio.Bus;
using UnityEngine;
using Utils;
using Zenject;

namespace Audio.Mono
{
    public class AudioObjManager : MonoBehaviour
    {
        [SerializeField] private Transform audioSourcesTransform;
        [Inject] private AudioService audioService;

        private void Awake()
        {

        }

        private void OnDestroy()
        {
            TaskUtils.OnSameThread(() => audioService.Unload());
        }

        public AudioSource InstantiateAudioSource(SimpleBusSetting setting)
        {
            AudioSource audioSource = new GameObject().AddComponent<AudioSource>();
            audioSource.transform.parent = audioSourcesTransform;
            audioSource.name = setting.mixerGroupName;
            audioSource.outputAudioMixerGroup = setting.mixerGroup;
            audioSource.playOnAwake = setting.playOnAwake;
            setting.AudioSource = audioSource;

            return audioSource;
        }
    }
}
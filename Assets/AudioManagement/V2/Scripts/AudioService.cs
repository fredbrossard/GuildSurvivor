using UnityEngine;

namespace Audio
{
    public class AudioService : MonoBehaviour
    {
        [SerializeField] private AudioSetting audioSetting;

        void Start()
        {
            AudioBusController controller = new GameObject().AddComponent<AudioBusController>();
            controller.name = "AudioBusController";
            controller.transform.parent = transform;
            controller.Bind(audioSetting);
        }

        public void Play(string _clipName, AudioType _audioType)
        {

        }
    }
}

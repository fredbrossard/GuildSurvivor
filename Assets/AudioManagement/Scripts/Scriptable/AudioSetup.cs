using UnityEngine;
using Zenject;

namespace AudioManagement.Scriptable
{
    [CreateAssetMenu(fileName = "AudioSetup", menuName = "Data/Audio/AudioDataSetup", order = 1)]
    public class AudioSetup : ScriptableObject
    {
        [Header("Mixer")]
        public AudioMixerData audioMixerData;

        [Header("AudioGroups")]
        [SerializeField] public AudioMasterGroupBind[] audioMasterGroups;

        [Header("Snapshot")]
        public AudioSnapshotData snapshatData;

        public void Bind(DiContainer container)
        {            
            container.BindInstance(audioMixerData);
            container.BindInstance(audioMasterGroups);
            container.BindInstance(snapshatData);
        }
    }
}

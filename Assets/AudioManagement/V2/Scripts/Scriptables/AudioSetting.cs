using Audio.Bus;
using UnityEngine;
using Zenject;

namespace Audio.Scriptable
{
    [CreateAssetMenu(fileName = "AudioSetting", menuName = "Data/Audio/AudioSetting", order = 1)]
    public class AudioSetting : ScriptableObject
    {
        //public AudioMixer audioMixer;

        [Header("Bus")]
        public MasterBusSetting[] masterBusSettings;
        public SimpleBusSetting[] simpleBusSettings;

        //[Header("Snapshot")]
        //public AudioSnapshotData snapshatData;

        public void Bind(DiContainer container)
        {
            container.BindInstance(masterBusSettings).AsSingle();
            container.BindInstance(simpleBusSettings).AsSingle();
            //container.BindInstance(snapshatData);
        }
    }
}
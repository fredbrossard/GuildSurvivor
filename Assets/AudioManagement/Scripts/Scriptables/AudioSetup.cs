using Audio.Bus;
using UnityEngine;
using UnityEngine.Audio;
using Zenject;

namespace Audio.Scriptable
{
    [CreateAssetMenu(fileName = "AudioSetup", menuName = "Data/Audio/AudioSetup", order = 1)]
    public class AudioSetup : ScriptableObject
    {
        public AudioMixer audioMixer;

        [Header("Bus")]
        public MasterBusModel[] masterBusSettings;
        public SimpleBusModel[] simpleBusSettings;

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
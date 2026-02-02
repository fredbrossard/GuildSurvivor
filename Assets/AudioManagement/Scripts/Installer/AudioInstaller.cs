using Audio.Mono;
using Audio.Scriptable;
using UnityEngine;
using Zenject;

namespace Audio.Installer
{ 
    public class AudioInstaller : MonoInstaller
    {
        [SerializeField] private AudioSourceController audioSources;
        [SerializeField] private AudioSetup audioSettings;

        public override void InstallBindings()
        {
            audioSettings.Bind(Container);
            Container.BindInstance(audioSettings).AsSingle();
            Container.Bind<AudioSourceController>().FromInstance(audioSources);

            Container.BindInterfacesAndSelfTo<AudioService>().AsSingle();
        }
    }
}

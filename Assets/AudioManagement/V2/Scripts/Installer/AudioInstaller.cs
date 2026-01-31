using Audio.Mono;
using Audio.Scriptable;
using UnityEngine;
using Zenject;

namespace Audio.Installer
{ 
    public class AudioInstaller : MonoInstaller
    {
        [SerializeField] private AudioObjManager audioSources;
        [SerializeField] private AudioSetting audioSettings;

        public override void InstallBindings()
        {
            audioSettings.Bind(Container);
            Container.BindInstance(audioSettings).AsSingle();
            Container.Bind<AudioObjManager>().FromInstance(audioSources);

            Container.BindInterfacesAndSelfTo<AudioService>().AsSingle();
        }
    }
}

using UnityEngine;
using Zenject;

namespace Audio
{ 
    public class AudioInstaller : MonoInstaller
    {
        [SerializeField] private AudioObjManager audioSources;
        [SerializeField] private Audio.AudioSetting audioSettings;

        public override void InstallBindings()
        {
            audioSettings.Bind(Container);
            Container.BindInstance(audioSettings).AsSingle();
            Container.Bind<AudioObjManager>().FromInstance(audioSources);

            Container.BindInterfacesAndSelfTo<AudioService>().AsSingle();
        }
    }
}

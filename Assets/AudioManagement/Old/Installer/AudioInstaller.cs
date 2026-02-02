using AudioManagement.Bind;
using AudioManagement.Scriptable;
using AudioManagement.Service;
using UnityEngine;
using Zenject;

namespace AudioManagement.Installer
{
    [RequireComponent(typeof(AudioSourceBusBind))]
    public class AudioInstaller : MonoInstaller
    {
        [SerializeField] private AudioSetup _audioSetup;
        private AudioSourceBusBind _audioSourceBind;

        public override void InstallBindings()
        {
            _audioSetup.Bind(Container);
            Container.BindInstance(_audioSetup).AsSingle();
            Container.BindInterfacesAndSelfTo<AudioService>().AsSingle();
            Container.BindInterfacesAndSelfTo<AudioSnapshotService>().AsSingle();
            
            _audioSourceBind = GetComponent<AudioSourceBusBind>();
            _audioSourceBind.Init();
            Container.BindInstance(_audioSourceBind);
        }
    }
}

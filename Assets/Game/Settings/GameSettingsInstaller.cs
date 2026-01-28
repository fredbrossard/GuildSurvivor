using UnityEngine;
using Zenject;

namespace Game.Settings
{
    [CreateAssetMenu(fileName = "LevelSettings", menuName = "Installers/LevelSettings")]
    public class GameSettingsInstaller : ScriptableObjectInstaller
    {
        [SerializeField] LevelSettings levelSettings;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<LevelSettings>().FromInstance(levelSettings).AsSingle();
        }
    }
}
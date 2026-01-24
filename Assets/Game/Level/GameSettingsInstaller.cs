using Managers.Spawner;
using UnityEngine;
using Zenject;

namespace Game.Level
{
    [CreateAssetMenu(fileName = "LevelSettings", menuName = "Installers/LevelSettings")]
    public class GameSettingsInstaller : ScriptableObjectInstaller
    {
        [SerializeField] GameSettings GameSettings;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameSettings>().FromInstance(GameSettings).AsSingle();
        }
    }
}
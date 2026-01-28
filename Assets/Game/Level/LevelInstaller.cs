using Game.Settings;
using Zenject;

namespace Game.Level
{
    public class LevelInstaller : MonoInstaller<LevelInstaller>
    {
        [Inject] private LevelSettings settings;

        public override void InstallBindings()
        {
            LevelService levelService = new LevelService();
            levelService.Bind(settings);

            Container.Bind<LevelService>().FromInstance(levelService).AsSingle();
        }
    }
}
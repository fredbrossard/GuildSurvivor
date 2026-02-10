using Zenject;

namespace Game.Level
{
    public class LevelInstaller : MonoInstaller<LevelInstaller>
    {
        public override void InstallBindings()
        {
            LevelService levelService = new LevelService();
            levelService.Bind();
            Container.Bind<LevelService>().FromInstance(levelService).AsSingle();
        }
    }
}
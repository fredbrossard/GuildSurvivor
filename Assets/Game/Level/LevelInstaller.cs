using Zenject;

public class LevelInstaller : MonoInstaller<LevelInstaller>
{
    [Inject] private GameSettings settings;
    public override void InstallBindings()
    {
        LevelService levelService = new LevelService();
        levelService.Bind(settings);                                                                                                                         
        Container.Bind<LevelService>().FromInstance(levelService).AsSingle();
    }
}
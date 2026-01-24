using Game.Level;
using Zenject;

public class LevelService
{
    public GameSettings gameSettings;

    public void Bind(GameSettings _gameSettings)
    {
        gameSettings = _gameSettings; 
    }
}

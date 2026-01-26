using Game.Settings;

namespace Game.Level
{
    public class LevelService
    {
        public GameSettings gameSettings;

        public void Bind(GameSettings _gameSettings)
        {
            gameSettings = _gameSettings; 
        }
    }
}

using Game.Settings;

namespace Game.Level
{
    public class LevelService
    {
        public LevelSettings levelSettings;

        public void Bind(LevelSettings _gameSettings)
        {
            levelSettings = _gameSettings; 
        }
    }
}

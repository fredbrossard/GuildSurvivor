using Game.Entity.Enemy;
using Zenject;

namespace Entity.Spawner
{
    public class EnemyFactory : Managers.Spawner.Factory<Enemy>
    {
        [Inject] LevelService levelService;

        private void Start()
        {
            Bind(levelService.gameSettings.enemyPoolRefs);
        }

        public override Enemy GetObj(Enemy obj)
        {
            Enemy currentEnemy = m_pools[obj].GetAvailableEntity() as Enemy; 
            currentEnemy.Alive();

            return currentEnemy;
        }
    }
}

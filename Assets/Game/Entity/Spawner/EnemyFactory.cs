using Game.Entity.Enemy;
using Game.Level;
using Zenject;

namespace Entity.Spawner
{
    public class EnemyFactory : Managers.Spawner.Factory<Enemy>
    {
        [Inject] LevelService m_service;

        private void Start()
        {
            Bind(m_service.gameSettings.enemySettings.poolRefs);
        }

        public override Enemy GetObj(Enemy obj)
        {
            Enemy currentEnemy = m_pools[obj].GetAvailableEntity() as Enemy; 
            currentEnemy.Alive();

            return currentEnemy;
        }
    }
}

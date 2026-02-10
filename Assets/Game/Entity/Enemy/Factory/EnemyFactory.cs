using Game.Entity.Enemy;
using Game.Settings;
using Zenject;

namespace Entity.Enemy
{
    public class EnemyFactory : Managers.Spawner.Factory<EnemyObj>
    {
        [Inject] EnemySpawnSettings enemySpawnSettings;

        private void Awake()
        {
            Bind(enemySpawnSettings.poolRefs);
        }

        public override EnemyObj GetObj(EnemyObj obj)
        {
            EnemyObj currentEnemy = m_pools[obj].GetAvailableEntity(); 
            currentEnemy.Alive();

            return currentEnemy;
        }
    }
}

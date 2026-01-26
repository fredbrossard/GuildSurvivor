using Game.Entity.Enemy;
using Game.Level;
using Zenject;

namespace Entity.Enemy
{
    public class EnemyFactory : Managers.Spawner.Factory<EnemyObj>
    {
        [Inject] LevelService m_service;

        private void Start()
        {
            Bind(m_service.gameSettings.enemySettings.poolRefs);
        }

        public override EnemyObj GetObj(EnemyObj obj)
        {
            EnemyObj currentEnemy = m_pools[obj].GetAvailableEntity() as EnemyObj; 
            currentEnemy.Alive();

            return currentEnemy;
        }
    }
}

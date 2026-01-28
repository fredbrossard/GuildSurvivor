using Entity.Enemy;
using Game.Entity.Enemy;
using Game.Settings;
using Game.Utils;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Game.Level
{
    [RequireComponent(typeof(EnemyFactory))]
    public class MotherBrain : MonoBehaviour
    {
        [Inject] private LevelService levelService;
        private EnemyFactory m_enemyFactory;
        private WavesSettings m_wavesSettings;

        //TODO to delete
        public InputActionReference spaceActionRef;
        EnemyObj objA;

        private void Awake()
        {
            m_enemyFactory = GetComponent<EnemyFactory>();
            m_wavesSettings = levelService.levelSettings.enemySpawnSettings.waveSpawnSettings;
        }

        private void Start()
        {
            TaskUtils.OnSameThread(() => SpawnEnemies());
        }

        //TODO to delete
        private void Update()
        {
            if (spaceActionRef.action.WasPressedThisFrame())
            {
                objA.Die();
            }
        }

        //TODO set algo to spawn ennemie with waves
        private async Task SpawnEnemies()
        {
            objA = m_enemyFactory.GetObj(m_wavesSettings.waveSettings[0].defaultRefPrefab.GetComponent<EnemyObj>());
            objA.transform.position = new Vector3(5f, 3f, 0f);

            EnemyObj objB = m_enemyFactory.GetObj(m_wavesSettings.waveSettings[0].waves[0].refPrefab.GetComponent<EnemyObj>());
            objB.transform.position = new Vector3(-5f, -3f, 0f);

            await new WaitForEndOfFrame();
        }
    }
}

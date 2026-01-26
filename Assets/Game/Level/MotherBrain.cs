using Entity.Enemy;
using Game.Entity.Enemy;
using Game.Settings;
using System.Collections;
using UnityEngine;
using Zenject;

namespace Game.Level
{
    [RequireComponent(typeof(EnemyFactory))]
    public class MotherBrain : MonoBehaviour
    {
        [Inject] LevelService levelService;

        EnemyFactory m_enemyFactory;
        WavesSettings m_wavesSettings;

        private void Awake()
        {
            m_enemyFactory = GetComponent<EnemyFactory>();
            m_wavesSettings = levelService.levelSettings.enemySpawnSettings.waveSpawnSettings;
        }

        private void Start()
        {
            //TODO delete coroutine for async task
            StartCoroutine(SpawnEnemies());
        }

        //TODO improve
        private IEnumerator SpawnEnemies()
        {
            EnemyObj objA = m_enemyFactory.GetObj(m_wavesSettings.waveSettings[0].defaultRefPrefab.GetComponent<EnemyObj>());
            objA.transform.position = new Vector3(5f, 3f, 0f);

            EnemyObj objB = m_enemyFactory.GetObj(m_wavesSettings.waveSettings[0].waves[0].refPrefab.GetComponent<EnemyObj>());
            objB.transform.position = new Vector3(-5f, -3f, 0f);

            yield return new WaitForEndOfFrame();
        }
    }
}

using Entity.Enemy;
using Game.Entity.Enemy;
using Utils;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;
using Game.Entity.Player;
using static Game.Settings.GameSettingsInstaller;

namespace Game.Level
{
    [RequireComponent(typeof(EnemyFactory))]
    public class MotherBrain : MonoBehaviour
    {
        [SerializeField] private short playerId = 0;
        [Inject] PlayerFactory playerFactory;
        //[Inject]  EnemySpawnSettings enemySpawnSettings;
        [Inject] PlayerSettings playerSettings;

        private EnemyFactory m_enemyFactory;

        //TODO to delete, just for test
        public InputActionReference spaceActionRef;
        EnemyObj objA;

        private void Awake()
        {
            //m_enemyFactory = GetComponent<EnemyFactory>();
        }

        private void Start()
        {
            PlayerObj player = playerFactory.Create(playerSettings.models[playerId]);
            TaskUtils.OnSameThread(() => SpawnEnemies());
        }

        //TODO to delete
        private void Update()
        {
            if (spaceActionRef.action.WasPressedThisFrame())
            {
                //objA.Die();
            }
        }

        //TODO set algo to spawn ennemie with waves
        private async Task SpawnEnemies()
        {
            //objA = m_enemyFactory.GetObj(enemySpawnSettings.waveSpawnSettings.waveSettings[0].defaultRefPrefab.GetComponent<EnemyObj>());
            //objA.transform.position = new Vector3(5f, 3f, 0f);

            //EnemyObj objB = m_enemyFactory.GetObj(enemySpawnSettings.waveSpawnSettings.waveSettings[0].waves[0].refPrefab.GetComponent<EnemyObj>());
            //objB.transform.position = new Vector3(-5f, -3f, 0f);

            await new WaitForEndOfFrame();
        }
    }
}

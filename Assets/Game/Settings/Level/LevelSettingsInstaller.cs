using Managers.Spawner;
using System;
using UnityEngine;
using Zenject;

namespace Game.Settings
{
    [CreateAssetMenu(fileName = "LevelSettingsInstaller", menuName = "Installers/LevelSettingsInstaller")]
    public class LevelSettingsInstaller : ScriptableObjectInstaller<LevelSettingsInstaller>
    {
        [Header("Settings")]
        [SerializeField] private LevelSettings levelSettings;

        [Serializable]
        public class LevelSettings
        {
            [Header("Enemy")]
            public EnemySpawnSettings enemySpawnSettings;
        }

        public override void InstallBindings()
        {
            Container.BindInstance(levelSettings);
            Container.BindInstances(levelSettings.enemySpawnSettings);
        }
    }
}
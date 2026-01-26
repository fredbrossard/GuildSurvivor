using System;
using UnityEngine;

namespace Game.Settings
{
    [Serializable]
    public class LevelSettings
    {
        [Header("Enemy")]
        public EnemySpawnSettings enemySpawnSettings;
    }
}

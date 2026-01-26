using System;
using UnityEngine;

namespace Game.Settings
{
    [Serializable]
    public class GameSettings
    {
        [Header("Enemy")]
        public EnemySettings enemySettings;
    }
}

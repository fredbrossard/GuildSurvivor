using Managers.Spawner;
using System;
using UnityEngine;

namespace Game.Settings
{
    [Serializable]
	public class EnemySpawnSettings
	{
        [Header("Objects Pool")]
        public PoolRef[] poolRefs;
        [Header("Waves")]
        public WavesSettings waveSpawnSettings;
    }
}
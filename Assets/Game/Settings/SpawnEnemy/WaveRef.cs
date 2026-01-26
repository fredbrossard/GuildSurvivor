using System;
using UnityEngine;

namespace Game.Settings
{
	[Serializable]
	public class WaveSetting
	{
        public GameObject defaultRefPrefab;
		public WaveRef[] waves;
    }

	[Serializable]
	public struct WaveRef
	{
		public GameObject refPrefab;
		public float spawnCount;
		public float percent;
	}
}
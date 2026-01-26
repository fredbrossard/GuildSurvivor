using System;
using UnityEngine;

namespace Game.Settings
{
	[Serializable]
	public class WavesSettings
	{
		[Tooltip("In secondes")]
        public float timeToChangeWaves;
        public WaveSetting[] waveSettings;
	}
}
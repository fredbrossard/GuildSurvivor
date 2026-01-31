using Audio.Scriptable;
using System;
using UnityEngine;

namespace Audio.Bus
{
	[Serializable]
	public class SimpleBusSetting : BusSetting
	{
		public bool playOnAwake;
        public AudioLibrary[] audioLibraries;
		public AudioSource AudioSource {  get; set; }
	}
}
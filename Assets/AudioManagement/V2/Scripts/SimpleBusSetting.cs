using AudioManagement.Scriptable;
using System;
using UnityEngine;

namespace Audio
{
	[Serializable]
	public class SimpleBusSetting : BusSetting
	{
        public AudioType audioType;
        public AudioLibrary[] audioLibraries;
		public AudioSource AudioSource {  get; set; }
	}
}
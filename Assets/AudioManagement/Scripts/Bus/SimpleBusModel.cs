using Audio.Scriptable;
using System;
using UnityEngine;

namespace Audio.Bus
{
	[Serializable]
	public class SimpleBusModel : BusModel
	{
		public bool playOnAwake;
        public AudioLibraryModel[] audioLibraries;
		public AudioSource AudioSource {  get; set; }
	}
}
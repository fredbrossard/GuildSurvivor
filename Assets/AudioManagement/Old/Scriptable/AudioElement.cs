using System;
using UnityEngine;

namespace AudioManagement.Scriptable
{
    [Serializable]
    //[CreateAssetMenu(fileName = "AudioElement", menuName = "Data/Audio/AudioElement",order = 3)]
    public class AudioElement : ScriptableObject
    {
        public AudioSource AudioSource { get; set; }
        public AudioClip clip;
        [Range(0f, 1f)] public float generalVolume = 1f;
        public (float min, float max) randomPitch = (1f, 1f); 
        public bool isLooping = false;
        public bool playOneShot = false;

        //TODO delay behaviour
        //public ulong delay = 0;

        internal bool IsNamed(string a)
        {
            return name.Equals(a);
        }
    }
}

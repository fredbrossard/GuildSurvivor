using System;
using UnityEngine;

namespace AudioManagement.Scriptable
{
    [Serializable]
    public struct AudioMasterGroupBind
    {
        [Header("General")]
        public AudioMasterType masterType;

        [Header("Group")]
        public string mixerGroupMasterName;
        public AudioBusGroupBind[] busGroups;

        [Header("Volume")]
        [Range(0f, 1f)] public float initialVolume;
        public string exposedVolumeParam;
        public string playerPrefSavingVolumeName;
    }
}
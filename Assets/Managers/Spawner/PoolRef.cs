using System;
using UnityEngine;

namespace Managers.Spawner
{
    [Serializable]
    public struct PoolRef
    {
        public string name;
        public GameObject prefab;

        public int initialObjCount;
    }
}
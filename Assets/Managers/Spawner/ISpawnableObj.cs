using UnityEngine;

namespace Managers.Spawner
{ 
    public interface ISpawnableObj 
    {
        //public uint UId { get; set; }
        public GameObject GameObject { get; }
        public void Initialize();
    }
}

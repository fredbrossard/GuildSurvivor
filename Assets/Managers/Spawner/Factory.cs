using System.Collections.Generic;
using UnityEngine;

namespace Managers.Spawner
{
	public abstract class Factory<T> : MonoBehaviour where T : ISpawnableObj 
	{
		protected Dictionary<T, ObjectPool> m_pools;
		
		public abstract T GetObj(T obj);

        public void Bind(PoolRef[] poolRefs)
        {
            m_pools = new Dictionary<T, ObjectPool>();
			
			//TODO improve object pool monobehaviour
			foreach (var poolRef in poolRefs) 
			{
                ObjectPool objectPoolObj = new GameObject("ObjectPool: " + poolRef.name).AddComponent<ObjectPool>();
				objectPoolObj.transform.SetParent(transform);
				objectPoolObj.Bind(poolRef);

                m_pools.Add(poolRef.prefab.GetComponent<T>(), objectPoolObj);
			}	
        }
	}
}
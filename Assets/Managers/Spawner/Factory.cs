using System.Collections.Generic;
using UnityEngine;

namespace Managers.Spawner
{
	public abstract class Factory<T> : MonoBehaviour where T : ISpawnableObj 
	{
		protected Dictionary<T, ObjectPool<T>> m_pools;
		
		public abstract T GetObj(T obj);

        public void Bind(PoolRef[] poolRefs)
        {
            m_pools = new Dictionary<T, ObjectPool<T>>();

			foreach (var poolRef in poolRefs) 
			{
                m_pools.Add(poolRef.prefab.GetComponent<T>(), new ObjectPool<T>(poolRef, transform));
			}	
        }
	}
}
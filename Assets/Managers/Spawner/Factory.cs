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
				ObjectPool objectPoolTemp = new GameObject().AddComponent<ObjectPool>();
				objectPoolTemp.name = "ObjectPool";
                ObjectPool objectPoolObj = Instantiate(objectPoolTemp, transform);
				objectPoolObj.Bind(poolRef);

                m_pools.Add(poolRef.prefab.GetComponent<T>(), objectPoolObj);
			}	
        }

        public GameObject InstantiateObj(GameObject go)
		{
			return Instantiate(go);
		}
	}
}
using System.Collections.Generic;
using UnityEngine;

namespace Managers.Spawner
{
	public class ObjectPool : MonoBehaviour
	{
        public PoolRef PoolRef { get; private set; }
        private Stack<ISpawnableObj> m_stack;

        public void Bind(PoolRef _poolRef)
        {
            PoolRef = _poolRef;
            m_stack = new Stack<ISpawnableObj>();
            
            InstantiateObjs(PoolRef.initialObjCount);
        }

        private void InstantiateObjs(int _count)
        {
            for (int i = 0; i < _count; ++i)
            {
                GameObject obj = Instantiate(PoolRef.prefab, transform);
                obj.SetActive(false);
                obj.GetComponent<ISpawnableObj>().Bind();
                m_stack.Push(obj.GetComponent<ISpawnableObj>());
            }
        }

        public ISpawnableObj GetAvailableEntity()
        {
            ISpawnableObj obj = null;

            obj = m_stack.Count == 0
            ? Instantiate(PoolRef.prefab).GetComponent<ISpawnableObj>()
            : m_stack.Pop();
            
            obj.GameObject.SetActive(true);
            
            return obj;
        }

        public void Release(ISpawnableObj obj)
        {
            obj.GameObject.SetActive(false);
            m_stack.Push(obj);
        }
    }
}

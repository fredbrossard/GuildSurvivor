using System.Collections.Generic;
using UnityEngine;
using Zenject;

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

                ISpawnableObj genericObj = obj.GetComponent<ISpawnableObj>();
                genericObj.Bind();
                m_stack.Push(genericObj);
            }
        }

        public ISpawnableObj GetAvailableEntity()
        {
            ISpawnableObj obj = default;

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

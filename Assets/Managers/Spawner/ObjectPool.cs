using System.Collections.Generic;
using UnityEngine;

namespace Managers.Spawner
{
	public class ObjectPool<T> where T : ISpawnableObj
	{
        public PoolRef PoolRef { get; private set; }
        private Stack<T> m_stack;
        private Transform m_parent;

        public ObjectPool(PoolRef _poolRef, Transform _parent)
        {
            PoolRef = _poolRef;
            m_stack = new Stack<T>();
            GameObject objParent = InstantiationManager.Instance.InstantiateEmptyObj(_poolRef.name, _parent);
            m_parent = objParent.transform;
            InstantiateObjs(PoolRef.initialObjCount);
        }

        private void InstantiateObjs(int _count)
        {
            for (int i = 0; i < _count; ++i)
            {
                GameObject obj = InstantiationManager.Instance.InstantiateObj(PoolRef.prefab, m_parent);
                obj.SetActive(false);

                T genericObj = obj.GetComponent<T>();
                genericObj.Bind();
                genericObj.OnReleaseObj += Release;
                m_stack.Push(genericObj);
            }
        }

        public T GetAvailableEntity()
        {
            T obj = default;

            obj = m_stack.Count == 0
            ? InstantiationManager.Instance.InstantiateObj(PoolRef.prefab, m_parent).GetComponent<T>()
            : m_stack.Pop();
            
            obj.GameObject.SetActive(true);
            
            return obj;
        }

        private void Release(GameObject obj)
        {
            obj.SetActive(false);
            m_stack.Push(obj.GetComponent<T>());
        }
    }
}

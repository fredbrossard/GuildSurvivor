using UnityEngine;

namespace Utils
{
    //TODO inject stuff, no singleton
    public class InstantiationManager : MonoBehaviour
    {
        public static InstantiationManager Instance { get; private set; }

        public void Awake()
        {
            if(Instance == null)
            {
                Instance = this;
            }
        }

        public GameObject InstantiateObj(GameObject _prefab, Transform _parent)
        {
            return Instantiate(_prefab, _parent);
        }

        public GameObject InstantiateObj(GameObject _prefab)
        {
            return Instantiate(_prefab);
        }

        public GameObject InstantiateEmptyObj(string _name)
        {
            GameObject obj = new GameObject(_name);
            return obj;
        }

        public GameObject InstantiateEmptyObj(string _name, Transform _parent)
        {
            GameObject obj = InstantiateEmptyObj(_name);
            obj.transform.parent = _parent;
            return obj;
        }
    }
}
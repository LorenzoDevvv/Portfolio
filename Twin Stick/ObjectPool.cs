using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance { get; private set; }

    [SerializeField] private List<PoolData> poolDataList = new List<PoolData>();

    private Dictionary<string, Queue<GameObject>> pooledObjects = new Dictionary<string, Queue<GameObject>>();

    [System.Serializable]
    public class PoolData
    {
        public string id;
        public GameObject prefab;
        public int bufferSize;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            InitializePools();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializePools()
    {
        foreach (PoolData poolData in poolDataList)
        {
            CreateObjectPool(poolData.id, poolData.prefab, poolData.bufferSize);
        }
    }

    private void CreateObjectPool(string id, GameObject prefab, int bufferSize)
    {
        if (!pooledObjects.ContainsKey(id))
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < bufferSize; i++)
            {
                GameObject obj = Instantiate(prefab, transform);    
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            pooledObjects.Add(id, objectPool);
        }
    }

    public GameObject GetPooledObject(string id)
    {
        if (pooledObjects.ContainsKey(id))
        {
            Queue<GameObject> objectPool = pooledObjects[id];

            if (objectPool.Count > 0)
            {
                GameObject obj = objectPool.Dequeue();
                obj.SetActive(true);
                return obj;
            }
            else
            {
                Debug.LogWarning("Object pool is empty for ID: " + id);
            }
        }
        else
        {
            Debug.LogWarning("Object pool does not contain ID: " + id);
        }

        return null;
    }

    public void ReturnObjectToPool(string id, GameObject obj)
    {
        if (pooledObjects.ContainsKey(id))
        {
            obj.SetActive(false);
            pooledObjects[id].Enqueue(obj);
        }
        else
        {
            Debug.LogWarning("Object pool does not contain ID: " + id);
            Destroy(obj);
        }
    }
}

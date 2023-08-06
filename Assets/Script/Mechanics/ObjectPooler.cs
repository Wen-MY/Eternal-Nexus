using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public GameObject prefab;
        public int poolSize = 10;
    }

    public List<Pool> pools;
    private Dictionary<GameObject, Queue<GameObject>> poolDictionary = new Dictionary<GameObject, Queue<GameObject>>();

    private void Start()
    {
        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.poolSize; i++)
            {
                GameObject obj = Instantiate(pool.prefab, transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.prefab, objectPool);
        }
    }

    public GameObject GetPooledObject(GameObject prefab)
    {
        if (poolDictionary.ContainsKey(prefab) && poolDictionary[prefab].Count > 0)
        {
            GameObject obj = poolDictionary[prefab].Dequeue();
            obj.SetActive(true);
            return obj;
        }

        Debug.LogWarning("Object pool is empty for prefab: " + prefab.name);
        return null;
    }

    public void ReturnToPool(GameObject prefab, GameObject obj)
    {
        obj.SetActive(false);
        poolDictionary[prefab].Enqueue(obj);
    }
}

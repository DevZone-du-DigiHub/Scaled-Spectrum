using System.Collections.Generic;
using UnityEngine;

public class ObjectPool
{
    private Dictionary<int, List<GameObject>> pooledObjects = new Dictionary<int, List<GameObject>>();
    private GameObject[] objectPrefabs;

    public ObjectPool(GameObject[] prefabs)
    {
        objectPrefabs = prefabs;
    }

    public GameObject GetObjectFromPool(int index)
    {
        if (!pooledObjects.ContainsKey(index))
        {
            pooledObjects.Add(index, new List<GameObject>());
        }

        List<GameObject> pool = pooledObjects[index];

        GameObject obj = null;
        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].activeInHierarchy)
            {
                obj = pool[i];
                break;
            }
        }

        if (obj == null)
        {
            obj = GameObject.Instantiate(objectPrefabs[index]);
            pool.Add(obj);
        }

        obj.SetActive(true);
        return obj;
    }
}

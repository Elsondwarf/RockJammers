using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooling : MonoBehaviour
{
    public List<GameObject> pooledObjects;
    public List<GameObject> activeObjects;
    public GameObject objectToPool;
    public int poolSize;

    private void Start()
    {
        pooledObjects = new List<GameObject>();
        activeObjects = new List<GameObject>();
        GameObject obj;

        for (int i = 0; i < poolSize; i++)
        {
            obj = Instantiate(objectToPool);
            obj.SetActive(false);
            obj.GetComponent<Particle>().objectPooling = this;
            pooledObjects.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < poolSize; i++)
        {
            if (!pooledObjects[i].activeInHierarchy)
            {
                return pooledObjects[i];
            }
        }

        return null;
    }
}

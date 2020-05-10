using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public List<Probability> probs;

    public Setup[] objectToPool;

    public List<Setup>[] pool;

    [SerializeField] private int startAmount = 1;

    [SerializeField] private bool canGrow = false;

    [SerializeField] private bool initializedAtStart;

    void Awake()
    {
        InitPool(startAmount);

        if (initializedAtStart)
            GetPooledObject();
    }

    public void InitPool(int amount)
    {
        if (amount == 0)
            return;

        pool = new List<Setup>[objectToPool.Length];

        for (int i = 0; i < pool.Length; i++)
            pool[i] = new List<Setup>();


        for (int i = 0; i < startAmount; i++)
            InstantiateNewObject(false);
    }

    public Setup InstantiateNewObject(bool isActive)
    {
        return InstantiateNewObject(isActive, 0);
    }

    public Setup InstantiateNewObject(bool isActive, int objectToInstantiateIndex)
    {
        Setup newObject;

        newObject = (Instantiate(objectToPool[objectToInstantiateIndex], this.transform));
        newObject.SetupAll();
        newObject.gameObject.SetActive(isActive);
        pool[objectToInstantiateIndex].Add(newObject);

        return newObject;
    }

    [ContextMenu("GetPooled")]
    public Setup GetPooledObject()
    {
        return GetPooledObject(0);
    }

    public Setup GetPooledObject(int poolIndex)
    {
        if (pool == null)
        {
            Debug.LogWarning("You are trying to acces the pool of objects instantiated before it is created, try to make the Get Pooled Object call on the Start Callback rather than the awake callback.");
            return null;
        }

        Setup obj;

        for (int i = 0; i < pool[poolIndex].Count; i++)
        {
            if (!pool[poolIndex][i].gameObject.activeInHierarchy)
            {
                obj = pool[poolIndex][i];
                obj.gameObject.SetActive(true);
                obj.SetupAll();
                return obj;
            }
        }

        if (canGrow)
            return obj = InstantiateNewObject(true, poolIndex);

        return null;
    }
    
}

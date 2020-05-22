using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pool : MonoBehaviour
{
    public Setup[] objectToPool;

    [SerializeField] public List<Setup> pool;

    [SerializeField] private int startAmount = 1;

    [SerializeField] private bool canGrow = false;

    [SerializeField] private bool initializedAtStart;


    // TODO: Revisar si realmente es necesario esto, posiblemnte se pueda resolver lo del preview cube en el futuro cuando solo haya que remplazar el mesh y la textura y no todo el gameobject.
    /// <summary>
    /// This is absolutly needed if for example you need to destry the object outside.
    /// </summary>
    [SerializeField] private bool detachObjectWhenPooled = false;

    void Awake()
    {
        InitPool(startAmount);

        if (initializedAtStart)
            GetPooledObject(0);
    }

    public void InitPool(int amount)
    {
        if (amount == 0)
            return;

        for (int i = 0; i < startAmount; i++)
            InstantiateNewObject(false,0);
    }

    public Setup InstantiateNewObject(bool isActive, int objIndex)
    {
        Setup newObject;

        newObject = (Instantiate(objectToPool[objIndex], this.transform));
        newObject.SetupAll();
        newObject.gameObject.SetActive(isActive);

        pool.Add(newObject);

        return newObject;
    }

    public Setup GetPooledObject()
    {
        return GetPooledObject(0);
    }
    
    public Setup GetPooledObject(Transform newParent)
    {
        Setup auxObject = GetPooledObject(0);
        auxObject.transform.SetParent(newParent);

        if (detachObjectWhenPooled)
        {
            pool.Remove(auxObject);
        }
    
        return auxObject;
    }

    [ContextMenu("Test")]
    public void Test()
    {
        pool.Remove(pool[0]);
    }

    public Setup GetPooledObject(int objIndex)
    {
        if (pool == null)
        {
            Debug.LogWarning("You are trying to acces the pool of objects instantiated before it is created, try to make the Get Pooled Object call on the Start Callback rather than the awake callback.");
            return null;
        }

        Setup obj;

        for (int i = 0; i < pool.Count; i++)
        {
            if (!pool[i].gameObject.activeInHierarchy)
            {
                obj = pool[i];
                obj.gameObject.SetActive(true);
                obj.SetupAll();
                return obj;
            }
        }

        if (canGrow)
            return obj = InstantiateNewObject(true, objIndex);

        return null;
    }
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Setup : MonoBehaviour
{
    public float probability;

    public IPoolable[] poolableComponents;

    public void SetupAll()
    {
        poolableComponents = GetComponents<IPoolable>();

        for (int i = 0; i < poolableComponents.Length; i++)
            poolableComponents[i].Initialize();
    }
}

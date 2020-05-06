using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pool))]
public class CubeSpawner : MonoBehaviour
{
    // TODO: Esta clase va a tner toda la logica tmabien de la randomizacion de caras segun la cantidad de recursos.

    private Pool _cubePool;
    private Cube currentSpawnedCube;

    private void Awake()
    {
        _cubePool = GetComponent<Pool>();
    }

    public void NextCube()
    {
        currentSpawnedCube = _cubePool.GetPooledObject().GetComponent<Cube>();
    }

}

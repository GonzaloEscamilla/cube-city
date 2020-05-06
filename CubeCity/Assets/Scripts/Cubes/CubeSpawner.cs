using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pool))]
public class CubeSpawner : MonoBehaviour
{
    // TODO: Esta clase va a tner toda la logica tmabien de la randomizacion de caras segun la cantidad de recursos.

    private Pool _cubePool;
    private Cube _currentSpawnedCube;

    private void Awake()
    {
        _cubePool = GetComponent<Pool>();
    }

    /// <summary>
    /// Creates the initial cube of the Level. It has a Civic Center on the Upper face.
    /// </summary>
    /// <returns></returns>
    public Cube GetInitialCube()
    {
        _currentSpawnedCube = _cubePool.GetPooledObject().GetComponent<Cube>();

        SetCubeFaces(_currentSpawnedCube, FaceTypes.CivicCenter, FaceOrientationType.Up);

        return _currentSpawnedCube;
    }

    /// <summary>
    /// Get a new random cube.
    /// </summary>
    /// <returns></returns>
    public Cube GetNextCube()
    {
        _currentSpawnedCube = _cubePool.GetPooledObject().GetComponent<Cube>();

        SetCubeFaces(_currentSpawnedCube);

        return _currentSpawnedCube;
    }

    /// <summary>
    /// Sets the faces of a desired cube randomly.
    /// </summary>
    /// <param name="spawnedCube"></param>
    private void SetCubeFaces(Cube spawnedCube)
    {
        Face[] cubeFaces = _currentSpawnedCube.GetComponentsInChildren<Face>();
        int randomType;

        for (int i = 0; i < cubeFaces.Length; i++)
        {
            //TODO aca tenemos que llamar a la funcion que aleatoriza la creacion. Por ahora vamos a hacer un aleatorio entre 1 y 6
            randomType = Random.Range(0, 6);

            cubeFaces[i].Type = (FaceTypes)randomType;
        }
    }

    /// <summary>
    /// Set the faces of the cube randomly but forcing a type of face on a designated site.
    /// </summary>
    /// <param name="spawnedCube"></param>
    /// <param name="forcedFaceType"></param>
    /// <param name="side"></param>
    private void SetCubeFaces(Cube spawnedCube, FaceTypes forcedFaceType, FaceOrientationType side)
    {
        Face[] cubeFaces = _currentSpawnedCube.GetComponentsInChildren<Face>();
        int randomType;

        for (int i = 0; i < cubeFaces.Length; i++)
        {
            //TODO aca tenemos que llamar a la funcion que aleatoriza la creacion. Por ahora vamos a hacer un aleatorio entre 1 y 6
            randomType = Random.Range(0, 6);

            if (cubeFaces[i].GetOrientationType() != side)
                cubeFaces[i].Type = (FaceTypes)randomType;
            else
                cubeFaces[i].Type = forcedFaceType;
        }
    }

}

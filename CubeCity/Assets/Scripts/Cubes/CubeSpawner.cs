using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Pool))]
public class CubeSpawner : MonoBehaviour
{
    // TODO: Esta clase va a tner toda la logica tmabien de la randomizacion de caras segun la cantidad de recursos.

    private Pool _cubePool;
    private Cube _currentSpawnedCube;

    [SerializeField] private Pool[] facePools;

    private void Awake()
    {
        _cubePool = GetComponent<Pool>();

        Pool[] auxPool = GetComponentsInChildren<Pool>();
        facePools = new Pool[auxPool.Length - 1]; // Removing the _cubePool;

        for (int i = 1; i < auxPool.Length; i++)
        {
            if (auxPool[i].tag == "FacePools")
            {
                facePools[i -1] = auxPool[i];
            }
        }

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
    public void NextCube()
    {
        _currentSpawnedCube = _cubePool.GetPooledObject().GetComponent<Cube>();

        SetCubeFaces(_currentSpawnedCube);
    }

    /// <summary>
    /// Returns the current spawned cube if any.
    /// </summary>
    /// <returns></returns>
    public Cube GetCurrentCube()
    {
        if (_currentSpawnedCube != null)
            return _currentSpawnedCube;
        else
            return null;
    }

    /// <summary>
    /// Sets the faces of a desired cube randomly.
    /// </summary>
    /// <param name="spawnedCube"></param>
    private void SetCubeFaces(Cube spawnedCube)
    {
        Face[] cubeFaces = _currentSpawnedCube.GetComponentsInChildren<Face>();
        Transform newFace;
        
        int randomType;


        for (int i = 0; i < cubeFaces.Length; i++)
        {
            //TODO aca tenemos que llamar a la funcion que aleatoriza la creacion. Por ahora vamos a hacer un aleatorio entre 1 y 6
            randomType = Random.Range(0, 6);

            cubeFaces[i].Type = (FaceTypes)randomType;

            newFace = facePools[randomType].GetPooledObject().transform;
            newFace.SetParent(cubeFaces[i].transform);
            newFace.SetPositionAndRotation(cubeFaces[i].transform.position, cubeFaces[i].transform.rotation);
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
        Transform newFace;

        int randomType;

        for (int i = 0; i < cubeFaces.Length; i++)
        {
            //TODO aca tenemos que llamar a la funcion que aleatoriza la creacion. Por ahora vamos a hacer un aleatorio entre 1 y 6
            randomType = Random.Range(0, 6);

            if (cubeFaces[i].GetOrientationType() != side)
                cubeFaces[i].Type = (FaceTypes)randomType;
            else
                cubeFaces[i].Type = forcedFaceType;

            newFace = facePools[randomType].GetPooledObject().transform;
            newFace.SetParent(cubeFaces[i].transform);
            newFace.SetPositionAndRotation(cubeFaces[i].transform.position, cubeFaces[i].transform.rotation);
        }
    }
}

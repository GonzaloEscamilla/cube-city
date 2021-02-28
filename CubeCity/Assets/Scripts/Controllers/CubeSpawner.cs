using FMODUnity;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(Pool))]
public class CubeSpawner : MonoBehaviour
{
    // TODO: Esta clase va a tner toda la logica tmabien de la randomizacion de caras segun la cantidad de recursos.

    [SerializeField] private CubeBehaviour _currentSpawnedCube;
    [SerializeField] private Pool[] facePools;

    FacesDistribution _facesDistribution;
    private Pool _cubePool;

    private void OnEnable()
    {
        EventsManager.Instance.OnPreviewCubeRotated += OnPreviewCubeRotatedEvent;
        EventsManager.Instance.OnPreviewFaceCollision += OnPreviewFaceCollisionEvent;
    }

    private void OnDisable()
    {
        if (EventsManager.Instance != null)
        {
            EventsManager.Instance.OnPreviewCubeRotated -= OnPreviewCubeRotatedEvent;
            EventsManager.Instance.OnPreviewFaceCollision -= OnPreviewFaceCollisionEvent;
        }
    }

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

    public void Start()
    {
        Debug.Log("Start del Spawner face dsitribution: " + LevelManager.control.GetLevelSystem().GetCurrentLevel().GetFacesDistribution());
        _facesDistribution = new FacesDistribution(LevelManager.control.GetLevelSystem().GetCurrentLevel().GetFacesDistribution());
    }

    /// <summary>
    /// Returns the current spawned cube if any.
    /// </summary>
    /// <returns></returns>
    public CubeBehaviour GetCurrentCube()
    {
        if (_currentSpawnedCube != null)
            return _currentSpawnedCube;
        else
            return null;
    }

    /// <summary>
    /// Creates the initial cube of the Level. It has a Civic Center on the Upper face.
    /// </summary>
    /// <returns></returns>
    public CubeBehaviour GetInitialCube()
    {
        _currentSpawnedCube = GetNewCube();

        SetCubeFaces(_currentSpawnedCube);

        EventsManager.Instance.CubeCreated(_currentSpawnedCube);

        return _currentSpawnedCube;
    }

    public bool AvailableCubeExists()
    {
        return _facesDistribution.GetTotalRemainingFaces() >= 6;
    }

    /// <summary>
    /// Creates a new Random Cube.
    /// </summary>
    /// <returns></returns>
    public void NextCube()
    {
        _currentSpawnedCube = GetNewCube();

        SetCubeFaces(_currentSpawnedCube);

        EventsManager.Instance.CubeCreated(_currentSpawnedCube);
    }

    public CubeBehaviour GetNewCube()
    {
        return _cubePool.GetPooledObject(this.transform).GetComponent<CubeBehaviour>();
    }

    /// <summary>
    /// Sets the faces of a desired cube randomly.
    /// </summary>
    /// <param name="spawnedCube"></param>
    private void SetCubeFaces(CubeBehaviour spawnedCube)
    {
        Face[] cubeFaces = _currentSpawnedCube.GetComponentsInChildren<Face>();

        int randomType;

        for (int i = 0; i < cubeFaces.Length; i++)
        {
            randomType = _facesDistribution.GetNewFaceTypeIndex();

            // TODO: Revisar si se puede meter el For dentro de esta misma funcion. Puede llegar a ser interesante y util en el futuro.
            SetFaceGraphics(cubeFaces, i, randomType);
        }
    }

    /// <summary>
    /// Set the faces of the cube randomly but forcing a type of face on a designated site.
    /// </summary>
    /// <param name="spawnedCube"></param>
    /// <param name="forcedFaceType"></param>
    /// <param name="side"></param>
    private void SetCubeFaces(CubeBehaviour spawnedCube, FaceTypes forcedFaceType, FaceOrientationType side)
    {
        Face[] cubeFaces = _currentSpawnedCube.GetComponentsInChildren<Face>();

        int randomType;

        for (int i = 0; i < cubeFaces.Length; i++)
        {
            randomType = _facesDistribution.GetNewFaceTypeIndex();

            if (cubeFaces[i].GetOrientationType() != side)
            {
                SetFaceGraphics(cubeFaces, i, randomType);
            }
            else
            {
                SetFaceGraphics(cubeFaces, i, (int)forcedFaceType);
            }
        }
    }

    private void SetFaceGraphics(Face[] faces, int index, int randomType)
    {
        Transform newFace;
        faces[index].Type = (FaceTypes)randomType;

        newFace = facePools[randomType].GetPooledObject().transform;
        newFace.SetParent(faces[index].transform);
        newFace.SetPositionAndRotation(faces[index].transform.position, faces[index].transform.rotation);
    }

    private void OnPreviewCubeRotatedEvent(Vector3 axis)
    {
        Face[] auxFaces = _currentSpawnedCube.GetFaces();

        for (int i = 0; i < auxFaces.Length; i++)
        {
            auxFaces[i].SetFaceCollisionState(FaceCollisionState.None);
        }

        // TODO: Revisar esto, no tiene setndio que llame un rotate behaviour a el primer hijo que encuentra. Deberia asegurarse de que es el cubo.
        //GetComponentInChildren<RotationBehaviour>().RotateObject(_currentSpawnedCube.gameObject, axis, 90);
    }

    private void OnPreviewFaceCollisionEvent(Face face)
    {
        Face[] auxFaces = _currentSpawnedCube.GetFaces();

        for (int i = 0; i < auxFaces.Length; i++)
        {
            if (face.GetOrientationType() == auxFaces[i].GetOrientationType())
            {
                auxFaces[i].SetFaceCollisionState(face.GetFaceCollisionState());
            }
        }

    }

}

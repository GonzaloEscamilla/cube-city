using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using System.Security.Policy;
using UnityEngine;
using UnityEngine.Animations;

[RequireComponent(typeof(Pool))]
public class CubeSpawner : MonoBehaviour
{
    // TODO: Esta clase va a tner toda la logica tmabien de la randomizacion de caras segun la cantidad de recursos.

    private Pool _cubePool;
    [SerializeField] private CubeBehaviour _currentSpawnedCube;
    private PreviewCube _previewCube;

    [SerializeField] private Pool[] facePools;

    private void OnEnable()
    {
        EventsManager.control.OnPreviewCubeRotated += OnPreviewCubeRotatedEvent;
    }

    private void OnDisable()
    {
        EventsManager.control.OnPreviewCubeRotated -= OnPreviewCubeRotatedEvent;
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

        _previewCube = FindObjectOfType(typeof(PreviewCube)) as PreviewCube;
        if (_previewCube == null)
        {
            Debug.LogError("There must be a previewcube component atached to a gameobject on the scene. Please make shure thats's done.");
        }

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
    /// Returns the current spawned cube if any.
    /// </summary>
    /// <returns></returns>
    public CubeBehaviour GetPreviewCube()
    {
        if (_previewCube != null)
            return _previewCube;
        else
            return null;
    }

    /// <summary>
    /// Creates the initial cube of the Level. It has a Civic Center on the Upper face.
    /// </summary>
    /// <returns></returns>
    public CubeBehaviour GetInitialCube()
    {
        _currentSpawnedCube = _cubePool.GetPooledObject(this.transform).GetComponent<CubeBehaviour>();

        SetCubeFaces(_currentSpawnedCube, FaceTypes.CivicCenter, FaceOrientationType.Up);

        return _currentSpawnedCube;
    }

    /// <summary>
    /// Creates a new Random Cube.
    /// </summary>
    /// <returns></returns>
    public void NextCube()
    {
        _currentSpawnedCube = _cubePool.GetPooledObject(this.transform).GetComponent<CubeBehaviour>();

        SetCubeFaces(_currentSpawnedCube);

        EventsManager.control.CubeCreated(_currentSpawnedCube);
    }

    /// <summary>
    /// Sets the faces of a desired cube randomly.
    /// </summary>
    /// <param name="spawnedCube"></param>
    private void SetCubeFaces(CubeBehaviour spawnedCube)
    {
        Face[] cubeFaces = _currentSpawnedCube.GetComponentsInChildren<Face>();
        Face[] previewCubeFaces = _previewCube.GetComponentsInChildren<Face>();

        _previewCube.ClearGraphics();

        int randomType;

        for (int i = 0; i < cubeFaces.Length; i++)
        {
            //TODO aca tenemos que llamar a la funcion que aleatoriza la creacion. Por ahora vamos a hacer un aleatorio entre 1 y 6
            
            randomType = Random.Range(1, 6);

            // TODO: Revisar si se puede meter el For dentro de esta misma funcion. Puede llegar a ser interesante y util en el futuro.
            SetFaceGraphics(cubeFaces, i, randomType);
            SetFaceGraphics(previewCubeFaces, i, randomType);
        }
        EventsManager.control.CubeCreated(spawnedCube);
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
        Face[] previewCubeFaces = _previewCube.GetComponentsInChildren<Face>();

        _previewCube.ClearGraphics();

        int randomType;

        for (int i = 0; i < cubeFaces.Length; i++)
        {
            //TODO aca tenemos que llamar a la funcion que aleatoriza la creacion. Por ahora vamos a hacer un aleatorio entre 1 y 7
            randomType = Random.Range(1, 6);

            if (cubeFaces[i].GetOrientationType() != side)
            {
                SetFaceGraphics(cubeFaces, i, randomType);
                SetFaceGraphics(previewCubeFaces, i, randomType);
            }
            else
            {
                SetFaceGraphics(cubeFaces, i, (int)forcedFaceType);
                SetFaceGraphics(previewCubeFaces, i, (int) forcedFaceType);
            }
            
        }
        EventsManager.control.CubeCreated(spawnedCube);
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
        GetComponentInChildren<RotationBehaviour>().RotateObject(_currentSpawnedCube.gameObject, axis, 90);
    }
}

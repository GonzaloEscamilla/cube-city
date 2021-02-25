using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBehaviour : MonoBehaviour
{
    private Movement _movement;

    private void Start()
    {
        if (!GetComponent<Movement>())
            Debug.LogWarning("Please add a movement script to the gameobject.");

        _movement = GetComponent<Movement>();

        EventsManager.Instance.onCubeCreated += CubeInnerSpawn;
    }

    /// <summary>
    /// Called after the Spawner spawns this cube, so this cube can performe individual tasks.
    /// </summary>
    public void CubeInnerSpawn(CubeBehaviour cube)
    {

    }

    /// <summary>
    /// Called after the LevelManager builds this cube, so this cube can performe individual tasks.
    /// </summary>
    public void CubeInnerBuild(CubeBehaviour cube)
    {

    }

    public void Move(Vector3[] positions, Action callBack)
    {
        _movement.StartMove(positions,callBack);
    }

    public Resources[] GetFacesData()
    {
        Face[] aux = GetComponentsInChildren<Face>();
        Resources[] facesData = new Resources[aux.Length];

        for (int i = 0; i < aux.Length; i++)
        {
            facesData[i] = aux[i].GetFaceData();
        }

        return facesData;
    }

    public Face[] GetFaces()
    {
        return GetComponentsInChildren<Face>();
    }

    public void InitializeAdjacentFaces()
    {
        foreach (Face face in GetFaces())
        {
            if (face.gameObject.activeSelf)
            {
                face.DiscoverAdjacentFaces();
                foreach (Face adjacentFace in face.GetAdjacentFaces())
                {
                    adjacentFace.AddAdjacentFace(face);
                }
            }
        }
    }

}

public enum RotationAxis
{
    X,
    Y,
    Z
}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeBehaviour : MonoBehaviour
{
    private Movement _movement;

    private void Awake()
    {
        if (!GetComponent<Movement>())
            Debug.LogWarning("Please add a movement script to the gameobject.");

        _movement = GetComponent<Movement>();

        EventsManager.control.onCubeCreated += CubeInnerSpawn;
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

    public FaceData[] GetFacesData()
    {
        Face[] aux = GetComponentsInChildren<Face>();
        FaceData[] facesData = new FaceData[aux.Length];

        for (int i = 0; i < aux.Length; i++)
        {
            facesData[i] = aux[i].GetFaceData();
        }

        return facesData;
    }
}

public enum RotationAxis
{
    X,
    Y,
    Z
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager control;
    public CubeSpawner spawner;

    public FaceDataSO facesData;

    [SerializeField] private Face _currentSelectedFace;

    public int CubeAmount
    {
        get
        {
            return _cubeAmount;
        }
        set
        {
            _cubeAmount = value;
        }
    }
    private int _cubeAmount;

    private void Awake()
    {
        if (control != null)
            Destroy(control);

        control = this;

        spawner = GetComponentInChildren<CubeSpawner>();
    }

    private void Start()
    {
        BuildInitialCube();
    }

    /// <summary>
    /// Creates the initial cube, with a city hall face looking upwards.
    /// </summary>
    public void BuildInitialCube()
    {
        Cube initialCube;
        initialCube = spawner.GetInitialCube();
        initialCube.transform.position = Vector3.zero;
    }

    [ContextMenu("Build")]
    public bool Build()
    {
        Cube newCube;
        newCube = spawner.GetNextCube();
        return false;
    }



}

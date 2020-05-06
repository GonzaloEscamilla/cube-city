using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager control;
    public CubeSpawner spawner;

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

    public bool Build()
    {
        return false;
    }

}

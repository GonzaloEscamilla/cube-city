using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager control;
    
    [SerializeField] private FaceDataSO facesData;

    private CubeSpawner spawner;
    private bool isFinishPlaying;

    public Face CurrentSelectedFace
    {
        get
        {
            return _currentSelectedFace;
        }
        set
        {
            _currentSelectedFace = value;
        }
    }
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
        EventsManager.control.onfaceSelected += OnFaceSelected;
        EventsManager.control.onFaceUnselected += OnFaceUnselected;
        EventsManager.control.onCreateButtonPressed += Build;
    }

    private void OnDestroy()
    {
        EventsManager.control.onfaceSelected -= OnFaceSelected;
        EventsManager.control.onFaceUnselected -= OnFaceUnselected;
        EventsManager.control.onCreateButtonPressed -= Build;
    }

    /// <summary>
    /// What happens when the EventsManager calls this Event.
    /// </summary>
    /// <param name="selectedFace"></param>
    private void OnFaceSelected(Face selectedFace)
    {
        CurrentSelectedFace = selectedFace;
    }

    /// <summary>
    /// What happens when the EventsManager calls this Event.
    /// </summary>
    /// <param name="selectedFace"></param>
    private void OnFaceUnselected()
    {
        CurrentSelectedFace = null;
    }

    /// <summary>
    /// Creates the initial cube, with a city hall face looking upwards.
    /// </summary>
    public void BuildInitialCube()
    {
        Cube initialCube;
        initialCube = spawner.GetInitialCube();
        initialCube.transform.position = Vector3.zero;
        
        NextTurn();
    }

    private void NextTurn()
    {
        if (!isFinishPlaying)
        {
            PreBuild();
        }
    }

    public void PreBuild()
    {
        spawner.NextCube();
    }

    public void Build()
    {
        if (CurrentSelectedFace == null)
            return;

        Cube newCube = spawner.GetCurrentCube();

        if (_currentSelectedFace != null && newCube != null)
        {
            newCube.transform.position = _currentSelectedFace.GetSpawnPosition().position;
        }

        OnFaceUnselected();

        EventsManager.control.CubeAdded(newCube);
        
        NextTurn();
    }

    public FaceData GetFaceData(FaceTypes type)
    {
        return facesData.GetStats(type);
    }

}

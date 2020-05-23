using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager control;

    [SerializeField] private LevelsSO _levelSystem;
    [SerializeField] private FaceDataSO _facesData;
    [SerializeField] private CityStatistics _cityStatistics;

    /// <summary>
    /// The current level running on the scene.
    /// </summary>
    private Level _currentLevel;

    private CubeSpawner _spawner;

    private bool _isFinishPlaying;

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

        _spawner = GetComponentInChildren<CubeSpawner>();
    }

    private void Start()
    {
        EventsManager.control.onfaceSelected += OnFaceSelectedEvent;
        EventsManager.control.onFaceUnselected += OnFaceUnselectedEvent;
        EventsManager.control.onCreateButtonPressed += Build;

        InitializeLevel();
    }

    private void OnDestroy()
    {
        EventsManager.control.onfaceSelected -= OnFaceSelectedEvent;
        EventsManager.control.onFaceUnselected -= OnFaceUnselectedEvent;
        EventsManager.control.onCreateButtonPressed -= Build;
    }

    public void InitializeLevel()
    {
        SetCurrentLevelPresets();

        BuildInitialCube();
    }

    /// <summary>
    /// 
    /// </summary>
    public void SetCurrentLevelPresets()
    {
        // TODO: Aca deberian setearse todas las cosas importantes del nivel, como seteos de dificultad, objetivos iluminacion todo todo.
        _currentLevel = _levelSystem.GetCurrentLevel();

        // Creo que es super necesario inicializar este objeto.
        _cityStatistics = new CityStatistics();
    }

    /// <summary>
    /// What happens when the EventsManager calls this Event.
    /// </summary>
    /// <param name="selectedFace"></param>
    private void OnFaceSelectedEvent(Face selectedFace)
    {
        CurrentSelectedFace = selectedFace;
    }

    /// <summary>
    /// What happens when the EventsManager calls this Event.
    /// </summary>
    /// <param name="selectedFace"></param>
    private void OnFaceUnselectedEvent()
    {
        CurrentSelectedFace = null;
    }

    /// <summary>
    /// Creates the initial cube, with a city hall face looking upwards.
    /// </summary>
    public void BuildInitialCube()
    {
        CubeBehaviour initialCube;
        initialCube = _spawner.GetInitialCube();
        initialCube.transform.position = Vector3.zero;
        
        NextTurn();
    }

    private void NextTurn()
    {
        WinOrLoss();

        if (!_isFinishPlaying)
        {
            PreBuild();
        }
    }

    public void WinOrLoss()
    {
        LevelObjective[] objetives = _currentLevel.GetObjectives();
        bool hasWin = false;

        for (int i = 0; i < objetives.Length; i++)
        {
            switch (objetives[i].GetObjectiveType())
            {
                case LevelObjetiveTypes.Resource:
                    hasWin = ConditionComparator.CompareConditions(_cityStatistics.GetResourceAmount(objetives[i].GetResoruceType()), objetives[i].GetResourceValue(), objetives[i].GetCondition());
                    break;
                default:
                    break;
            }
        }
        Debug.Log("The condition hasWin is: " + hasWin);
    }

    public void PreBuild()
    {
        _spawner.NextCube();
    }

    public void Build()
    {
        if (CurrentSelectedFace == null)
            return;

        CubeBehaviour newCube = _spawner.GetCurrentCube();

        if (_currentSelectedFace != null && newCube != null)
        {
            MoveBuildedCube(newCube);
        }

        OnFaceUnselectedEvent();
    }

    public void OnBuildFinish()
    {
        EventsManager.control.CubeBuilded(_spawner.GetCurrentCube());
        UpdateFaceStatistics();

        CubeAmount++;

        NextTurn();
    }

    public FaceData GetFaceData(FaceTypes type)
    {
        return _facesData.GetStats(type);
    }

    private void MoveBuildedCube(CubeBehaviour buildedCube)
    {
        Action callBack = OnBuildFinish;
        buildedCube.Move(_currentSelectedFace.GetSpawnPositions(), callBack);
    }

    private void UpdateFaceStatistics()
    {
        _cityStatistics.CalculateStatistics(_spawner.GetCurrentCube().GetFacesData());
    }

}

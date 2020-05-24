using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] bool[] completedObjectives;

    public static LevelManager control;

    [SerializeField] private LevelsSO _levelSystem;
    [SerializeField] private FaceDataSO _facesData;
    [SerializeField] private LevelStatistics _levelStatistics;

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

    private void Update()
    {
        
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

        // Resetear el ScriptableObject a sus valores por defecto.
        _levelStatistics.Reset();

        SetLevelConstraints();
    }

    public void SetLevelConstraints()
    {
        if (_currentLevel.HasConstraints())
        {
            LevelConstraints[] auxConstraints = _currentLevel.GetLevelConstraints();

            for (int i = 0; i < auxConstraints.Length; i++)
            {
                switch (auxConstraints[i].Type)
                {
                    case ConstraintTypes.CubeAmount:
                        Debug.Log("Oli ");
                         _levelStatistics.SetMaxCubeAmount(auxConstraints[i].GetMaxCubes());
                        break;
                    case ConstraintTypes.TimeAmount:
                        _levelStatistics.SetTimeThereshold(auxConstraints[i].GetTimeAmount());
                        break;
                    case ConstraintTypes.FaceTypeAvailable:
                        Debug.Log("There is not implementation of thes feature yet.");
                        break;
                    default:
                        break;
                }
            }
        }
    }

    /// <summary>
    /// Creates the initial cube, with a city hall face looking upwards.
    /// </summary>
    public void BuildInitialCube()
    {
        CubeBehaviour initialCube;
        initialCube = _spawner.GetInitialCube();
        initialCube.transform.position = Vector3.zero;

        if (_levelStatistics.GetTimeThreshold() > 0)
        {
            StartCoroutine(RunLevelTimeLapse());
        }

        NextTurn();
    }

    private void NextTurn()
    {
        _levelStatistics.CurrentCubeAmount++;

        UpdateFaceStatistics();

        EventsManager.control.StatisticsUpdate();

        EvaluateLevelEnding();
        WinOrLoss();

        if (!_isFinishPlaying)
        {
            PreBuild();
        }
        else
        {
            LevelEnd();
        }
    }

    public void EvaluateLevelEnding()
    {
        if (_currentLevel.HasConstraints())
        {
            Debug.Log("HasLevelEnded = " + _levelStatistics.HasLevelEnded());
            _isFinishPlaying = _levelStatistics.HasLevelEnded();

           
        }
    }

    public void WinOrLoss()
    {
        LevelObjective[] objetives = _currentLevel.GetObjectives();
        completedObjectives = new bool[objetives.Length];


        // TODO: Esto tendria que ser algo global. Hay que implementar todo este metodo.
        bool hasWin = false;

        for (int i = 0; i < objetives.Length; i++)
        {
            switch (objetives[i].GetObjectiveType())
            {
                case LevelObjetiveTypes.Resource:
                    completedObjectives[i] = ConditionComparator.CompareConditions(_levelStatistics.GetResourceAmount(objetives[i].GetResoruceType()), objetives[i].GetResourceValue(), objetives[i].GetCondition());
                    break;
                default:
                    break;
            }
            if (i == 0)
            {
                hasWin = completedObjectives[i];
            }
            else if (i > 0)
            {
                hasWin = completedObjectives[i - 1] & completedObjectives[i];
            }
        }
        Debug.Log("The condition hasWin is: " + hasWin);
    }

    public void LevelEnd()
    {
        // TODO: Hay que implementar todo este metodo. De alguna manera tiene que mostrarse en UI asi que hay que ver si se hace con un evento o algo asi. Seee esa es la que va. Re simple. Un aciton onLevel

        EventsManager.control.onCreateButtonPressed -= Build;
        Debug.Log("Level ended.");
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
        _levelStatistics.CalculateNextResourcers(_spawner.GetCurrentCube().GetFacesData());
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

    public IEnumerator RunLevelTimeLapse()
    {
        while (!_isFinishPlaying)
        {
            _levelStatistics.ElapsedTime += Time.deltaTime;
            if (_levelStatistics.ElapsedTime >= _levelStatistics.GetTimeThreshold())
            {
                _isFinishPlaying = true;
                LevelEnd();
            }
            yield return null;
        }
    }

}

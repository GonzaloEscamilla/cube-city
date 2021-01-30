using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager control;

    [SerializeField] private LevelsSO _levelSystem;
    [SerializeField] private LevelStatistics _levelStatistics;
    [SerializeField] private FaceCollisionHandler _faceCollisionHandler;
    [SerializeField] private AdjacencyBonusesSO adjacencyBonusesSO;

    [SerializeField] bool[] completedObjectives;



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

    public FaceCollisionHandler GetFaceCollidionsHandler()
    {
        return _faceCollisionHandler;
    }

    public CubeSpawner GetCubeSpawner()
    {
        return _spawner;
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
        if (!_isFinishPlaying)
        {
            PreBuild();
        }
        else
        {
            LevelEnd();
        }
    }

    public void PreBuild()
    {
        _spawner.NextCube();
    }

    /// <summary>
    /// Called when ever a new cube is virtualy builded on the current selected face. When this ends the builded cube starts moving towards the face.
    /// </summary>
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

        _levelStatistics.CurrentCubeAmount++;
    }

    /// <summary>
    /// Callback function. Its called when the new builded cube sets in its final position.
    /// </summary>
    private void OnBuildFinish()
    {
        CubeBehaviour currentCube = _spawner.GetCurrentCube();
        EventsManager.control.CubeBuilded(currentCube);

        currentCube.InitializeAdjacentFaces();
        CheckExtraPoints();

        UpdateFaceStatistics();

        EventsManager.control.StatisticsUpdate();

        EvaluateLevelEnding();
        WinOrLoss();

        NextTurn();
    }

    private void CheckExtraPoints()
    {
        // Puntos extra por bonus de adyacencia
        CheckAdjacentBonus(_spawner.GetCurrentCube());

        // Puntos extra por combos
        foreach (List<Face> combo in GetCombos(_spawner.GetCurrentCube()))
        {
            foreach (Face face in combo)
            {
                _levelStatistics.CalculateNextResources(face.GetFaceData());
                face.Upgrade();
            }
        }
    }

    private void CheckAdjacentBonus(CubeBehaviour cube)
    {
        foreach (Face face in cube.GetFaces())
        {
            foreach (Face adjacentFace in face.GetAdjacentFaces())
            {
                _levelStatistics.CalculateNextResources(
                    adjacencyBonusesSO.GetBonusForFaces(face.Type, adjacentFace.Type)
                );
            }
        }
    }

   
    List<List<Face>> GetCombos(CubeBehaviour cube)
    {
        // TODO: pasar esta constante a otro lado
        const int MIN_ELEMS_FOR_COMBO = 5;
        List<List<Face>> result = new List<List<Face>>();
        foreach (Face face in cube.GetFaces())
        {
            List<Face> group = face.GetAdjacentGroup();
            if (group.Count >= MIN_ELEMS_FOR_COMBO)
            {
                result.Add(group);
            }
        }
        return result;
    }

    private void MoveBuildedCube(CubeBehaviour buildedCube)
    {
        Action callBack = OnBuildFinish;
        buildedCube.Move(_currentSelectedFace.GetSpawnPositions(), callBack);
    }

    private void EvaluateLevelEnding()
    {
        if (_currentLevel.HasConstraints())
        {
            _isFinishPlaying = _levelStatistics.HasLevelEnded();
        }
    }

    private void WinOrLoss()
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

    private void UpdateFaceStatistics()
    {
        // This is done before the "NextTurn" so the current cube is the one that is being putted on.
        _levelStatistics.CalculateNextResources(_spawner.GetCurrentCube().GetFacesData());
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

using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

public class LevelManager : MonoBehaviour
{
    public static LevelManager control;
    [SerializeField] private GameSettingsSO _gameSettings;
    [SerializeField] private LevelsSettingsSO _levelsSettings;
    [SerializeField] private LevelStatistics _levelStatistics;
    [SerializeField] private FaceCollisionHandler _faceCollisionHandler;
    [SerializeField] private AdjacencyBonusesSO _adjacencyBonusesSO;

    [SerializeField] bool _mainObjectiveCompleted;
    [SerializeField] bool[] _secondaryObjectivesCompleted;

    [SerializeField] private List<CubeBehaviour> _cubesBuilded = new List<CubeBehaviour>();


    /// <summary>
    /// The current level running on the scene.
    /// </summary>
    private Level _currentLevel;

    private CubeSpawner _spawner;

    private bool _isFinishPlaying = false;
    private bool _hasWin = false;
    private bool _cubeIsMoving = false;
    private bool _isPaused = false;

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

    public GameSettingsSO GameSettings
    {
        get
        {
            return _gameSettings;
        }
    }
   

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
        _cubeIsMoving = false;
        SetCurrentLevelPresets();

        BuildInitialCube();
    }

    /// <summary>
    /// 
    /// </summary>
    public void SetCurrentLevelPresets()
    {
        // TODO: Aca deberian setearse todas las cosas importantes del nivel, como seteos de dificultad, objetivos iluminacion todo todo.
        _currentLevel = _levelsSettings.GetCurrentLevel();

        _mainObjectiveCompleted = false;
        _secondaryObjectivesCompleted = new bool[_currentLevel.GetSecondaryObjetives().Length];

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
                        Debug.Log("Seting Cube Amount for this level. ");
                         _levelStatistics.SetMaxCubeAmount(auxConstraints[i].GetMaxCubes());
                        break;
                    case ConstraintTypes.TimeAmount:
                        Debug.Log("Seting Time Threshold for the current level.");
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

        OnBuildFinish();
    }

    private void NextTurn()
    {
        if (!_isFinishPlaying && _spawner.AvailableCubeExists())
        {
            PreBuild();
        }
        else
        {
            LevelEnd(_hasWin);
        }
    }

    public void PreBuild()
    {
        _spawner.NextCube();
    }

    public void LevelEnd(bool hasWin)
    {
        _isFinishPlaying = true;
        EventsManager.control.onCreateButtonPressed -= Build;

        // TODO: completar campos del struct
        LevelEndData data = new LevelEndData();
        data.success = _hasWin;
        data.finalResources = _levelStatistics.GetResources();
        data.timeSpent = _levelStatistics.ElapsedTime;

        EventsManager.control.EndLevel(data);

        Debug.Log("Level ended." + " you have: " + _hasWin);
    }

    public bool HasLevelEnded()
    {
        return _isFinishPlaying;
    }

    public bool IsCubeMoving()
    {
        return _cubeIsMoving;
    }

    public LevelsSettingsSO GetLevelSystem()
    {
        return _levelsSettings;
    }
    /// <summary>
    /// Called when ever a new cube is virtualy builded on the current selected face. When this ends the builded cube starts moving towards the face.
    /// </summary>
    public void Build()
    {
        if (CurrentSelectedFace == null)
            return;

        _cubeIsMoving = true;

        CubeBehaviour newCube = _spawner.GetCurrentCube();

        if (_currentSelectedFace != null && newCube != null)
        {
            MoveBuildedCube(newCube);
        }

        OnFaceUnselectedEvent();

        _levelStatistics.CurrentCubeAmount++;
    }

    public List<CubeBehaviour> GetCubesBuilded()
    {
        return _cubesBuilded;
    }

    /// <summary>
    /// Callback function. Its called when the new builded cube sets in its final position.
    /// </summary>
    private void OnBuildFinish()
    {
        SoundManager.Instance.PlayOneShoot(CubeSound.CubePlaced.ToString());

        _cubeIsMoving = false;
        CubeBehaviour currentCube = _spawner.GetCurrentCube();

        SubstractOverlappedFacesPoints();

        EventsManager.control.CubeBuilded(currentCube);

        currentCube.InitializeAdjacentFaces();

        if (!_cubesBuilded.Contains(currentCube))
            _cubesBuilded.Add(currentCube);

        CheckExtraPoints();

        UpdateFaceStatistics();

        EventsManager.control.StatisticsUpdate();

        CheckSecondaryObjectives();

        EvaluateLevelEnding();
        _hasWin = WinOrLoss();

        NextTurn();
    }

   

    private void SubstractOverlappedFacesPoints() 
    {
        List<Face> affectedFaces = _faceCollisionHandler.GetAffectedFaces();
        Face[] cubeFaces = _spawner.GetCurrentCube().GetFaces();

        foreach (Face affectedFace in affectedFaces)
        {
            if (!cubeFaces.Contains<Face>(affectedFace))
            {
                _levelStatistics.CalculateNextResources(-affectedFace.GetFaceData());
                foreach (Face adjacentFace in affectedFace.GetAdjacentFaces())
                {
                    Debug.Log("AdjacentFace Type: " + adjacentFace.Type);
                    _levelStatistics.CalculateNextResources(-_adjacencyBonusesSO.GetBonusForFaces(affectedFace.Type, adjacentFace.Type));
                }
            }
        }
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
                    _adjacencyBonusesSO.GetBonusForFaces(face.Type, adjacentFace.Type)
                );
            }
        }
    }

    private List<List<Face>> GetCombos(CubeBehaviour cube)
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

    private bool WinOrLoss()
    {
        // TODO: Esto tendria que ser algo global. Hay que implementar todo este metodo.
        bool hasWin = false;

        _mainObjectiveCompleted = ConditionComparator.CompareConditions(_levelStatistics.GetResourceAmount(_currentLevel.GetMainObjective().GetResourceType()),
                                                                                _currentLevel.GetMainObjective().GetResourceValue(),
                                                                                _currentLevel.GetMainObjective().GetCondition());
        hasWin = _mainObjectiveCompleted;

        if (hasWin)
            _isFinishPlaying = true;

        return hasWin;
    }

    private void CheckSecondaryObjectives()
    {
        LevelSecondaryObjective[] secondaryObjectives = _currentLevel.GetSecondaryObjetives();

        for (int i = 0; i < _secondaryObjectivesCompleted.Length; i++)
        {
            _secondaryObjectivesCompleted[i] = ConditionComparator.CompareConditions(_levelStatistics.GetResourceAmount(secondaryObjectives[i].GetResourceType()),
                                                                                secondaryObjectives[i].GetResourceValue(),
                                                                                secondaryObjectives[i].GetCondition());
        }
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
        //CurrentSelectedFace = null;
    }

    public IEnumerator RunLevelTimeLapse()
    {
        while (!_isFinishPlaying && !_isPaused)
        {
            _levelStatistics.ElapsedTime += Time.deltaTime;
            if (_levelStatistics.ElapsedTime >= _levelStatistics.GetTimeThreshold())
            {
                LevelEnd(false);
            }
            yield return null;
        }
    }

}

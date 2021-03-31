using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;
using TMPro;

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
    private bool _firstCubeBuild = true;

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
        EventsManager.Instance.onfaceSelected += OnFaceSelectedEvent;
        EventsManager.Instance.onFaceUnselected += OnFaceUnselectedEvent;
        EventsManager.Instance.onCreateButtonPressed += Build;

        InitializeLevel();
    }

    private void OnDestroy()
    {
        if (EventsManager.Instance != null)
        {
            EventsManager.Instance.onfaceSelected -= OnFaceSelectedEvent;
            EventsManager.Instance.onFaceUnselected -= OnFaceUnselectedEvent;
            EventsManager.Instance.onCreateButtonPressed -= Build;
        }
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

        //BuildInitialCube();
        StartCoroutine(InitializingLevel());
    }

    private IEnumerator InitializingLevel()
    {
        yield return new WaitForEndOfFrame();
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

        if(_currentLevel.GetTutorial() != null)
            TutorialManager.Instance.SetTutorials(_currentLevel, _levelStatistics);

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
        Debug.Log("Build Initial Cube");

        CubeBehaviour initialCube;
        initialCube = _spawner.GetInitialCube();
        initialCube.transform.position = Vector3.zero;

        if (_levelStatistics.GetTimeThreshold() > 0)
        {
            StartCoroutine(RunLevelTimeLapse());
        }

        OnBuildFinish();

        _firstCubeBuild = false;

        Debug.Log("On Build FInish");
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
        EventsManager.Instance.onCreateButtonPressed -= Build;

        // TODO: completar campos del struct
        LevelEndData data = new LevelEndData();
        data.levelNumber = _currentLevel.LevelNumber;
        data.success = _hasWin;
        data.finalResources = _levelStatistics.GetResources();
        data.timeSpent = _levelStatistics.ElapsedTime;
        data.secondaryObjectives = new bool[_secondaryObjectivesCompleted.Length];

        for (int i = 0; i < data.secondaryObjectives.Length; i++)
            data.secondaryObjectives[i] = _secondaryObjectivesCompleted[i];

        if (Player.Instance.MaxProsperityMade < data.finalResources.GetResourceByType(ResourceTypes.Prosperity))
            Player.Instance.MaxProsperityMade = data.finalResources.GetResourceByType(ResourceTypes.Prosperity);

        Player.Instance.MaxProsperityMade = Player.Instance.MaxProsperityMade;

        EventsManager.Instance.EndLevel(data);

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
    public void OnBuildFinish() // No se hace pero bue, lo de poner publico este metodo. es para probar el DOTween
    {
        Debug.LogWarning("OnBuild Finish", this.gameObject);

        if (!_firstCubeBuild)
            SoundManager.Instance.PlayOneShoot(CubeSound.CubePlaced.ToString());

        _cubeIsMoving = false;
        CubeBehaviour currentCube = _spawner.GetCurrentCube();

        if (!_cubesBuilded.Contains(currentCube))
            _cubesBuilded.Add(currentCube);

        SubstractOverlappedFacesPoints();

        EventsManager.Instance.CubeBuilded(currentCube);

        currentCube.InitializeAdjacentFaces();

        CheckExtraPoints();

        UpdateFaceStatistics();

        EventsManager.Instance.StatisticsUpdate();

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
                _levelStatistics.CalculateNextResources(_adjacencyBonusesSO.GetBonusForFaces(face.Type, adjacentFace.Type));
            }
        }
    }

    private List<List<Face>> GetCombos(CubeBehaviour cube)
    {
        List<List<Face>> result = new List<List<Face>>();
        foreach (Face face in cube.GetFaces())
        {
            List<Face> group = face.GetAdjacentGroup();
            if (group.Count >= _gameSettings.MIN_ELEMS_FOR_COMBO)
            {
                result.Add(group);
                _levelStatistics.AmountOfCombosMade++;
                EventsManager.Instance.ComboMade();
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
                                                                                _currentLevel.GetMainObjective().GetObjetiveValue(),
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
            switch (secondaryObjectives[i].GetObjectiveType())
            {
                case LevelObjetiveTypes.Resource:
                    _secondaryObjectivesCompleted[i] = ConditionComparator.CompareConditions(_levelStatistics.GetResourceAmount(secondaryObjectives[i].GetResourceType()),
                                                                                        secondaryObjectives[i].GetObjetiveValue(),
                                                                                        secondaryObjectives[i].GetCondition());
                    break;
                case LevelObjetiveTypes.Combo:
                    _secondaryObjectivesCompleted[i] = ConditionComparator.CompareConditions(_levelStatistics.AmountOfCombosMade,
                                                                                        secondaryObjectives[i].GetObjetiveValue(),
                                                                                        secondaryObjectives[i].GetCondition());
                    break;
                default:
                    break;
            }
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

    public LevelStatistics GetLevelStatistics()
    {
        return _levelStatistics;
    }

    public Level GetCurrentLevel()
    {
        return _currentLevel;
    }

    public AdjacencyBonusesSO GetAdjacencyBounisesSO()
    {
        return _adjacencyBonusesSO;
    }
}

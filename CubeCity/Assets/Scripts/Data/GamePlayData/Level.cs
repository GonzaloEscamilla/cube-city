using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewLevel", menuName = "ScriptableObjects/LevelSystem/Level", order = 2)]
public class Level : ScriptableObject
{
    [Header("Settings")]

    public string Name;
    public int LevelNumber;

    [SerializeField] private TutorialSO tutorial;
    [SerializeField] private List<LevelConstraints> _levelConstraints = new List<LevelConstraints>();
    [SerializeField] private LevelObjective _mainObjective;
    [SerializeField] private List<LevelSecondaryObjective> _secondaryObjetives = new List<LevelSecondaryObjective>();
    [SerializeField] private string[] secondaryObjetivesNames;

    static FaceTypes[] faceTypes = (FaceTypes[])Enum.GetValues(typeof(FaceTypes));
    [SerializeField] private FacesDistribution _facesDistribution = new FacesDistribution();
    [SerializeField] private int amountOfCubes;

    [Header("Audio Settings")]

    [SerializeField] private SoundsDefinition.LevelClip levelSound;

    [Header("PowerUp Settings")]

    [SerializeField] private int amountOfExtraCubes = 3;


    private void OnValidate()
    {
        amountOfCubes = _facesDistribution.GetTotalRemainingFaces() / 6;
        foreach (LevelConstraints constrait in _levelConstraints)
        {
            constrait.Type = constrait.Type;
        }

        amountOfExtraCubes = 3;
    }

    public int GetExtraCubes()
    {
        return amountOfExtraCubes;
    }

    public int GetCubesAmount()
    {
        return amountOfCubes;
    }

    /// <summary>
    /// Return all the levels objetives.
    /// </summary>
    /// <returns></returns>
    public LevelObjective GetMainObjective()
    {
        if (_mainObjective != null)
            return _mainObjective;

        Debug.LogWarning("There is not an objetive assigned to this level yet. Please ensure that a level objetive is assigned for proper functionality");
        return null;
    }

    public LevelSecondaryObjective[] GetSecondaryObjetives()
    {
        if (_secondaryObjetives.Count > 0)
            return _secondaryObjetives.ToArray();
        else
        {
            Debug.LogError("The level don't have secondary objetives.");
            return null;
        }
    }

    public int GetResourceAmountByType(ResourceTypes type)
    {
        foreach (LevelSecondaryObjective secondaryObjective in _secondaryObjetives)
        {
            if (secondaryObjective.GetResourceType() == type)
            {
                return secondaryObjective.GetObjetiveValue();
            }
        }
        return -1;
    }

    public bool HasConstraints()
    {
        return _levelConstraints.Count != 0;
    }

    public LevelConstraints[] GetLevelConstraints()
    {
        if (_levelConstraints.Count != 0)
        {
            return _levelConstraints.ToArray();
        }
        else
        {
            Debug.Log("Hey, the level dosn't have any constraints.");
            return null;
        }
    }

    public FacesDistribution GetFacesDistribution()
    {
        return _facesDistribution;
    }

    public SoundsDefinition.LevelClip GetLevelClipSound()
    {
        return levelSound;
    }

    public TutorialSO GetTutorial()
    {
        return tutorial;
    }

    public void SetSecondaryObjectivesNames(string[] names)
    {
        secondaryObjetivesNames = new string[names.Length];

        for (int i = 0; i < secondaryObjetivesNames.Length; i++)
            secondaryObjetivesNames[i] = names[i];
    }

    public string[] GetSecondaryObjectivesNames()
    {
        return secondaryObjetivesNames;
    }
}

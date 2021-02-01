using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "NewLevel", menuName = "ScriptableObjects/LevelSystem/Level", order = 2)]
public class Level : ScriptableObject
{
    public string Name;

    [SerializeField] private List<LevelConstraints> _levelConstraints = new List<LevelConstraints>(); 
    [SerializeField] private List<LevelObjective> _objectives = new List<LevelObjective>();
    
    static FaceTypes[] faceTypes = (FaceTypes[]) Enum.GetValues(typeof(FaceTypes));
    [SerializeField] FacesDistribution _facesDistribution = new FacesDistribution();


    private void OnValidate()
    {
        foreach (LevelConstraints constrait in _levelConstraints)
        {
            constrait.Type = constrait.Type;
        }
    }

    /// <summary>
    /// Return all the levels objetives.
    /// </summary>
    /// <returns></returns>
    public LevelObjective[] GetObjectives()
    {
        if(_objectives.Count > 0)
            return _objectives.ToArray();
        else
        {
            Debug.LogError("The level should have at lest on objetive.");
            return null;
        }
    }

    public bool HasConstraints()
    {
        return _levelConstraints != null;
    }

    public LevelConstraints[] GetLevelConstraints()
    {
        if (_levelConstraints != null)
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
}

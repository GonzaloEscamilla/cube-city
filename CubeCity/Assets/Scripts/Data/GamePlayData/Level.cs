using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevel", menuName = "ScriptableObjects/LevelSystem/Level", order = 2)]
public class Level: ScriptableObject
{
    public string Name;
    
    [SerializeField] private  int _maxCubeAmount;
    [SerializeField] private  List<LevelObjective> _objectives = new List<LevelObjective>();

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

    /// <summary>
    /// Gets the maximum amount of cubes you can put in the Level.
    /// </summary>
    /// <returns></returns>
    public int GetMaxCubes()
    {
        return _maxCubeAmount;
    }
}

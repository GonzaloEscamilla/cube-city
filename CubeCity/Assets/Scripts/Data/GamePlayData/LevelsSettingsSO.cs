using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelsSettings", menuName = "ScriptableObjects/LevelSystem/LevelsSettings", order = 0)]
public class LevelsSettingsSO : ScriptableObject
{
    [SerializeField] private Level _currentLevel;
    
    [Space(10)]

    public List<World> _worlds = new List<World>();

    public World GetWorld(int worldIndex)
    {
        if (_worlds.Count > worldIndex)
            return _worlds[worldIndex];
        else
        {
            Debug.LogError("That World does not exist");
            return null;
        }
    }

    public Level GetCurrentLevel()
    {
        if (_currentLevel != null)
        {
            return _currentLevel;
        }
        else
        {
            Debug.LogWarning("Hey theres not an assigend level yet to load. Please Check if there is a miss level reference somewhere.");
            return null;
        }
    }

    /// <summary>
    /// Sets the next level to load. This should be used from the select level menu.
    /// </summary>
    /// <param name="newLevel"></param>
    public void SetCurrentLevel(Level newLevel)
    {
        _currentLevel = newLevel;
    }
}

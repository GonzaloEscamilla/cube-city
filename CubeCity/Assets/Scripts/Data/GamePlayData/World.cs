using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewWorld", menuName = "ScriptableObjects/LevelSystem/World", order = 1)]
public class World: ScriptableObject
{
    public string Name;
    
    [SerializeField] private List<Level> _levels = new List<Level>();

    public Level GetLevel(int levelIndex)
    {
        if (_levels.Count > levelIndex)
            return _levels[levelIndex];
        else
        {
            Debug.LogError("That level does not exist");
            return null;
        }
    }
}

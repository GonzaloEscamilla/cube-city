using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettings", order = 1)]
public class GameSettingsSO : ScriptableObject
{
    public int MIN_ELEMS_FOR_COMBO = 3;
    public bool EditorMode;
    public float loadingTime;

    private bool alreadyInitialized = false;
    
    public void SetInitialization()
    {
        alreadyInitialized = true;
    }
    public bool IsInitialized()
    {
        return alreadyInitialized;
    }

    [ContextMenu("ResetPlayerPrefs")]
    public void ResetPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }

}

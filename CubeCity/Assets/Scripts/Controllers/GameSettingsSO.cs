using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameSettings", menuName = "ScriptableObjects/GameSettings", order = 1)]
public class GameSettingsSO : ScriptableObject
{
    public int MIN_ELEMS_FOR_COMBO = 5;
    public bool EditorMode;
    public float loadingTime;
}

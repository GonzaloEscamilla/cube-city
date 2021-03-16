using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct SaveData
{
    //Generic Data or specific structs.

    public List<levelData> levelDatas;
}

[System.Serializable]
public struct levelData
{
    public int levelNumber;
    public int starsAmount;
    public int levelScore;
}
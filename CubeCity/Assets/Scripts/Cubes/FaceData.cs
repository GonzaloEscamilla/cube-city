using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FaceData
{
    [HideInInspector] public string name;

    [Header("Stats")]
    public int _prosperity;
    public int _population;
    public int _pullution;
    public int _productivity;
    public int _sustainability;
    public int _happiness;
    public int _consumption;
    public int _technology;
    public int _knowledge;
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FaceData
{
    [HideInInspector] public string name;

    //TODO: reutilizar ResourceTypes
    [Header("Stats")]
    public int _prosperity;
    public int _happiness;
    public int _sustainability;
    public int _wealth;

    public static FaceData operator +(FaceData a, FaceData b)
    {
        FaceData result = new FaceData();
        result._prosperity = a._prosperity + b._prosperity;
        result._happiness = a._happiness + b._happiness;
        result._sustainability = a._sustainability + b._sustainability;
        result._wealth = a._wealth + b._wealth;

        return result;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FaceData
{
    [HideInInspector] public string name;

    [Header("Stats")]
    public int _prosperity; // Only special faces should give this "resource".
    public int _population;
    public int _pullution;
    public int _productivity;
    public int _sustainability;
    public int _happiness;
    public int _consumption;
    public int _technology;
    public int _knowledge;

    public static FaceData operator +(FaceData a, FaceData b)
    {
        FaceData result = new FaceData();
        result._prosperity = a._prosperity + b._prosperity;
        result._population = a._population + b._population;
        result._pullution = a._pullution + b._pullution;
        result._productivity = a._productivity + b._productivity;
        result._productivity = a._productivity + b._productivity;
        result._happiness = a._happiness + b._happiness;
        result._consumption = a._consumption + b._consumption;
        result._technology = a._technology + b._technology;
        result._knowledge = a._knowledge + b._knowledge;
        return result;
    }
}

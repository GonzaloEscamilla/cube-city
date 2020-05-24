using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

[System.Serializable]
public class LevelConstraints
{
    [HideInInspector] public string Name;

    [SerializeField] public ConstraintTypes Type
    {
        set
        {
            _type = value;
            Name = _type.ToString();
        }
        get
        {
            return _type;
        }
    }
    [SerializeField] private ConstraintTypes _type;

    [SerializeField] private int _maxCubeAmount;
    [SerializeField] private int _timeAmount;

    // TODO: Esto posiblemtne tenga que ser una lista o arreglo con las caras machadas con una probabilidad. Luego ponerlo como serializable.
    private FaceTypes _availableFaces;

    /// <summary>
    /// Gets the maximum amount of cubes you can put in the Level.
    /// </summary>
    /// <returns></returns>
    public int GetMaxCubes()
    {
        return _maxCubeAmount;
    }

    public bool HasTime()
    {
        return _timeAmount > 0;
    }

    public int GetTimeAmount()
    {
        return _timeAmount;
    }
}

public enum ConstraintTypes
{
    CubeAmount,
    TimeAmount,
    FaceTypeAvailable
}
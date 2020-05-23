using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelObjective 
{
    [SerializeField] private LevelObjetiveTypes _type;
    [SerializeField] private ResourceTypes _resourceType;
    [SerializeField] private Comparator _condition;
    [SerializeField] private int _resourceValue;

    /// <summary>
    /// Returns the ObjetiveType of this objetive.
    /// </summary>
    /// <returns>The objetive type.</returns>
    public LevelObjetiveTypes GetObjectiveType()
    {
        return _type;
    }

    /// <summary>
    /// Gets the resource type of the objetive.
    /// </summary>
    /// <returns></returns>
    public ResourceTypes GetResoruceType()
    {
        return _resourceType;
    }

    /// <summary>
    /// Get the condition comparator of the objetive.
    /// </summary>
    /// <returns></returns>
    public Comparator GetCondition()
    {
        return _condition;
    }

    /// <summary>
    /// Gets the value of the resource to be compared.
    /// </summary>
    /// <returns></returns>
    public int GetResourceValue()
    {
        return _resourceValue;
    }
}

public enum Comparator
{
    GreaterThan,
    GreatherOrEqualTo,
    LesserThan,
    LesserOrEqualTo,
    EqualsTo
}

public enum LevelObjetiveTypes
{
    Resource,
}
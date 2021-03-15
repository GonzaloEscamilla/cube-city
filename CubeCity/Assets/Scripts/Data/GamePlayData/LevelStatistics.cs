using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CityStatistics", menuName = "ScriptableObjects/CityStatistics", order = 0)]
public class LevelStatistics: ScriptableObject
{
    [Header("Status")]
    [SerializeField] private int _maxCubeAmount;

    public int CurrentCubeAmount
    {
        set
        {
            _currentCubeAmount = value;
        }
        get
        {
            return _currentCubeAmount;
        }
    }
    [SerializeField] private int _currentCubeAmount;

    [SerializeField] private int _timeThreshold;

    public float ElapsedTime
    {
        set
        {
            _elapsedTime = value;
        }
        get
        {
            return _elapsedTime;
        }
    }
    [SerializeField] private float _elapsedTime;

    [Header("Resources")]
    [SerializeField] private Resources _resources = new Resources();

    public int AmountOfCombosMade
    {
        get
        {
            return _amountOfCombosMade;
        }
        set
        {
            _amountOfCombosMade = value;
        }
    }
    [SerializeField] private int _amountOfCombosMade;

    public LevelStatistics(Resources[] data)
    {
        CalculateNextResources(data);
    }

    [ContextMenu("ResetToInitial")]
    public void Reset()
    {
        _maxCubeAmount = 0;
        CurrentCubeAmount = 0;
        
        _timeThreshold = 0;
        ElapsedTime = 0;

        _resources = new Resources();
        _amountOfCombosMade = 0;
    }

    public void CalculateNextResources(Resources data)
    {
        _resources += data;
    }

    public void CalculateNextResources(Resources[] data)
    {
        for (int i = 0; i < data.Length; i++)
        {
            _resources += data[i];
        }
    }

    public bool HasLevelEnded()
    {
        bool noMoreCubes = true;
        bool timeEnded = true;

        if (_maxCubeAmount > 0)
        {
            noMoreCubes = CurrentCubeAmount >= _maxCubeAmount;
        }
        else
            return false;
        if (_timeThreshold > 0)
        {
            timeEnded = ElapsedTime >= _timeThreshold;
        }
        else
            return false;

        return (noMoreCubes && timeEnded);
    }

    public Resources GetResources()
    {
        return _resources;
    }

    /// <summary>
    /// Return the current amount of a resource in the City.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public int GetResourceAmount(ResourceTypes type)
    {
        return _resources.GetResourceType(type);
    }

    public void SetMaxCubeAmount(int amount)
    {
        _maxCubeAmount = amount;
    }

    public float GetTimeThreshold()
    {
        return _timeThreshold;
    }

    public void SetTimeThereshold(int amount)
    {
        _timeThreshold = amount;
    }
}

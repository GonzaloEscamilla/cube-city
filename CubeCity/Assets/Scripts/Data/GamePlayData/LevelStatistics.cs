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
    [SerializeField] private FaceData _faceData = new FaceData();

    [Space(5)]
    [SerializeField] private int _civicCenter = 0;
    [SerializeField] private int _housingArea = 0;
    [SerializeField] private int _factory = 0;
    [SerializeField] private int _park = 0;
    [SerializeField] private int _shoppingArea = 0;
    [SerializeField] private int _dump = 0;
    [SerializeField] private int _agriculturalArea = 0;
    [SerializeField] private int _college = 0;
    [SerializeField] private int _solarPowerPlant = 0;
    [SerializeField] private int _meteorite = 0;
    [SerializeField] private int _naturalReserve = 0;
    [SerializeField] private int _radioactiveZone = 0;
    [SerializeField] private int _UFO = 0;
    [SerializeField] private int _metropolis = 0;
    [SerializeField] private int _spaceCompany = 0;

    public LevelStatistics(FaceData[] data)
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

        _faceData = new FaceData();

        _civicCenter = 0;
        _housingArea = 0;
        _factory = 0;
        _park = 0;
        _shoppingArea = 0;
        _dump = 0;
        _agriculturalArea = 0;
        _college = 0;
        _solarPowerPlant = 0;
        _meteorite = 0;
        _naturalReserve = 0;
        _radioactiveZone = 0;
        _UFO = 0;
        _metropolis = 0;
        _spaceCompany = 0;
    }

    public void CalculateNextResources(FaceData data)
    {
        _faceData += data;
    }

    public void CalculateNextResources(FaceData[] data)
    {
        for (int i = 0; i < data.Length; i++)
        {
            _faceData += data[i];
        }
    }

    public bool HasLevelEnded()
    {
        bool noMoreCubes = true;
        bool timeEnded = true;

        if (_maxCubeAmount > 0)
        {
            noMoreCubes = CurrentCubeAmount >= _maxCubeAmount;
            Debug.Log("No More Cubes : " + noMoreCubes);
        }
        else
            return false;
        if (_timeThreshold > 0)
        {
            Debug.Log("Time");
            timeEnded = ElapsedTime >= _timeThreshold;
        }
        else
            return false;

        return (noMoreCubes && timeEnded);
    }

    /// <summary>
    /// Return the current amount of a resource in the City.
    /// </summary>
    /// <param name="type"></param>
    /// <returns></returns>
    public int GetResourceAmount(ResourceTypes type)
    {
        switch (type)
        {
            case ResourceTypes.Prosperity:
                return _faceData._prosperity;
            case ResourceTypes.Happiness:
                return _faceData._happiness;
            case ResourceTypes.Sustainability:
                return _faceData._sustainability;
            case ResourceTypes.Wealth:
                return _faceData._wealth;

            default:
                return 0;
        }
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

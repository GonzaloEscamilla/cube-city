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
    [SerializeField] private int _totalProsperity = 0; // Only special faces should give this "resource".
    [SerializeField] private int _prosperityModifier = 0;
    
    [Space(5)]

    [SerializeField] private int _totalPopulation = 0;
    [SerializeField] private int _totalPullution = 0;
    [SerializeField] private int _totalProductivity = 0;
    [SerializeField] private int _totalSustainability = 0;
    [SerializeField] private int _totalHappiness = 0;
    [SerializeField] private int _totalConsumption = 0;
    [SerializeField] private int _totalTechnology = 0;
    [SerializeField] private int _totalKnowledge = 0;

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
        CalculateNextResourcers(data);
    }

    [ContextMenu("ResetToInitial")]
    public void Reset()
    {
        _maxCubeAmount = 0;
        CurrentCubeAmount = 0;
        
        _timeThreshold = 0;
        ElapsedTime = 0;


        _totalProsperity = 0;
        _prosperityModifier = 0;
        _totalPopulation = 0;
        _totalPullution = 0;
        _totalProductivity = 0;
        _totalSustainability = 0;
        _totalHappiness = 0;
        _totalConsumption = 0;
        _totalTechnology = 0;
        _totalKnowledge = 0;

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

    public void CalculateNextResourcers(FaceData[] data)
    {
        for (int i = 0; i < data.Length; i++)
        {
            _prosperityModifier += data[i]._prosperity;
            _totalPopulation += data[i]._population;
            _totalPullution += data[i]._pullution;
            _totalProductivity += data[i]._productivity;
            _totalSustainability += data[i]._sustainability;
            _totalHappiness += data[i]._happiness;
            _totalConsumption += data[i]._consumption;
            _totalTechnology += data[i]._technology;
            _totalKnowledge += data[i]._knowledge;
        }
        CalculateProsperity();
    }

    private void CalculateProsperity()
    {
        int firstEquation = _totalPopulation * _totalProductivity;
        int secondEquation = _totalPullution / 20;
        int thirdEquation = (_totalConsumption + _totalHappiness) / 4;
        int fourthEquation = (_totalTechnology * _totalKnowledge) / 2;

        if (secondEquation == 0)
            secondEquation = 1;

        _totalProsperity = (firstEquation / secondEquation) + thirdEquation + fourthEquation; 
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
                return _totalProsperity;
            case ResourceTypes.Population:
                return _totalPopulation;
            case ResourceTypes.Pullution:
                return _totalPullution;
            case ResourceTypes.Productivity:
                return _totalProductivity;
            case ResourceTypes.Sustainability:
                return _totalSustainability;
            case ResourceTypes.Happiness:
                return _totalHappiness;
            case ResourceTypes.Consumption:
                return _totalConsumption;
            case ResourceTypes.Technology:
                return _totalTechnology;
            case ResourceTypes.Knowledge:
                return _totalKnowledge;
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

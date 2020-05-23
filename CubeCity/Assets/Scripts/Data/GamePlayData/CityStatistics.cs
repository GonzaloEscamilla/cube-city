using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CityStatistics
{
    [Header("Stats")]
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

    public CityStatistics(FaceData[] data)
    {
        CalculateStatistics(data);
    }

    public CityStatistics()
    {
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
    }

    public void CalculateStatistics(FaceData[] data)
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
}

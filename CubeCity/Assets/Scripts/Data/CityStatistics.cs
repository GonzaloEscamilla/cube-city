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
}

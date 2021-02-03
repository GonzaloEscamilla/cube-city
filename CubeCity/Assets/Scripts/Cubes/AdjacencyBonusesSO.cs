using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Adjacency", menuName = "ScriptableObjects/AdjacencyBonus", order = 1)]

public class AdjacencyBonusesSO : ScriptableObject
{
    [System.Serializable]
    public struct BonusTuple {
        [SerializeField] private string Name;
        [SerializeField] public FaceTypes type1;
        [SerializeField] public FaceTypes type2;
        [SerializeField] public Resources bonusData;
    }
    
    [SerializeField]
    private BonusTuple[] _bonusTuples;

    public Resources GetBonusForFaces(FaceTypes type1, FaceTypes type2)
    {
        foreach (BonusTuple bonus in _bonusTuples)
        {
            if ((bonus.type1 == type1 && bonus.type2 == type2) ||
                (bonus.type1 == type2 && bonus.type2 == type1))
            {
                return bonus.bonusData;
            }
        }
        return new Resources();
    }

    #region UNITY CALLBACKS
    private void OnValidate()
    {
        for (int i = 0; i < _bonusTuples.Length; i++)
        {
            _bonusTuples[i].bonusData = new Resources(_bonusTuples[i].bonusData);
        }
    }
    #endregion
}

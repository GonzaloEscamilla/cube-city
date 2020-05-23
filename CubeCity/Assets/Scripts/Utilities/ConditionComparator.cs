using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ConditionComparator 
{
    public static bool CompareConditions(int firstValue, int secondValue, Comparator condition)
    {
        switch (condition)
        {
            case Comparator.GreaterThan:
                return firstValue > secondValue;
            case Comparator.GreatherOrEqualTo:
                return firstValue >= secondValue;
            case Comparator.LesserThan:
                return firstValue < secondValue;
            case Comparator.LesserOrEqualTo:
                return firstValue <= secondValue;
            case Comparator.EqualsTo:
                return firstValue == secondValue;
            default:
                break;
        }

        return false;
    }
}

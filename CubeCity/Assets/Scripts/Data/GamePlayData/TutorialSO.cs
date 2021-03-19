using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewTutorial", menuName = "ScriptableObjects/LevelSystem/Tutorial")]
public class TutorialSO : ScriptableObject
{
    public TutorialInfo[] levelTurotials;

    private LevelStatistics levelStatistics;

    public void Init(LevelStatistics statistics)
    {
        levelStatistics = statistics;

        EventsManager.Instance.OnStatisticsUpdate += Check;
        EventsManager.Instance.OnBonusMade += Check;
        EventsManager.Instance.OnComboMade += Check;
    }

    private void OnDestroy()
    {
        EventsManager.Instance.OnStatisticsUpdate -= Check;
        EventsManager.Instance.OnBonusMade -= Check;
        EventsManager.Instance.OnComboMade -= Check;
    }

    private void Check()
    {
        for (int i = 0; i < levelTurotials.Length; i++)
        {
            List<bool> result = new List<bool>();

            for (int j = 0; j < levelTurotials[i].tutorialConditions.Length; j++)
            {
                switch (levelTurotials[i].tutorialConditions[j].type)
                {
                    case TutorialConditionsType.AmountOfCubes:
                        result.Add(ConditionComparator.CompareConditions(levelStatistics.CurrentCubeAmount,
                                                                         levelTurotials[i].tutorialConditions[j].amount,                                                                  
                                                                         levelTurotials[i].tutorialConditions[j].condition));
                        break;
                    case TutorialConditionsType.AmountOfResources:
                        result.Add(ConditionComparator.CompareConditions(levelStatistics.GetResourceAmount(levelTurotials[i].tutorialConditions[j].resourceType), 
                                                                         levelTurotials[i].tutorialConditions[j].amount,                                                                  
                                                                         levelTurotials[i].tutorialConditions[j].condition));
                        break;
                    case TutorialConditionsType.AmountOfCombos:
                        result.Add(ConditionComparator.CompareConditions(levelStatistics.AmountOfCombosMade, 
                                                                         levelTurotials[i].tutorialConditions[j].amount,                                                                  
                                                                         levelTurotials[i].tutorialConditions[j].condition));
                        break;
                    case TutorialConditionsType.AmountOfBonus:
                        //result.Add(ConditionComparator.CompareConditions(levelStatistics.BonusAmount,
                        //                                                 levelTurotials[i].tutorialConditions[j].amount,
                        //                                                 levelTurotials[i].tutorialConditions[j].condition));
                        break;
                    default:
                        break;
                }

            }

            if (result.Contains(false))
                return;
            else
            {
                TutorialManager.Instance.SetTutorialScreen(levelTurotials[i].image, levelTurotials[i].description);
            }
        }
    }
}

[System.Serializable]
public struct TutorialInfo
{
    public string name;
    [TextArea] public string description;
    public Sprite image;
    public TutorialConditions[] tutorialConditions;
}

[System.Serializable]
public struct TutorialConditions
{
    public TutorialConditionsType type;
    public Comparator condition;
    public ResourceTypes resourceType;
    public int amount;
}

public enum TutorialConditionsType
{
    AmountOfCubes,
    AmountOfResources,
    AmountOfCombos,
    AmountOfBonus
}

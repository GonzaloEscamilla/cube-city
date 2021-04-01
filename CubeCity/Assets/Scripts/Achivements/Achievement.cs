using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "NewAchievement", menuName = "ScriptableObjects/Achievement")]
public class Achievement : ScriptableObject
{
    [Header("Settings")]
    public int index;
    public string title;
    public string information;
    public AchievementCondition achievementCondition;
    public AchievementReward achievementReward;
    public Sprite rewardIcon;

    [Header("Current Progress")]
    public int currentValue;

    public Action OnAchievementCompleted;

    [SerializeField] private AchivementState state;
    public int State
    {
        get
        {
            return PlayerPrefs.GetInt("Achivement_" + index + "_State");
        }
        set
        {
            PlayerPrefs.SetInt("Achivement_" + index + "_State", value);
            state = (AchivementState)value;
        }
    }

    [ContextMenu("Initialize")]
    public void Init()
    {
        if (!PlayerPrefs.HasKey("Achivement_" + index + "_State")) 
            PlayerPrefs.SetInt("Achivement_" + index + "_State", 0);

        state = (AchivementState)State;

        if (State != (int)AchivementState.Redimable && State != (int)AchivementState.Done)
        {
            SubscribeToEvent();
            State = (int)AchivementState.InProgress;
        }
    }

    [ContextMenu("LogStatus")]
    private void LogStatus()
    {
        Debug.Log((AchivementState)State);
    }

    [ContextMenu("ResetAchievement")]
    private void ResetAchivementStatus()
    {
        if (PlayerPrefs.HasKey("Achivement_" + index + "_State"))
            PlayerPrefs.SetInt("Achivement_" + index + "_State", 0);
    }

    private void SubscribeToEvent()
    {
        switch (achievementCondition.achievementType)
        {
            case AchivementType.StartsAmoun:
                EventsManager.Instance.OnStartsUpdate += UpdateCurrentValue;
                break;
            case AchivementType.AmountOfProsperityOnLevel:
                EventsManager.Instance.OnMaxProsperityUpdate += OnLevelEndCheckProsperity;
                break;
            case AchivementType.AmountOfCombosAcumulated:
                EventsManager.Instance.OnCombosUpdate += UpdateCurrentValue;
                break;
            case AchivementType.AmountOfPowerUpsUsed:
                EventsManager.Instance.OnPowerupsUpdate += UpdateCurrentValue;
                break;
            case AchivementType.CompleteALevel:
                EventsManager.Instance.onLevelEndEvent += LevelEnd;
                break;
            default:
                break;
        }
    }

    public void UnsubscribeToEvents()
    {
        switch (achievementCondition.achievementType)
        {
            case AchivementType.StartsAmoun:
                EventsManager.Instance.OnStartsUpdate -= UpdateCurrentValue;
                break;
            case AchivementType.AmountOfProsperityOnLevel:
                EventsManager.Instance.OnMaxProsperityUpdate -= OnLevelEndCheckProsperity;
                break;
            case AchivementType.AmountOfCombosAcumulated:
                EventsManager.Instance.OnCombosUpdate -= UpdateCurrentValue;
                break;
            case AchivementType.AmountOfPowerUpsUsed:
                EventsManager.Instance.OnPowerupsUpdate -= UpdateCurrentValue;
                break;
            case AchivementType.CompleteALevel:
                EventsManager.Instance.onLevelEndEvent -= LevelEnd;
                break;
            default:
                break;
        }
    }

    public void UpdateCurrentValue(int amount)
    {
        currentValue = amount;
        CheckState();
    }

    private void OnLevelEndCheckProsperity(int amount)
    {
        Debug.Log("Checking prosperity");
        UpdateCurrentValue(amount);
    }

    private void CheckState()
    {
        if (ConditionComparator.CompareConditions(currentValue, achievementCondition.value, achievementCondition.condition))
        {
            Debug.Log("Achivement completed");
            State = (int)AchivementState.Redimable;
            OnAchievementCompleted?.Invoke();
            UnsubscribeToEvents();
        }
    }


    private void LevelEnd(LevelEndData data)
    {
        if (data.success)
        {
            currentValue = data.levelNumber;
            CheckState();
        }
    }

}

[System.Serializable]
public struct AchievementCondition
{
    public AchivementType achievementType;
    public Comparator condition;
    public int value;
}

[System.Serializable]
public struct AchievementReward
{
    public RewardType rewardType;
    public int amount;

    [Tooltip("Only if the reward type ys powerup")]
    public PowerUpType powerupType;
}

public enum RewardType
{
    Cristal,
    Powerup
}

public enum AchivementState
{
    None,       // 0
    InProgress, // 1
    Redimable,  // 2
    Done        // 3
}
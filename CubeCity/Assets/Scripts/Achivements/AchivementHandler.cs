using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class AchivementHandler : MonoBehaviour
{
    [SerializeField] private Achievement achievement;
    [SerializeField] private Button rewardButton;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI progressNumber;
    [SerializeField] private Image fillBar;
    [SerializeField] private Image rewardIcon;
    [SerializeField] private Image notificationIcon;
    [SerializeField] private Image completedIcon;

    [SerializeField] private AchivementState state;


    private void Start()
    {
        Debug.LogWarning("Start on Achive");

        achievement.Init();
        SetValues();
        StateUpdate();

        achievement.OnAchievementCompleted += AchievementCompleted;
        EventsManager.Instance.OnSceneLoaded += SceneLoaded;

        rewardButton.onClick.AddListener(() => GetReward());
    }

    private void GetReward()
    {
        if (state == AchivementState.Redimable)
        {
            AchievementReward newReward = achievement.GetAchievementReward();

            switch (newReward.rewardType)
            {
                case RewardType.Cristal:
                    Player.Instance.CristalsAmount += newReward.amount;
                    break;
                case RewardType.Powerup:
                    Player.Instance.Inventory.AddPowerUpToInventory(newReward.powerupType, newReward.amount);
                    break;
                default:
                    break;
            }
            state = AchivementState.Done;
            achievement.State = (int)state;
            SetValues();
            EventsManager.Instance.AchievementRedimed();
        }
    }

    private void OnDisable()
    {
        if (EventsManager.Instance != null)
            EventsManager.Instance.OnSceneLoaded -= SceneLoaded;

        if (achievement != null)
           achievement.OnAchievementCompleted -= AchievementCompleted;
    }

    private void SceneLoaded(GameScenes obj)
    {
        Debug.LogWarning("SceneLoaded on Achive");
    }

    private void AchievementCompleted()
    {
        notificationIcon.gameObject.SetActive(true);
    }

    private void SetValues()
    {
        title.text = achievement.title;
        rewardIcon.sprite = achievement.rewardIcon;

        state = (AchivementState)achievement.State;

        switch (state)
        {
            case AchivementState.None:
                break;
            case AchivementState.InProgress:
                break;
            case AchivementState.Redimable:
                notificationIcon.gameObject.SetActive(true);
                break;
            case AchivementState.Done:
                completedIcon.gameObject.SetActive(true);
                notificationIcon.gameObject.SetActive(false);
                break;
            default:
                break;
        }

    }

    private void StateUpdate()
    {
        progressNumber.text = achievement.currentValue.ToString() + " / " + achievement.achievementCondition.value.ToString();
        fillBar.fillAmount = Mathf.InverseLerp(0, achievement.achievementCondition.value, achievement.currentValue);
    }
}
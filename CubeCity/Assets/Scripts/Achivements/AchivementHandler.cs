using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class AchivementHandler : MonoBehaviour
{
    [SerializeField] private Achievement achievement;
    [SerializeField] private TextMeshProUGUI title;
    [SerializeField] private TextMeshProUGUI progressNumber;
    [SerializeField] private Image fillBar;
    [SerializeField] private Image rewardIcon;

    private void Start()
    {
        achievement.Init();
        SetValues();
        StateUpdate();

        achievement.OnAchievementCompleted += achievementCompleted;
    }

    private void achievementCompleted()
    {

    }

    private void SetValues()
    {
        title.text = achievement.title;
        rewardIcon = achievement.rewardIcon;
    }

    private void StateUpdate()
    {
        progressNumber.text = achievement.currentValue.ToString() + " / " + achievement.achievementCondition.value.ToString();
        fillBar.fillAmount = Mathf.InverseLerp(0, achievement.achievementCondition.value, achievement.currentValue);
    }
}